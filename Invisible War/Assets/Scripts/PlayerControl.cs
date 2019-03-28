using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public string playerID;
    public Transform enemy;
    public Transform indicatorCenter;
    public GameObject indicator;
    public bool inTutorial;
    public Animator moveAnimator;
    public GameObject HunterHelpMenu;
    public GameObject GhostHelpMenu;
    public Text GhostStatus;
    private bool firstTimeInRange = true;
    public UIFader hunterInstruction;
    public Text hunterInstructionText;
    private bool fadeoutFirst = true;
    public Image crosshair;

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
    public Camera playerCamera;
    [SerializeField] private MouseLook m_MouseLook = null;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = this.GetComponent<Rigidbody>();
        healthbar = this.GetComponent<HealthBar>();
        moveMode = MovePattern.Walking;
        updateIndicator();
        m_MouseLook.Init(transform, playerCamera.transform, playerID);
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerID == "01")
        {
            if (hunterInstruction.FadeInOver && fadeoutFirst && inTutorial)
            {
                fadeoutFirst = false;
                StartCoroutine(FadeOut());
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            HunterHelpMenu.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            HunterHelpMenu.SetActive(false);
        }
        if (Input.GetButtonDown("GhostHelp"))
        {
            GhostHelpMenu.SetActive(true);
        }
        if (Input.GetButtonUp("GhostHelp"))
        {
            GhostHelpMenu.SetActive(false);
        }
            RotateView();
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
        if (inTutorial) isGround = Physics.Raycast(transform.position, -transform.up, 10.0f);
        else isGround = Physics.Raycast(transform.position, -transform.up, 10.0f);
        if (isGround && Input.GetButton("Jump" + playerID))
        {
            //playerBody.velocity += transform.up * jumpSpeed;
            //playerBody.AddRelativeForce(Vector3.up * jumpSpeed);
            Vector3 vel = playerBody.velocity;
            vel.y = jumpSpeed;
            playerBody.velocity = vel;
        }
        updateIndicator();
    }

    private void FixedUpdate()
    {
        m_MouseLook.UpdateCursorLock();
    }

    IEnumerator FadeOut()
    {

        yield return new WaitForSeconds(10);

        hunterInstruction.FadeOut();
        yield return new WaitForSeconds(1);
        crosshair.enabled = true;
        gameObject.GetComponent<shooting>().enabled = true;
    }

    private void move()
    {
        float moveSpeed = moveForce;
        switch (moveMode) {
            case MovePattern.Walking:
                moveSpeed = moveForce;
                break;
            case MovePattern.Running:
                moveSpeed = moveForce;
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
        if (Mathf.Abs(xMove - 0f) > .3f || Mathf.Abs(zMove - 0f) > .3f)
        {
            moveAnimator.SetBool("move", true);
        }
        else
        {
            moveAnimator.SetBool("move", false);
        }
        //playerBody.AddForce(this.transform.right * xMove * moveForce);
        //playerBody.AddForce(this.transform.forward * zMove * moveForce);
        Vector3 movement = this.transform.right * xMove * moveSpeed;
        movement += this.transform.forward * zMove * moveSpeed;
        movement.y += playerBody.velocity.y;
        playerBody.velocity = movement;
    }

    private void updateIndicator()
    {
        if (enemy != null && indicatorCenter != null)
        {
            if (playerID == "01")
            {
                if (GameConfig.instance.enableHunterIndicator)
                {
                    float distance = Vector3.Distance(enemy.position, transform.position);
                    if (!GameConfig.instance.hunterIndicatorNearBy)
                    {
                        if (distance < 100)
                        {
                            GhostStatus.text = "              Ghost\n             NearBy!";

                            indicatorCenter.gameObject.SetActive(false);
                            if (inTutorial && firstTimeInRange)
                            {
                                firstTimeInRange = false;
                                hunterInstructionText.text = "Fire the paintball gun to your estimated direction. The ghost will become visible to you when you hit it.";
                                hunterInstruction.FadeIn();

                            }
                        }
                        else
                        {
                            GhostStatus.text = "              Ghost\n         Out Of Range";
                            indicatorCenter.gameObject.SetActive(true);
                        }

                    }
                    else
                    {
                        GhostStatus.text = "              Ghost\n           Direction";
                    }
                    Vector3 difference = enemy.position - transform.position;
                    Vector3 faceDirection = transform.forward;
                    Vector2 face2D = new Vector2(faceDirection.x, faceDirection.z);
                    Vector2 difference2D = new Vector2(difference.x, difference.z);
                    float rotateDegree = Vector2.SignedAngle(face2D, difference2D);
                    indicatorCenter.transform.localEulerAngles = new Vector3(0, 0, rotateDegree);
                }
                else
                {
                    indicator.gameObject.SetActive(false);
                }
            }
            else if (playerID == "02")
            {
                if (indicator)
                {
                    float distance = Vector3.Distance(enemy.position, transform.position);
                    if (distance > 100)
                    {
                        indicator.GetComponent<Image>().color = new Color32(0, 255, 0, 200);
                    }else if (distance <= 100 && distance >= 50)
                    {
                        float value = 100;
                        float rValue = (value - distance) * 5.1f;
                        indicator.GetComponent<Image>().color = new Color32((byte)rValue, 255, 0, 200);
                    }else if (distance < 50)
                    {
                        float gValue = distance * 5.1f - 90f;
                        if (gValue < 0) gValue = 0;
                        indicator.GetComponent<Image>().color = new Color32(255, (byte)gValue, 0, 200);
                    }
                }
            }
        }
    }
    private void RotateView()
    {
        m_MouseLook.LookRotation(transform, playerCamera.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.collider, this.gameObject.GetComponent<CapsuleCollider>());
        }
        if (playerID == "02")
        {
            if (collision.collider.gameObject.tag == "Furniture")
            {
                Physics.IgnoreCollision(collision.collider, this.gameObject.GetComponent<CapsuleCollider>());
            }
        }
    }
}