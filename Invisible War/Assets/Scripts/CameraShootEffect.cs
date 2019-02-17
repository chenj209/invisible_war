using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShootEffect : MonoBehaviour
{
    public bool start = false;

    public IEnumerator Shake(float duration, float magnitude)
    {

        Vector3 originalPos = transform.localPosition;
        float elapsed = duration;

        while (elapsed > .0f)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            Debug.Log("Shake");
            Debug.Log(transform.localPosition);

            elapsed -= Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
        Debug.Log("Return");
    }
}
