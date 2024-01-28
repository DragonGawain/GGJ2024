using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class CapacitySlider : MonoBehaviour
{
    
    Slider slider;
    
    [SerializeField]
    PlayerController player;

    [SerializeField]
    Image cheeseImage;

    [SerializeField]
    Image fillImage;

    [SerializeField]
    Sprite[] images;


    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
        slider.value = player.getCarryingQuantity() *1.0f/ player.getMaxCarryingCapacity();
        if (player.carryingType == cheese.MELTED)
        {
            cheeseImage.enabled = true;
            cheeseImage.sprite = images[2];
            cheeseImage.color = Color.yellow;
            
            fillImage.color = Color.yellow;
        }
        else if (player.carryingType == cheese.SHREDDED)
        {
            cheeseImage.enabled = true;
            cheeseImage.sprite = images[1];
            fillImage.color = Color.blue;
        }
        else if (player.carryingType == cheese.CURD)
        {
            cheeseImage.enabled = true;
            cheeseImage.sprite = images[0];
            fillImage.color = Color.cyan;
        }
        else
        {
            
            cheeseImage.enabled = false;
            //cheeseImage.color = Color.white;
            fillImage.color = Color.white;
        }
    }

}
