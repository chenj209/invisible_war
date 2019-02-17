using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortcutTip : MonoBehaviour
{
    // Start is called before the first frame update
    public Text shortcuttip;
    void Start()
    {
        shortcuttip.text = "";
                
    }

    // Update is called once per frame
    public void ShowShortcutTip(string tip)
    {
        shortcuttip.text = tip;
    }
}
