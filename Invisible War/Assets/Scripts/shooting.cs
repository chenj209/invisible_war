using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shooting : MonoBehaviour
{
    public ParticleSystem particleLauncher;
    public ParticleDecalPool decalPool;
    public AudioClip Fire_Sound;
    public Image cdImage;
    public Image crosshair;
    private bool Pressed = false;
    private AudioSource[] sources;
    private string playerID;
    private bool On_CoolDown = false;
    public bool inTutorial;
    public UIFader hunterInstruction;
    public Text hunterInstructionText;
    public GameObject huntershootingEffect;
    private bool firstTimeshoot = true;
    private bool fadeoutfirst = true;
    // Start is called before the first frame update
    void Start()
    {
        PlayerControl pc = gameObject.GetComponent<PlayerControl>();
        playerID = pc.playerID;
        sources = gameObject.GetComponents<AudioSource>();
        cdImage.fillAmount = 0;
        // center crosshair
        //RectTransform rectTransform = crosshair.gameObject.GetComponent<RectTransform>();
        //Vector2 oldPosition = rectTransform.anchoredPosition;
        //rectTransform.anchoredPosition = new Vector2(oldPosition.x - Screen.width * 0.25f, oldPosition.y - Screen.height*0.1f);
        crosshair.transform.position = new Vector3((float)(Screen.width * 0.49 / 2), Screen.height/2,crosshair.transform.position.z);
        particleLauncher.startLifetime = GameConfig.instance.paintgunShootDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (inTutorial) { 
            if (hunterInstruction.FadeInOver && fadeoutfirst)
            {
                fadeoutfirst = false;
                StartCoroutine(FadeOut());
            }
        }
        if (Input.GetButtonDown("Fire" + playerID) && !Pressed)
        {
            Pressed = true;
            if (!On_CoolDown)
            {
                Shooting();
            }
        } else if (Input.GetButtonUp("Fire" + playerID))
        {
            Pressed = false;
        }

        if (On_CoolDown)
        {
            cdImage.fillAmount -= 1 / GameConfig.instance.paintgunCooldown * Time.deltaTime;
            if (cdImage.fillAmount<=0)
            {
                On_CoolDown = false;
            }
            // hide paintgun and crosshair
            // crosshair.gameObject.SetActive(false);
        }
        else
        {
            // crosshair.gameObject.SetActive(true);
        }
    }
    IEnumerator FadeOut()
    {

        yield return new WaitForSeconds(4);
        if (hunterInstruction.isActiveAndEnabled)
        {
            hunterInstruction.FadeOut();
            hunterInstruction.FadeInOver = false;
        }
        huntershootingEffect.SetActive(false);
        crosshair.enabled = true;
    }

    void Shooting()
    {
        if (inTutorial && firstTimeshoot)
        {
            firstTimeshoot = false;
            crosshair.enabled = false;
            hunterInstructionText.text = "Bottom left icon is your shooting cooldown.";
            hunterInstruction.FadeIn();
            huntershootingEffect.SetActive(true);
        }
        decalPool.ClearParticles();
        ParticleSystem.MainModule psMain = particleLauncher.main;
        particleLauncher.Emit(GameConfig.instance.paintgunBulletsCount);
        cdImage.fillAmount = 1;
        sources[0].PlayOneShot(Fire_Sound, 1f);
        On_CoolDown = true;
    }

}
