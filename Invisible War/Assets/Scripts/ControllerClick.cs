using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControllerClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(1).GetComponent<Button>().Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            Button b = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            b.onClick.Invoke();
        }
    }
}
