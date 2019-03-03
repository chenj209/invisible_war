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
                if (GameStateController.HunterCatchDone)
                {
           
                    animator.SetBool("Opening", true);
                }
            }else if (doorID == 3)
            {
                if (GameStateController.HunterShootDone)
                {
                    animator.SetBool("Opening", true);
                }
            }else if (doorID == 6)
            {
                if (GameStateController.GhostFreezeDone)
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
                GameStateController.HunterTutOne = true;
            }
            else if (doorID == 2 && GameStateController.HunterCatchDone)
            {
                GameStateController.HunterTutTwo = true;
            }
            else if (doorID == 3 && GameStateController.HunterShootDone)
            {
                GameStateController.HunterTutThree = true;
            }
            else if (doorID == 7)
            {
                GameStateController.HunterTutFour = true;
                GameStateController.HunterTutDone = true;
            }
            else if (doorID == 4)
            {
                GameStateController.GhostTutOne = true;
            }
            else if (doorID == 5)
            {
                GameStateController.GhostTutTwo = true;
            }
            else if (doorID == 6 && GameStateController.GhostFreezeDone)
            {
                GameStateController.GhostTutThree = true;
                GameStateController.GhostTutDone = true;
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
