using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject playerWeapon;
    private PlayerController playerCtrl;
    private PlayerPowerUps playerPowerUps;

    // FSM (unused)
    public static int WEAPON_STATE_IDLE = 0;
    public static int WEAPON_STATE_FIRE = 1;
    public static int WEAPON_STATE_FIRE_DOUBLE_RATE = 2;
    public static int WEAPON_STATE_FIRE_THREE = 3;

    private static int currentState = -1;

    private const float BASE_FIRE_RATE = 0.16f;

    // Animation clip (fire) ! Must be marged as legacy from the debug window !
    //Animation fireAnimation;

    // Components
    static AudioSource[] audioSources;
    static AudioSource audioFire;
    static AudioSource audioFireLoud;
    static Animator animator;
    static ParticleSystem particleSys;
    //RaycastController raycaster;

    // Weapon vars
    private const int INIT_BULLET_COUNT = 240;
    private const int CLIP_SIZE = 30;
    private int totalBulletCount;
    private int clipBulletCount;
    private int leftoverBulletCount;
    private float fireRate = BASE_FIRE_RATE;
    private float nextFire = 0.0f;
    private bool justReloaded = false;

    private RaycastHit ray;
    private int range = 1000;

    // Start is called before the first frame update
    void Start()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        playerWeapon = GameObject.FindGameObjectWithTag(Tags.PlayerWeapon);
        playerCtrl = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerController>();
        playerPowerUps = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerPowerUps>();
        mainCamera = GameObject.FindGameObjectWithTag(Tags.MainCamera).GetComponent<Camera>();
        animator = GetComponent<Animator>();
        //fireAnimation = GetComponent<Animation>();
        particleSys = GetComponentInChildren<ParticleSystem>();

        audioSources = GetComponents<AudioSource>();
        audioFire = audioSources[0];
        audioFireLoud = audioSources[1];

        //raycaster = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<RaycastController>();

        totalBulletCount = INIT_BULLET_COUNT;
        clipBulletCount = CLIP_SIZE;
        leftoverBulletCount = totalBulletCount - clipBulletCount;
    }

    private void Update()
    {
        //DisplayBullets();

        if (Input.GetButton("Fire1"))// && IsReloadingAnim() == false)
        {
            if (Time.time > nextFire)
            {
                if (playerPowerUps.PowerFireThree.IsActive() == true)
                {
                    Instantiate(playerCtrl.GetAmmoType(Ammo.Type.Cabbage), playerWeapon.transform.position + (playerWeapon.transform.forward / 1.75f) + (playerWeapon.transform.up * 0.75f) + (playerWeapon.transform.right * 0.0f), Quaternion.identity);
                    Instantiate(playerCtrl.GetAmmoType(Ammo.Type.Tomato), playerWeapon.transform.position + (playerWeapon.transform.forward / 1.75f) + (playerWeapon.transform.up * 0.6f) + (playerWeapon.transform.right * -0.125f), Quaternion.identity);
                    Instantiate(playerCtrl.GetAmmoType(Ammo.Type.Carrot), playerWeapon.transform.position + (playerWeapon.transform.forward / 1.75f) + (playerWeapon.transform.up * 0.6f) + (playerWeapon.transform.right * 0.125f), Quaternion.identity);
                }
                else
                    Instantiate(playerCtrl.GetSelectedAmmoType(), playerWeapon.transform.position + (playerWeapon.transform.forward / 1.75f) + (playerWeapon.transform.up * 0.6f), Quaternion.identity);

                if (playerPowerUps.PowerDoubleFireRate.IsActive() == true)
                    UpdateState(WEAPON_STATE_FIRE_DOUBLE_RATE);
                else if (playerPowerUps.PowerFireThree.IsActive() == true)
                    UpdateState(WEAPON_STATE_FIRE_THREE);
                else
                    UpdateState(WEAPON_STATE_FIRE);

                WeaponFireSound();
                nextFire = Time.time + (fireRate * 1.6666f);

                //raycaster.IncrementBulletBurstCount();
                //justReloaded = false;
            }
            //}

            //if (IsClipEmpty() == true && justReloaded == false)
            //{
            //    UpdateState(WEAPON_STATE_REALOAD);
            //    raycaster.ResetBulletBurstCount();
            //    justReloaded = true;
            //}
        }
        else
        {
            UpdateState(WEAPON_STATE_IDLE);
            //raycaster.ResetBulletBurstCount();
        }

        //raycaster.RaycastEnemyHealth();
    }

    public void UpdateState(int stateVal)
    {
        animator.SetInteger("WeaponState", currentState = stateVal);

        //if (stateVal == WEAPON_STATE_FIRE)
        //    FireWeapon();

        //else if (stateVal == WEAPON_STATE_REALOAD)
        //    ReloadWeapon();
    }

    private void WeaponFireSound()
    {
        //if (IsClipEmpty() == false)
        //{
        //    clipBulletCount--;
        //    totalBulletCount--;
        //}

        // Audio stuff
        if (playerPowerUps.PowerFireThree.IsActive() == false)
        {
            audioFire.pitch = Random.Range(0.85f, 1.05f);
            audioFire.Play();
        }
        else if (playerPowerUps.PowerFireThree.IsActive() == true)
        {
            audioFireLoud.pitch = Random.Range(0.85f, 1.05f);
            audioFireLoud.Play();
        }
        //particleSys.Play();
    }

    public void ReloadWeapon()
    {
        //// Audio stuff
        //if (!audioReload.isPlaying)
        //    audioReload.Play();

        //// Clip reload and wait until animation finishes
        //StartCoroutine(WaitForReloadRoutine());
    }

    //IEnumerator WaitForReloadRoutine()
    //{
    //    yield return new WaitUntil(() => audioReload.isPlaying == false);

    //    if (totalBulletCount <= CLIP_SIZE)
    //    {
    //        clipBulletCount = totalBulletCount;
    //        leftoverBulletCount = 0;
    //    }
    //    else
    //    {
    //        clipBulletCount = CLIP_SIZE;
    //        leftoverBulletCount = totalBulletCount - clipBulletCount;
    //    }
    //}

    public void IncreaseAmmo(int ammoAmount)
    {
        //totalBulletCount += ammoAmount;
        //leftoverBulletCount = totalBulletCount - clipBulletCount;

        //if (clipBulletCount == 0)
        //    StartCoroutine(WaitForReloadRoutine());
    }

    //public static bool IsReloadingAnim()
    //{
    //    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Reload_2"))
    //        return true;
    //    else return false;
    //}

    public bool IsClipEmpty()
    {
        if (clipBulletCount == 0)
            return true;
        else return false;
    }

    public bool IsClipFull()
    {
        if (clipBulletCount == CLIP_SIZE)
            return true;
        else
            return false;
    }

    public int GetCurrentState()
    {
        return currentState;
    }

    public void DisplayBullets()
    {
        //Debug.Log("Bullets: " + clipBulletCount + "/" + leftoverBulletCount);
    }

    public int GetBulletCount()
    {
        return totalBulletCount;
    }

    public int GetClipCount()
    {
        return clipBulletCount;
    }

    public void FireRateSetMultiplier(int multiplier)
    {
        fireRate = BASE_FIRE_RATE / 2;
    }

    public void FireRateSetValue(int value)
    {
        fireRate = value;
    }

    public void FireRateReset()
    {
        fireRate = BASE_FIRE_RATE;
    }

    public int GetLeftoverBulletsCount()
    {
        return leftoverBulletCount;
    }
}
