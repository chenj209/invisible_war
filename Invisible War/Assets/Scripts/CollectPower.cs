using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CollectPower : MonoBehaviour
{
    // type of ablities
    enum Ability { Speed, Detect };

    // ui indicators of ablity
    public Text specialAbility1;
    public Text specialAbility2;

    // countdown for active ablities
    private Dictionary<Ability, int> activeAbilityCountDown;

    // countdown length config for abilities
    public int speedAblityCooldown;
    public int detectAblityCooldown;
    private Dictionary<Ability, int> abilityCountdowns;

    // global canvas
    public Canvas canvas;

    void Start()
    {

        abilityCountdowns = new Dictionary<Ability, int>()
        {
            {Ability.Speed, speedAblityCooldown},
            {Ability.Detect, detectAblityCooldown},
        };
        specialAbility1.text = "";
        specialAbility2.text = "";
        activeAbilityCountDown = new Dictionary<Ability, int>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SpecialPower")
        {
            Ability ability;
            if (Enum.TryParse(collision.gameObject.name, false, out ability))
            {
                if (specialAbility1.text == "")
                {
                    specialAbility1.text = collision.gameObject.name;
                    Debug.Log(collision.gameObject.name);
                }
                else
                {
                    specialAbility2.text = collision.gameObject.name;
                }
                activeAbilityCountDown.Add(ability, abilityCountdowns[ability]);
            }
            else
            {
                Debug.Log("wtf");
                Debug.Log(collision.gameObject.name);
            }
            Destroy(collision.gameObject);
        }
    }
    void Update()
    {

    }

    public void useSpeed()
    {

    }
    public void useDetect()
    {

    }
}
