using System;
using System.Collections;
using System.Collections.Generic;
using ActionEvents;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HeartTextBehavior : MonoBehaviour
{
    [SerializeField]
    private IntData health;
    [SerializeField]
    private VoidAction reloadScene;

    private string heartSymbol = "♥";
    private TextMeshProUGUI _myTextComponent;

    private void Awake()
    {
        _myTextComponent = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateHealthText()
    {
        if (health.value <= 0)
        {
            reloadScene.Raise();
            return;
        }
        _myTextComponent.text = "";
        for (int i = 0; i < health.GetInt(); i++)
        {
            _myTextComponent.text += heartSymbol;
        }
    }
}
