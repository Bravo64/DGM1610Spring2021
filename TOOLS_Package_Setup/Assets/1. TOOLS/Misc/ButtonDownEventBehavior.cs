using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonDownEventBehavior : MonoBehaviour
{
    private enum ButtonInputs {Jump, Fire1, Fire2, Fire3, Horizontal, Vertical }

    [SerializeField]
    private ButtonInputs selectedButton = ButtonInputs.Jump;
    [SerializeField] 
    private UnityEvent buttonDownEvent;

    private string inputString;
    
    void Start()
    {
        switch (selectedButton)
        {
            case ButtonInputs.Jump:
                inputString = "Jump";
                break;
            case ButtonInputs.Fire1:
                inputString = "Fire1";
                break;
            case ButtonInputs.Fire2:
                inputString = "Fire2";
                break;
            case ButtonInputs.Fire3:
                inputString = "Fire3";
                break;
            case ButtonInputs.Horizontal:
                inputString = "Horizontal";
                break;
            case ButtonInputs.Vertical:
                inputString = "Vertical";
                break;
            default:
                inputString = "Jump";
                break;
        }
    }

    public void ReassignButton(string newButtonInput)
    {
        inputString = newButtonInput;
    }

    void Update()
    {
        if (Input.GetButtonDown(inputString))
        {
            buttonDownEvent.Invoke();
        }
    }
}
