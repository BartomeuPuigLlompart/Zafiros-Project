using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour
{
    private void Start()
    {
        Physics.IgnoreLayerCollision(11, 11, true);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Limit") Destroy(gameObject);
        else if (collision.gameObject.transform.parent != null)
        {
            if (collision.gameObject.transform.parent.name == "Enemies")
            {
                collision.gameObject.GetComponent<enemyController>().hit();
                Destroy(gameObject);
            }
            else if (collision.gameObject.transform.parent.name == "Obstacles") Destroy(gameObject);
        }
        else if (collision.gameObject.GetComponent<enemyProjectile>() != null)
        {
            Destroy(gameObject);
        }
    }

}
