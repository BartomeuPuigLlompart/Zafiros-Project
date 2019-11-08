using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    int lifes;

    int invulnerabilityFrames;
    int invulnerabilityFramesRef;

    [SerializeField]
    int initialLifes;
    // Start is called before the first frame update
    void Start()
    {
        invulnerabilityFramesRef = 0;
        invulnerabilityFrames = 10;
        respawn();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void hit()
    {
        if (invulnerabilityFramesRef + invulnerabilityFrames < Time.frameCount)
        {
            invulnerabilityFramesRef = Time.frameCount;
            lifes--;
        }
        if (lifes == 0) kill();
    }
    void kill()
    {
        gameObject.SetActive(false);
        transform.parent.GetComponent<enemiesManager>().checkEnemies();
    }

    public void respawn()
    {
        lifes = initialLifes;
    }
}
