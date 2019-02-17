using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShootEffect : MonoBehaviour
{
    public bool start;

    public IEnumerator Shake(float duration, float magnitude)
    {
        if (!start)
        {
            start = true;

            Vector3 originalPos = transform.localPosition;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

                Debug.Log("Shake");
                Debug.Log(elapsed);
                Debug.Log(transform.localPosition);

                elapsed += Time.deltaTime;

                yield return null;
            }

            transform.localPosition = originalPos;
            Debug.Log("Return");
        }
    }
}
