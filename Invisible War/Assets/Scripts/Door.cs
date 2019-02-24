using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    ShortcutTip shortcutTip;
    private bool showCanvas = false;
    public ParticleDecalPool decalPool;
    public int doorID = -1;
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
            decalPool.ClearParticles();

          
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
            decalPool.ClearParticles();

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
