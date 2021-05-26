using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft.MixedReality.OpenXR.Samples;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;
//using UnityEngine.InputSystem.XR;
//using UnityEngine.SpatialTracking;


public class SetTracableFuncionality : MonoBehaviour
{
    private TrackedPoseDriver trackedPoseDriver;

    public GameObject rootObjectForTrackDriver;

    public static GameObject lHandCurrentTrackDriver;
    public static GameObject rHandCurrentTrackDriver;


    public static Transform lHandParent;
    public static Transform rHandParent;

    public Transform originalParent;
    public IEnumerator Start()
    {
        originalParent = transform.parent;

        yield return new WaitUntil(() =>
        {

            lHandParent = GameObject.FindGameObjectWithTag("LHANDPARENT").transform;
            rHandParent = GameObject.FindGameObjectWithTag("RHANDPARENT").transform;


            if (lHandParent && rHandParent)
                return true;
            else
                return false;
        }
        );


    }
    //public static bool leftHandHasObject;
    //public static bool rightHandHasObject;
    // Start is called before the first frame update
    //void Start()
    //{
    //    SetTrackerSpace();
    //    Application.onBeforeRender += Application_onBeforeRender;
    //}

    //private void Application_onBeforeRender()
    //{
    //    // Apply pose before rendering
    //}
    public bool leftHandAlreadySelected;
    public bool rightHandAlreadySelected;
    public bool isFlippedAlreadyL;
    public bool isFlippedAlreadyR;
    public void OnTriggerEnter(Collider other)
    {
        
    }
    public void OnTriggerExit(Collider other)
    {
        leftHandAlreadySelected = false;
        rightHandAlreadySelected = false;
        //isFlippedAlreadyL = false;
        //isFlippedAlreadyR = false;
        //  rootObjectForTrackDriver.transform.SetParent(null, true);
    }


    public void OnTriggerStay(Collider other)
    {
        InputDevice device = default;
        Vector3 handPosition;
        bool isHandTapping;

        if (other.CompareTag("RHAND"))
        {
            device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);// ? XRNode.RightHand : XRNode.LeftHand);

            if (device.TryGetFeatureValue(CommonUsages.devicePosition, out handPosition) && device.TryGetFeatureValue(CommonUsages.primaryButton, out isHandTapping) )
            {
                if (!isHandTapping)
                    return;
                
                    if (rHandParent == null)
                    return;

                if (rightHandAlreadySelected)
                {
                    var pos = rootObjectForTrackDriver.transform.GetChild(0).localPosition;
                    var rot = rootObjectForTrackDriver.transform.GetChild(0).localEulerAngles;


                    if (Mathf.Sign(pos.x) == 1)
                    {
                        rootObjectForTrackDriver.transform.GetChild(0).localPosition *= -1;
                      //  rootObjectForTrackDriver.transform.GetChild(0).localRotation = Quaternion.Euler(-rot);
                    }
                 //   return;

                }

                rightHandAlreadySelected = true;

           

                EnsureTrackedPoseDriver();
                //rootObjectForTrackDriver.transform.localPosition = rHandParent.TransformPoint(Vector3.zero);
                //rootObjectForTrackDriver.transform.eulerAngles = rHandParent.TransformDirection(Vector3.zero);


                trackedPoseDriver.SetPoseSource(TrackedPoseDriver.DeviceType.GenericXRController, TrackedPoseDriver.TrackedPose.RightPose);

                rHandCurrentTrackDriver = rootObjectForTrackDriver;

            }

        }
        if (other.CompareTag("LHAND"))
        {
            device = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

            if (device.TryGetFeatureValue(CommonUsages.devicePosition, out handPosition) && device.TryGetFeatureValue(CommonUsages.primaryButton, out isHandTapping))
            {
                if (!isHandTapping)
                    return;

                if (lHandParent == null)
                    return;


                //if (leftHandAlreadySelected)
                //    return;


                if (leftHandAlreadySelected)
                {
                    var pos = rootObjectForTrackDriver.transform.GetChild(0).localPosition;
                    var rot = rootObjectForTrackDriver.transform.GetChild(0).localEulerAngles;


                    if (Mathf.Sign(pos.x) == -1)
                    {
                        rootObjectForTrackDriver.transform.GetChild(0).localPosition *= -1;

                       // rootObjectForTrackDriver.transform.GetChild(0).localRotation = Quaternion.Euler(-rot);
                    }
                    //   return;

                }



                leftHandAlreadySelected = true;

               


              

                EnsureTrackedPoseDriver();
                //rootObjectForTrackDriver.transform.localPosition = lHandParent.TransformPoint(Vector3.zero);
                //rootObjectForTrackDriver.transform.eulerAngles = lHandParent.TransformDirection(Vector3.zero);


                trackedPoseDriver.SetPoseSource(TrackedPoseDriver.DeviceType.GenericXRController, TrackedPoseDriver.TrackedPose.LeftPose);

                lHandCurrentTrackDriver = rootObjectForTrackDriver;
                //  }
                //  leftHandHasObject = true;
            }
        }




    }

    public void OnDisable()
    {
        //RemoveTrackedPosedDriver();
    }

    public void EnsureTrackedPoseDriver()
    {
        trackedPoseDriver = rootObjectForTrackDriver.GetComponent<TrackedPoseDriver>();
        if (trackedPoseDriver == null)
        {
            trackedPoseDriver = rootObjectForTrackDriver.AddComponent<TrackedPoseDriver>();


           
         //   trackedPoseDriver.UseRelativeTransform = true;


        }

    }

    public void RemoveTrackedPosedDriver()
    {

        trackedPoseDriver = rootObjectForTrackDriver.GetComponent<TrackedPoseDriver>();
        if (trackedPoseDriver != null)
        {
            Destroy(trackedPoseDriver);
            trackedPoseDriver = null;
        }

      //  rootObjectForTrackDriver.transform.SetParent(null);

        rHandCurrentTrackDriver = null;
        lHandCurrentTrackDriver = null;
    }




}



