using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour
{

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    

}
