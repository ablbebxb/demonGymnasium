using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
    public Vector3 janitorCameraPosition;
    public Vector3 demonCameraPosition;

    public Vector3 janitorCameraRotation;
    public Vector3 demonCameraRotation;

    Vector3 goalPoistion;
    Quaternion goalRotation;
    Camera mainCamera;

    void Start()
    {
        mainCamera = GameObject.FindObjectOfType<Camera>();
    }

    void Update()
    {

    }

}
