using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent nav;
    Animator anim;

    Transform player1;
    Transform player2;

    public bool player1InRange = false;
    public bool player2InRange = false;

    bool moving = false;
    bool attacking = false;

    public float health = 100f;
    public float damage = 10f;

    bool takingDamage = false;
    bool alive = true;

    bool isSinking = false;

    public float sinkSpeed = 2.5f;

    GameObject gm;

    public GameObject bloodSplat;
    float initialSpeed;


    void Awake()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("GameManager");
        player1 = GameObject.FindGameObjectWithTag("Player1").transform;
        player2 = GameObject.FindGameObjectWithTag("Player2").transform;
    }

    void Start()
    {
        initialSpeed = nav.speed;
    }


    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }

        if (alive)
        {
            if (!takingDamage)
            {
                Transform closestPlayer = GetTarget();

                if (closestPlayer == null)
                {
                    nav.enabled = false;
                    return;
                }
                    

                if (nav.enabled)
                {
                    nav.SetDestination(closestPlayer.position);
                }

                if (((player1InRange) || (player2InRange)) && !attacking)
                {
                    attacking = true;
                    anim.SetTrigger("Attack");
                    //nav.enabled = false;
                    nav.speed = 0f;
                    moving = false;
                }
                else if (!moving && !attacking)
                {
                    moving = true;
                    anim.SetTrigger("MoveTo");
                    //nav.enabled = true;
                    nav.speed = initialSpeed;
                }
            }
        }
    }

    Transform GetTarget()
    {
        if(!player1.GetComponent<PlayerHealth>().alive && !player2.GetComponent<PlayerHealth>().alive)
        {
            anim.SetBool("Idle", true);
            return null;
        }

        if (Vector3.Distance(player1.position, transform.position) < Vector3.Distance(player2.position, transform.position))
        {
            if (player1.GetComponent<PlayerHealth>().alive == false)
            {
                return player2;
            }
            else
            {
                return player1;
            }
        }
        else if (player2.GetComponent<PlayerHealth>().alive == false)
        {
            return player1;
        }
        else
        {
            return player2;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            player1InRange = true;
        }
        if (other.tag == "Player2")
        {
            player2InRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player1")
        {
            player1InRange = false;
        }
        if (other.tag == "Player2")
        {
            player2InRange = false;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        GameObject blood = Instantiate(bloodSplat, this.transform.position + new Vector3(0, 1, 0), this.transform.rotation) as GameObject;
        Destroy(blood, 0.5f);


        if (health <= 0)
        {
            StartCoroutine("Death");
        }
        else
        {
            anim.SetTrigger("TakeDamage");
            takingDamage = true;
            nav.enabled = false;
        }

    }

    void DamageTaken()
    {
        takingDamage = false;
        nav.enabled = true;
        attacking = false;

        if (health > 0)
            anim.SetTrigger("MoveTo");
    }

    void AttackEnds()
    {
        attacking = false;
    }

    void DealDamage()
    {
        if (player1InRange)
        {
            player1.GetComponent<PlayerHealth>().TakeDamage(damage);

            //Debug.Log ("Dealt damage to player 1");
            if (player1.GetComponent<PlayerHealth>().currentHealth <= 0)
            {
                player1InRange = false;
            }
        }

        if (player2InRange)
        {
            player2.GetComponent<PlayerHealth>().TakeDamage((int)damage);
            //Debug.Log ("Dealt damage to player 2");
            if (player2.GetComponent<PlayerHealth>().currentHealth <= 0)
            {
                player2InRange = false;
            }
        }
    }

    public void IncreaseStats()
    {
        health += health * 0.2f;
        damage += damage * 0.2f;
    }

    IEnumerator Death()
    {
        anim.SetTrigger("Death");
        alive = false;
        Destroy(GetComponent<Rigidbody>());
        gm.GetComponent<GameManager>().enemyCount--;
        gm.GetComponent<GameManager>().AddBlood(5);
        nav.enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(4f);
        isSinking = true;
        Destroy(gameObject, 3);
    }

}
