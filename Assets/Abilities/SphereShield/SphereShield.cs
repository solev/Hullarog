using UnityEngine;
using System.Collections;

public class SphereShield : MonoBehaviour
{
    public float Duration;
    public float Absorb;

    PlayerHealth ph;

    // Use this for initialization
    void Start()
    {       
        ph = transform.parent.parent.gameObject.GetComponent<PlayerHealth>();
        ph.SetAbsorb(Absorb);
        StartCoroutine(DisableShield());
    }

    // Update is called once per frame
    void Update()
    {
        if(ph.absorb  <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    IEnumerator DisableShield()
    {
        yield return new WaitForSeconds(Duration);
        Destroy(gameObject);
        ph.SetAbsorb(0);
    }
}
