using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAmGoingToExplode : MonoBehaviour
{
    public GameObject END;
    public void ENDME()
    {
        print("YES");
        END.SetActive(true);
    }
}
