using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour
{

    public PaladinAbilities pa;
    public PlayerHealth ph;
    BoxCollider bcollider;

    // Use this for initialization
    void Start()
    {
        bcollider = GetComponent<BoxCollider>();
        bcollider.enabled = false;
    }

    void Update()
    {
        bcollider.enabled = pa.IsSlashing;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyMovement>().TakeDamage(pa.Damage);
            if (!AutoIntensity.Instance.IsNight)
                ph.currentHealth = Mathf.Clamp(ph.currentHealth + ph.totalHealth * 0.005f, 0, ph.totalHealth);
        }

        //Destroy(other.gameObject);
    }
}
