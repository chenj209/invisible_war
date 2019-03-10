using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatchInstruction : MonoBehaviour
{
    private bool firstTime = true;
    public GameObject panel;
    public Text instruction;
    public int tutNum;
    public GameObject hunter;
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
            if (tutNum == 2) {
                instruction.text = "Look, Ghost!\n Move Close And Press C To Catch It ";
             
            }
            else if (tutNum == 3)
            {
                shooting st = hunter.GetComponent<shooting>();
                st.enabled = true;
                instruction.text = "Left Click To Fire Paintballs\n Hit Ghost To Track It!\n It Has 8s Cooldown";
         
            }
            firstTime = false;
           


        }
    }
}
