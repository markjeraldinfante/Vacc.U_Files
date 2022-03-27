﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed=10f, jumpPower=10f;
    public SpriteRenderer sprite;
    public GameObject Player0;
    public Vector3 localScale;
    public Transform InjectP;
    public float InjectRange=0.5f;
    public LayerMask peopleLayers;

    Rigidbody2D body;
    bool isGrounded;
    float horizontal;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("isJumping");
            body.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
            //body.AddForce(transform.right*horizontal*3f,ForceMode2D.Impulse);

        }
        if (isGrounded && Input.GetKey(KeyCode.G))
        {
            Vaccine();
            //Collider2D[] enemytoDamage = Physics2D.OverlapCircleAll(attackp.position,attackr,Wvaccine);            }
            //timeA = startA;
        }

        //Player0.transform.localScale = new Vector3(-0.10f, .10f, 0.11f);


    }

    void FixedUpdate()
    {
        if (isGrounded)
        {
            body.AddForce(transform.right* horizontal * moveSpeed);
            if (horizontal == 0)
            {
                //anim.SetBool("isJumping", false);
                anim.SetBool("isWalking", false);
                
                //Player0.transform.localScale = new Vector3(-0.10f, .10f, 0.11f);
            }
            else
            {
               // anim.SetBool("isJumping", false);
                anim.SetBool("isWalking", true);
                
                // Player0.transform.localScale = new Vector3(-0.10f, .10f, 0.11f);

            }
        }
        sprite.flipX = horizontal > 0 ? false : (horizontal < 0 ? true : sprite.flipX);
    }

    void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("Planet"))
        {
            body.drag = 1.2f;

            float distance = Mathf.Abs(obj.GetComponent<GravityPoint>().planetRadius - Vector2.Distance(transform.position, obj.transform.position));
            if (distance < 1f)
            {
                isGrounded = distance < 0.5f;
            }
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Planet"))
        {
            body.drag = 0.5f;
        }
    }
    void Vaccine() 
    {


        anim.SetTrigger("isVaccinating");
        Collider2D[] injectPeople=Physics2D.OverlapCircleAll(InjectP.position, InjectRange,peopleLayers);

    }
     void OnDrawGizmosSelected()
    { 
        if (InjectP == null)
            return;
        Gizmos.DrawWireSphere(InjectP.position,InjectRange);
    }

}
