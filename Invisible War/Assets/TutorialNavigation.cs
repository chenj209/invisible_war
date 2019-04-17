using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialNavigation : MonoBehaviour
{
    // Start is called before the first frame update
    public Text HunterMechTitle;
    public Text GhostMechTitle;
    public Image HunterLeftImage;
    public Image HunterRightImage;
    public Image GhostLeftImage;
    public Image GhostRightImage;
    public Sprite CatchImage;
    public Sprite ShootImage;
    public Sprite FreezeImage;
    public Sprite RitualImage;
    public Sprite LeftJoystick;
    public RenderTexture HunterMove;
    public RenderTexture HunterIndicator;
    public RenderTexture HunterCatch;
    public RenderTexture HunterRitual;
    public RenderTexture HunterShoot;
    public RenderTexture GhostMove;
    public RenderTexture GhostIndicator;
    public RenderTexture GhostFreeze;
    public RenderTexture GhostRitual;
    public RenderTexture GhostCaught;
    public RawImage HunterVideo;
    public RawImage GhostVideo;
    public VideoPlayer HunterVideoPlayer;
    public VideoPlayer GhostVideoPlayer;
    public VideoClip HunterMoveClip;
    public VideoClip HunterIndicatorClip;
    public VideoClip HunterCatchClip;
    public VideoClip HunterRitualClip;
    public VideoClip HunterShootClip;
    public VideoClip GhostMoveClip;
    public VideoClip GhostIndicatorClip;
    public VideoClip GhostFreezeClip;
    public VideoClip GhostRitualClip;
    public VideoClip GhostCaughtClip;
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
    public Text HunterReady;
    public Text GhostReady;
   


    private int HunterPageNum;
    private int GhostPageNum;
    private GameStateController gsc;
    private bool hcpressed;
    private bool hbpressed;
    private bool gcpressed;
    private bool gbpressed;
    private bool hunterReady;
    private bool ghostReady;
    private bool hunterLastPage;
    private bool ghostLastPage;

    void Start()
    {
        gsc = GetComponent<GameStateController>();
        HunterPageNum = 0;
        GhostPageNum = 0;
        gsc.gamestart = false;
        hcpressed = false;
        hbpressed = false;
        gcpressed = false;
        gbpressed = false;
        hunterReady = false;
        ghostReady = false;
        hunterLastPage = false;
        ghostLastPage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("HunterContinue") > 0 || Input.GetKeyDown(KeyCode.H))
        {
            if (!hcpressed)
            {
                if (hunterLastPage)
                {
                    if (ghostReady)
                    {
                        HunterReady.text = "Loading Game . . .";
                        GhostReady.text = "Loading Game . . .";
                        StartCoroutine(startGame());
                    }
                    hunterReady = true;
                    HunterLoadingScreen.SetActive(true);
                    HunterNav.SetActive(false);
                }
                if (HunterPageNum == 0)
                {
                    HunterMechTitle.text = "Indicator :";
                    HunterExplaination.text = "Indicates the direction of the ghost\nWill disappear when very close";
                    HunterLeftImage.enabled = false;
                    HunterRightImage.enabled = false;
                    HunterBack.SetActive(true);
                    HunterVideo.texture = HunterIndicator;
                    HunterVideoPlayer.clip = HunterIndicatorClip;
                    HunterVideoPlayer.targetTexture = HunterIndicator;
                    HunterPageNum++;
                }
                else if (HunterPageNum == 1)
                {
                    HunterMechTitle.text = "Catch :";
                    HunterExplaination.text = "Catch the ghost for the win";
                    HunterLeftImage.enabled = true;
                    HunterLeftImage.sprite = CatchImage;
                    HunterVideo.texture = HunterCatch;
                    HunterVideoPlayer.clip = HunterCatchClip;
                    HunterVideoPlayer.targetTexture = HunterCatch;
                    HunterPageNum++;
                }
                else if (HunterPageNum == 2)
                {
                    HunterMechTitle.text = "Ritual Counter :";
                    HunterExplaination.text = "Shows the number of rituals completed";
                    HunterLeftImage.enabled = false;
                    HunterVideo.texture = HunterRitual;
                    HunterVideoPlayer.clip = HunterRitualClip;
                    HunterVideoPlayer.targetTexture = HunterRitual;
                    HunterPageNum++;
                }
                else if (HunterPageNum == 3)
                {
                    HunterMechTitle.text = "Shoot :";
                    HunterExplaination.text = "Paints whatever gets hit\nCan interrupt spell casting";
                    HunterLeftImage.enabled = true;
                    HunterLeftImage.sprite = ShootImage;
                    HunterContinue.text = "Ready";
                    HunterVideo.texture = HunterShoot;
                    HunterVideoPlayer.clip = HunterShootClip;
                    HunterVideoPlayer.targetTexture = HunterShoot;
                    HunterPageNum++;
                    hunterLastPage = true;
                }
            }

            hcpressed = true;
        }

        if (Input.GetAxis("HunterContinue") == 0)
        {
            hcpressed = false;
        }

        if (Input.GetAxis("HunterBack") > 0 || Input.GetKeyDown(KeyCode.J))
        {
            if (!hbpressed)
            {
                if (HunterPageNum == 1)
                {
                    HunterMechTitle.text = "Movement :";
                    HunterExplaination.text = "Character Movement Control";
                    HunterLeftImage.enabled = true;
                    HunterRightImage.enabled = true;
                    HunterLeftImage.sprite = LeftJoystick;
                    HunterBack.SetActive(false);
                    HunterVideo.texture = HunterMove;
                    HunterVideoPlayer.clip = HunterMoveClip;
                    HunterVideoPlayer.targetTexture = HunterMove;
                    HunterPageNum--;
                }
                else if (HunterPageNum == 2)
                {
                    HunterMechTitle.text = "Indicator :";
                    HunterExplaination.text = "Indicates the direction of the ghost\nWill disappear when very close";
                    HunterLeftImage.enabled = false;
                    HunterRightImage.enabled = false;
                    HunterBack.SetActive(true);
                    HunterVideo.texture = HunterIndicator;
                    HunterVideoPlayer.clip = HunterIndicatorClip;
                    HunterVideoPlayer.targetTexture = HunterIndicator;
                    HunterPageNum--;
                }
                else if (HunterPageNum == 3)
                {
                    HunterMechTitle.text = "Catch :";
                    HunterExplaination.text = "Catch the ghost for the win";
                    HunterLeftImage.enabled = true;
                    HunterLeftImage.sprite = CatchImage;
                    HunterVideo.texture = HunterCatch;
                    HunterVideoPlayer.clip = HunterCatchClip;
                    HunterVideoPlayer.targetTexture = HunterCatch;
                    HunterPageNum--;
                }
                else if (HunterPageNum == 4)
                {
                    HunterMechTitle.text = "Ritual Counter :";
                    HunterExplaination.text = "Shows the number of rituals completed";
                    HunterLeftImage.enabled = false;
                    HunterContinue.text = "Continue";
                    HunterVideo.texture = HunterRitual;
                    HunterVideoPlayer.clip = HunterRitualClip;
                    HunterVideoPlayer.targetTexture = HunterRitual;
                    HunterPageNum--;
                    hunterLastPage = false;
                }
            }

            hbpressed = true;
        }

        if (Input.GetAxis("HunterBack") == 0)
        {
            hbpressed = false;
        }


        if (Input.GetAxis("Continue") > 0)
        {
            if (!gcpressed)
            {
                if (ghostLastPage)
                {
                    if (hunterReady)
                    {
                        HunterReady.text = "Loading Game . . .";
                        GhostReady.text = "Loading Game . . .";
                        StartCoroutine(startGame());
                    }
                    ghostReady = true;
                    GhostLoadingScreen.SetActive(true);
                    GhostNav.SetActive(false);
                }
                if (GhostPageNum == 0)
                {
                    GhostMechTitle.text = "Indicator :";
                    GhostExplaination.text = "Red means hunter very close\nGreen means hunter far away";
                    GhostLeftImage.enabled = false;
                    GhostRightImage.enabled = false;
                    GhostBack.SetActive(true);
                    GhostVideo.texture = GhostIndicator;
                    GhostVideoPlayer.clip = GhostIndicatorClip;
                    GhostVideoPlayer.targetTexture = GhostIndicator;
                    GhostPageNum++;
                }
                else if (GhostPageNum == 1)
                {
                    GhostMechTitle.text = "Freeze :";
                    GhostExplaination.text = "Freeze the hunter to avoid\ngetting caught";
                    GhostLeftImage.enabled = true;
                    GhostLeftImage.sprite = FreezeImage;
                    GhostVideo.texture = GhostFreeze;
                    GhostVideoPlayer.clip = GhostFreezeClip;
                    GhostVideoPlayer.targetTexture = GhostFreeze;
                    GhostPageNum++;
                }
                else if (GhostPageNum == 2)
                {
                    GhostMechTitle.text = "Complete Ritual :";
                    GhostExplaination.text = "Complete 4 out of 5 rituals to win";
                    GhostLeftImage.enabled = true;
                    GhostLeftImage.sprite = RitualImage;
                    GhostVideo.texture = GhostRitual;
                    GhostVideoPlayer.clip = GhostRitualClip;
                    GhostVideoPlayer.targetTexture = GhostRitual;
                    GhostPageNum++;
                }
                else if (GhostPageNum == 3)
                {
                    GhostMechTitle.text = "Get Caught :";
                    GhostExplaination.text = "Avoid getting caught by hunter";
                    GhostLeftImage.enabled = false;
                    GhostContinue.text = "Ready";
                    GhostVideo.texture = GhostCaught;
                    GhostVideoPlayer.clip = GhostCaughtClip;
                    GhostVideoPlayer.targetTexture = GhostCaught;
                    GhostPageNum++;
                    ghostLastPage = true;
                }
            }

            gcpressed = true;
        }

        if (Input.GetAxis("Continue") == 0)
        {
            gcpressed = false;
        }

        if (Input.GetAxis("Back") > 0)
        {
            if (!gbpressed)
            {
                if (GhostPageNum == 1)
                {
                    GhostMechTitle.text = "Movement :";
                    GhostExplaination.text = "Character Movement Control\nCan move through furnitures";
                    GhostLeftImage.enabled = true;
                    GhostRightImage.enabled = true;
                    GhostLeftImage.sprite = LeftJoystick;
                    GhostBack.SetActive(false);
                    GhostVideo.texture = GhostMove;
                    GhostVideoPlayer.clip = GhostMoveClip;
                    GhostVideoPlayer.targetTexture = GhostMove;
                    GhostPageNum--;
                }
                else if (GhostPageNum == 2)
                {
                    GhostMechTitle.text = "Indicator :";
                    GhostExplaination.text = "Red means hunter very close\nGreen means hunter far away";
                    GhostLeftImage.enabled = false;
                    GhostRightImage.enabled = false;
                    GhostBack.SetActive(true);
                    GhostVideo.texture = GhostIndicator;
                    GhostVideoPlayer.clip = GhostIndicatorClip;
                    GhostVideoPlayer.targetTexture = GhostIndicator;
                    GhostPageNum--;
                }
                else if (GhostPageNum == 3)
                {
                    GhostMechTitle.text = "Freeze :";
                    GhostExplaination.text = "Freeze the hunter to avoid\ngetting caught";
                    GhostLeftImage.enabled = true;
                    GhostLeftImage.sprite = FreezeImage;
                    GhostVideo.texture = GhostFreeze;
                    GhostVideoPlayer.clip = GhostFreezeClip;
                    GhostVideoPlayer.targetTexture = GhostFreeze;
                    GhostPageNum--;
                }
                else if (GhostPageNum == 4)
                {
                    GhostMechTitle.text = "Complete Ritual :";
                    GhostExplaination.text = "Complete 4 out of 5 rituals to win";
                    GhostLeftImage.enabled = true;
                    GhostLeftImage.sprite = RitualImage;
                    GhostVideo.texture = GhostRitual;
                    GhostVideoPlayer.clip = GhostRitualClip;
                    GhostVideoPlayer.targetTexture = GhostRitual;
                    GhostPageNum--;
                    ghostLastPage = false;
                }
            }

            gbpressed = true;
        }

        if (Input.GetAxis("Back") == 0)
        {
            gbpressed = false;
        }
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(5);
        gsc.gamestart = true;
        HunterLoadingScreen.SetActive(false);
        GhostLoadingScreen.SetActive(false);
        GhostNav.SetActive(false);
        HunterNav.SetActive(false);
    }
}
