﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    private Animator an;
    private int level;

    // Start is called before the first frame update
    void Start()
    {
        an = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    // void Update()
    // {
    //    if (Input.GetKeyDown(KeyCode.G))
    //    {
    //        an.SetTrigger("FadeOut");
    //    }
    // }

    public void transit(int level)
    {
        this.level = level;
        an.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(level);
    }
}
