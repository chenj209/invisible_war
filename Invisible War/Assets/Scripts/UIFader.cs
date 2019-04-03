using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFader : MonoBehaviour
{
    
    public CanvasGroup uiElement;
    public bool FadeInOver = false;
    public bool FadeOutOver = false;
    public void FadeIn()
    {
       
        FadeInOver = false;
        FadeOutOver = false;
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1));
    }

    public void FadeOut()
    {
        FadeInOver = false;
        FadeOutOver = false;
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0));
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 1.0f)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentangeComplete = timeSinceStarted / lerpTime;
        while (!FadeInOver && !FadeOutOver)
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentangeComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentangeComplete);

            cg.alpha = currentValue;

            if (Mathf.Abs(end - 1) < 0.0000001 && Mathf.Abs(cg.alpha - 1) < 0.0000001)
            {
                 FadeInOver = true;
                
            }
            if (Mathf.Abs(end - 0) < 0.0000001 && Mathf.Abs(cg.alpha - 0) < 0.0000001)
            {
                FadeOutOver = true;

            }
            yield return new WaitForEndOfFrame();
        }
    }
}
