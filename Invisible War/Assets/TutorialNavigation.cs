using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialNavigation : MonoBehaviour
{
    // Start is called before the first frame update
    public Text HunterMechTitle;
    public Text GhostMechTitle;
    public Image HunterLeftImage;
    public Image HunterRightImage;
    public Sprite CatchImage;
    public Sprite ShootImage;
    public Sprite FreezeImage;
    public Sprite RitualImage;
    public Text HunterExplaination;
    public Text GhostExplaination;
    public Text HunterContinue;
    public Text GhostContinue;
    public GameObject HunterBack;
    public GameObject GhostBack;
    public GameObject HunterNav;
    public GameObject GhostNav;
    public GameObject HunterLoadingScreen;
    public GameObject GhostLoadingScreen;


    private int HunterPageNum;
    private int GhostPageNum;
    private GameStateController gsc;

    void Start()
    {
        HunterPageNum = 0;
        GhostPageNum = 0;
        gsc.gamestart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("HunterContinue") || Input.GetKeyDown(KeyCode.H))
        {
            if (HunterPageNum == 0)
            {
                HunterMechTitle.text = "Indicator :";
                HunterExplaination.text = "Indicates the direction of the ghost";
                HunterLeftImage.enabled = false;
                HunterRightImage.enabled = false;
                HunterBack.SetActive(true);
                HunterPageNum++;
            }else if (HunterPageNum == 1)
            {
                HunterMechTitle.text = "Catch :";
                HunterExplaination.text = "Catch the ghost for the win";
                HunterLeftImage.enabled = true;
                HunterLeftImage.sprite = CatchImage;
                HunterPageNum++;
            }else if (HunterPageNum == 2)
            {
                HunterMechTitle.text = "Ritual Counter :";
                HunterExplaination.text = "Shows the number of rituals completed";
                HunterLeftImage.enabled = false;
                HunterPageNum++;
            }else if (HunterPageNum == 3)
            {
                HunterMechTitle.text = "Shoot :";
                HunterExplaination.text = "Paints whatever gets hit\nCan interrupt spell casting";
                HunterLeftImage.enabled = true;
                HunterLeftImage.sprite = ShootImage;
                HunterContinue.text = "Ready";
            }
        }
    }
}
