using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutoriallvl1 : MonoBehaviour
{
    public string playerID;

    public bool knowMove;
    private float timer = 0f;
    private bool firstTime = true;
    // Start is called before the first frame update
    void Start()
    {
        knowMove = false;
        this.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal" + playerID);
        float zMove = Input.GetAxis("Vertical" + playerID);
        if (Mathf.Abs(xMove) < .3f)
        {
            xMove = 0f;
        }
        if (Mathf.Abs(zMove) < .3f)
        {
            zMove = 0f;
        }

        if ((xMove != 0) || (zMove != 0))
        {
            knowMove = true;
        }

        if (knowMove == true && firstTime)
        {
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                this.gameObject.SetActive(false);
                timer = 0.0f;
                knowMove = false;
                firstTime = false;
            }
        }
    }
}
