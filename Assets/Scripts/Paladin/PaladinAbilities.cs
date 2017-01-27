using UnityEngine;
using System.Collections;

public class PaladinAbilities : PlayerAbilities
{

    public bool IsSlashing { get { return slashing; } }



    [Header("Ability AFX")]
    public AudioClip slash;
    public AudioClip shield;

    
    AudioSource audio;
    bool isBlocking, slashing;

    // Use this for initialization
    void Start()
    {
        Init();
        SetCooldowns(1f, 20f, 60f, 0);
        audio = GetComponent<AudioSource>();
        ph = GetComponent<PlayerHealth>();

        totalStamina = stamina = 10f;
        damage = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer1 += Time.deltaTime;
        cooldownTimer2 += Time.deltaTime;
        cooldownTimer3 += Time.deltaTime;
        cooldownTimer4 += Time.deltaTime;

        if (Input.GetButton("Shield_P2") && !isAtacking && stamina > Time.deltaTime)
        {
            if(!isBlocking)
            {
                audio.clip = shield;
                audio.Play();
            }

            isBlocking = true;
            stamina -= Time.deltaTime;
            ph.SetAbsorb(float.MaxValue);
        }
        else
        {
            ph.SetAbsorb(0);
            isBlocking = false;
            stamina = Mathf.Clamp(stamina + (Time.deltaTime / 3f), 0, totalStamina);
        }

        animator.SetBool("isBlocking", isBlocking);

        if (!isAtacking && !isBlocking && cooldownTimer1 >= cooldown1 && Input.GetButton("Attack_P2"))
        {
            isAtacking = true;
            cooldownTimer1 = 0;
            animator.SetTrigger("PrimaryAttack");
            audio.clip = slash;
            gameObject.GetComponent<AudioSource>().Play();
        }

        if(!isAtacking && !isBlocking && cooldownTimer3 > cooldown3 && Input.GetButton("Spell_P2"))
        {
            isAtacking = true;
            cooldownTimer3 = 0;
            animator.SetTrigger("Spell");
            
        }
        

        canWalk = !isAtacking && !isBlocking;
    }


    protected override void PrimaryAttack()
    {
        base.PrimaryAttack();        
    }

    public override void HealParticle()
    {
        base.HealParticle();
        isAtacking = false;
    }

    void StartSlash()
    {
        slashing = true;
        
    }

    void EndSlash()
    {
        slashing = false;
    }
}
