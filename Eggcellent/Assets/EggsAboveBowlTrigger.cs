using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SpatialTracking;

public class EggsAboveBowlTrigger : MonoBehaviour
{
    public UnityEvent onAllEggsGathered;
    public int eggsGathered;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("egg"))
        {
            ++eggsGathered;

            other.transform.parent = null;
            other.transform.rotation = Quaternion.Euler(120,-90,0);

            Vector3 curPos = other.transform.position;
            RemoveTrackedPosedDriver(other.gameObject);

            other.transform.position = curPos;

            other.enabled = false;
            other.GetComponent<Animator>().SetTrigger("Open");


         if(other.TryGetComponent(out AssociatedTappable aT))
            aT.associatedTapplable.gameObject.SetActive(false);

           
            if(eggsGathered ==3)
            onAllEggsGathered.Invoke();

        }
    }

    TrackedPoseDriver trackedPoseDriver;
    void RemoveTrackedPosedDriver(GameObject gO)
    {
        trackedPoseDriver = gO.GetComponent<TrackedPoseDriver>();
        if (trackedPoseDriver != null)
        {
            Destroy(trackedPoseDriver);
            trackedPoseDriver = null;
        }
    }
}
