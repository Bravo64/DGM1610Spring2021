﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    // Random color to choose from
    [SerializeField] private Color[] colorOptions;

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            GetComponent<SpriteRenderer>().color = colorOptions[Random.Range(0, colorOptions.Length)];
    }
}
