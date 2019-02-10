using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour
{
    public GameObject targ;
    private float speed = 0.1f;
    private bool Frozen = false;
    private float count = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {                       
        if (Frozen)
        {
            count += Time.deltaTime;
            speed *= 0.95f;
            if (count > 3)
            {
                count = 0.0f;
                Frozen = false;
                speed = 0.1f;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, targ.transform.position, speed);
    }

    public void stopRunning()
    {
        Frozen = true;
    }
}
