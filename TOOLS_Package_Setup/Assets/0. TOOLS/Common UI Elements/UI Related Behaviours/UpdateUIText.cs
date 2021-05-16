using System;
using System.Globalization;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateUIText : MonoBehaviour
{
    public enum Modes { UseIntData, UseFloatData }

    public bool runOnStart = true;
    public string valueStringPrefix = "Score: ";
    public string valueStringSuffix;
    public Modes usageMode = Modes.UseIntData;
    public IntData intDataDisplayObj;
    public FloatData floatDataDisplayObj;

    private TextMeshProUGUI _myTextComponent;
    private string _savedString;
    
    public void Start()
    {
        _myTextComponent = GetComponent<TextMeshProUGUI>();
        if (runOnStart)
        {
            RefreshUIText();
        }
    }
    
    public void RefreshUIText()
    {
        switch (usageMode)
        {
            case Modes.UseIntData:
                _savedString = intDataDisplayObj.value.ToString();
                break;
            case Modes.UseFloatData:
                _savedString = floatDataDisplayObj.value.ToString(CultureInfo.InvariantCulture);
                break;
        }
        _savedString = valueStringPrefix + _savedString + valueStringSuffix;
        _myTextComponent.text = _savedString;
    }
}
