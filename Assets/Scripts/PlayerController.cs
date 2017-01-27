using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed;

    [Header("Axis Mapping Names")]
    public string Horizontal;
    public string Vertical;

    PlayerAbilities pAttack;

    Rigidbody rbody;
    Animator animator;

    Vector3 movement;
 
    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        pAttack = GetComponent<PlayerAbilities>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw(Horizontal);
        float v = Input.GetAxisRaw(Vertical);

        if(pAttack.CanWalk)
        {
            Move(h, v);
            Animate(h, v);
        }          
    }

    private void Animate(float h, float v)
    {
        bool isMoving = h != 0 || v != 0;   
        animator.SetBool("isMoving", isMoving);
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0, v);
        movement = movement.normalized * speed * Time.deltaTime;

        var facingRotation = new Vector3(h, 0, v);
        if (facingRotation != Vector3.zero)
        {
            //transform.forward = Vector3.Lerp(transform.forward, facingRotation, 10 * Time.deltaTime);
            transform.forward = facingRotation;
        }

        rbody.MovePosition(transform.position + movement);
    }


}
