using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RandomAbilityController1 : MonoBehaviour
{
    private List<string> hunterAbilities = new List<string>() { "Speed Up", "Shorten Cooldown", "Inhance Indicator" };
    private List<string> ghostAbilities = new List<string>() { "Speed Up", "Shorten Coolown", "Increase Freeze Time"};
    public GameObject hunterPanel;
    public GameObject ghostPanel;
    private bool isShown;
    private GameStateController gsc;
    public int winner;
    public string chosenAbility;

    // Start is called before the first frame update
    void Start()
    {
        gsc = gameObject.GetComponent<GameStateController>();
        isShown = false;
        hunterPanel.SetActive(false);
        ghostPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gsc.GetRound() >= 1)
        {
            if (gsc.GetRoundOver() && gsc.GetWinner() != -1)
            {
                winner = gsc.GetWinner();
            }
            if (!isShown && (winner == 1 || winner == 2))
            {
                if (winner == 1)
                {
                    ghostPanel.SetActive(true);
                    GetRandomAbilities(ghostAbilities, ghostPanel);
                }
                else if (winner == 2)
                {
                    hunterPanel.SetActive(true);
                    GetRandomAbilities(hunterAbilities, hunterPanel);
                }
                isShown = true;
            }
        }
    }

    private void GetRandomAbilities(List<string> abilities,GameObject panel)
    {
        List<int> usedIntegers = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            getRandomInt(usedIntegers);
            panel.transform.GetChild(i + 1).GetChild(0).GetComponent<Text>().text = abilities[usedIntegers[i]];
        }
    }

    private void getRandomInt(List<int> integers)
    {
        int val = Random.Range(0,3);
        while (integers.Contains(val))
        {
            val = Random.Range(0,3);
        }
        integers.Add(val);
    }

    public void ChooseAbility()
    {
        if (chosenAbility == "")
        {
            GameObject b = EventSystem.current.currentSelectedGameObject;
            chosenAbility = b.transform.GetChild(0).GetComponent<Text>().text;
            gsc.ReceiveAbility(chosenAbility);
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    public string GetChosenAbility()
    {
        return chosenAbility;
    }
}
