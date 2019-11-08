using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour
{

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Limit") Destroy(gameObject);
        else if (collision.gameObject.transform.parent != null && collision.gameObject.transform.parent.name == "Enemies")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "Limits") Destroy(gameObject);      
    }

}
