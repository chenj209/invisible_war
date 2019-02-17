using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public string playerID;
    public Transform enemy;
    public Transform indicatorCenter;
    public enum MovePattern
    {
        Walking,
        Running,
        Sneaking
    };
    public MovePattern moveMode;

    public float moveForce = 5;

    private Rigidbody playerBody;

    public bool isGround;
    public float jumpSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = this.GetComponent<Rigidbody>();
        moveMode = MovePattern.Walking;
        Vector3 difference = enemy.position - transform.position;
        float rotateDegree = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg + 90;
        indicatorCenter.transform.localEulerAngles = new Vector3(0,0,rotateDegree);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButton("Running" + playerID))
        {
            moveMode = MovePattern.Running;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            moveMode = MovePattern.Sneaking;
        }
        else
        {
            moveMode = MovePattern.Walking;
        }
        move();

        isGround = Physics.Raycast(transform.position, -transform.up, 2.0f);
        if (isGround && Input.GetButton("Jump" + playerID))
        {
            //playerBody.velocity += transform.up * jumpSpeed;
            //playerBody.AddRelativeForce(Vector3.up * jumpSpeed);
            Vector3 vel = playerBody.velocity;
            vel.y = jumpSpeed;
            playerBody.velocity = vel;
        }
        Vector3 difference = enemy.position - transform.position;
        float rotateDegree = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg + 90;
        indicatorCenter.transform.localEulerAngles = new Vector3(0, 0, rotateDegree);
    }

    private void move()
    {
        float moveSpeed = moveForce;
        switch (moveMode) {
            case MovePattern.Walking:
                moveSpeed = moveForce;
                break;
            case MovePattern.Running:
                moveSpeed = 2 * moveForce;
                break;
            case MovePattern.Sneaking:
                moveSpeed = .6f * moveForce;
                break;
        }

        //Vector3 move = playerBody.velocity;
        float xMove = Input.GetAxis("Horizontal" + playerID);
        float zMove = Input.GetAxis("Vertical" + playerID);
        if (Mathf.Abs(xMove) < .3f)
        {
            xMove = 0f;
        }
        if (Mathf.Abs(zMove) < .3f)
        {
            zMove = 0f;
        }
        //playerBody.AddForce(this.transform.right * xMove * moveForce);
        //playerBody.AddForce(this.transform.forward * zMove * moveForce);
        Vector3 move = this.transform.right * xMove * moveSpeed;
        move += this.transform.forward * zMove * moveSpeed;
        move.y += playerBody.velocity.y;
        playerBody.velocity = move;
    }
}
