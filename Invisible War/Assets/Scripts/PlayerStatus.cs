using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private int health = 100;

    public void GetHit()
    {
        if (health > 0)
        {
            health -= 10;
        }
    }

    private void FixedUpdate()
    {
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }
}
