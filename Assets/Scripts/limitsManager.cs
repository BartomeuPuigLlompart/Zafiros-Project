using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limitsManager : MonoBehaviour
{

    public static Vector3 cameraPosRef;
    bool lerping;
    static Vector3 lastCameraPos;
    static int lerpFrames;

    // Start is called before the first frame update
    void Start()
    {
        lerpFrames = 0;
        lerping = false;
        cameraPosRef = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        lerpCamera();
        checkEnemiesLeft();
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
        if(lerpFrames == 100)
        {
            lerpFrames = 0;
            lerping = false;
            Controller.freeze = false;
            GameObject.Find("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY;
            Vector3 impulse = Camera.main.transform.position - GameObject.Find("Player").transform.position;
            impulse = new Vector3(impulse.x, 0, impulse.z);
            GameObject.Find("Player").transform.position += impulse.normalized * 3;
            if (transform.parent.GetChild(1).childCount != 0) roomsManager.cleanRoom = false;
        }
    }

    void checkEnemiesLeft()
    {
        if (roomsManager.cleanRoom) return;

        else if(transform.parent.GetChild(1).childCount == 0 && Camera.main.transform.position - cameraPosRef == transform.position) roomsManager.cleanRoom = true;
    }
}
