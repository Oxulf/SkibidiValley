using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleActionWheel : MonoBehaviour
{
    public GameObject actionWheelCanvas;
    private bool isActionWheelOpen = false;
    void Start()
    {
        actionWheelCanvas.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            toggleActionWheel();
        }
    }
    void toggleActionWheel()
    {
        isActionWheelOpen = !isActionWheelOpen;

        actionWheelCanvas.SetActive(isActionWheelOpen);
    }
}