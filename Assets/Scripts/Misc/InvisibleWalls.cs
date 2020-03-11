using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWalls : MonoBehaviour
{
    BoxCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("SADFUJIAHSFUIHSDUIASD");
    
        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.collider, collider);
        }
    }
}
