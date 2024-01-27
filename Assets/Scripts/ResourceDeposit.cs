using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDeposit : MonoBehaviour
{
    [SerializeField, Range(0, 20)] int maxQuantity = 5;
    int currentQuantity;
    bool active = true;
    // Start is called before the first frame update
    void Awake()
    {
        currentQuantity = maxQuantity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}
