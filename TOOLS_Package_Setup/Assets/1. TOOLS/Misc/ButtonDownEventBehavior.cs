using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonDownEventBehavior : MonoBehaviour
{
    public enum ButtonInputs {Jump, Fire1, Fire2, Fire3, Horizontal, Vertical }
    
    public ButtonInputs selectedButton = ButtonInputs.Jump;
    public UnityEvent buttonDownEvent;

    private string _inputString;
    
    void Start()
    {
        switch (selectedButton)
        {
            case ButtonInputs.Jump:
                _inputString = "Jump";
                break;
            case ButtonInputs.Fire1:
                _inputString = "Fire1";
                break;
            case ButtonInputs.Fire2:
                _inputString = "Fire2";
                break;
            case ButtonInputs.Fire3:
                _inputString = "Fire3";
                break;
            case ButtonInputs.Horizontal:
                _inputString = "Horizontal";
                break;
            case ButtonInputs.Vertical:
                _inputString = "Vertical";
                break;
            default:
                _inputString = "Jump";
                break;
        }
    }

    public void ReassignButton(string newButtonInput)
    {
        _inputString = newButtonInput;
    }

    void Update()
    {
        if (Input.GetButtonDown(_inputString))
        {
            buttonDownEvent.Invoke();
        }
    }
}
