using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Jobs;
using System.ComponentModel;

public class inventory : MonoBehaviour
{
    public struct playerInventory
    {
        public int lifes;
        public int scrap;
        public bool weaponBlueprint;
    }
    public static playerInventory pInv;

    int invulnerabilityFrames;
    int invulnerabilityFramesRef;

    // Start is called before the first frame update
    void Start()
    {
        pInv = new playerInventory();
        if (!PlayerPrefs.HasKey("Scrap"))
        {
            pInv.lifes = 100;
            pInv.scrap = 0;
            pInv.weaponBlueprint = false;
        }
        else loadInventory();

        invulnerabilityFrames = 30;
        invulnerabilityFramesRef = 0;
    }

    public void saveInventory()
    {
        PlayerPrefs.SetInt("Scrap", pInv.scrap);
        PlayerPrefs.SetInt("Blueprint", pInv.weaponBlueprint == true ? 1 : 0);
    }

    public void loadInventory()
    {
        pInv.lifes = 100;
        pInv.scrap = PlayerPrefs.GetInt("Scrap");
        pInv.weaponBlueprint = PlayerPrefs.GetInt("Blueprint") == 1 ? true : false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && invulnerabilityFramesRef + invulnerabilityFrames < Time.frameCount)
        {
            invulnerabilityFramesRef = Time.frameCount;
            if(collision.transform.GetChild(0).name == "Alien_LOD")
                pInv.lifes -= 20;
            else if (collision.transform.GetChild(0).name == "Mamalien_LOD")
                pInv.lifes -= 40;
            else pInv.lifes -= 10;

            if (pInv.lifes <= 0) SceneManager.LoadScene("Alien Ship");
        }
        else if(collision.gameObject.tag == "Scrap")
        {
            pInv.scrap += Random.Range(5, 30);
            Destroy(collision.gameObject);
        }
    }
}
