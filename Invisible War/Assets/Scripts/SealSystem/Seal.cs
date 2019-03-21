using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Seal : MonoBehaviour
{
    public GameObject sealSys;

    private SealSystem ss;
    private float destroyTime;
    private GameObject ghost;
    private GameObject loadingBar;
    private float curTime;

    // Start is called before the first frame update
    void Start()
    {
        ss = sealSys.GetComponent<SealSystem>();
        destroyTime = ss.destoryTime;
        ghost = ss.ghost;
        loadingBar = ss.loadingBar;

        ResetSeal();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetSeal()
    {
        curTime = destroyTime;
    }

    // Detect the collision with the ghost.
    private void OnCollisionStay(Collision collision)
    {
        // Check is the collision object the ghost.
        if (collision.gameObject == ghost)
        {
            // Check is the ghost press the unlockseal button.
            if (Input.GetButton("Unlock"))
            {
                // CountDown for the unlock process.
                if (curTime <= 0)
                {
                    // Destory itself and update to the SealSystem.
                    ss.destroy();
                    loadingBar.SetActive(false);
                    Debug.Log("Trigered");
                    gameObject.SetActive(false);
                } else
                {
                    curTime -= Time.deltaTime;
                    loadingBar.SetActive(true);
                    loadingBar.GetComponent<Image>().fillAmount = (destroyTime - curTime) / destroyTime;
                }
            } else
            {
                loadingBar.SetActive(false);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        loadingBar.SetActive(false);
    }
}
