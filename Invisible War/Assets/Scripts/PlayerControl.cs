using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public string playerID;

    public enum MovePattern
    {
        Walking,
        Running,
        Sneaking
    };
    public MovePattern moveMode;

    public float moveForce = 5;

    private Rigidbody playerBody;
    HealthBar healthbar;

    public bool isGround;
    public float jumpSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = this.GetComponent<Rigidbody>();
        healthbar = this.GetComponent<HealthBar>();
        moveMode = MovePattern.Walking;

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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
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

    CursorLockMode wantedMode;

    // Apply requested cursor state
    void SetCursorState()
    {
        Cursor.lockState = wantedMode;
        // Hide cursor when locking
        Cursor.visible = (CursorLockMode.Locked != wantedMode);
    }

    void OnGUI()
    {
        GUILayout.BeginVertical();
        // Release cursor on escape keypress
        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = wantedMode = CursorLockMode.None;

        switch (Cursor.lockState)
        {
            case CursorLockMode.None:
                GUILayout.Label("Cursor is normal");
                if (GUILayout.Button("Lock cursor"))
                    wantedMode = CursorLockMode.Locked;
                if (GUILayout.Button("Confine cursor"))
                    wantedMode = CursorLockMode.Confined;
                break;
            case CursorLockMode.Confined:
                GUILayout.Label("Cursor is confined");
                if (GUILayout.Button("Lock cursor"))
                    wantedMode = CursorLockMode.Locked;
                if (GUILayout.Button("Release cursor"))
                    wantedMode = CursorLockMode.None;
                break;
            case CursorLockMode.Locked:
                GUILayout.Label("Cursor is locked");
                if (GUILayout.Button("Unlock cursor"))
                    wantedMode = CursorLockMode.None;
                if (GUILayout.Button("Confine cursor"))
                    wantedMode = CursorLockMode.Confined;
                break;
        }

        GUILayout.EndVertical();

        SetCursorState();
    }
}
