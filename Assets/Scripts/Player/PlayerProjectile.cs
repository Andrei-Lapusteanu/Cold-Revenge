using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private float velocity;
    private Vector3 projectileTarget;
    private GameObject playerWeapon;
    private PlayerAmmoTypes ammoTypes;
    private GameObject particleSplashType;

    // Start is called before the first frame update
    void Start()
    {
        velocity = 25f;
        projectileTarget = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerController>().GetRaycastDirection();
        playerWeapon = GameObject.FindGameObjectWithTag(Tags.PlayerWeapon);
        ammoTypes = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerAmmoTypes>();

        
        SetParticleSplashType();
        SetColliderIgnores();
        RotateProjectile();
        DestroyAfterSeconds();
    }

    // Update is called once per frame
    void Update()
    {
        // Update projectile motion (linear non-accelerated)
        transform.position += projectileTarget * Time.deltaTime * velocity;
    }

    private void SetParticleSplashType()
    {
        switch (gameObject.tag)
        {
            case Tags.AmmoCabbage:
                particleSplashType = ammoTypes.GetSplashParticleType(Ammo.Type.Cabbage);
                break;

            case Tags.AmmoCarrot:
                particleSplashType = ammoTypes.GetSplashParticleType(Ammo.Type.Carrot);
                break;

            case Tags.AmmoOnion:
                particleSplashType = ammoTypes.GetSplashParticleType(Ammo.Type.Onion);
                break;

            case Tags.AmmoTomato:
                particleSplashType = ammoTypes.GetSplashParticleType(Ammo.Type.Tomato);
                break;

            default:
                particleSplashType = ammoTypes.GetSplashParticleType(Ammo.Type.Cabbage);
                break;
        }
    }

    private void SetColliderIgnores()
    {
    
    }

    private void RotateProjectile()
    {
        // Rotate projectile to face target
        if (gameObject.tag == Tags.AmmoCarrot)
            transform.rotation = Quaternion.LookRotation(playerWeapon.transform.up);
        else
            transform.rotation = Quaternion.LookRotation(playerWeapon.transform.forward, playerWeapon.transform.up);

    }

    private void DestroyAfterSeconds()
    {
        Destroy(gameObject, 5f);
    }


    // Not pretty code, works for now
    private void OnCollisionEnter(Collision collision)
    {
        var time = Time.time;
        string collisionTag = collision.gameObject.tag;

        if (collisionTag == Tags.EnemyFridge)
        {
            PlayParticleEffect(collision.GetContact(0).point, collision.GetContact(0).normal);
            Destroy(gameObject);
        }

        else if (collisionTag == Tags.Player || collisionTag == Tags.PlayerWeapon)
        {
            /* Ignore */
        }

        else if (Tags.IsNotAnyProjectile(collisionTag))
        {
            PlayParticleEffect(collision.GetContact(0).point, collision.GetContact(0).normal);
            Destroy(gameObject);
        }

        else
        {
            var thisCollider = GetComponent<SphereCollider>();
            var thatCollider = collision.collider;

            Physics.IgnoreCollision(thisCollider, thatCollider);
            int a = 0;
        }

        Debug.Log(Time.time - time);
    }

    private void PlayParticleEffect(Vector3 position, Vector3 collisionNormal)
    {
        particleSplashType.transform.position = position;
        particleSplashType.transform.localRotation = Quaternion.LookRotation(collisionNormal);

        var particleInst = Instantiate(particleSplashType);
        particleInst.GetComponent<ParticleSystem>().Play();

        Destroy(particleInst.gameObject, 0.5f);
    }
}