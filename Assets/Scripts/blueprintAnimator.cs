using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blueprintAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.AxisAngle(transform.up, Time.realtimeSinceStartup);
    }
}
