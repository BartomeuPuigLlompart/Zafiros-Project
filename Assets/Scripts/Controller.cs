using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    Rigidbody rb;

    Vector3 mousePos;

   [SerializeField]
    float speed;

    [SerializeField]
    float zValue;

    int shootFrames;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        shootFrames = 0;
    }

    // Update is called once per frame
    void Update()
    {
        updateSpeed();
        updateOrientation();
        checkShoot();
    }

    void updateSpeed()
    {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        rb.velocity = rb.velocity.normalized * speed;
    }

    void updateOrientation()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zValue));       

        RaycastHit hit;

        Physics.Raycast(Camera.main.gameObject.transform.position, mousePos - Camera.main.transform.position, out hit, zValue, 9);

        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance(Camera.main.transform.position, hit.point)));
        transform.LookAt(new Vector3(mousePos.x, transform.position.y, mousePos.z));
    }

    void checkShoot()
    {
        if (Input.GetAxis("Fire") > 0)
        {
            if (shootFrames == 0 || shootFrames == 30)
            {
                GameObject shot = Instantiate(transform.GetChild(0).GetChild(0).GetChild(1).gameObject as GameObject);
                shot.transform.position = transform.GetChild(0).GetChild(0).GetChild(1).transform.position;
                shot.transform.rotation = transform.GetChild(0).GetChild(0).GetChild(1).transform.rotation;
                shot.GetComponent<Collider>().enabled = true;
                shot.transform.LookAt(transform.position + transform.forward * 10);
                shot.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                shot.GetComponent<MeshRenderer>().enabled = true;
                shot.AddComponent<Rigidbody>();
                shot.GetComponent<Rigidbody>().useGravity = false;
                shot.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                shot.GetComponent<Rigidbody>().AddForce(transform.forward * 1000.0f);
                shot.AddComponent<shot>();
            }
            else if (shootFrames == 15 || shootFrames == 45)
            {
                GameObject shot = Instantiate(transform.GetChild(0).GetChild(1).GetChild(1).gameObject as GameObject);
                shot.transform.position = transform.GetChild(0).GetChild(1).GetChild(1).transform.position;
                shot.transform.rotation = transform.GetChild(0).GetChild(1).GetChild(1).transform.rotation;
                shot.GetComponent<Collider>().enabled = true;
                shot.transform.LookAt(transform.position + transform.forward * 10);
                shot.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                shot.GetComponent<MeshRenderer>().enabled = true;
                shot.AddComponent<Rigidbody>();
                shot.GetComponent<Rigidbody>().useGravity = false;
                shot.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                shot.GetComponent<Rigidbody>().AddForce(transform.forward * 1000.0f);
                shot.AddComponent<shot>();
            }
            shootFrames++;
            if (shootFrames == 60) shootFrames = 0;
        }

        else shootFrames = 0;
    }
}
