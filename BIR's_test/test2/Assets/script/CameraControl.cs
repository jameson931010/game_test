using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] float moveUnit;
    [SerializeField] float rotateUnit;
    [SerializeField] Vector3 zoomUnit;
    [SerializeField] float fastMultiple;
    [SerializeField] float movementTime;

    private Vector3 newPosition;
    private Quaternion newRotation;
    private Vector3 newZoom;
    private float fastMode;

    private Vector3 startPosition;
    private Vector3 endPosition;
    void Start()
    {
        Init();
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
        fastMode = 1;
    }

    void Update()
    {
        keyboardInput();
        mouseInput();
        movement();
    }
    void Init()
    {
        moveUnit = 0.1f;
        rotateUnit = 0.5f;
        zoomUnit = new Vector3(0, 0, 5);
        fastMultiple = 3;
        movementTime = 5; 
    }
    void keyboardInput()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            fastMode = fastMultiple;
        }
        else
        {
            fastMode = 1;
        }

        //move
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += transform.up*moveUnit*fastMode;
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition -= transform.up*moveUnit*fastMode;
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition -= transform.right*moveUnit*fastMode;
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += transform.right*moveUnit*fastMode;
        }

        //rotate
        if(Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(-Vector3.forward*rotateUnit*fastMode);
        }
        if(Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.forward*rotateUnit*fastMode);
        }

        //zoom
        if(Input.GetKey(KeyCode.R))
        {
            newZoom += zoomUnit;
        }
        if(Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomUnit;
        }
    }
    void mouseInput()
    {
        //move
        if(Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.forward, Vector3.zero);                                          //Plane(normal vector, position)
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                                //ray used as position vector
            float length;
            if(plane.Raycast(ray, out length))                                                          //out == pass by reference; calculate the distance from the plane (along the ray)
            {
                startPosition = ray.GetPoint(length);
            }
        }
        if(Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.forward, Vector3.zero);                                          //Plane(normal vector, position)
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                                //ray used as position vector
            float length;
            if(plane.Raycast(ray, out length))                                                          //out == pass by reference; calculate the distance from the plane (along the ray)
            {
                endPosition = ray.GetPoint(length);
                newPosition = transform.position + (startPosition - endPosition);
            }
        }
        //rotate
        if(Input.GetMouseButtonDown(2))
        {
            startPosition = Input.mousePosition;
        }
        if(Input.GetMouseButton(2))
        {
            endPosition = Input.mousePosition;
            newRotation *= Quaternion.Euler(Vector3.forward*(startPosition-endPosition).x/3f);
            startPosition = endPosition;
        }
        //zoom
        if(Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y*zoomUnit;
        }
    }

    void movement()
    {
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime*movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime*movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime*movementTime);
    }
}