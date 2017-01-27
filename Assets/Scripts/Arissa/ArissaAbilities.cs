using UnityEngine;
using System.Collections;

public class ArissaAbilities : PlayerAbilities
{
    public GameObject ProjectilePrefab;
    public GameObject ShieldPrefab;

    Transform shieldAttach;

    
    // Use this for initialization
    void Start()
    {
        Init();
        shieldAttach = transform.FindChild("ShieldAttach");
        SetCooldowns(1f, 20f, 60f, 0);
        damage = 35f;
        ph = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer1 += Time.deltaTime;
        cooldownTimer2 += Time.deltaTime;
        cooldownTimer3 += Time.deltaTime;
        cooldownTimer4 += Time.deltaTime;

        if (!isAtacking && cooldownTimer1 >= cooldown1 && Input.GetButton("Attack_P1"))
        {
            isAtacking = true;
            canWalk = false;
            cooldownTimer1 = 0;
            animator.SetTrigger("PrimaryAttack");
        }

        if(!isAtacking && cooldownTimer2 >= cooldown2 && Input.GetButton("Shield_P1"))
        {
            isAtacking = true;
            cooldownTimer2 = 0;
            animator.SetTrigger("Block");
        }

        if(!isAtacking && cooldownTimer3 >= cooldown3 && Input.GetButton("Spell_P1"))
        {
            isAtacking = true;
            cooldownTimer3 = 0;
            animator.SetTrigger("Spell");
        }

        if (AutoIntensity.Instance.IsNight)
        {
            GetComponent<PlayerController>().speed = 5;
        }
        else
        {
            GetComponent<PlayerController>().speed = 3;
        }

        canWalk = !isAtacking;
    }

    protected override void PrimaryAttack()
    {
        base.PrimaryAttack();
        var fireball = Instantiate(ProjectilePrefab, transform.position + new Vector3(0, 1, 0), transform.rotation) as GameObject;
        fireball.GetComponent<Projectile>().damage = damage;
    }

    protected override void Block()
    {
        base.Block();
        var shield = Instantiate(ShieldPrefab);
        shield.transform.SetParent(shieldAttach, true);
        shield.transform.localPosition = Vector3.zero;
    }

    public override void HealParticle()
    {
        base.HealParticle();
        isAtacking = false;
    }

}
