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
    private List<GameObject> respawnList;
    private int pointIdx;
    private int pointNum;
    
    // Start is called before the first frame update
    void Start()
    {
        ps = ghost.GetComponent<PlayerStatus>();

        curSTime = respawnTime;
        isPowerUp = false;

        disablePP();
        rd = new Random();
        respawn();
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
                // Activate a new pp.
                respawn();
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

    // Respawn new power point.
    void respawn()
    {
        if (pointNum > 0)
        {
            pointIdx = rd.Next(0, respawnList.Count);
            respawnList[pointIdx].SetActive(true);
            respawnList.RemoveAt(pointIdx);
            pointNum --;
        }
    }

    // Disable all the powerpoint.
    void disablePP()
    {
        for (int i = 0; i < pointList.Length; i++)
        {
            pointList[i].SetActive(false);
            pointNum = pointList.Length;
            respawnList = new List<GameObject>(pointList);
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
