using UnityEngine;
using System.Collections;

public class PlayerAbilities : MonoBehaviour
{   
    public bool CanWalk { get { return canWalk; } }
    public float Damage { get { return damage; } }

    public GameObject HealParticlePrefab;
    public GameObject HealFrom;
    GameObject HealObj;

    protected PlayerHealth ph;
    //Cooldowns
    protected float cooldown1 = 1f;
    protected float cooldown2 = 10f;
    protected float cooldown3 = 2f;
    protected float cooldown4 = 5f;
    protected float cooldownTimer1, cooldownTimer2, cooldownTimer3, cooldownTimer4;

    protected float stamina, totalStamina, damage;
    
    protected Animator animator;
    protected bool isAtacking, canWalk;
    

    // Use this for initialization
    void Start()
    {
        
    }

    protected void Init()
    {
        animator = GetComponent<Animator>();        
        cooldownTimer1 = cooldownTimer2 = cooldownTimer3 = cooldownTimer4 = 500f;
    }

    protected void SetCooldowns(float c1, float c2, float c3, float c4)
    {
        cooldown1 = c1;
        cooldown2 = c2;
        cooldown3 = c3;
        cooldown4 = c4;
    }
    
    // Update is called once per frame
    void Update()
    {        
        
    }

    protected virtual void PrimaryAttack()
    {

    }

    protected virtual void Block()
    {
        
    }

    protected void CastEnded()
    {
        isAtacking = false;
    }

    protected bool GetPrimaryInput()
    {
        return Input.GetButton("Fire1");
    }

    protected bool GetBlockInput()
    {
        return Input.GetButton("Fire2");
    }

    public float getCooldown(Ability ability)
    {
        switch (ability)
        {
            case Ability.First:
                return cooldownTimer1;
            case Ability.Second:
                return cooldownTimer2;
            case Ability.Third:
                return cooldownTimer3;
            case Ability.Fourth:
                return cooldownTimer4;
            default:
                return 0;
        }
    }

    public float getCooldownMax(Ability ability)
    {
        switch (ability)
        {
            case Ability.First:
                return cooldown1;
            case Ability.Second:
                return cooldown2;
            case Ability.Third:
                return cooldown3;
            case Ability.Fourth:
                return cooldown4;
            default:
                return 0;
        }
    }
    
    public float getStamina()
    {
        return stamina;
    }

    public float getTotalStamina()
    {
        return totalStamina;
    }

    public virtual void HealParticle()
    {
        HealObj = Instantiate(HealParticlePrefab, HealFrom.transform.position, Quaternion.identity) as GameObject;
        ph.currentHealth = Mathf.Clamp(ph.currentHealth + ph.totalHealth * 0.33f, 0, ph.totalHealth);
    }

    public void DestroyHeal()
    {
        if(HealObj != null)
            Destroy(HealObj);
    }

}

public enum Ability
{
    First,
    Second,
    Third,
    Fourth
}