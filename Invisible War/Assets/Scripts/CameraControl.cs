using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float rotSpeed = 5000;

    private GameObject player;

    private float maxRot = 90f;
    private float minRot = -90f;

    private string playerID;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        player = this.transform.parent.gameObject;
        PlayerControl pc = player.GetComponent("PlayerControl") as PlayerControl;
        playerID = pc.playerID;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }

        // Y-axis respect rotation.
        float yRot = Input.GetAxis("Mouse X" + playerID);
        if (Mathf.Abs(yRot) < .3f)
        {
            yRot = 0f;
        }
        player.transform.Rotate(Vector3.up, Mathf.Lerp(0f, yRot * rotSpeed * Time.deltaTime, 1f));

        // X-axis respect rotation.
        float xRot = -Input.GetAxis("Mouse Y" + playerID);
        if (Mathf.Abs(xRot) < .3f)
        {
            xRot = 0f;
        }
        float curX = this.transform.eulerAngles.x;
        float rotDegree = Mathf.Lerp(0f, xRot * rotSpeed * Time.deltaTime, 1f);
        // this.transform.Rotate(Vector3.right, Mathf.Clamp(Mathf.Lerp(0f, xRot * rotSpeed, Time.deltaTime) + curX, upRotLimit, downRotLimit));
        //if (rotDegree > maxRot - curX)
        //{
        //    rotDegree = maxRot - curX;
        //} else if (rotDegree < curX - minRot)
        //{
        //    rotDegree = curX - minRot;
        //}
        // transform.Rotate(Vector3.right, Mathf.Clamp(rotDegree, curX - minRot, maxRot - curX));
        this.transform.Rotate(Vector3.right, rotDegree);
        
    }

    private void FixedUpdate()
    {

    }
}
