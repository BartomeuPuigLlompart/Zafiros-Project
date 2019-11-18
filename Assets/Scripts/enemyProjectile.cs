using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            inventory.pInv.lifes -= 30 - 30 / inventory.pInv.armour;
            if (inventory.pInv.lifes <= 0) SceneManager.LoadScene("Alien Ship");
            Destroy(gameObject);
        }
    }

    public void setOldParent(GameObject _oldParent)
    {
        oldParent = _oldParent;

        Physics.IgnoreCollision(transform.GetComponent<Collider>(), oldParent.transform.GetComponent<Collider>(), true);
    }
}
