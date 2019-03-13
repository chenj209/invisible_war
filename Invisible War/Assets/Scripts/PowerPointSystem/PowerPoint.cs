using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPoint : MonoBehaviour
{
    public GameObject controlSystem;
    public GameObject ghost;
    public GameObject hunter;

    private PPSystem system;

    // Start is called before the first frame update
    void Start()
    {
        system = controlSystem.GetComponent<PPSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == hunter)
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            Debug.Log("Collide with hunter.");
        } else if (collision.gameObject == ghost)
        {
            system.pUActivate();
            Debug.Log("Collide with ghost.");
        }
    }
}
