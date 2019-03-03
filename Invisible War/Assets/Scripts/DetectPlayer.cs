using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStatus>().setTransparent(true);
            GetComponentInParent<PlayerStatus>().setTransparent(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStatus>().setTransparent(false);
            GetComponentInParent<PlayerStatus>().setTransparent(false);
        }
    }
}
