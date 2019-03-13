using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State
{
    Start,
    Menu
}

public class MainMenuManager : MonoBehaviour
{
    private State curState;
    public GameObject startPannel;
    public GameObject menuPannel;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible=true;
        Cursor.lockState = CursorLockMode.None;
        curState = State.Start;
        startPannel.SetActive(true);
        menuPannel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Detect and translate state from Start -> Menu
        if (curState == State.Start && Input.GetKeyDown(KeyCode.Space))
        {
            curState = State.Menu;
            startPannel.SetActive(false);
            menuPannel.SetActive(true);
        }
    }

    public void TestExit()
    {
        Application.Quit();
        Debug.Log("Exit has been pushed.");
    }
}
