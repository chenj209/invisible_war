using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = System.Random;

public class PPSystem : MonoBehaviour
{
    public GameObject[] pointList;
    public float respawnTime;
    public float powerUpTime;
    public bool isPowerUp;
    public GameObject ghost;

    private float curSTime;
    private float curPTime;
    private Random rd;
    private PlayerStatus ps;
    
    // Start is called before the first frame update
    void Start()
    {
        disablePP();
        rd = new Random();
        pointList[rd.Next(0, 4)].SetActive(true);
        curSTime = respawnTime;
        isPowerUp = false;

        ps = ghost.GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        // Time activate when power up is not activated.
        if (!isPowerUp)
        {
            curSTime -= Time.deltaTime;
            if (curSTime <= 0)
            {
                disablePP();
                // Activate a new pp.
                pointList[rd.Next(0, 4)].SetActive(true);
                curSTime = respawnTime;
            }
        } else
        {
            // Power up is activated.
            curPTime -= Time.deltaTime;
            if (curPTime <= 0)
            {
                isPowerUp = false;
                curSTime = respawnTime;
            }
        }
    }

    // Disable all the powerpoint.
    void disablePP()
    {
        for (int i = 0; i < pointList.Length; i++)
        {
            pointList[i].SetActive(false);
        }
    }

    public void pUActivate()
    {
        disablePP();
        isPowerUp = true;
        curPTime = powerUpTime;
        ps.invincible = true;
        Debug.Log(ps.invincible);
    }
}
