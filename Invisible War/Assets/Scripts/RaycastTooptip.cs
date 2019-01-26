using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTooptip : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform camPos;
    public float distance;
    private ShortcutTip shortcuttip;
    private List<string> raycastTargetTags;
    void Start()
    {
        raycastTargetTags = new List<string>() { "Item", "Door" };
        shortcuttip = GetComponent<ShortcutTip>();
    }

    public GameObject DetectItem()
    {
        RaycastHit hit;
        GameObject target = null;
        if (Physics.Raycast(camPos.position, camPos.TransformDirection(Vector3.forward), out hit, distance))
        {
            //pickUpText.text = "";
            //Debug.Log("Cannot detect anything.");
            //if (hit.rigidbody == null)
            //{
            //    return null;
            //}
            if (hit.rigidbody != null && hit.rigidbody.gameObject.tag == "Item")
            {
                //pickUpText.text = "";
                target = hit.rigidbody.gameObject;
                shortcuttip.ShowShortcutTip(@"Press P to pick up
            Press T to throw");
            }
            if (hit.collider.gameObject.tag == "Door")
            {
                shortcuttip.ShowShortcutTip("Press E to Open");
            }
            if (target != null && !raycastTargetTags.Contains(target.tag))
            {
                //pickUpText.text = "";
                //Debug.Log("The object detected is not movable.");
                return null;
            }
            //if (target.transform.parent != null)
            //{
            //    //pickUpText.text = "";
            //    //Debug.Log("The object belongs to another player.");
            //    return null;
            //}

            //Debug.Log("Find an object.");
            //Debug.Log(hit.distance);
            return target;
        }
        shortcuttip.ShowShortcutTip("");
        return null;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
