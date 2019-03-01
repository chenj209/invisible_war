using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameStateController : MonoBehaviour
{
    public Text p1S;
    public Text p2S;
    public float delay;

    public Text cdUI3;
    public Text cdUI4;
    public float startCD;

    // Start is called before the first frame update
    void Start()
    {
        cdUI3.text = ""; 
        cdUI4.text = "";
        p1S.text = "";
        p2S.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (startCD >= 0)
        {
            StandBy();
        }
    }

    void StandBy()
    {
        startCD -= Time.deltaTime;
        int tl = (int)(startCD + 1);
        cdUI3.text = tl.ToString();
        cdUI4.text = tl.ToString();
        if (startCD < 0)
        {
            cdUI3.text = "";
            cdUI4.text = "";
            StartCoroutine(PopUp("Game Start", p1S));
            StartCoroutine(PopUp("Game Start", p2S));
        }
    }

    IEnumerator PopUp(string state, Text text)
    {
        text.text = state;
        yield return new WaitForSeconds(delay);
        text.text = "";
    }
}
