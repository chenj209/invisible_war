using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveObject : MonoBehaviour
{
    public Text pickUpText;
    public Transform holdPoint;

    private Rigidbody rb;
    private bool ishold;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pickUpText.text = "";
        ishold = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && ishold == false)
        {
            pickUpText.text = "Press F to pick up";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            pickUpText.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            rb.useGravity = false;
            pickUpText.text = "";
            transform.position = holdPoint.position;
            transform.parent = GameObject.Find("Player").transform;
        }
    }

}
