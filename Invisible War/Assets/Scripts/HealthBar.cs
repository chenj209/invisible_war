using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthbar;
    // Start is called before the first frame update
    void Start()
    {
        healthbar.value = 100;                
    }

    // Update is called once per frame
    public void DecrementHealth(int value)
    {
        if (healthbar.value > value) {
            healthbar.value -= value;
        }
        else
        {
            healthbar.value = 0;
            DestroyHealthBar();
        }
    }

    public void DestroyHealthBar()
    {
        healthbar.gameObject.SetActive(false);
    }
}
