using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapacitySlider : MonoBehaviour
{
    Slider slider;
    
    [SerializeField]
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.getCarryingQuantity() *1.0f/ player.getMaxCarryingCapacity();

    }

}
