using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShoot : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void shoot(int message)
    {
        GameObject shot = Instantiate(transform.parent.GetChild(message).gameObject as GameObject);
        shot.transform.position = transform.parent.GetChild(message).transform.position;
        shot.transform.rotation = transform.parent.GetChild(message).transform.rotation;
        shot.GetComponent<Collider>().enabled = true;
        shot.transform.LookAt(player.transform.position);
        shot.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        shot.GetComponent<MeshRenderer>().enabled = true;
        shot.AddComponent<Rigidbody>();
        shot.AddComponent<enemyProjectile>();
        shot.GetComponent<enemyProjectile>().setOldParent(transform.parent.gameObject);
        shot.GetComponent<Rigidbody>().useGravity = false;
        shot.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        Vector3 initialVelocity = shot.GetComponent<Rigidbody>().velocity;
        //shot.GetComponent<Rigidbody>().AddForce((player.transform.position - shot.transform.position).normalized * 1000.0f);
        Vector3 predictedTarget = getPredictedTarget(shot);
        shot.GetComponent<Rigidbody>().velocity = initialVelocity;
        shot.GetComponent<Rigidbody>().AddForce((predictedTarget - shot.transform.position).normalized * 750.0f);
        shot.transform.LookAt((shot.GetComponent<Rigidbody>().velocity).normalized * 10);
    }

    Vector3 getPredictedTarget(GameObject _shot)
    {
        return ((player.transform.position + player.GetComponent<Rigidbody>().velocity) + player.transform.position) / 2;
    }
}
