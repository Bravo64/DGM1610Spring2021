using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciateRandom : MonoBehaviour
{
    public GameObject[] prefabList;

        // Start is called before the first frame update
    void Start()
    {
        Instantiate(prefabList[Random.Range(0, prefabList.Length)], transform.position, Quaternion.identity);
    }
}
