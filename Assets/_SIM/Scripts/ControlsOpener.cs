using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsOpener : MonoBehaviour
{
    public GameObject controls;
    
    public void ControlWindow()
    {
        controls.SetActive(!controls.activeSelf);
    }
}
