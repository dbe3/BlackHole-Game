using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;

    [Header("Camera Attributes")]
    public Camera Cam;
    public float Speed;

    void Update()
    {
        Vector3 PositionLerp = Vector3.Lerp(transform.position, Player.position, Time.deltaTime * Speed);

        PositionLerp.z = transform.position.z;

        transform.position = PositionLerp;

        Cam.orthographicSize = Mathf.Lerp(Cam.orthographicSize, 3 * Player.localScale.x, Speed * Time.deltaTime);
    }
}
