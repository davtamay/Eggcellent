// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

//namespace Microsoft.MixedReality.OpenXR.Samples
//{
    public class Tappable : MonoBehaviour
    {

        public UnityEvent OnAirTapped;
        public UnityEvent OnToggledOn;
        public UnityEvent OnToggledOff;

        public Material HoveredMaterial;
        public Material TappedMaterial;
        public MeshRenderer VolumeRenderer;
        public BoxCollider VolumeBox;
        public MeshRenderer TogglePlate;

        public bool IsToggleable = false;
        public bool IsToggledOn = false;
        private bool[] m_wasHandTapping = { false, false };

        private const float TAPPABLE_TAP_RANGE = 0.5f;

        public bool isItemWithGhost;
        
        private void OnTriggerStay(Collider coll)
        {
            //new skip if it has its tappable bounding box off
            if (!VolumeBox.gameObject.activeInHierarchy)
                return;


                bool showVolumeHovered = false;
            bool showVolumeTapped = false;

            Vector3 handPosition;
            bool isHandTapping;

            InputDevice device = default;
            int i = 0;
            //for (int i = 0; i < 2; i++)
            //{
            if (coll.CompareTag("LHAND"))
            {
                device = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);// (i == 0) ? XRNode.RightHand : XRNode.LeftHand);
                i = 1;

            //if (isItemWithGhost)
            //{


            //}
             //   if(SetTracableFuncionality.lHandCurrentTrackDriver)
            }

            if (coll.CompareTag("RHAND"))
            {
                device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
                i = 0;
            }

            if (!device.TryGetFeatureValue(CommonUsages.primaryButton, out isHandTapping) ||
               !device.TryGetFeatureValue(CommonUsages.devicePosition, out handPosition)) return ;
            //if (!device.TryGetFeatureValue(CommonUsages.primaryButton, out isHandTapping)) continue;
            //    if (!device.TryGetFeatureValue(CommonUsages.devicePosition, out handPosition)) continue;

                // Transform the hand position into a coordinate system defined by the volume's transform.
                // If it's within the cube at the origin in these coordinates, it's within the box in the Unity space.
                //Vector3 positionInVolumeCoordinates = VolumeBox.transform.InverseTransformPoint(handPosition);
                //if (Mathf.Abs(positionInVolumeCoordinates.x) < TAPPABLE_TAP_RANGE &&
                //    Mathf.Abs(positionInVolumeCoordinates.y) < TAPPABLE_TAP_RANGE &&
                //    Mathf.Abs(positionInVolumeCoordinates.z) < TAPPABLE_TAP_RANGE)
                //{
               


                    showVolumeHovered = true;

                    if (isHandTapping)
                    {
                        showVolumeTapped = true;

                        
                        if (!m_wasHandTapping[i])
                        {
                            OnAirTapped?.Invoke();
                            if (IsToggleable)
                            {
                                IsToggledOn = !IsToggledOn;
                  
                                if (IsToggledOn)
                                {
                        //if (coll.CompareTag("LHAND") && SetTracableFuncionality.lHandCurrentTrackDriver)
                        //    return;
                        //if (coll.CompareTag("RHAND") && SetTracableFuncionality.rHandCurrentTrackDriver)
                        //    return;

                        OnToggledOn.Invoke();
                                }
                                else
                                {
                                    OnToggledOff.Invoke();
                                }
                    
                    //(IsToggledOn ? OnToggledOn : OnToggledOff)?.Invoke();
                            }
                        }
                    }
               // }
                m_wasHandTapping[i] = isHandTapping;
         //   }

            VolumeRenderer.enabled = showVolumeHovered;
            VolumeRenderer.material = showVolumeTapped ? TappedMaterial : HoveredMaterial;
          //  TogglePlate.enabled = IsToggleable && IsToggledOn;
        }
    }
//}
