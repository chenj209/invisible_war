using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public static GameConfig instance = null;

    // Hunter ability config
    public bool hunterTransparent = true;
    public float hunterSpeed = 35.0f;
    public float hunterJumpSpeed = 13.0f;
    public float hunterCameraHorizontalSpeed = 2.0f;
    public float hunterCameraVerticalSpeed = 2.0f;
    public bool enableHunterIndicator = false;
    public float paintgunCooldown = 1.0f;
    public float paintgunEffectDuration = 5.0f;
    public float paintgunShootDistance = Mathf.Infinity;

    // Ghost ability config
    public bool ghostTransparent = true;
    public float ghostSpeed = 30.0f;
    public float ghostJumpSpeed = 13.0f;
    public float ghostCameraHorizontalSpeed = 2.0f;
    public float ghostCameraVerticalSpeed = 0;
    public bool enableGhostIndicator = true;
    public float freezeCoolDown = 5.0f;
    public float freezeEffectDuration = 5.0f;
    public float freezeDistance = 5.0f;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
