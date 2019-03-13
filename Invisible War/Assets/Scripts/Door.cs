using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    ShortcutTip shortcutTip;
    public ParticleDecalPool decalPool;
    public int doorID;
    public bool requireKeyPress = false;

    private void Start()
    {
        //text.gameObject.SetActive(false);
        animator = gameObject.GetComponentInChildren<Animator>();
        shortcutTip = gameObject.GetComponent<ShortcutTip>();
        SetDoorAnimationStatus("Closed");
    }
    public void OnTriggerEnterDirection(Collider col, string direction)
    {
        if (col.gameObject.tag == "Player" && !col.gameObject.name.Contains("Bot"))
        {
            string currentDoorAnimationStatus = GetDoorAnimationStatus();
            PlayerControl playerControl = col.gameObject.GetComponent<PlayerControl>();
            string playerID = playerControl == null ? "" : playerControl.playerID;
            if (doorID == 1)
            {
                TutorialStateController.HunterTutOne = true;
                InteractOpen(playerID, direction, requireKeyPress);
            }
            else if (doorID == 2)
            {
                if (TutorialStateController.HunterCatchDone)
                {
                    InteractOpen(playerID, direction, requireKeyPress);
                    TutorialStateController.HunterTutTwo = true;
                }
            }else if (doorID == 3)
            {
                //if (TutorialStateController.GhostFreezeDone)
                TutorialStateController.HunterTutThree = true;
                InteractOpen(playerID, direction, requireKeyPress);
            }
            else if (doorID == 7)
            {
                if (TutorialStateController.HunterShootDone)
                {
                    InteractOpen(playerID, direction, requireKeyPress);
                    TutorialStateController.HunterTutFour = true;
                    TutorialStateController.HunterTutDone = true;
                }
            }
            else if (doorID == 4)
            {
                TutorialStateController.GhostTutOne = true;
                InteractOpen(playerID, direction, requireKeyPress);
            }
            else if (doorID == 5)
            {
                TutorialStateController.GhostTutTwo = true;
                InteractOpen(playerID, direction, requireKeyPress);
            }
            else if (doorID == 6)
            {
                if (TutorialStateController.GhostFreezeDone)
                {
                    TutorialStateController.GhostTutThree = true;
                    TutorialStateController.GhostTutDone = true;
                    InteractOpen(playerID, direction, requireKeyPress);
                }
            }
            else
            {
                InteractOpen(playerID, direction, requireKeyPress);
            }

        }
    }

    public void OnTriggerExitDirection(Collider col, string direction)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerControl playerControl = col.gameObject.GetComponent<PlayerControl>();
            string playerID = playerControl == null ? "" : playerControl.playerID;
            InteractClose(playerID, direction, requireKeyPress);

        }
    }

    public void OnTriggerStayDirection(Collider other, string direction)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerControl playerControl = other.gameObject.GetComponent<PlayerControl>();
            string playerID = playerControl == null ? "" : playerControl.playerID;
            InteractOpen(playerID, direction, true);
            InteractClose(playerID, direction, true);
        }
    }

    public void SetDoorAnimationStatus(string statusString)
    {
        animator.SetBool(statusString, true);
        if (statusString == "Opened")
        {
            animator.SetBool("Opening-positive", false);
            animator.SetBool("Opening-negative", false);
            animator.SetBool("Closed", false);
        }
        if (statusString == "Closed")
        {
            animator.SetBool("Closing", false);
            animator.SetBool("Opened", false);
        }
    }

    public string GetDoorAnimationStatus()
    {
        if (animator.GetBool("Opened"))
            return "Opened";
        if (animator.GetBool("Opening-positive") || animator.GetBool("Opening-negative"))
            return "Opening";
        if (animator.GetBool("Closed"))
            return "Closed";
        return "Closing";
    }

    public void InteractOpen(string playerID, string direction, bool requireKeyPressVal = false)
    {
        // direction: intereact direction, one of "positive" or "negative"
        if (playerID != "")
        {
            string currentDoorAnimationStatus = GetDoorAnimationStatus();
            if (currentDoorAnimationStatus == "Closed")
            {
                if (!requireKeyPressVal || (Input.GetButton("Interact" + playerID)))
                {
                    SetDoorAnimationStatus("Opening-" + direction);
                }
            }
        }
    }

    public void InteractClose(string playerID, string direction, bool requireKeyPressVal = false)
    { 
        if (playerID != "")
        {
            string currentDoorAnimationStatus = GetDoorAnimationStatus();
            if (currentDoorAnimationStatus == "Opened") 
            {
                if (!requireKeyPressVal || (Input.GetButton("Interact" + playerID)))
                {
                    SetDoorAnimationStatus("Closing");
                }
            }
            if (decalPool != null) decalPool.ClearParticles();

        }
    }
    public void AnimateStateExit(AnimatorStateInfo stateInfo)
    {
        if (stateInfo.IsName("OpeningPositive") || stateInfo.IsName("OpeningNegative"))
        {
            SetDoorAnimationStatus("Opened");
        }
        if (stateInfo.IsName("ClosingPositive") || stateInfo.IsName("ClosingNegative"))
        {
            SetDoorAnimationStatus("Closed");
        }

    }
}
