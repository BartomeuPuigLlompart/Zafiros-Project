using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    static Color openedColor, closedColor;
    Material doorMaterial;
    Collider doorCollider;
    // Start is called before the first frame update
    void Start()
    {
        doorMaterial = GetComponent<Renderer>().material;
        openedColor = doorMaterial.color;
        closedColor = openedColor;
        closedColor.r = 1.0f;
        closedColor.g = 0.0f;
        doorCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (roomsManager.cleanRoom)
        {
            doorCollider.enabled = false;
            doorMaterial.color = openedColor;
        }
        else
        {
            doorCollider.enabled = true;
            doorMaterial.color = closedColor;
        }
    }
}
