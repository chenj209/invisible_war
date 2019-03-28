using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Seal : MonoBehaviour
{
    public GameObject sealSys;
    public GameObject SealDestroyInstruction;

    private SealSystem ss;
    private float destroyTime;
    private GameObject ghost;
    private GameObject loadingBar;
    private float curTime;
    private bool destroying = false;

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
        if (destroying)
        {
            // Check is the ghost press the unlockseal button.
            if (Input.GetButton("Unlock"))
            {
                // CountDown for the unlock process.
                if (curTime <= 0)
                {
                    // Destory itself and update to the SealSystem.
                    ss.destroy();
                    SealDestroyInstruction.SetActive(false);
                    loadingBar.SetActive(false);
                    gameObject.SetActive(false);
                }
                else
                {
                    curTime -= Time.deltaTime;
                    loadingBar.SetActive(true);
                    loadingBar.GetComponent<Image>().fillAmount = (destroyTime - curTime) / destroyTime;
                }
            }
            else
            {
                loadingBar.SetActive(false);
            }

        }

    }

    public void ResetSeal()
    {
        curTime = destroyTime;
    }

    public void DestroySeal(bool destroyable)
    {
        SealDestroyInstruction.SetActive(destroyable);
        destroying = destroyable;
    }

    private void OnCollisionExit(Collision collision)
    {
        loadingBar.SetActive(false);
    }
}
