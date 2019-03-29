using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameConfig : MonoBehaviour
{
    public static GameConfig instance = null;
    public GameObject Hunter;
    public GameObject Ghost;

    // Hunter ability config
    public bool hunterTransparent = true;
    public float hunterSpeed = 35.0f;
    public float hunterJumpSpeed = 13.0f;
    public float hunterCameraHorizontalSpeed = 2.0f;
    public float hunterCameraVerticalSpeed = 2.0f;
    public bool enableHunterIndicator = true;
    public bool hunterIndicatorNearBy = false;
    public float paintgunCooldown = 1.0f;
    public float paintgunEffectDuration = 5.0f;
    public float paintgunShootDistance = 60.0f;
    public int paintgunBulletsCount = 30;
    public bool paintgunStopGhostAction = false;

    // Ghost ability config
    public bool ghostTransparent = true;
    public float ghostSpeed = 30.0f;
    public float ghostJumpSpeed = 13.0f;
    public float ghostCameraHorizontalSpeed = 2.0f;
    public float ghostCameraVerticalSpeed = 0;
    public bool enableGhostIndicator = true;
    public float freezeCoolDown = 60.0f;
    public float freezeEffectDuration = 5.0f;
    public float freezeDistance = 50.0f;
    public bool freezeBlockScreen = false;
    public Sprite freezeBlockScreenSprite;
    public Image freezeEffectObj;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        if (freezeBlockScreen)
        {
            freezeEffectObj.sprite = freezeBlockScreenSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
