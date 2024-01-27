using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField, Range(1f, 10f)]
    float smoothFactor = 5f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            -10
        );

        targetPosition = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothFactor * Time.fixedDeltaTime
        );
        transform.position = targetPosition;
    }
}
