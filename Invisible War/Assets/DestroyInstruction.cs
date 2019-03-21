using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyInstruction : MonoBehaviour
{
    private bool firstTime = true;
    public GameObject panel;
    public Text instruction;
    public GameObject ghost;
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
            instruction.text = "Press □ To Destroy\nThe Force Field Base";
            firstTime = false;
        }
    }
}