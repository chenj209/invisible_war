using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintGunCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Tag: " + collision.collider.gameObject.tag);
        if (collision.collider.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.collider, this.gameObject.GetComponent<CapsuleCollider>());
        }
    }
}
