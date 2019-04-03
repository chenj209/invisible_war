using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyboardClick : MonoBehaviour
{
    private int selected;

    // Start is called before the first frame update
    void Start()
    {
        selected = 1;
        transform.GetChild(selected).GetComponent<Button>().Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Button b = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            b.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.A) && selected > 1)
        {
            selected--;
            transform.GetChild(selected).GetComponent<Button>().Select();
        }
        if (Input.GetKeyDown(KeyCode.D) && selected < 3)
        {
            selected++;
            transform.GetChild(selected).GetComponent<Button>().Select();
        }
        if (Input.GetButtonDown("HunterContinue"))
        {
            Button b = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            b.onClick.Invoke();
        }

    }
}
