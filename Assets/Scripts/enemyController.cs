using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{
    int lifes;

    int invulnerabilityFrames;
    int invulnerabilityFramesRef;

    [SerializeField]
    int initialLifes;

    NavMeshAgent agent;

    Animator anim;

    Rigidbody rb;

    GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        invulnerabilityFramesRef = 0;
        invulnerabilityFrames = 10;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        player = GameObject.Find("Player");
        respawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.parent.GetChild(0).GetComponent<limitsManager>().isPlayerInsideRoom() && !Controller.freeze)
        {
            agent.isStopped = false;
            anim.SetBool("Move", true);
            agent.destination = player.transform.position;
        }
        else agent.isStopped = true;
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
