using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class PPSystem : MonoBehaviour
{
    public GameObject[] pointList;
    public float respawnTime;
    public float powerUpTime;
    public bool isPowerUp;
    public GameObject ghost;
    public GameObject hunter;
    public float blinkTime;
    public Image exchangeArrow;
    public Image[] screenEffect;

    private float curSTime;
    private float curPTime;
    private Random rd;
    private PlayerStatus gps;
    private PlayerStatus hps;
    private List<GameObject> respawnList;
    private int pointIdx;
    private int pointNum;
    private bool isBlinking;
    private float blinkTimer;
    private bool firstTime;
    // Start is called before the first frame update
    void Start()
    {
        gps = ghost.GetComponent<PlayerStatus>();
        hps = hunter.GetComponent<PlayerStatus>();

        curSTime = respawnTime;
        isPowerUp = false;
        isBlinking = false;
        firstTime = true;
        blinkTimer = 0;

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
            firstTime = true;

        } else
        {
            // Power up is activated.
            curPTime -= Time.deltaTime;
            if (curPTime <= blinkTime && firstTime)
            {
                isBlinking = true;
                StartCoroutine(Blink());
                firstTime = false;
            }
            if (curPTime <= 0)
            {
                isPowerUp = false;
                curSTime = respawnTime;
                gps.invincible = false;
                hps.invincible = true;
                GameConfig.instance.ghostSpeed /= 1.5f;
            }
        }

        if (isBlinking)
        {
            blinkTimer += Time.deltaTime;
        }
        if (blinkTimer >= blinkTime)
        {
            isBlinking = false;
            blinkTimer = 0;
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
        isBlinking = true;
        StartCoroutine(Blink());
        disablePP();
        isPowerUp = true;
        curPTime = powerUpTime;
        gps.invincible = true;
        hps.invincible = false;
        GameConfig.instance.ghostSpeed *= 1.5f;
    }

    public IEnumerator Blink()
    {
        while (isBlinking)
        {
            exchangeArrow.enabled = true;
            yield return new WaitForSeconds(0.5f);
            exchangeArrow.enabled = false;
            yield return new WaitForSeconds(0.5f);
        }
        exchangeArrow.enabled = false;
        for (int i = 0; i < screenEffect.Length; i++)
        {
            screenEffect[i].enabled = !screenEffect[i].enabled;
        }
    }
}
