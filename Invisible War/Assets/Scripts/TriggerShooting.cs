using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerShooting : MonoBehaviour
{
    public GameObject player;
    public GameObject shootBot;
    public Text instruction;
    public GameObject panel;
    public GameObject cam;
    private bool firstTime = true;
    private bool rotate = false;
    public GameObject directionArrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            var rotation = Quaternion.LookRotation(shootBot.transform.position - player.transform.position);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rotation, Time.deltaTime * 10);
        }
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && firstTime)
        {
            panel.SetActive(false);
            PlayerControl pc = player.GetComponent<PlayerControl>();
            pc.enabled = false;
        
            yield return new WaitForSeconds(1);
            cam.transform.eulerAngles = new Vector3(0, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
            rotate = true;
          
            yield return new WaitForSeconds(2);
            shootBot.transform.LookAt(player.transform);
            rotate = false;
            BotShoot bs = shootBot.GetComponent<BotShoot>();
            bs.shoot = true;
            panel.SetActive(true);
            instruction.text = "Got Painted By Hunter's Paintball Gun! \n You Are Visible To The Hunter For 5s \n Run For Life!";
            firstTime = false;
            directionArrow.SetActive(true);
        }
    }

    //private IEnumerator OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        yield return new WaitForSeconds(1);
    //        panel.SetActive(false);
    //    }

    //}
}
