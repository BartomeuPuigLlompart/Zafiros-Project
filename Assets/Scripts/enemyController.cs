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
    bool shooter;

    [SerializeField]
    int initialLifes;

    NavMeshAgent agent;

    Animator anim;

    Rigidbody rb;

    GameObject player;

    Vector3 initialModelLocalPos;
    
    // Start is called before the first frame update
    void Start()
    {
        invulnerabilityFramesRef = 0;
        invulnerabilityFrames = 10;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        player = GameObject.Find("Player");
        initialModelLocalPos = transform.GetChild(0).transform.localPosition;
        respawn();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.parent.parent.GetChild(0).GetComponent<limitsManager>().isPlayerInsideRoom() && !Controller.freeze)
        {
            if (!shooter || !checkAimed())
            {
                agent.isStopped = false;
                anim.SetBool("Move", true);
                anim.SetBool("Attack", false);
                agent.destination = player.transform.position;
            }
            else
            {
                agent.isStopped = true;
                anim.SetBool("Move", false);
                anim.SetBool("Attack", true);
            }
        }
        else agent.isStopped = true;

        transform.GetChild(0).transform.localPosition = initialModelLocalPos;
    }

    public void hit(int damage)
    {
        if (invulnerabilityFramesRef + invulnerabilityFrames < Time.frameCount)
        {
            invulnerabilityFramesRef = Time.frameCount;
            lifes -= damage;
        }
        if (lifes <= 0) kill();
    }
    void kill()
    {
        if (shooter)
        {
            GameObject scrapDrop = Instantiate(GameObject.Find("Scrap") as GameObject);
            scrapDrop.transform.position = new Vector3(transform.position.x + 3, -6.05f, transform.position.z + 3.75f);
        }
        gameObject.SetActive(false);
        transform.parent.GetComponent<enemiesManager>().checkEnemies();       
    }

    public void respawn()
    {
        lifes = initialLifes;
    }

    bool checkAimed()
    {
        transform.LookAt(player.transform);
        RaycastHit hit;
        Physics.Raycast(transform.GetChild(1).transform.position, -transform.GetChild(1).transform.position + player.transform.position, out hit, 24);
        if (hit.transform != null && hit.transform.gameObject.tag == "Player") return true;

        return false;
    }
}
