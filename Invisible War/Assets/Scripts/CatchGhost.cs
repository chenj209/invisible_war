using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchGhost : MonoBehaviour
{
    private GameObject ghost;
    public bool inTutorial;
    public GameObject arrow;
    public Animator catchArm;
    public bool isGhost;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.C) && !isGhost) || (Input.GetButtonDown("GhostCatch") && isGhost))// catch
        {
            if (catchArm)
            {
                catchArm.SetBool("catch", true);
            }
            if (ghost != null)
            {
                Catch(ghost);
                ghost = null;
                //Debug.Log("You got the hider");
                if (inTutorial)
                {
                    TutorialStateController.HunterCatchDone = true;
                    arrow.SetActive(true);
                    Animator animator = GetComponentInChildren<Animator>();
                    if (animator)
                        animator.SetBool("oscillate", true);
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
            if (catchArm)
                catchArm.SetBool("catch", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            ghost = other.gameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            ghost = other.gameObject;
        }
    }

    private void Catch(GameObject player)
    {
        if (player)
        player.GetComponent<PlayerStatus>().Caught();
    }
}
