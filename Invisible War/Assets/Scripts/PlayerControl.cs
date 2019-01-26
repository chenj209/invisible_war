using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveForce;
    public float rotSpeed;

    private Rigidbody playerBody;
    HealthBar healthbar;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = this.GetComponent<Rigidbody>();
        healthbar = this.GetComponent<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Vector3 move = playerBody.velocity;
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        //playerBody.AddForce(this.transform.right * xMove * moveForce);
        //playerBody.AddForce(this.transform.forward * zMove * moveForce);
        Vector3 move = this.transform.right * xMove * moveForce;
        move += this.transform.forward * zMove * moveForce;
        playerBody.velocity = move;

        // Y-axis respect rotation.
        float yRot = Input.GetAxis("Mouse X");
        this.transform.Rotate(Vector3.up, yRot * rotSpeed);
        if (Input.GetKeyDown(KeyCode.X))
        {
            healthbar.DecrementHealth(10);
        }
    }
}
