using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemiesManager : MonoBehaviour
{
    [SerializeField]
    float respawnTime;
    float cleanRef;
    bool emptyEnemies;

    GameObject[] enemies;
    Vector3[] respawnPos;

    // Start is called before the first frame update
    void Start()
    {
        cleanRef = Time.realtimeSinceStartup;
        emptyEnemies = false;

        enemies = new GameObject[transform.childCount];
        respawnPos = new Vector3[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            enemies[i] = transform.GetChild(i).gameObject;
            respawnPos[i] = transform.GetChild(i).transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {        
        if(emptyEnemies && cleanRef + respawnTime < Time.realtimeSinceStartup && !transform.parent.GetChild(0).GetComponent<limitsManager>().isPlayerInsideRoom())
        {
            emptyEnemies = false;

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].SetActive(true);
                enemies[i].transform.position = respawnPos[i];
                enemies[i].GetComponent<enemyController>().respawn();
            }
        }
    }

    void rommCleaned()
    {
        emptyEnemies = true;
        roomsManager.cleanRoom = true;
        if(emptyEnemies && cleanRef + respawnTime < Time.realtimeSinceStartup) cleanRef = Time.realtimeSinceStartup;
    }

    public void checkEnemies(bool cleanCheck = true)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].activeSelf)
            {
                roomsManager.cleanRoom = false;
                return;
            }
        }

        if(cleanCheck)rommCleaned();
    }
}
