using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenuController : MonoBehaviour
{
    public int levelToLoad;

    private Animator an;

    // Start is called before the first frame update
    void Start()
    {
        an = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            an.SetTrigger("FadeOut");
        }
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
