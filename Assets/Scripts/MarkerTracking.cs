using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.MagicLeap;
using UnityEngine.XR.ARSubsystems;

public class MarkerTracking : MonoBehaviour
{
    public float QrCodeMarkerSize = 0.1f;
    public float ArucoMarkerSize = 0.1f;
    public MLMarkerTracker.MarkerType Type = MLMarkerTracker.MarkerType.QR;
    public MLMarkerTracker.ArucoDictionaryName ArucoDict = MLMarkerTracker.ArucoDictionaryName.DICT_5X5_100;
    private Dictionary<string, GameObject> _markers = new Dictionary<string, GameObject>();
    private ASCIIEncoding _asciiEncoder = new System.Text.ASCIIEncoding();
    public GameObject trackerObject;
    public GameObject dimmerObject;

    private MagicLeapInputs magicLeapInputs;
    private MagicLeapInputs.ControllerActions controllerActions;

    private readonly MLPermissions.Callbacks permissionCallbacks = new MLPermissions.Callbacks();


    private void Awake()
    {
        permissionCallbacks.OnPermissionGranted += OnPermissionGranted;
        permissionCallbacks.OnPermissionDenied += OnPermissionDenied;
        permissionCallbacks.OnPermissionDeniedAndDontAskAgain += OnPermissionDenied;
    }

    private void OnDestroy()
    {
        permissionCallbacks.OnPermissionGranted -= OnPermissionGranted;
        permissionCallbacks.OnPermissionDenied -= OnPermissionDenied;
        permissionCallbacks.OnPermissionDeniedAndDontAskAgain -= OnPermissionDenied;
    }

#if UNITY_ANDROID
    private void OnEnable()
    {
        MLMarkerTracker.OnMLMarkerTrackerResultsFound += OnTrackerResultsFound;
    }
    private void Start()
    {
        // A value of 0 uses the world cameras and 1 uses the RGB camera to scan for markers
        // by default markers will be tracked with the world cameras.
        int arucoCamera = 0;
        MLMarkerTracker.Settings trackerSettings = MLMarkerTracker.Settings.Create(
            true, Type, QrCodeMarkerSize, ArucoDict, ArucoMarkerSize, arucoCamera);
        _ = MLMarkerTracker.SetSettingsAsync(trackerSettings);
        Debug.Log("Start tracking");


        magicLeapInputs = new MagicLeapInputs();
        magicLeapInputs.Enable();
        controllerActions = new MagicLeapInputs.ControllerActions(magicLeapInputs);
        controllerActions.Bumper.performed += Bumper_performed;
        controllerActions.Trigger.performed += Trigger_performed;

        MLSegmentedDimmer.Activate();

    }

    private void Trigger_performed(InputAction.CallbackContext obj)
    {
        _ = MLMarkerTracker.StopScanningAsync();
    }

    private void OnDisable()
    {
        MLMarkerTracker.OnMLMarkerTrackerResultsFound -= OnTrackerResultsFound;
    }
    private void OnTrackerResultsFound(MLMarkerTracker.MarkerData data)
    {
        string id = "";
        float markerSize = .01f;
        switch (data.Type)
        {
            case MLMarkerTracker.MarkerType.Aruco_April:
                id = data.ArucoData.Id.ToString();
                markerSize = ArucoMarkerSize;
                break;
            case MLMarkerTracker.MarkerType.QR:
                id = _asciiEncoder.GetString(data.BinaryData.Data, 0, data.BinaryData.Data.Length);
                markerSize = QrCodeMarkerSize;
                break;
            case MLMarkerTracker.MarkerType.EAN_13:
            case MLMarkerTracker.MarkerType.UPC_A:
                id = _asciiEncoder.GetString(data.BinaryData.Data, 0, data.BinaryData.Data.Length);
                Debug.Log("No pose is given for marker type " + data.Type + " value is " + data.BinaryData.Data);
                break;
        }
        if (!string.IsNullOrEmpty(id))
        {
            if (_markers.ContainsKey(id))
            {
                GameObject marker = _markers[id];
                marker.transform.position = data.Pose.position;
                marker.transform.rotation = data.Pose.rotation;
                
            }
            else
            {
                //Create a primitive cube
                //GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //Render the cube with the default URP shader
                //marker.AddComponent<Renderer>();
                //marker.GetComponent<Renderer>().material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
                trackerObject.transform.localScale = new Vector3(markerSize, markerSize, markerSize);
                trackerObject.SetActive(true);
                _markers.Add(id, trackerObject);
                Debug.Log("Marker Found");
               
            }
        }
    }
#endif

    private void Bumper_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Bumper pressed");
        if (dimmerObject.activeSelf)
        {
            dimmerObject.SetActive(false);
            Debug.Log("Dimmer switched off");

        }
        else
        {
            dimmerObject.SetActive(true);
            Debug.Log("Dimmer switched on");
        }
    }


    private void OnPermissionGranted(string permission)
    {
     


    }
    private void OnPermissionDenied(string permission)
    {
        Debug.LogError($"Failed to create Planes Subsystem due to missing or denied {MLPermission.SpatialMapping} permission. Please add to manifest. Disabling script.");
        enabled = false;
    }
}
