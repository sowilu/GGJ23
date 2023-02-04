using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerDisabler : MonoBehaviour
{
    public static PlayerDisabler inst;
    public PlayerController player;

    public bool listenForInput = false;

    public UnityEvent onX;
    public UnityEvent onY;
    public UnityEvent onB;

    PlayerInput _input;

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        _input = GetComponent<PlayerInput>();
    }

    public void Enable()
    {
        player.enabled = true;
        listenForInput = false;
    }

    public void Disable()
    {
        player.enabled = false;
        listenForInput = true;
    }

    private void Update()
    {
        if (listenForInput)
        {
            if (_input.actions["x"].triggered)
            {
                onX.Invoke();
            }
            if (_input.actions["y"].triggered)
            {
                onY.Invoke();
            }
            if (_input.actions["b"].triggered)
            {
                onB.Invoke();
            }
        }
    }
}
