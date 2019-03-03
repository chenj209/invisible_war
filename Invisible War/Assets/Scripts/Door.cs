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

    private void Start()
    {
        //text.gameObject.SetActive(false);
        animator = gameObject.GetComponentInChildren<Animator>();
        shortcutTip = gameObject.GetComponent<ShortcutTip>();
        animator.SetBool("Openable", true);
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
            if (doorID == 2)
            {
                if (TutorialStateController.HunterCatchDone)
                {
           
                    animator.SetBool("Opening", true);
                }
            }else if (doorID == 3)
            {
                if (TutorialStateController.HunterShootDone)
                {
                    animator.SetBool("Opening", true);
                }
            }else if (doorID == 6)
            {
                if (TutorialStateController.GhostFreezeDone)
                {
                    animator.SetBool("Opening", true);
                }
            }
            else
            {
                animator.SetBool("Opening", true);
            }
            if (decalPool != null) decalPool.ClearParticles();

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
            //bool opened = animator.GetBool("Opened");
            //bool opening = animator.GetBool("Opening");
            //bool closing = animator.GetBool("Closing");

            animator = gameObject.GetComponentInChildren<Animator>();
            animator.SetBool("Closing", true);
            if (decalPool != null) decalPool.ClearParticles();

        }
    }

    //void Update()
    //{
    //    bool opened = animator.GetBool("opened");
    //    if (opened)
    //    {
    //        shortcutTip.ShowShortcutTip("");
    //    }
    //}
}
