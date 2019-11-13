using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limitsManager : MonoBehaviour
{

    public static Vector3 cameraPosRef;
    bool lerping;
    static Vector3 lastCameraPos;
    static int lerpFrames;

    bool playerInside;

    // Start is called before the first frame update
    void Start()
    {
        lerpFrames = 0;
        lerping = false;
        cameraPosRef = Camera.main.transform.position;
        playerInside = false;
    }

    // Update is called once per frame
    void Update()
    {
        lerpCamera();
        checkRoomLimits();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
            {
                lerping = true;
                Controller.freeze = true;
                lastCameraPos = Camera.main.transform.position;
            }
    }

    void lerpCamera()
    {
        if (!lerping) return;
        lerpFrames++;
        Camera.main.transform.position = Vector3.Lerp(lastCameraPos, transform.position + cameraPosRef, (float)lerpFrames / 100.0f);        
        if(lerpFrames == 100 || lastCameraPos == transform.position + cameraPosRef)
        {
            if(lerpFrames == 100)
            {
                Vector3 impulse = Camera.main.transform.position - GameObject.Find("Player").transform.position;
                impulse = new Vector3(impulse.x, 0, impulse.z);
                GameObject.Find("Player").transform.position += impulse.normalized * 3;
                Controller.roomLastPos = GameObject.Find("Player").transform.position;
                Controller.room = transform.parent.gameObject;
                if (Controller.room.name.Substring(0, 4) == "Base") inventory.pInv.lifes = 100;
            }
            lerpFrames = 0;
            lerping = false;
            Controller.freeze = false;
            GameObject.Find("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY;            
            if(transform.parent.GetChild(1).GetComponent<enemiesManager>() != null) transform.parent.GetChild(1).GetComponent<enemiesManager>().checkEnemies(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player") playerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           playerInside = false;
        }
        if (other.gameObject.name.Substring(0, 4) == "ammo" && other.gameObject.transform.parent == null) Destroy(other.gameObject);
    }

    public bool isPlayerInsideRoom()
    {
        return playerInside;
    }

    void checkRoomLimits()
    {
        if (!roomsManager.cleanRoom) GetComponent<Collider>().enabled = false;
        else GetComponent<Collider>().enabled = true;
    }
}
