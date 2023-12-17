using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SmellableCoin : ICollectible
{

    public static event Action OnCoinCollected;

    public void Smelled()
    {
        //How do i get this to integrate with Dees stuff 
        Debug.Log("Yup, it's been smelled");
        //below line throwing error, might have to do w/ class not deriving from monobehavior -dee
        //Destroy(gameObject);
        OnCoinCollected?.Invoke();

    }
}
