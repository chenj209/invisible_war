using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    ShortcutTip shortcutTip;
    private bool showCanvas = false;
    public ParticleDecalPool decalPool;
    public int doorID;
    public bool requireKeyPress = false;
    public string DoorOpeningState;
    public string DoorClosingState;
    static readonly Dictionary<string, int> DOOR_STATUS_MAPPING =
    new Dictionary<string, int>() {
        {"Closed", 0},
        {"Opening", 1 },
        {"Opened", 2},
        {"Closing", 3},
    };

    private void Start()
    {
        //text.gameObject.SetActive(false);
        animator = gameObject.GetComponentInChildren<Animator>();
        shortcutTip = gameObject.GetComponent<ShortcutTip>();
        SetDoorAnimationStatus("Closed");
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("wtf");
            //bool opened = animator.GetBool("Opened");
            //bool opening = animator.GetBool("Opening");
            //bool closing = animator.GetBool("Closing");

            animator = gameObject.GetComponentInChildren<Animator>();
            string currentDoorAnimationStatus = GetDoorAnimationStatus();
            PlayerControl playerControl = col.gameObject.GetComponent<PlayerControl>();
            string playerID = playerControl == null ? "" : playerControl.playerID;
            if (doorID == 2)
            {
                if (TutorialStateController.HunterCatchDone)
                    Interact(playerID, requireKeyPress);
            }else if (doorID == 3)
            {
                if (TutorialStateController.HunterShootDone)
                    Interact(playerID, requireKeyPress);
            }
            else if (doorID == 6)
            {
                if (TutorialStateController.GhostFreezeDone)
                    Interact(playerID, requireKeyPress);
            }
            else
            {
                Interact(playerID, requireKeyPress);
            }

            if (doorID == 1)
            {
               TutorialStateController.HunterTutOne = true;
            }
            else if (doorID == 2 && TutorialStateController.HunterCatchDone)
            {
                TutorialStateController.HunterTutTwo = true;
            }
            else if (doorID == 3 && TutorialStateController.HunterShootDone)
            {
                TutorialStateController.HunterTutThree = true;
            }
            else if (doorID == 7)
            {
                TutorialStateController.HunterTutFour = true;
                TutorialStateController.HunterTutDone = true;
            }
            else if (doorID == 4)
            {
                TutorialStateController.GhostTutOne = true;
            }
            else if (doorID == 5)
            {
                TutorialStateController.GhostTutTwo = true;
            }
            else if (doorID == 6 && TutorialStateController.GhostFreezeDone)
            {
                TutorialStateController.GhostTutThree = true;
                TutorialStateController.GhostTutDone = true;
            }

        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerControl playerControl = col.gameObject.GetComponent<PlayerControl>();
            string playerID = playerControl == null ? "" : playerControl.playerID;
            Interact(playerID, requireKeyPress);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerControl playerControl = other.gameObject.GetComponent<PlayerControl>();
            string playerID = playerControl == null ? "" : playerControl.playerID;
            Interact(playerID, true);
        }
    }

    public void SetDoorAnimationStatus(string statusString)
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        int status = DOOR_STATUS_MAPPING[statusString];
        string statusBinaryString = Convert.ToString(status, 2);
        if (statusBinaryString.Length == 1)
            statusBinaryString = "0" + statusBinaryString;
        animator.SetInteger("StatusBit1", Convert.ToInt32(statusBinaryString[0].ToString()));
        animator.SetInteger("StatusBit0", Convert.ToInt32(statusBinaryString[1].ToString()));
    }

    public string GetDoorAnimationStatus()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        string statusBinaryString = animator.GetInteger("StatusBit1").ToString() + animator.GetInteger("StatusBit0").ToString();
        int statusCode = Convert.ToInt32(statusBinaryString, 2);
        foreach (string key in DOOR_STATUS_MAPPING.Keys)
        {
            if (DOOR_STATUS_MAPPING[key] == statusCode)
            {
                return key;
            }
        }
        return "NotFound";
    }

    public void Interact(string playerID, bool requireKeyPressVal)
    {
        if (playerID != "")
        {
            string currentDoorAnimationStatus = GetDoorAnimationStatus();
            if (currentDoorAnimationStatus == "Closed")
            {
                if (!requireKeyPressVal || (Input.GetButton("Interact" + playerID)))
                {
                    SetDoorAnimationStatus("Opening");
                }
            }
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
        if (stateInfo.IsName(DoorOpeningState))
        {
            SetDoorAnimationStatus("Opened");
        }
        if (stateInfo.IsName(DoorClosingState))
        {
            SetDoorAnimationStatus("Closed");
        }

    }


}
