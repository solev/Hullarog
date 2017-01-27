using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{    
    public float totalHealth = 100;
    public float currentHealth = 100;

    public bool alive = true;

    [HideInInspector]
    public float absorb;
    Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        //StartCoroutine(DealDemageOvertime(15f));  
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {                
        if(absorb > 0)
        {
            absorb -= damage;
            if (absorb < 0)
                currentHealth += absorb;
        }
        else
        {
            currentHealth -= damage;
        }

        
        if (currentHealth <= 0 && alive)
        {
            StartCoroutine(Death());
        }
    }

    public void SetAbsorb(float abs)
    {
        absorb = abs;
    }

    IEnumerator Death()
    {
        alive = false;
        gameObject.GetComponent<PlayerController>().enabled = false;
        gameObject.GetComponent<PlayerAbilities>().enabled = false;
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(1f);
    }

    IEnumerator DealDemageOvertime(float dmg)
    {
        while(currentHealth > 0)
        {
            TakeDamage(dmg);
            yield return new WaitForSeconds(1f);
        }
    }
}
