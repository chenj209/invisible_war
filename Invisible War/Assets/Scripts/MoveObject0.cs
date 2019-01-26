using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveObject0 : MonoBehaviour
{
    public Text pickUpText;
    public Transform camPos;
    public Transform rightHand;
    public Transform leftHand;
    public float distance;
    public float force;

    private float startTime;

    void Start()
    {
        pickUpText.text = "";
    }

    void Update()
    {
        GameObject item = DetectItem();
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

    private GameObject DetectItem() 
    {
        RaycastHit hit;
        GameObject target;
        if (!Physics.Raycast(camPos.position, camPos.TransformDirection(Vector3.forward), out hit, distance))
        {
            pickUpText.text = "";
            //Debug.Log("Cannot detect anything.");
            return null;
        }
        if (hit.rigidbody != null)
        {
            pickUpText.text = "";
            target = hit.rigidbody.gameObject;
        }
        else
        {
            pickUpText.text = "";
            //Debug.Log("No movable object is detected.");
            return null;
        }
        if (target.tag != "Item")
        {
            pickUpText.text = "";
            //Debug.Log("The object detected is not movable.");
            return null;
        }
        if (target.transform.parent != null)
        {
            pickUpText.text = "";
            //Debug.Log("The object belongs to another player.");
            return null;
        }

        pickUpText.text = @"Press P to pick up
Press T to throw";
        //Debug.Log("Find an object.");
        //Debug.Log(hit.distance);
        return target;
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
        target.GetComponent<BoxCollider>().isTrigger = true;
        target.GetComponent<Rigidbody>().isKinematic = true;
        target.transform.position = holdPoint.position;
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
        item.GetComponent<BoxCollider>().isTrigger = false;
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.transform.parent = null;
        item.GetComponent<Rigidbody>().AddForce(camPos.forward * force * duration);
    }
}