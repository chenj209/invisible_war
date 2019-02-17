using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    ShortcutTip shortcutTip;
    private void Start()
    {
        //text.gameObject.SetActive(false);
        animator = gameObject.GetComponent<Animator>();
        shortcutTip = gameObject.GetComponent<ShortcutTip>();
        animator.SetBool("Openable", true);
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("wtf");
            bool opened = animator.GetBool("Opened");
            bool opening = animator.GetBool("Opening");
            bool closing = animator.GetBool("Closing");
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (opened) {
                    animator.SetBool("Closing", true);
                }
                else
                {
                    animator.SetBool("Opening", true);
                }
            }
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
