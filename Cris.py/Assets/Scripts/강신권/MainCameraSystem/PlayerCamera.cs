using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private const float cameraMovingSpeed = 3.0f;
    [SerializeField]
    private const float cameraHeight = 15.0f;
    [SerializeField]
    private KeyCode cameraToggleKey = KeyCode.V;
    [SerializeField]
    private bool isTraceTarget = true;
    [SerializeField]
    private bool enableCameraZoom = true;
    [SerializeField]
    private const float fixedCameraZ = -10.0f;


    private Transform targetPlayer;

    // Start is called before the first frame update
    void Start()
    {
        // It's already done in the unity camera settings page
        // target = this.GameObject.Find("PlayerGFX").transform;
        // slower find than GameObject.FindObjectOfType<> ???
        // There is no performance degradation if you only need the reference once.
        // Do not insert in Update or FixedUpdate.

        targetPlayer = GameObject.FindObjectOfType<Player_Cris_py>().transform;
    }


    void Update()
    {
        CheckKeyDownToggleKey();

        if (enableCameraZoom == true && isTraceTarget == true)
        {
            CameraZoom();
        }
    }

    void FixedUpdate()
    {
        if (isTraceTarget == true)
        {
            SmoothTraceTarget();
        }
    }

    void CheckKeyDownToggleKey()
    {
        if (Input.GetKeyDown(cameraToggleKey) == true)
        {
            isTraceTarget = !isTraceTarget;
        }
    }
    void CameraZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.orthographicSize > 5.0f)
            {
                Camera.main.orthographicSize -= 0.25f;
            }

            // Debug.Log("Camera Scale : " + Camera.main.orthographicSize);
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.orthographicSize < 15.0f)
            {
                Camera.main.orthographicSize += 0.25f;
            }

            // Debug.Log("Camera Scale : " + Camera.main.orthographicSize);
        }

        else
        {
            // Nothing..
        }
    }

    void InstanceTraceTarget()
    {
        transform.position = new Vector3(targetPlayer.position.x, targetPlayer.position.y + 0.01f * cameraHeight, fixedCameraZ);
    }

    void SmoothTraceTarget()
    {
        Vector3 targetPosition = targetPlayer.position;

        // keep The Camera out
        // Fix Camera Z Position
        targetPosition.y += 0.01f * cameraHeight;
        targetPosition.z = fixedCameraZ;

        this.transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraMovingSpeed);
    }
}
