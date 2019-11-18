using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Armour") == 2) transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Button>().interactable = false;
        if ((PlayerPrefs.GetInt("Weapon Bought") == 1 ? true : false) == true) transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Button>().interactable = false;

        gameObject.SetActive(false);
    }

    public void upgradeArmour()
    {
        if(inventory.pInv.scrap < 300) return;

        inventory.pInv.armour /= 2;
        inventory.pInv.scrap -= 300;

        if(inventory.pInv.armour == 2) transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Button>().interactable = false;       
    }

    public void buyWeapon()
    {
        if (inventory.pInv.scrap < 300) return;

        inventory.pInv.weaponBought = true;
        inventory.pInv.scrap -= 300;

        transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Button>().interactable = false;
    }
}
