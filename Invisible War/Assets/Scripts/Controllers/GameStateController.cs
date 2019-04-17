using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    public static int round = 3;
    private static int roundNum = 1;
    public int levelToLoad;
    public GameObject hunter;
    public GameObject ghost;
    public GameObject sealSystem;
    public Image crosshair;
    private Animator an;
    private bool roundOver;
    public static bool caught = false;

    public Text p1S;
    public Text p2S;
    public float delay;

    public Text cdUI3;
    public Text cdUI4;
    public float startCD;
    public float endCD;

    private bool cdBool;

    public Image scoreBoard;
    public Text name;
    public Text Player1Score;
    public Text Player2Score;
    public float scoreCD;
    public static int p1w;
    public static int p2w;


    public bool Intutorial;
    public UIFader hunterInstruction;
    public GameObject hunterIndicatorEffect;
    public Text hunterInstructionText;
    public UIFader ghostInstruction;
    public GameObject ghostIndicatorEffect;
    public Text ghostInstructionText;
    private bool fadeoutFirst = true;

    public static List<int> winners = new List<int>();
    public static List<string> chosenAbilities = new List<string>();
    private RandomAbilityController1 rac1;

    public bool gamestart = true;

    void Start()
    {
        scoreBoard.enabled = false;
        name.enabled = false;
        Player1Score.enabled = false;
        Player2Score.enabled = false;
        caught = false;
        roundOver = false;
        cdUI3.enabled = true;
        cdUI4.enabled = true;
        p1S.enabled = false;
        p2S.enabled = false;
        an = gameObject.GetComponent<Animator>();
        cdBool = true;
        rac1 = gameObject.GetComponent<RandomAbilityController1>();
        ApplyAbility();
        DisablePlayers();
    }

    // Update is called once per frame
    void Update()
    {
        if (cdBool && gamestart)
        {
            if (startCD >= 0)
            {
                StandBy();
            }
            else
            {
                Game();
            }
        }
        if (Intutorial){
            if (hunterInstruction.FadeInOver && fadeoutFirst)
            {
                fadeoutFirst = false;
                StartCoroutine(FadeOut());
            }
        }
        
        if (!roundOver)
        {
            PlayerStatus ps1 = hunter.GetComponent<PlayerStatus>();
            SealSystem ss = sealSystem.GetComponent<SealSystem>();
            if (caught && ps1.invincible && !Intutorial)
            {
                StopAllCoroutines();
                StartCoroutine(Win(1));
            }
            if (ss.allDestroyed)
            {
                if (!Intutorial)
                {
                    StopAllCoroutines();
                    StartCoroutine(Win(2));
                }
                else
                {
                    Debug.Log("??????????????");
                    StartCoroutine(FinalFadeOut());
                }
            }
        }
    }

    private void Game()
    {
        if (!Intutorial)
        {
            ActivePlayers();
            cdUI3.enabled = false;
            cdUI4.enabled = false;
            cdBool = false;
            StartCoroutine(PopUp("Game Start", p1S));
            StartCoroutine(PopUp("Game Start", p2S));
        }
        else
        {
            cdUI3.enabled = false;
            cdUI4.enabled = false;
            cdBool = false;
            hunterIndicatorEffect.SetActive(true);
            hunterInstruction.FadeIn();
            ghostIndicatorEffect.SetActive(true);
            ghostInstruction.FadeIn();
        }
    }

    IEnumerator FadeOut()
    {

        yield return new WaitForSeconds(8);
        hunterInstruction.FadeOut();
        ghostInstruction.FadeOut();
        hunter.GetComponent<PlayerControl>().enabled = true;
        ghost.GetComponent<PlayerControl>().enabled = true;
        ghost.GetComponent<Freeze>().enabled = true;
        hunterIndicatorEffect.SetActive(false);
        ghostIndicatorEffect.SetActive(false);
        
    }

    IEnumerator FinalFadeOut()
    {
        DisablePlayers();
        scoreBoard.enabled = true;
        name.enabled = true;
        yield return new WaitForSeconds(4);
        an.SetTrigger("FadeOut");
    }

    IEnumerator Win(int player)
    {
        DisablePlayers();
        winners.Add(player);
        roundOver = true;
        if (player == 1)
        {
            p1w++;
            StartCoroutine(PopUp("You Win", p1S));
            StartCoroutine(PopUp("You Lose", p2S));
        }
        else
        {
            p2w++;
            StartCoroutine(PopUp("You Win", p2S));
            StartCoroutine(PopUp("You Lose", p1S));
        }
        round--;
        roundNum++;
        yield return new WaitForSeconds(delay);

        if (round >= 1)
        {
            rac1.enabled = true;
        }
        else
        {
            DisplayScore();
            round = 3;
            roundNum = 1;
            p1w = 0;
            p2w = 0;
            winners = new List<int>();
            chosenAbilities = new List<string>();
            yield return new WaitForSeconds(scoreCD);
            an.SetTrigger("FadeOut");
        }
    }

    private void DisplayScore()
    {
        scoreBoard.enabled = true;
        name.enabled = true;
        Player1Score.enabled = true;
        Player2Score.enabled = true;
        Player1Score.text = p1w.ToString();
        Player2Score.text = p2w.ToString();
    }

    void StandBy()
    {
        startCD -= Time.deltaTime;
        int tl = (int)(startCD + 1);
        cdUI3.text = tl.ToString();
        cdUI4.text = tl.ToString();
    }

    private void DisablePlayers()
    {
        crosshair.enabled = false;
        hunter.GetComponent<PlayerControl>().enabled = false;
        hunter.GetComponent<shooting>().enabled = false;
        if (ghost)
        {
            ghost.GetComponent<PlayerControl>().enabled = false;
            ghost.GetComponent<Freeze>().enabled = false;
        }
    }

    private void ActivePlayers()
    {
        crosshair.enabled = true;
        hunter.GetComponent<PlayerControl>().enabled = true;
        hunter.GetComponent<shooting>().enabled = true;
        ghost.GetComponent<PlayerControl>().enabled = true;
        ghost.GetComponent<Freeze>().enabled = true;
    }

    IEnumerator PopUp(string state, Text text)
    {
        text.text = state;
        text.enabled = true;
        yield return new WaitForSeconds(delay);
        text.enabled = false;
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public int GetRoundNum()
    {
        return roundNum;
    }

    public int GetWinner()
    {
        if (winners.Count>=1)
        {
            return winners[winners.Count - 1];
        }
        else
        {
            return -1;
        }
    }

    public bool GetRoundOver()
    {
        return roundOver;
    }

    public int GetRound()
    {
        return round;
    }

    public void ReceiveAbility(string ability)
    {
        chosenAbilities.Add(ability);
        //ApplyAbility();
    }

    private void ApplyAbility()
    {
        Debug.Log("Applying ablity");
        for (int i = 0;i<roundNum-1;i++)
        {
            Debug.Log("here" + i);
            if (winners[i] == 1)//ghost gets ability
            {
                Debug.Log("winner hunter" + chosenAbilities[i]);
                switch (chosenAbilities[i])
                {
                    case "Speed Up":
                        Debug.Log("Apply Ghost Speed Up");
                        GameConfig.instance.ghostSpeed += GameConfig.instance.abilityModifier * 10;
                        if (GameConfig.instance.ghostSpeed > 60)
                        {
                            GameConfig.instance.ghostSpeed = 60;
                        }
                        break;
                    case "Shorten Cooldown":
                        Debug.Log("Apply Ghost Shorten Cooldown");
                        GameConfig.instance.freezeCoolDown /= GameConfig.instance.abilityModifier;
                        break;
                    case "Increase Freeze Time":
                        Debug.Log("Apply Ghost Increase Freeze Time");
                        GameConfig.instance.freezeEffectDuration *= GameConfig.instance.abilityModifier;
                        break;
                    default:
                        break;
                }
            }
            else if (winners[i] == 2)//hunter gets ability
            {
                Debug.Log("winner ghost, " + chosenAbilities[i]);
                switch (chosenAbilities[i])
                {
                    case "Speed Up":
                        Debug.Log("Apply Hunter Speed Up");
                        GameConfig.instance.hunterSpeed += GameConfig.instance.abilityModifier * 10;
                        if (GameConfig.instance.hunterSpeed > 60)
                        {
                            GameConfig.instance.hunterSpeed = 60;
                        }
                        break;
                    case "Shorten Cooldown":
                        Debug.Log("Apply Hunter Shorten Cooldown");
                        GameConfig.instance.paintgunCooldown /= GameConfig.instance.abilityModifier;
                        break;
                    case "Inhance Indicator":
                        Debug.Log("Apply Hunter Inhance Indicator");
                        GameConfig.instance.hunterIndicatorNearBy = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}