using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInteractionInstruction : MonoBehaviour
{
    public GameObject loadingBar;

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
            loadingBar.SetActive(false);
        }
    }
}
