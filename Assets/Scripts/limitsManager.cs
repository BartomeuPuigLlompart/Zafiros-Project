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
        Debug.Log(Vector3.Lerp(lastCameraPos, transform.position + cameraPosRef, (float)lerpFrames / 100.0f));
        Camera.main.transform.position = Vector3.Lerp(lastCameraPos, transform.position + cameraPosRef, (float)lerpFrames / 100.0f);        
        if(lerpFrames == 100)
        {
            lerpFrames = 0;
            lerping = false;
            Controller.freeze = false;
        }
    }
}
