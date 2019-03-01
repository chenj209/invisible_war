using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorInstruction : MonoBehaviour
{
    private bool firstTime = true;
    public GameObject panel;
    public GameObject Indicator;
    public Text instruction;
    public bool show;
    private float timer = 0f;
    private bool showText = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (showText)
        {
            timer += Time.deltaTime;
            if (timer < 3)
            {
                panel.SetActive(true);
                instruction.text = "A Ghost Location Indicator Showed\n Up On The Topleft Corner!";
            }
            else if (timer > 3 && timer < 6)
            {
                instruction.text = "It Will Be Activated When\n You Are Far Away From Ghost";
            }
            else if (timer > 6 && timer < 9)
            {
                instruction.text = "The Activation Range Will Keep\n Decreasing Throughout The Game";
            }
            else
            {
                panel.SetActive(false);
                timer = 0f;
                showText = false;
            }
       
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (show)
        {
            if (other.gameObject.tag == "Player" && firstTime)
            {

                showText = true;
                firstTime = false;

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (show)
        {
            Indicator.SetActive(true);
        }
        else
        {
            Indicator.SetActive(false);
        }
    }

}
