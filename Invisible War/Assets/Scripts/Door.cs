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
    public Collider doorCollider;
    private bool openable = true;

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
            }
            else if (doorID == 2)
            {
                if (TutorialStateController.HunterCatchDone)
                {
                    openable = true;
                    TutorialStateController.HunterTutTwo = true;
                }
                else
                {
                    openable = false;
                }
            }
            else if (doorID == 3)
            {
                if (TutorialStateController.HunterFreezeDone)
                {
                    openable = true;
                    TutorialStateController.HunterTutThree = true;
                }
                else
                {
                    openable = false;
                }
            }
            else if (doorID == 7)
            {
                if (TutorialStateController.HunterShootDone)
                {
                    openable = true;
                    TutorialStateController.HunterTutFour = true;
                    TutorialStateController.HunterTutDone = true;
                }
                else
                {
                    openable = false;
                }
            }
            else if (doorID == 4)
            {
                TutorialStateController.GhostTutOne = true;
            }
            else if (doorID == 5)
            {
                TutorialStateController.GhostTutTwo = true;
            }
            else if (doorID == 6)
            {
                if (TutorialStateController.GhostFreezeDone)
                {
                    openable = true;
                    TutorialStateController.GhostTutThree = true;
                    TutorialStateController.GhostTutDone = true;
                }
                else
                {
                    openable = false;
                }
            }
            if (openable)
                InteractOpen(playerID, direction, requireKeyPress);

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
            if (requireKeyPress)
            {
                if (openable)
                    InteractOpen(playerID, direction, true);
                InteractClose(playerID, direction, true);
            }
            else
            {
                if (openable)
                    InteractOpen(playerID, direction, false);
            }
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
    public void AnimateStateEnter()
    {
        //doorCollider.isTrigger = true;
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
        //doorCollider.isTrigger = false;

    }
}
