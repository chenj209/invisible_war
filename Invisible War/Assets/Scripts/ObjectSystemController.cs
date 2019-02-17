using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSystemController : MonoBehaviour
{
    public Transform objectSystem;
    public float holdTime;
    public float distance;
    public float force;

    private float startTime;
    private Transform rightHand;
    private Transform leftHand;
    private List<GameObject> list;

    void Start()
    {
        rightHand = transform.GetChild(0);
        leftHand = transform.GetChild(1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(Count());
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (startTime + holdTime <= Time.time && rightHand.childCount == 0)
            {
                StopAllCoroutines();
            }
        }
        else if (Input.GetKey(KeyCode.L))
        {
            if (startTime + holdTime <= Time.time && leftHand.childCount == 0)
            {
                StopAllCoroutines();
            }
        }

        if (Input.GetKeyUp(KeyCode.R))// right hand actions
        {
            DoActions(rightHand);
        }
        else if (Input.GetKeyUp(KeyCode.L))// left hand actions
        {
            DoActions(leftHand);
        }

    }

    IEnumerator Count()
    {
        startTime = Time.time;
        yield return new WaitForSeconds(holdTime);
        Debug.Log("Realese to throw away.");
    }

    private void Release(Transform hand, int mode)
    {
        GameObject item = hand.GetChild(0).gameObject;
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.transform.parent = null;
        if (mode == 0)// throw away
        {
            item.GetComponent<Rigidbody>().AddForce(transform.forward * force);
        }
        if (hand == rightHand)
        {
            item.GetComponent<MeshRenderer>().enabled = true;//make the object visible
        }

        item.transform.SetParent(objectSystem);
    }

    private void PickUp(Transform hand)
    {
        Transform target = objectSystem.GetChild(0);
        for (int i = 1; i < objectSystem.childCount; i++)// find the closest item
        {
            if (Vector3.Distance(target.position, transform.position) >
               Vector3.Distance(objectSystem.GetChild(i).position, transform.position))
            {
                target = objectSystem.GetChild(i);
            }
        }
        if (target.parent != null && target.parent.tag == "Player")// the object cannot be picked up if it is already hold
        {
            Debug.Log("The object belongs to another player.");
            return;
        }
        float currentDist = Vector3.Distance(transform.position, target.position);
        Debug.Log(currentDist);
        if (currentDist < distance)
        {
            target.parent = null;
            target.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            target.position = hand.position;
            target.SetParent(hand);
            if (hand == rightHand)
            {
                target.gameObject.GetComponent<MeshRenderer>().enabled = false;//make the object invisible
            }
        }
    }

    private void DoActions(Transform hand)
    {
        StopAllCoroutines();
        if (startTime + holdTime <= Time.time && hand.childCount == 1)//long press to throw away
        {
            Release(hand, 0);
        }
        else
        {
            if (hand.childCount == 1)// leave
            {
                Release(hand, 1);
            }
            else// pick up whether long press or not
            {
                PickUp(hand);
            }
        }
    }

}
