using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Image scoreBoard;
    public Text name;
    public Text Player1Score;
    public Text Player2Score;
    public static int score1 = 0;
    public static int score2 = 0;
    public float countInterval;
    private float interval1;
    private float interval2;
    private float timer1;
    private float timer2;
    private int i = 0;
    private int j = 0;
    private bool countFlag1;
    private bool countFlag2;
    private bool display;
    public bool goToNext;

    public Text dList1;
    private Dictionary<string, int> list1;

    // Start is called before the first frame update
    void Start()
    {
        scoreBoard.enabled = false;
        name.enabled = false;
        Player1Score.enabled = false;
        Player2Score.enabled = false;
        countFlag1 = true; 
        countFlag2 = true;
        interval1 = countInterval;
        interval2 = countInterval;
        display = false;
        goToNext = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (display) {
            //hunter score
            timer1 += Time.deltaTime;
            if (i < score1 && timer1 >= interval1)
            {
                i += 10;
                Player1Score.text = i.ToString();
                timer1 = 0;
                if (score1 < 100)
                {
                    if (countFlag1)
                    {
                        for (int k = 100; k >= score1; k -= 10)
                        {
                            interval1 = 1 - Mathf.Exp(-1.3f * interval1);
                        }
                        countFlag1 = false;
                    }
                    interval1 = 1 - Mathf.Exp(-1.3f * interval1);
                }
                else
                {
                    if (i >= (score1 - 100))
                    {
                        interval1 = 1 - Mathf.Exp(-1.3f * interval1);
                    }
                }
            }
            else if (score1 == 0)
            {
                Player1Score.text = i.ToString();
            }

            //ghost score
            timer2 += Time.deltaTime;
            if (j < score2 && timer2 >= interval2)
            {
                j += 10;
                Player2Score.text = j.ToString();
                timer2 = 0;
                if (score2 < 100)
                {
                    if (countFlag2)
                    {
                        for (int k = 100; k >= score1; k -= 10)
                        {
                            interval2 = 1 - Mathf.Exp(-1.3f * interval2);
                        }
                        countFlag2 = false;
                    }
                    interval2 = 1 - Mathf.Exp(-1.3f * interval2);
                }
                else
                {
                    if (j >= (score2 - 100))
                    {
                        interval2 = 1 - Mathf.Exp(-1.3f * interval2);
                    }
                }
            }
            else if (score2 == 0)
            {
                Player2Score.text = j.ToString();
            }

            if (i ==score1 && j ==score2)
            {
                goToNext = true;
            }
        }
    }

    private void addScore(int score,int player)
    {
        if (player == 1)
        {
            score1 += score;
        }
        else if (player ==2)
        {
            score2 += score;
        }
    }

    public void Win(int player)
    {
        addScore(300, player);
    }

    public void FreezePoint()
    {
        addScore(20, 2);
    }

    public void HitPoint()
    {
        addScore(20, 1);
    }

    public void displayScoreBoard()
    {
        scoreBoard.enabled = true;
        name.enabled = true;
        Player1Score.enabled = true;
        Player2Score.enabled = true;
        display = true;
    }
}
