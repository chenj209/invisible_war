using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchGhost : MonoBehaviour
{
    private GameObject ghost;
    public bool inTutorial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))// catch
        {
            if (ghost != null)
            {
                Catch(ghost);
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
            else
            {
                //Debug.Log("No player is detected");
            }
        }
        else
        {
            ghost = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            ghost = other.gameObject;
        }
    }

    private void Catch(GameObject player)
    {
        player.GetComponent<PlayerStatus>().Caught();
    }
}
