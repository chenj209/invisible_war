﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = System.Random;

public class SealSystem : MonoBehaviour
{
    public List<GameObject> seals;
    // public List<GameObject> sealsUIG;
    // public List<GameObject> sealsUIH;
    public List<GameObject> sealsUI;
    public GameObject ghost;
    public GameObject loadingBar;
    public int sealNum;
    // The time need that need to destory a seal in second.
    public float destoryTime;
    // This flag indicate that all the seals has been destoryed.
    public bool allDestroyed;
    // This flag is for tutorial.
    public bool isTut;
    public GameObject hunterEffect;
    public GameObject ghostEffect;
    private Random rand;
    private int sealLeft;

    // For test purpose.
    /*
    public float resetTimer;
    private float curTime;
    */

    // Start is called before the first frame update.
    void Start()
    {
        rand = new Random();
        ResetSys();

        // For test purpose.
        /*
        curTime = resetTimer;
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Reset the system : Tested.
    public void ResetSys()
    {
        // Reset the Seals.
        int activeSeals = sealNum;
        int totalSeals = seals.Count;
        foreach (GameObject seal in seals)
        {
            Seal s = seal.GetComponent<Seal>();
            s.ResetSeal();
            if (rand.Next(totalSeals) < activeSeals)
            {
                // This seal is picked as activate. 
                seal.SetActive(true);
                activeSeals--;
            } else
            {
                // This seal is not picked.
                seal.SetActive(false);
            }
            totalSeals--;
        }

        /*
        foreach (GameObject seal in sealsUIG)
        {
            seal.SetActive(true);
        }
        foreach (GameObject seal in sealsUIH)
        {
            seal.SetActive(true);
        }
        */
        foreach (GameObject seal in sealsUI)
        {
            seal.SetActive(false);
        }
        sealsUI[0].SetActive(true);

        // Reset statistics.
        allDestroyed = false;
        sealLeft = sealNum;
    }

    // Get called when seal destroyed.
    public void destroy()
    {
        sealLeft--;
        if (isTut)
        {
            hunterEffect.SetActive(true);
            ghostEffect.SetActive(true);
            StartCoroutine(DisableEffect());
        }
        sealsUI[5 - sealLeft - 1].SetActive(false);
        sealsUI[5 - sealLeft].SetActive(true);

       
        /*
        sealsUIG[sealLeft - 1].SetActive(false);
        sealsUIH[sealLeft - 1].SetActive(false);
        */

        // Check if all the seals got destroyed.
        if (sealLeft == 1)
        {
            allDestroyed = true;
           // Debug.Log("Enough seals destroyed!");
        }
    
    }
    IEnumerator DisableEffect()
    {
        yield return new WaitForSeconds(6);
        ghostEffect.SetActive(false);
        hunterEffect.SetActive(false);
    }
}
