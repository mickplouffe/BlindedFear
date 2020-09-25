using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    private static Func<float, float, float, bool> OnWithin;

    [SerializeField]
    private bool _isUsingMouse = true;

    [Header("Move Information")]
    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private float _xMin = -38;
    [SerializeField]
    private float _xMax = -12;
    [SerializeField]
    private float _yCameraHeight = 4f;
    [SerializeField]
    private float _zMin = -10;
    [SerializeField]
    private float _zMax = 10;
    [SerializeField]
    private float _screenOffset = 0.05f;
    private float _leftMoveZone;
    private float _rightMoveZone;
    private float _topMoveZone;
    private float _bottomMoveZone;

    [Header("Zoom Information")]
    [SerializeField]
    private float _zoomSpeed = 15f;
    [SerializeField]
    private float _minZoomDist = 2f;
    [SerializeField]
    private float _maxZoomDist = 8f;

    [Header("Panning Information")]
    [SerializeField]
    private float _rotateSpeed = 15f;
    [SerializeField]
    private float _xMinRotation = -20f;
    [SerializeField]
    private float _xMaxRotation = 15f;
    private float _pitch;
    private float _yaw;

    private Camera _cam;
    [SerializeField]
    private bool _isWaveActive = false;

    private void Awake()
    {
        _cam = Camera.main;

        if (_cam == null)
            Debug.LogError("No main camera set");
    }

    private void Start()
    {
        //Create a delegate to do the screen comparison
        OnWithin = (current, min, max) => ((current > min && current < max));

        //Setup screen zones to move camera
        _topMoveZone = Screen.height * (1 - _screenOffset);
        _bottomMoveZone = Screen.height * _screenOffset;
        _leftMoveZone = Screen.width * _screenOffset;
        _rightMoveZone = Screen.width * (1 - _screenOffset);

        //FIX BUG WHERE THIS HAS TO BE HARD CODED
        _pitch = transform.localEulerAngles.x; //-18f;
        _yaw = transform.localEulerAngles.y; // 0f;
    }

    private void Update()
    {
        //prevent camera from moving between waves
        if (_isWaveActive == false)
            return;

        Pan();
    }


    void Pan()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector2 mousePos = Input.mousePosition; //Mouse.current.position.ReadValue();

        //Reset the input flags to 0
        float xInput = 0f;
        float yInput = 0f;

        //Determine whether the mouse is at either edge of the screen
        xInput = OnWithin(mousePos.x, _leftMoveZone, _rightMoveZone) ? 0 : 1;
        //Determine which side
        if (xInput != 0)
            xInput = mousePos.x > _rightMoveZone ? 1 : -1;

        //Determine whether the mouse is at either edge of the screen
        yInput = OnWithin(mousePos.y, _bottomMoveZone, _topMoveZone) ? 0 : 1;
        //Determine which side
        if (yInput != 0)
            yInput = mousePos.y > _topMoveZone ? 1 : -1;


        if (_isUsingMouse)
        {
            horizontal = xInput;
            vertical = yInput;
        }


        if (horizontal != 0 || vertical != 0)
        {
            _pitch = Mathf.Clamp(_pitch + (-vertical * _rotateSpeed * Time.deltaTime), _xMinRotation, _xMaxRotation);
            _yaw += horizontal * _rotateSpeed * Time.deltaTime;   //this will automatically clamp between -180 and 180
            transform.rotation = Quaternion.Euler(_pitch, _yaw, 0f);
        }
    }

}
