using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EggbeaterMixing : MonoBehaviour
{
    public float timeUntilFinishedMix;
    public Material halfwayDoneMaterial;
  
    public Material doneMaterial;

    public float currentTime = 0;

    public UnityEvent onDone;

    public string tagName = "whisk";
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(tagName))
        {
            currentTime += Time.deltaTime;

            if (timeUntilFinishedMix < currentTime)
            {
                onDone.Invoke();
                gameObject.SetActive(false);
            }

        }
    }
}
