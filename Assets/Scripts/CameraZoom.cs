﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraZoom : MonoBehaviour
{
    private Camera cam;

    private Vector3 firstPosition;
    private float firstSize;

    private float doubleClickTime = 0.25f;
    private float lastClickTime;

    private Vector2 firstTouchPrevPos;
    private Vector2 secondTouchPrevPos;

    private float prevPosDifference;
    private float currentPosDifference;

    private Vector3 touchStart;
    private float zoomModifier;
    private float zoomSpeed = 0.01f;
    private float minSize = 2;
    private float maxSize = 8;

    private void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        firstPosition = cam.transform.position;
        firstSize = cam.orthographicSize;
    }

    private void Update()
    {
        MoveCameraPosition();
        BackToNormalSize();
    }

    private void BackToNormalSize()
    {
        //detect double click
        if (Input.GetMouseButtonDown(0))
        {
            float offsetTime = Time.time - lastClickTime;

            if (offsetTime < doubleClickTime)
            {
                cam.orthographicSize = firstSize;
                cam.transform.position = firstPosition;
            }

            lastClickTime = Time.time;
        }
    }

    private void Zoom()
    {
        //pinch to zoom
        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            prevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            currentPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomSpeed;

            if (prevPosDifference > currentPosDifference)
            {
                //zoom in
                cam.orthographicSize += zoomModifier;
            }

            if (prevPosDifference < currentPosDifference)
            {
                //zoom out
                cam.orthographicSize -= zoomModifier;
            }
        }

        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minSize, maxSize);
    }

    private void MoveCameraPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                touchStart = cam.ScreenToWorldPoint(Input.mousePosition);
            }
            
        }

        //drag camera 
        if (Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 dir = touchStart - cam.ScreenToWorldPoint(Input.mousePosition);
                cam.transform.position += dir;

                //bound camera
                if (cam.orthographicSize < 4)
                {
                    if (cam.transform.position.x >= 20.77f)
                    {
                        cam.transform.position = new Vector3(20.77f, cam.transform.position.y, cam.transform.position.z);
                    }
                    else if (cam.transform.position.x <= -10.48f)
                    {
                        cam.transform.position = new Vector3(-10.48f, cam.transform.position.y, cam.transform.position.z);
                    }

                    if (cam.transform.position.y > 6.27f)
                    {
                        cam.transform.position = new Vector3(cam.transform.position.x, 6.27f, cam.transform.position.z);
                    }
                    else if (cam.transform.position.y < -4.12f)
                    {
                        cam.transform.position = new Vector3(cam.transform.position.x, -4.12f, cam.transform.position.z);
                    }
                }
                else if (cam.orthographicSize < 9)
                {
                    if (cam.transform.position.x >= 10.49f)
                    {
                        cam.transform.position = new Vector3(10.49f, cam.transform.position.y, cam.transform.position.z);
                    }
                    else if (cam.transform.position.x <= -1.49f)
                    {
                        cam.transform.position = new Vector3(-1.49f, cam.transform.position.y, cam.transform.position.z);
                    }

                    if (cam.transform.position.y > 1.44f)
                    {
                        cam.transform.position = new Vector3(cam.transform.position.x, 1.44f, cam.transform.position.z);
                    }
                    else if (cam.transform.position.y < -0.54f)
                    {
                        cam.transform.position = new Vector3(cam.transform.position.x, -0.54f, cam.transform.position.z);
                    }
                }
            }

            Zoom();
        }
            
    }
}
