using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    // Dealt (ranged) damage to player
    private const int MIN_RANGED_DEALTH_DAMAGE = 5;
    private const int MAX_RANGED_DEALTH_DAMAGE = 10;

    private float velocity;
    private GameObject player;
    private Vector3 projectileTargetPos;
    private Vector3 projectileSourcePos;

    // Start is called before the first frame update
    void Start()
    {
        velocity = 25f;
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        projectileTargetPos = player.transform.position;
        projectileSourcePos = transform.position;

        RotateProjectile();
        DestroyAfterSeconds();
    }

    // Update is called once per frame
    void Update()
    {
        // Update projectile motion (linear non-accelerated)
        transform.position += (projectileTargetPos - projectileSourcePos).normalized * Time.deltaTime * velocity;
    }

    private void RotateProjectile()
    {
        // Rotate projectile to face target
        if (gameObject.tag == Tags.EnemyProjectileCarrot)
            transform.rotation = Quaternion.LookRotation(Vector3.Cross((projectileTargetPos - projectileSourcePos).normalized, GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Transform>().right));
    }

    private void DestroyAfterSeconds()
    {
        Destroy(gameObject, 5f);
    }

    private int GetDamageDealt()
    {
        // Get a random damage value
        return Random.Range(MIN_RANGED_DEALTH_DAMAGE, MAX_RANGED_DEALTH_DAMAGE + 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If player is hit
        if (collision.gameObject.tag == Tags.Player)
        {
            // Deal damage
            player.GetComponent<PlayerController>().TakeDamage(this.GetDamageDealt());
            Destroy(this.gameObject);

        }
        // If hit object is NOT any other projectile
        else if(Tags.IsNotAnyProjectile(tag))
            Destroy(this.gameObject);

        else { /* Do nothing, proj hitting another proj should not affect eachother */ }
    }
}
