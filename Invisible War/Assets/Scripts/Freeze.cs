using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Freeze : MonoBehaviour
{
    public Image cdImage;
    public float Freeze_CD = 30.0f;
    private bool On_CoolDown = false;

    // Start is called before the first frame update
    void Start()
    {
        cdImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (!On_CoolDown)
            {
                Skill();
            }
        }
        if (On_CoolDown)
        {
            cdImage.fillAmount -= 1 / Freeze_CD * Time.deltaTime;
            if (cdImage.fillAmount <= 0)
            {
                On_CoolDown = false;
            }
        }
    }

    void Skill()
    {
        cdImage.fillAmount = 1;
        On_CoolDown = true;
    }
}
