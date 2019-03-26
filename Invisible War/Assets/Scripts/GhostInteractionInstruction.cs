using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInteractionInstruction : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Seal")
        {
            Seal sealScript = other.GetComponent<Seal>();
            sealScript.DestroySeal(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Seal")
        {
            Seal sealScript = other.GetComponent<Seal>();
            sealScript.DestroySeal(false);
        }
    }
}
