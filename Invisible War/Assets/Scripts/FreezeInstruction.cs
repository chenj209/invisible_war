using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeInstruction : MonoBehaviour
{
    private bool firstTime = true;
    public GameObject panel;
    public Text instruction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && firstTime)
        {
            panel.SetActive(true);
            instruction.text = "Danger! Hunter Nearby!\n Press R2 To Freeze Him\n It Has 20s Cooldown";
            firstTime = false;
        }
    }

    private IEnumerator OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            yield return new WaitForSeconds(3);
            panel.SetActive(false);
        }

    }
}
