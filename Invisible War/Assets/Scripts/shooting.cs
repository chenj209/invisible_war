using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
	public GameObject Spawn_Object;
    public GameObject Bullet;
    public float Bullet_Forward_Force;
    private bool Pressed = false;

    private string playerID;
    // Start is called before the first frame update
    void Start()
    {
        PlayerControl pc = gameObject.GetComponent("PlayerControl") as PlayerControl;
        playerID = pc.playerID;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire" + playerID) && !Pressed)
        {
            Pressed = true;
            Shooting();
        } else if (Input.GetButtonUp("Fire" + playerID))
        {
            Pressed = false;
        }
    }

    void Shooting()
    {

        Spawn_Bullet(new Vector3(0, 0, 0), new Vector3(0, 0, 0));
        Spawn_Bullet(new Vector3(-1, 0, 0), new Vector3(-0.1f, 0, 0));
        Spawn_Bullet(new Vector3(1, 0, 0), new Vector3(0.1f, 0, 0));
        Spawn_Bullet(new Vector3(0, 1, 0), new Vector3(0, 0.05f, 0));
  
    }

    void Spawn_Bullet(Vector3 position_offset, Vector3 bullet_offset)
    {
        GameObject Temporary_Bullet_Handler;
        Temporary_Bullet_Handler = Instantiate(Bullet, (Spawn_Object.transform.position + position_offset), Spawn_Object.transform.rotation) as GameObject;

        //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
        //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
        Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);
        Rigidbody Temporary_RigidBody;
        //Retrieve the Rigidbody component from the instantiated Bullet and control it.
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

        //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
        Temporary_RigidBody.AddForce((transform.forward + bullet_offset) * Bullet_Forward_Force);

        //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
        Destroy(Temporary_Bullet_Handler, 3.0f);
    }
}
