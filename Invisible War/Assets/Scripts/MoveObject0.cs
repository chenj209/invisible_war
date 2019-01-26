using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveObject0 : MonoBehaviour
{
    public Transform camPos;
    public Transform rightHand;
    public Transform leftHand;
    //public float distance;
    public float force;
    private ShortcutTip shortcuttip;
    private float startTime;
    private RaycastTooptip raycasttooltip;

    void Start()
    {
        raycasttooltip = GetComponent<RaycastTooptip>();
        shortcuttip = GetComponent<ShortcutTip>();
    }

    void Update()
    {
        GameObject item = raycasttooltip.DetectItem();
        if (Input.GetKeyDown(KeyCode.P) && item != null)// pick up
        {
            PickUp(item);
        }
        if (Input.GetKeyDown(KeyCode.T))// get the start time of pressing the key
        {
            startTime = Time.time;
        }
        else if (Input.GetKeyUp(KeyCode.T))// throw or drop
        {
            Throw();
        }
    }


    private void PickUp(GameObject target)
    {
        Transform holdPoint;
        if (rightHand.childCount == 0)// pick up with right hand
        {
            holdPoint = rightHand;
            target.GetComponent<MeshRenderer>().enabled = false;//make the object invisible
        }
        else if(leftHand.childCount == 0)// pick up with left hand
        {
            holdPoint = leftHand;
        }
        else// neither hand is empty
        {
            Debug.Log("Neither hand is empty.");
            return;
        }
        target.GetComponent<Collider>().isTrigger = true;
        target.GetComponent<Rigidbody>().isKinematic = true;
        target.transform.position = holdPoint.position;
        shortcuttip.ShowShortcutTip("");
        target.transform.SetParent(holdPoint);
    }

    private void Throw()
    {
        GameObject item;
        float duration = Time.time - startTime;
        if (leftHand.childCount == 1)// throw or drop object on left hand
        {
            item = leftHand.GetChild(0).gameObject;
        }
        else if (rightHand.childCount == 1)// throw or drop object on right hand
        {
            item = rightHand.GetChild(0).gameObject;
            item.GetComponent<MeshRenderer>().enabled = true;//make the object visible
        }
        else// both hands are empty
        {
            Debug.Log("Both hands are empty.");
            return;
        }
        item.GetComponent<Collider>().isTrigger = false;
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.transform.parent = null;
        item.GetComponent<Rigidbody>().AddForce(camPos.forward * force * duration);
    }
}