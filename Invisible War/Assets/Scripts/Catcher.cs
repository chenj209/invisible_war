using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour
{
    public GameObject pointOne;
    public GameObject pointTwo;
    private float speed = 0.5f;
    private bool Frozen = false;
    private float count = 0.0f;
    private bool moveToP2 = false;
    private bool moveToP1 = true;
    public bool freezeBot;
    public GameObject directionArrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Frozen && freezeBot)
        {
            count += Time.deltaTime;
            speed *= 0.95f;
            if (count > 5)
            {
                count = 0.0f;
                Frozen = false;
                speed = 0.5f;
                gameObject.layer = 9;
                foreach (Transform trans in gameObject.GetComponentsInChildren<Transform>(true))
                {
                    trans.gameObject.layer = 9;
                }
                Canvas can = gameObject.GetComponentInChildren<Canvas>();
                if (can != null) can.gameObject.layer = 0;
            }
        }
        
        
        if (Vector3.Distance(transform.position, pointOne.transform.position) < 2)
        {
            moveToP2 = true;
            moveToP1 = false;
        }
        if (moveToP2)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointTwo.transform.position, speed);
        }
        if (Vector3.Distance(transform.position, pointTwo.transform.position) < 2)
        {
            moveToP1 = true;
            moveToP2 = false;
        }
        if (moveToP1)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointOne.transform.position, speed);
        }
         
    }


    public void stopRunning()
    {
        if (freezeBot)
        {
            Frozen = true;
            gameObject.layer = 0;
            foreach (Transform trans in gameObject.GetComponentsInChildren<Transform>(true))
            {
                trans.gameObject.layer = 0;
                directionArrow.SetActive(true);
                TutorialStateController.GhostFreezeDone = true;
                
            }
        }
    }
}
