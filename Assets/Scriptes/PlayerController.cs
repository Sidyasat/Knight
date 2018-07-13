using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    float speed = 2f;
    float gravity = 20f;
    Vector3 direction;
    CharacterController controller;
    Animator animator;
    public Image UIHP;
    public float HP = 1f;
    public Text HPBottleText;
    public int HPBottle = 0;
    float horizontal;
    float vertical;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }
    public void AnimationTriggerDeath(string value)
    {
        animator.SetTrigger(value);
    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (controller.isGrounded)
        {
            direction = new Vector3(x, 0f, z);
            direction = transform.TransformDirection(direction) * speed;
        }

        direction.y -= gravity * Time.deltaTime;
        controller.Move(direction * Time.deltaTime);
    }

    public void MoveAnimation()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (vertical == 0)
        {
            animator.SetBool("Run", false);
            animator.SetBool("RunBack", false);
        }
        if (vertical >= 0.1f)
        {
            animator.SetBool("Run", true);
        }
        if (vertical <= -0.1f)
        {
            animator.SetBool("RunBack", true);
        }

        if (horizontal == 0)
        {
            animator.SetBool("RunRight", false);
            animator.SetBool("RunLeft", false);
        }
        if (horizontal >= 0.1f)
        {
            animator.SetBool("RunRight", true);
        }
        if (horizontal <= -0.1f)
        {
            animator.SetBool("RunLeft", true);
        }
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Attack");
        }
    }

    public void RestoringLife()
    {
        if (Input.GetKeyDown(KeyCode.E) && HPBottle > 0 && HP < 1f)
        {
            HP += 0.15f;
            HPBottle--;
            animator.SetTrigger("Drinking");
        }

        if (HP > 1f)
        {
            HP = 1f;
        }
    }

    public void AddElixir()
    {
        UIHP.fillAmount = HP;
        HPBottleText.text = "" + HPBottle;
    }

    void Update()
    {
        if (HP <= 0)
        {
            AnimationTriggerDeath("Dead");
        }
        else
        {
            Attack();

            RestoringLife();

            Move();

            MoveAnimation();

            AddElixir();

        }
    }

    

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "HPBottle" & Input.GetKeyDown(KeyCode.F))
        {
            HPBottle++;
        }
        if (other.tag == "Damage")
        {
            HP -= Time.deltaTime / 10f;
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Sword")
        {
            HP -= 0.05f;
        }
    }
}
