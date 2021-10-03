using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{

    public PlaneTilt[] planes;

    public float camDistance = 10f;
    public float camAngle = 30f;
    public float rotationSpeed = 30f;
    public float startingDip = -20f;
    public float riseTime = 1f;

    Vector3 averageLevelPosition;
    Vector3 mouseDelta;
    Vector3 lastMousePosition;

    float timer;
    float height;

    void Start()
    {

        UpdateStartingCamPos();
    }

    public void UpdateStartingCamPos()
    {       
        planes = FindObjectsOfType<PlaneTilt>();
        averageLevelPosition = Vector3.zero;
        foreach(var p in planes)
        {
            averageLevelPosition += p.transform.position;
        }
        averageLevelPosition /= planes.Length;

        height = camDistance / Mathf.Sin(Mathf.PI / 2f) * Mathf.Sin(camAngle * Mathf.Deg2Rad);
        float offset = Mathf.Sqrt(camDistance*camDistance - height*height);

        transform.position = averageLevelPosition + Vector3.up * (height) + new Vector3(transform.position.x, 0f, transform.position.z).normalized * offset;
        transform.LookAt(averageLevelPosition, Vector3.up);

        //transform.position = startingPos;
    }

    void Update()
    {

        float rotSign = 0f;
        float rotAmount = 1f;
        if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow)){
            rotSign = 1f;
        }
        else if(Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.RightArrow))
        {
            rotSign = -1f;
        }

        if(Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        if(Input.GetMouseButton(0))
        {
            mouseDelta = lastMousePosition - Input.mousePosition;
            rotSign = -Mathf.Sign(mouseDelta.x);
            rotAmount = Mathf.Abs(mouseDelta.x);
            lastMousePosition = Input.mousePosition;
        }

        transform.RotateAround(averageLevelPosition, Vector3.up, rotAmount * rotationSpeed * rotSign * Time.deltaTime);
    }
}
