using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using System;

public class CameraParmetersAndroid : MonoBehaviour
{
    public static float horizontalViewAngle { get; private set; }
    public static float verticalViewAngle { get; private set; }
    public static int numCameras { get; private set; }

    static CameraParmetersAndroid()
    {
        AndroidJavaClass unityPlayerClass = new AndroidJavaClass("android.hardware.Camera");
   //     numCameras = cameraClass.CallStatic<int>("getNumberOfCameras");
    }
}
