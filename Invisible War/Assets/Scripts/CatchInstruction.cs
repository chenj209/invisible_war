using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatchInstruction : MonoBehaviour
{
    private bool firstTimeOne = true;
    private bool firstTimeTwo = true;
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
        if (other.gameObject.tag == "Player")
        {

            panel.SetActive(true);
            if (tutNum == 2 && firstTimeOne) {
                instruction.text = "Look, Ghost!\n Move Close And Press C To Catch It ";
                TutorialStateController.BlockSceneFn(true, 3, "");
                firstTimeOne = false;
            }
            else if (tutNum == 3 && firstTimeTwo && !other.gameObject.name.EndsWith("Bot"))
            {
                panel.SetActive(true);
                shooting st = hunter.GetComponent<shooting>();
                st.enabled = true;
                instruction.text = "Left Click To Fire Paintballs\n Hit Ghost To Track It!\n It Has 8s Cooldown";
                TutorialStateController.BlockSceneFn(true, 3, "huntercd");
                firstTimeTwo = false;
            }
           
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           
            if (tutNum == 3)
            {
                panel.SetActive(true);
                if (firstTimeTwo && !other.gameObject.name.EndsWith("Bot"))
                {
                    instruction.text = "Left Click To Fire Paintballs\n Hit Ghost To Track It!\n It Has 8s Cooldown";
                    TutorialStateController.BlockSceneFn(true, 3, "huntercd");
                }
            }
           
        }
    }
}
