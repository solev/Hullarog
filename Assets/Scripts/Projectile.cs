using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float speed = 10;
    public float damage = 50;

    public GameObject ExplosionPrefab;

    Vector3 direction;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 5f);        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    void OnTriggerEnter(Collider other)
    {
        var tag = other.gameObject.tag;

        if (tag == "Enemy")
        {
            if(other is CapsuleCollider)
                other.gameObject.GetComponent<EnemyMovement> ().TakeDamage (damage);
        }
        else if(tag == "Player1" || tag == "Player2")
        {
            
        }
        else
        {
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }
}
