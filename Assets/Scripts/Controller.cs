using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{

    Rigidbody rb;

    Vector3 mousePos;

   [SerializeField]
    float speed;

    int overheat;

    bool overheated;

    [SerializeField]
    int overheatLimit;

    [SerializeField]
    float zValue;

    int shootFrames;

    [SerializeField]
    float smoothRotation;

    [SerializeField]
    GameObject leftArm;

    [SerializeField]
    GameObject rightArm;

    Vector3[] leftHandPos;
    Vector3[] leftHandRot;
    Vector3[] rightHandPos;
    Vector3[] rightHandRot;

    Vector3 startLeftHandPos;
    Vector3 startLeftHandRot;
    Vector3 startRightHandPos;
    Vector3 startRightHandRot;

    [SerializeField]
    GameObject canvas;

    static public bool freeze;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        shootFrames = 0;
        overheat = 0;
        overheated = false;
        freeze = false;

        startLeftHandPos = leftArm.transform.localPosition;
        startLeftHandRot = leftArm.transform.localEulerAngles;
        startRightHandPos = rightArm.transform.localPosition;
        startRightHandRot = rightArm.transform.localEulerAngles;

        leftHandPos = new Vector3[2];
        leftHandRot = new Vector3[2];
        rightHandPos = new Vector3[2];
        rightHandRot = new Vector3[2];


        leftHandPos[0] = transform.GetChild(0).GetChild(2).transform.localPosition;
        leftHandRot[0] = transform.GetChild(0).GetChild(2).transform.localEulerAngles;
        rightHandPos[0] = transform.GetChild(0).GetChild(3).transform.localPosition;
        rightHandRot[0] = transform.GetChild(0).GetChild(3).transform.localEulerAngles;
        leftHandPos[1] = transform.GetChild(0).GetChild(4).transform.localPosition;
        leftHandRot[1] = transform.GetChild(0).GetChild(4).transform.localEulerAngles;
        rightHandPos[1] = transform.GetChild(0).GetChild(5).transform.localPosition;
        rightHandRot[1] = transform.GetChild(0).GetChild(5).transform.localEulerAngles;     

        for (int i = 5; i > 1; i--)
        {
            Destroy(transform.GetChild(0).GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!freeze)
        {
            updateSpeed();
            updateOrientation();
            checkShootMode();
            checkShoot();
        }
        else rb.constraints = RigidbodyConstraints.FreezeAll;
        updateUI();
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

        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Vector3.Distance(Camera.main.transform.position, hit.point) - Mathf.Abs(Camera.main.transform.position.z - limitsManager.cameraPosRef.z - 24)));
        mousePos = new Vector3(mousePos.x, transform.position.y, mousePos.z);
        transform.GetChild(1).transform.position = mousePos;

        Quaternion lastRotation = transform.rotation, nextRotation = transform.rotation;
        if (!Input.GetKey(KeyCode.Space)){
            transform.LookAt(new Vector3(mousePos.x, transform.position.y, mousePos.z));
            nextRotation = transform.rotation;
        }

        transform.rotation = lastRotation;
        transform.rotation = Quaternion.Slerp(lastRotation, nextRotation, smoothRotation * Time.deltaTime);
    }

    void checkShootMode()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            if (rb.velocity.magnitude > 1) transform.LookAt(transform.position + rb.velocity);

            float lerpRef = ((Input.mousePosition.y - (Screen.height / 7)) / (Screen.height - (Screen.height / 3)));

            leftArm.transform.localPosition = Vector3.Lerp(leftHandPos[1], leftHandPos[0], lerpRef);
            leftArm.transform.localEulerAngles = Vector3.Lerp(leftHandRot[1], leftHandRot[0], lerpRef);
            rightArm.transform.localPosition = Vector3.Lerp(rightHandPos[1], rightHandPos[0], lerpRef);
            rightArm.transform.localEulerAngles = Vector3.Lerp(rightHandRot[1], rightHandRot[0], lerpRef);

        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY;

            leftArm.transform.localPosition = startLeftHandPos;
            leftArm.transform.localEulerAngles = startLeftHandRot;
            rightArm.transform.localPosition = startRightHandPos;
            rightArm.transform.localEulerAngles = startRightHandRot;
        }
    }

    void checkShoot()
    {
        if (!overheated && Input.GetAxis("Fire") > 0 && overheat < overheatLimit)
        {
            if (shootFrames == 0 || shootFrames == 30)
            {
                GameObject shot = Instantiate(transform.GetChild(0).GetChild(0).GetChild(1).gameObject as GameObject);
                shot.transform.position = transform.GetChild(0).GetChild(0).GetChild(1).transform.position;
                shot.transform.rotation = transform.GetChild(0).GetChild(0).GetChild(1).transform.rotation;
                shot.GetComponent<Collider>().enabled = true;
                shot.transform.LookAt(transform.position + transform.GetChild(0).GetChild(0).GetChild(0).transform.forward * 10);
                shot.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                shot.GetComponent<MeshRenderer>().enabled = true;
                shot.AddComponent<Rigidbody>();
                shot.GetComponent<Rigidbody>().useGravity = false;
                shot.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                shot.GetComponent<Rigidbody>().AddForce(shot.transform.forward * 1000.0f);
                shot.AddComponent<shot>();
            }
            else if (shootFrames == 15 || shootFrames == 45)
            {
                GameObject shot = Instantiate(transform.GetChild(0).GetChild(1).GetChild(1).gameObject as GameObject);
                shot.transform.position = transform.GetChild(0).GetChild(1).GetChild(1).transform.position;
                shot.transform.rotation = transform.GetChild(0).GetChild(1).GetChild(1).transform.rotation;
                shot.GetComponent<Collider>().enabled = true;
                shot.transform.LookAt(transform.position + transform.GetChild(0).GetChild(1).GetChild(0).transform.forward * 10);
                shot.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                shot.GetComponent<MeshRenderer>().enabled = true;
                shot.AddComponent<Rigidbody>();
                shot.GetComponent<Rigidbody>().useGravity = false;
                shot.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                shot.GetComponent<Rigidbody>().AddForce(shot.transform.forward * 1000.0f);
                shot.AddComponent<shot>();
            }
            shootFrames++;
            overheat += 2;
            if (shootFrames == 60) shootFrames = 0;
        }

        else
        {
            shootFrames = 0;
            if (overheat == overheatLimit) overheated = true;
            if (overheat > 0)
            {
                if (!overheated) overheat -= 2;
                else
                {
                    overheat--;
                    if (overheat == 0) overheated = false;
                }                
            }
        }
    }

    void updateUI()
    {
        float range = (float)overheat / (float)overheatLimit;
        canvas.transform.GetChild(0).GetComponent<Slider>().value = range;

        Color sliderColor, backgroundColor;
        ColorBlock colorBlockSlider = canvas.transform.GetChild(0).GetComponent<Slider>().colors;
        sliderColor = colorBlockSlider.disabledColor;

        backgroundColor = canvas.transform.GetChild(0).GetChild(0).GetComponent<Image>().color;

        if (!overheated)
        {            
            sliderColor.r = 1;
            sliderColor.g = 194.0f / 255.0f;
            sliderColor.b = 0;
            colorBlockSlider.disabledColor = sliderColor;
            canvas.transform.GetChild(0).GetComponent<Slider>().colors = colorBlockSlider;

            backgroundColor.g = 155.0f / 255.0f - (range * 155) / 255.0f;
            canvas.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = backgroundColor;
            canvas.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = backgroundColor;
        }
        else
        {
            backgroundColor.r = sliderColor.r = 1;
            backgroundColor.g = backgroundColor.b = sliderColor.g = sliderColor.b = 0;
            colorBlockSlider.disabledColor = sliderColor;
            canvas.transform.GetChild(0).GetComponent<Slider>().colors = colorBlockSlider;
            canvas.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = backgroundColor;
            canvas.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = backgroundColor;

        }
    }
}
