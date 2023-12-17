using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour
{
    public GameObject[] collectibles;
    // Start is called before the first frame update
    void Awake()
    {
        int children = transform.childCount;
        collectibles = new GameObject[children];
        for (int i = 0; i < children; ++i)
        {
            collectibles[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
