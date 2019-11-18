using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour
{
    int damage;
    private void Start()
    {
        if (GameObject.Find("Player").transform.GetChild(0).GetChild(0).GetChild(0).GetChild(5).gameObject.activeSelf) damage = 2;
        else damage = 1;
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
                collision.gameObject.GetComponent<enemyController>().hit(damage);
                Destroy(gameObject);
            }
            else if (collision.gameObject.transform.parent.name == "Obstacles") Destroy(gameObject);
        }
        else if (collision.gameObject.GetComponent<enemyProjectile>() != null)
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.name == "Scrap(Clone)") Destroy(gameObject);
    }

}
