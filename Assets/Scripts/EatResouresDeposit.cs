using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatResouresDeposit : MonoBehaviour
{
    [SerializeField, Range(0, 20)]
    int maxQuantity = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void eat(int qt)
    {
        maxQuantity -= qt;
        if (maxQuantity == 0)
        {
            var gameManager = GameObject
                .FindGameObjectWithTag("GameManager")
                .GetComponent<GameManager>();
            gameManager.UpdateAvailableResources(-1);

            Destroy(this.gameObject);
        }
    }
}
