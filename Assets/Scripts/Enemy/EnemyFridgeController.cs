using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFridgeController : MonoBehaviour
{
    // Test
    public ParticleSystem cabbageParticle;

    // FSM
    private const int STATE_FRIDGE_IDLE = 0;
    private const int STATE_FRIDGE_WALK = 1;
    private const int STATE_FRIDGE_ATTACK_MELEE = 2;
    private const int STATE_FRIDGE_ATTACK_RANGED = 3;
    //private const int STATE_FRIDGE_REMOVAL = 4;

    // Dealt (melee) damage to player
    private const int MIN_MELEE_DEALT_DAMAGE = 5;
    private const int MAX_MELEE_DEALT_DAMAGE = 10;

    // Nav agent vars
    private const float AGENT_WALK_SPEED = 2.5f;
    private const float PUSH_BACK_FORCE = 10f;

    // Melee and ranged fire rates
    private float nextMeleeAttack = 0.0f;
    private float meleeAttackRate = 1.0f;

    private float nextRangedAttack = 0.0f;
    private float rangedAttackRate = 1.0f;

    // Ranged attack frequency vars
    private const float MIN_RANGED_DELAY = 6f;
    private const float MAX_RANGED_DELAY = 12.5f;

    // Ranged attack duration
    private const float MIN_RANGED_ATTACK_DURATION = 1.5f;
    private const float MAX_RANGED_ATTACK_DURATION = 4.5f;
    private float rangedAttackTimeTarget = 0.0f;

    // Melee attack distance
    private float meleeAttackDistance = 3.5f;

    // Components
    private GameObject player;
    private EnemyRemovalRequirements enemyRemovalReqs;
    private PlayerAmmoTypes ammoTypes;
    private EnemyFridgeCanvas canvasScript;
    private NavMeshAgent navAgent;
    private Rigidbody rigidBody;
    private Camera playerCamera;
    private Animator animator;
    private bool shootsBack;
    private bool isFunctional = true;
    private bool isInRangedAttack = false;
    private int cashValue;

    // Start is called before the first frame update
    void Start()
    {
        InitEnemyFridge();
    }

    private void InitEnemyFridge()
    {
        // Get combonents and GameObjects
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        navAgent = GetComponent<NavMeshAgent>();
        rigidBody = GetComponent<Rigidbody>();
        playerCamera = GameObject.FindGameObjectWithTag(Tags.MainCamera).GetComponent<Camera>();
        animator = GetComponent<Animator>();
        ammoTypes = GameObject.Find("AmmoController").GetComponent<PlayerAmmoTypes>();
        enemyRemovalReqs = GetComponent<EnemyRemovalRequirements>();
        canvasScript = GetComponentInChildren<EnemyFridgeCanvas>();

        // Generate removal requirements
        enemyRemovalReqs.GenerateFridgeRemovalReqs(Random.Range(1, 4));
        canvasScript.GenerateCanvas(enemyRemovalReqs.GetRemovalReqsList(), ammoTypes);

        shootsBack = true;

        // Start ranged attack coroutine
        if (shootsBack == true)
            StartCoroutine(RangedAttackRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        // A.k.a is alive
        if (isFunctional == true)
        {
            // Is ranged attack inactive
            if (isInRangedAttack == false)
            {
                // If too far from player, walk to him
                if (GetDistanceToPlayer() > meleeAttackDistance)
                {
                    //LookAtPlayer();
                    WalkToPlayer();
                    UpdateState(STATE_FRIDGE_WALK);
                }

                // If in proximity, attack melee
                else
                {
                    LookAtPlayer();
                    StartCoroutine(AttackMelee());
                    UpdateState(STATE_FRIDGE_ATTACK_MELEE);
                }
            }

            // Else, ranged attack is active
            else
            {
                LookAtPlayer();
            }
        }


        // WalkToPlayer();
    }

    private void UpdateState(int stateVal)
    {
        animator.SetInteger("EnemyState", stateVal);
    }

    public IEnumerator RangedAttackRoutine()
    {
        for (; ; )
        {
            // Wait random amount of time until next ranged attack
            float randWaitTime = Random.Range(MIN_RANGED_DELAY, MAX_RANGED_DELAY);
            yield return new WaitForSeconds(randWaitTime);

            if (isFunctional == true)
                // Ranged attack with selected ammo type
                StartCoroutine(AttackRanged());

        }
    }

    private IEnumerator AttackRanged()
    {
        isInRangedAttack = true;
        navAgent.isStopped = true;
        Ammo.Type ammoType = new Ammo.Type();
        StartRangedAttackTimer();

        for (; ; )
        {


            // Do this for ranged attack duration seconds
            if (rangedAttackTimeTarget > Time.time)
            {
                if (Time.time > nextRangedAttack)
                {
                    nextRangedAttack = Time.time + rangedAttackRate;

                    // Check if vegetables are left to be thrown
                    if (enemyRemovalReqs.HasAnyRequirements())
                    {
                        UpdateState(STATE_FRIDGE_ATTACK_RANGED);

                        // Attack start delay
                        yield return new WaitForSeconds(0.5f);

                        ammoType = enemyRemovalReqs.GetMostFulfilledRequirement();

                        // Instantiate projectile
                        Instantiate(
                            ammoTypes.GetEnemyProjectileType(ammoType),
                            transform.position + (transform.forward * 2),
                            Quaternion.identity);

                        // Update enemy canvas items
                        canvasScript.UpdateReqBarForInstance(ammoType, enemyRemovalReqs, false);
                    }
                    // Else, end ranged attack routine
                    else
                    {
                        rangedAttackTimeTarget = Time.time;
                        isInRangedAttack = false;
                    }

                }
            }
            else
                isInRangedAttack = false;

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public IEnumerator AttackMelee()
    {
        yield return new WaitForSeconds(0.3f);

        if (Time.time > nextMeleeAttack)
        {
            nextMeleeAttack = Time.time + meleeAttackRate;

            if (GetDistanceToPlayer() <= meleeAttackDistance)
                player.GetComponent<PlayerController>().TakeDamage(Random.Range(MIN_MELEE_DEALT_DAMAGE, MAX_MELEE_DEALT_DAMAGE + 1));
        }
    }

    private void StartRangedAttackTimer()
    {
        // Get random range attack timer
        rangedAttackTimeTarget = Time.time + Random.Range(MIN_RANGED_ATTACK_DURATION, MAX_RANGED_ATTACK_DURATION);
    }

    public float GetDistanceToPlayer()
    {
        return (player.transform.position - transform.position).magnitude;
    }

    private void LookAtPlayer()
    {
        //transform.LookAt(player.transform.position - new Vector3(0, player.transform.localScale.y, 0), Vector3.up);

        Vector3 direction = (player.transform.position - new Vector3(0, player.transform.localScale.y * 1.5f, 0) - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    private void WalkToPlayer()
    {
        //animator.SetInteger("EnemyState", STATE_WALK);
        navAgent.isStopped = false;
        navAgent.SetDestination(player.transform.position);
        navAgent.speed = AGENT_WALK_SPEED;
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.transform.tag)
        {
            case Tags.AmmoCabbage:
                canvasScript.UpdateReqBarForInstance(Ammo.Type.Cabbage, enemyRemovalReqs, true);
                break;

            case Tags.AmmoTomato:
                canvasScript.UpdateReqBarForInstance(Ammo.Type.Tomato, enemyRemovalReqs, true);
                break;

            case Tags.AmmoCarrot:
                canvasScript.UpdateReqBarForInstance(Ammo.Type.Carrot, enemyRemovalReqs, true);
                break;

            case Tags.AmmoOnion:
                canvasScript.UpdateReqBarForInstance(Ammo.Type.Cabbage, enemyRemovalReqs, true);
                break;

            default: break;
        }

        CheckIfToBeRemoved();
    }

    private void CheckIfToBeRemoved()
    {
        if (enemyRemovalReqs.CheckIfToBeRemoved())
            Remove();
    }

    private void Remove()
    {
        // A.k.a dead
        isFunctional = false;

        // Add cash to player
        player.GetComponent<PlayerController>().AddCash(enemyRemovalReqs.GetCashValue());

        // Play cash update animation on UI animation
        GameObject.FindGameObjectWithTag(Tags.UICashRectTransform).GetComponent<UICashCanvasController>().PlayCashCanvasAnimation();

        // Update removal count
        FridgeRemovalCount.UpdateCount();

        Destroy(this.gameObject);
    }

}
