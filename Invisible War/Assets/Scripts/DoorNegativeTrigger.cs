using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNegativeTrigger : MonoBehaviour
{
    private Door doorScript;
    void Start()
    {
        doorScript = gameObject.GetComponentInParent<Door>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        doorScript.OnTriggerEnterDirection(other, "negative");
    }

    private void OnTriggerStay(Collider other)
    {
        doorScript.OnTriggerStayDirection(other, "negative");
    }

    private void OnTriggerExit(Collider other)
    {
        doorScript.OnTriggerExitDirection(other, "negative");
    }
}
