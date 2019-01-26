using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Canvas text;
    private Animator animator;
    ShortcutTip shortcutTip;
    private void Start()
    {
        //text.gameObject.SetActive(false);
        animator = gameObject.GetComponentInParent<Animator>();
        shortcutTip = gameObject.GetComponent<ShortcutTip>();
        //Debug.Log(animator.parameters);
        //animator.SetBool("opening", false);
        //animator.SetBool("closing", false);
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            bool opened = animator.GetBool("opened");
            bool opening = animator.GetBool("opening");
            bool closing = animator.GetBool("closing");
            if (opened && !opening && !closing)
            {
                shortcutTip.ShowShortcutTip("Press E to Close");
            }
            if (!opened && !opening && !closing)
            {
                shortcutTip.ShowShortcutTip("Press E to Open");
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("pressed");
                if (opened) {
                    animator.SetBool("closing", true);
                }
                else
                {
                    animator.SetBool("opening", true);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            shortcutTip.ShowShortcutTip("");
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
