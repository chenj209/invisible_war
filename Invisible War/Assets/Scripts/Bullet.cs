using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerStatus ps = collision.gameObject.GetComponent("PlayerStatus") as PlayerStatus;
            ps.GetHit();
        }
        Destroy(gameObject);
    }
}
