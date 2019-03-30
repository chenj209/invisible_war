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
    public GameConfig gc;

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
        rac1 = gameObject.GetComponent<RandomAbilityController1>();
        cdBool = true;
        ApplyAbility();
        DisablePlayers();
    }

    // Update is called once per frame
    void Update()
    {
       if (Intutorial){
            if (hunterInstruction.FadeInOver && fadeoutFirst)
            {
                fadeoutFirst = false;
                StartCoroutine(FadeOut());
            }
        }
        if (cdBool)
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
        if (!roundOver)
        {
            PlayerStatus ps1 = hunter.GetComponent<PlayerStatus>();
            SealSystem ss = sealSystem.GetComponent<SealSystem>();
            if (caught && ps1.invincible)
            {
                StopAllCoroutines();
                StartCoroutine(Win(1));
            }
            if (ss.allDestroyed)
            {
                StopAllCoroutines();
                StartCoroutine(Win(2));
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
        hunter.GetComponent<PlayerControl>().enabled = true;
        ghost.GetComponent<PlayerControl>().enabled = true;
        ghost.GetComponent<Freeze>().enabled = true;
        hunterIndicatorEffect.SetActive(false);
        ghostIndicatorEffect.SetActive(false);
        hunterInstruction.FadeOut();
        ghostInstruction.FadeOut();
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
    }

    private void ApplyAbility()
    {
        for (int i = 0;i<roundNum-1;i++)
        {
            if (winners[i] == 1)//ghost gets ability
            {
                switch (chosenAbilities[i])
                {
                    case "Speed Up":
                        Debug.Log("Apply Ghost Speed Up");
                        gc.ghostSpeed *= 1.1f;
                        Debug.Log(gc.ghostSpeed);
                        break;
                    case "Shorten Cooldown":
                        Debug.Log("Apply Ghost Shorten Cooldown");
                        gc.paintgunCooldown /= 1.1f;
                        break;
                    case "Increase Freeze Time":
                        Debug.Log("Apply Ghost Increase Freeze Time");
                        gc.freezeEffectDuration *= 1.1f;
                        break;
                    default:
                        break;
                }
            }
            else if (winners[i] == 2)//hunter gets ability
            {
                switch (chosenAbilities[i])
                {
                    case "Speed Up":
                        Debug.Log("Apply Hunter Speed Up");
                        gc.hunterSpeed *= 1.1f;
                        break;
                    case "Shorten Cooldown":
                        Debug.Log("Apply Hunter Shorten Cooldown");
                        gc.freezeCoolDown /= 1.1f;
                        break;
                    case "Inhance Indicator":
                        Debug.Log("Apply Hunter Inhance Indicator");
                        gc.disappearRange /= 1.1f;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
