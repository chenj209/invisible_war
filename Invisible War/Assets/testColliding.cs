using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testColliding : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.collider, this.gameObject.GetComponent<CapsuleCollider>());
        }
        else
        {
            Debug.Log("bot" + collision.collider.gameObject.name);
        }
    }
}
