using System;
using System.Collections;
using System.Collections.Generic;
using ActionEvents;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoinTextBehavior : MonoBehaviour
{
    [SerializeField]
    private IntData score;

    private string coinSymbol = "●";
    private TextMeshProUGUI _myTextComponent;

    private void Awake()
    {
        _myTextComponent = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCoinText()
    {
        _myTextComponent.text = coinSymbol + score.value.ToString();
    }
}
