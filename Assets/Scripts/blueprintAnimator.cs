using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blueprintAnimator : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).transform.rotation = transform.GetChild(1).transform.rotation = transform.GetChild(2).transform.rotation = Quaternion.AxisAngle(Vector3.up, Time.realtimeSinceStartup);
        transform.GetChild(0).transform.eulerAngles = transform.GetChild(1).transform.eulerAngles = transform.GetChild(2).transform.eulerAngles += new Vector3(0, 0, 90);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            inventory.pInv.weaponBlueprint = true;
            Destroy(gameObject);
        }
    }
}
