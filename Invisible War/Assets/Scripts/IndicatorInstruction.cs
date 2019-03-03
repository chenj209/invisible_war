﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorInstruction : MonoBehaviour
{
    private bool firstTime = true;
    private bool noIndicFirstime = true;
    public GameObject panel;
    public GameObject Indicator;
    public Text instruction;
    public bool show;
    private float timer = 0f;
    private bool showText = false;
    public GameObject ghost;
    private PlayerControl pc;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (showText && show)
        {
            timer += Time.deltaTime;
            if (timer < 2)
            {
                panel.SetActive(true);
                instruction.text = "A Ghost Location Indicator Showed Up!";
            }
            else if (timer > 2 && timer < 5)
            {
                instruction.text = "It Will Be Activated When\n You Are Far Away From Ghost";
            }
            else if (timer > 5 && timer < 8)
            {
                instruction.text = "The Activation Range Will Keep\n Decreasing Throughout The Game";
            }
            else
            {
                panel.SetActive(false);
                timer = 0f;
                showText = false;
                pc.enabled = true;
            }
       
        }else if (showText && !show)
        {
            timer += Time.deltaTime;
            if (timer < 4)
            {
                panel.SetActive(true);
                instruction.text = "Oops, You Are Freezed By The Ghost!\n Player Two Can Use This \n Ability To Escape From You";
            }
            else
            {
                panel.SetActive(false);
                timer = 0f;
                showText = false;
                pc.enabled = true;
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
                pc = other.GetComponent<PlayerControl>();
                pc.enabled = false;
            }
        }
        else
        {
            if (noIndicFirstime)
            {
                Freeze bot = ghost.GetComponent<Freeze>();
                bot.Skill();
                noIndicFirstime = false;
                pc = other.GetComponent<PlayerControl>();
                showText = true;
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
            pc = other.GetComponent<PlayerControl>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (show)
        {
            Indicator.SetActive(false);
        }
    }

}
