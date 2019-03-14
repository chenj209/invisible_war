using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpiner : MonoBehaviour
{

    public string playerID;

    private GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Player" + playerID + "/PlayerCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform != null && camera != null)
        {
            transform.LookAt(transform.position * 2 - camera.transform.position);
        }
    }
}
