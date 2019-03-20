using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = System.Random;

public class SealSystem : MonoBehaviour
{
    public List<GameObject> seals;
    public GameObject ghost;
    public int sealNum;
    // The time need that need to destory a seal in second.
    public float destoryTime;
    // This flag indicate that all the seals has been destoryed.
    public bool allDestroyed;

    private Random rand;
    private int sealLeft;

    // For test purpose.
    public float resetTimer;
    private float curTime;

    // Start is called before the first frame update.
    void Start()
    {
        rand = new Random();
        ResetSys();

        // For test purpose.
        curTime = resetTimer;
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
            if (rand.Next(totalSeals) < activeSeals)
            {
                // This seal is picked as activate.
                Seal s = seal.GetComponent<Seal>();
                s.ResetSeal();
                seal.SetActive(true);
                activeSeals--;
            } else
            {
                // This seal is not picked.
                seal.SetActive(false);
            }
            totalSeals--;
        }

        // Reset statistics.
        allDestroyed = false;
        sealLeft = sealNum;
    }

    // Get called when seal destroyed.
    public void destroy()
    {
        sealLeft--;
        // Check if all the seals got destroyed.
        if (sealLeft == 0)
        {
            allDestroyed = true;
            Debug.Log("All seals destroyed!");
        }
    }
}
