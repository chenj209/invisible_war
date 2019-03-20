using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = System.Random;

public class SealSystem : MonoBehaviour
{
    public List<GameObject> seals;
    public GameObject ghost;

    private Random rand;

    // For test purpose.
    public float resetTimer;
    private float curTime;

    // Start is called before the first frame update
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
        if (curTime <= 0f)
        {
            ResetSys();
            curTime = resetTimer;
        } else
        {
            curTime -= Time.deltaTime;
        }
    }

    // Reset the system : Tested.
    void ResetSys()
    {
        int activeSeals = 5;
        int totalSeals = seals.Count;
        foreach (GameObject seal in seals)
        {
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
    }
}
