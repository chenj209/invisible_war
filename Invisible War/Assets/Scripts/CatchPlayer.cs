using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchPlayer : MonoBehaviour
{
    public float distance;
    public Transform camPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = DetectItem();
        if (Input.GetKeyDown(KeyCode.C))// catch
        {
            if(player != null)
            {
                Catch(player);
                //Debug.Log("You got the hider");
            }
            else{
                //Debug.Log("No player is detected");
            }
        }
    }

    private GameObject DetectItem()
    {
        RaycastHit hit;
        GameObject target;
        if(camPos == null) 
        {
            return null; 
        }
        if (!Physics.Raycast(camPos.position, camPos.TransformDirection(Vector3.forward), out hit, distance))
        {
            //Debug.Log("Cannot detect anything.");
            return null;
        }
        if (hit.rigidbody != null)
        {
            target = hit.rigidbody.gameObject;
        }
        else
        {
            //Debug.Log("No movable object is detected.");
            return null;
        }
        if (target.name == "Player02")
        {
            //Debug.Log("Get the hider.");
            return target;
        }
        return null;
    }

    private void Catch(GameObject player)
    {
        player.GetComponent<MeshRenderer>().enabled=true;
    }
}
