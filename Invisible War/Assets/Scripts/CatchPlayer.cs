using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchPlayer : MonoBehaviour
{
    public float sphereRadius;
    public float distance;
    public Transform camPos;
    public bool inTutorial;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = DetectItem();
        if (Input.GetKeyDown(KeyCode.C) || Input.GetButton("Catch"))// catch
        {
            if(player != null)
            {
                Catch(player);
                //Debug.Log("You got the hider");
                if (inTutorial)
                {
                    TutorialStateController.HunterCatchDone = true;
                }
                else
                {
                    GameStateController.caught = true;
                }
            }
            else{
                //Debug.Log("No player is detected");
            }
        }
    }

    private GameObject DetectItem()
    {
        RaycastHit hit;
        GameObject target;
        if(camPos == null)
        {
            return null;
        }
        if (Physics.SphereCast(camPos.position, sphereRadius, camPos.TransformDirection(Vector3.forward), out hit, distance))
        {
            target = hit.transform.gameObject;
        }
        else
        {
            return null;
        }
        if (target.tag == "Player")
        {
            //Debug.Log("get ghost");

            return target;
        }
        return null;
    }

    private void Catch(GameObject player)
    {
        player.GetComponent<PlayerStatus>().Caught();
    }
}
