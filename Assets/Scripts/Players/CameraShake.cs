using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Basic Shake Variables")]
    public float shakeDuration;
    public float shakeMagnitude;

    [Header("Minimum and Maximum Shake Distance")]
    public float minShake = -1f;
    public float maxShake = 1f;

    public void Awake()
    {
        GameObject.Find("GameManager").GetComponent<UniversalImpacts>().activeCameras.Add(gameObject);
    }

    public IEnumerator ShakeTheCamera()
    {
        Debug.Log("Attempting to shake " + gameObject);
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0f;

        
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(minShake, maxShake) * shakeMagnitude;
            float y = Random.Range(minShake, maxShake) * shakeMagnitude;

            transform.localPosition = new Vector3((originalPos.x + x),(originalPos.y + y), originalPos.z);
            elapsed += Time.deltaTime;

            yield return null;
        }
        
        transform.localPosition = originalPos;
    }
}
