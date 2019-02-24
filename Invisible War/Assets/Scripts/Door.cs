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
            animator.SetBool("Opening", true);
            if (decalPool != null) decalPool.ClearParticles();

            /*if (doorID == 1)
            {
                GameStateController.HunterTutOne = true;
            }
            else if (doorID == 2)
            {
                GameStateController.HunterTutTwo = true;
            }
            else if (doorID == 3)
            {
                GameStateController.HunterTutThree = true;
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
            else if (doorID == 6)
            {
                GameStateController.GhostTutThree = true;
                GameStateController.GhostTutDone = true;
            }*/

        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("wtf");
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
