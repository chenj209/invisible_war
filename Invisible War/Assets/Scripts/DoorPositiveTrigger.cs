using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPositiveTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private Door doorScript;
    void Start()
    {
        doorScript = gameObject.GetComponentInParent<Door>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        doorScript.OnTriggerEnterDirection(other, "positive");
    }

    private void OnTriggerStay(Collider other)
    {
        doorScript.OnTriggerStayDirection(other, "positive");
    }

    private void OnTriggerExit(Collider other)
    {
        doorScript.OnTriggerExitDirection(other, "positive");
    }
}
