using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputMapper : MonoBehaviour
{

    public UnityEvent<Vector2> onMove;
    public UnityEvent<Vector2> onLook;
    public UnityEvent onRightTrigger;
    public UnityEvent onLeftTrigger;
    public UnityEvent onStart;
    public UnityEvent onBack;
    public UnityEvent onJump;
    public UnityEvent onB;
    public UnityEvent onY;
    public UnityEvent onX;

    private PlayerInput _playerInput;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        var leftStick = _playerInput.actions["move"].ReadValue<Vector2>();
        
        if (leftStick != Vector2.zero)
        {
            onMove.Invoke(leftStick);
        }
        
        var rightStick = _playerInput.actions["look"].ReadValue<Vector2>();
        if (rightStick != Vector2.zero)
        {
            onLook.Invoke(rightStick);
        }

        if (_playerInput.actions["triggerRight"].ReadValue<float>() > 0)
        {
            onRightTrigger.Invoke();
        }
        
        if (_playerInput.actions["triggerLeft"].ReadValue<float>() > 0)
        {
            onLeftTrigger.Invoke();
        }
        
        if (_playerInput.actions["a"].ReadValue<float>() > 0)
        {
            onJump.Invoke();
        }
        
        if (_playerInput.actions["b"].ReadValue<float>() > 0)
        {
            onB.Invoke();
        }
        
        if (_playerInput.actions["y"].ReadValue<float>() > 0)
        {
            onY.Invoke();
        }
        
        if (_playerInput.actions["x"].ReadValue<float>() > 0)
        {
            onX.Invoke();
        }
        
        if (_playerInput.actions["menu"].ReadValue<float>() > 0)
        {
            onStart.Invoke();
        }
        
        if (_playerInput.actions["back"].ReadValue<float>() > 0)
        {
            onBack.Invoke();
        }
    }
}
