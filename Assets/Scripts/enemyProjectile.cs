using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    GameObject oldParent;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Limit") Destroy(gameObject);
        else if (collision.gameObject != oldParent && collision.gameObject.transform.parent != null && (collision.gameObject.transform.parent.name == "Enemies" || collision.gameObject.transform.parent.name == "Obstacles"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            inventory.pInv.lifes -= 30;
            Destroy(gameObject);
        }
    }

    public void setOldParent(GameObject _oldParent)
    {
        oldParent = _oldParent;

        Physics.IgnoreCollision(transform.GetComponent<Collider>(), oldParent.transform.GetComponent<Collider>(), true);
    }
}
