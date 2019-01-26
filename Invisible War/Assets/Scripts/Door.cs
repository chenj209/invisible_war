using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Canvas text;
    private Animator animator;
    private void Start()
    {
        text.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        animator.SetBool("opened", false);
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            text.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("pressed");
                animator.SetBool("opened", true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            text.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        bool opened = animator.GetBool("opened");
        if (opened)
        {
            text.gameObject.SetActive(false);
        }
    }
}
