using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyboardClick : MonoBehaviour
{
    private int selected;
    private int prev_selected;

    // Start is called before the first frame update
    void Start()
    {
        selected = 0;
        prev_selected = -1;
        transform.GetChild(selected+1).GetComponent<Button>().Select();
    }

    // Update is called once per frame
    void Update()
    {
        bool selectInputRight = Input.GetAxis("Horizontal01") > GameConfig.instance.hunterSelectAblitySensitivity;
        bool selectInputLeft = Input.GetAxis("Horizontal01") < -GameConfig.instance.hunterSelectAblitySensitivity;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("HunterContinue"))
        {
            Button b = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            b.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.A) && selected > 1)
        {
            selected = (selected + 2) % 3;
            transform.GetChild(selected+1).GetComponent<Button>().Select();
        }
        if (Input.GetKeyDown(KeyCode.D) && selected < 2)
        {
            selected = (selected + 1) % 3;
            transform.GetChild(selected+1).GetComponent<Button>().Select();
        }
        if ((selectInputLeft || selectInputRight) && prev_selected == selected)
        {
            if (selectInputRight)
            {
                selected = (selected + 1) % 3;
                transform.GetChild(selected+1).GetComponent<Button>().Select();
            }
            else
            {
                selected = (selected + 2) % 3;
                transform.GetChild(selected+1).GetComponent<Button>().Select();
            }
        }
        if ((!selectInputLeft && !selectInputRight))
        {
            prev_selected = selected;
        }

    }
}
