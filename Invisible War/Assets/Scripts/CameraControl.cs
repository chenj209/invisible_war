using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public float rotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; 
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }
    }

    private void FixedUpdate()
    {
        // X-axis respect rotation.
        float xRot = -Input.GetAxis("Mouse Y");
        this.transform.Rotate(Vector3.right, xRot * rotSpeed);
    }
}
