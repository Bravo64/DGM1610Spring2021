using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreTextBehaviour : MonoBehaviour
{
    [SerializeField] 
    private IntData scoreObj;
    
    private TextMeshProUGUI _myTextComponent;
    
    // Start is called before the first frame update
    void Awake()
    {
        _myTextComponent = GetComponent<TextMeshProUGUI>();
        UpdateScoreText();
    }
    
    public void UpdateScoreText()
    {
        _myTextComponent.text = "Score: " + scoreObj.value.ToString();
    }
}
