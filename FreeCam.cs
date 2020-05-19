using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;

namespace VRCFreeCam
{
    public class FreeCam : MelonMod
    {
        public override void OnUpdate()
        {
            if (CamEye)
            {
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F9))
                {
                    if (CamFree.GetComponent<Camera>().enabled)
                    {
                        CameraPosition = CamFree.transform.position;
                    }
                    else
                    {
                        CameraPosition = CamEye.transform.position;
                    }
                }
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F5))
                {
                    Camera CamEyeComponent = CamEye.GetComponent<Camera>();
                    Camera CamFreeComponent = CamFree.GetComponent<Camera>();
                    if (!CamFreeComponent.enabled)
                    {
                        CamFree.transform.position = CameraPosition;
                        CamFreeComponent.enabled = true;
                        CamEyeComponent.enabled = false;
                    }
                    else
                    {
                        CamEyeComponent.enabled = true;
                        CamFreeComponent.enabled = false;
                    }
                }
                if (CamFree)
                {
                    CamFree.transform.rotation = CamEye.transform.rotation;
                }
            }
        }
        public override void OnFixedUpdate()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                CamFree.transform.position += CamFree.transform.forward * 0.2f;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                CamFree.transform.position -= CamFree.transform.forward * 0.2f;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                CamFree.transform.position += CamFree.transform.right * 0.2f;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                CamFree.transform.position -= CamFree.transform.right * 0.2f;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                CamFree.transform.position += CamFree.transform.up * 0.12f;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                CamFree.transform.position -= CamFree.transform.up * 0.12f;
            }
        }
        public static GameObject CamFree
        {
            get
            {
                if (!_CamFree)
                {
                    _CamFree = new GameObject("CamFree");
                    GameObject.DontDestroyOnLoad(_CamFree);
                    _CamFree.transform.localScale = CamEye.transform.localScale;
                    _CamFree.AddComponent<Camera>().enabled = false;
                }
                return _CamFree;
            }
        }
        public static Vector3 CameraPosition;
        public static GameObject CamEye
        {
            get
            {
                if (!_CamEye)
                {
                    _CamEye = GameObject.Find("Camera (eye)");
                }
                return _CamEye;
            }
        }
        private static GameObject _CamEye;
        private static GameObject _CamFree;
    }
}
