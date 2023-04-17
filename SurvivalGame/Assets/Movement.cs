using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    CharacterController controller;

    [SerializeField] float 
        ogSpeed;
    [SerializeField] float speed = 10f;
    [SerializeField] float runSpeed = 15f;

    [SerializeField][Range(-1, 20)] float stamina = 50f;
    [SerializeField] float maxStamina = 15f;

    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 15f;

    [SerializeField] float groundDistance = .4f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;

    bool isGrounded;
    Vector3 velocity;

    [SerializeField] Animator anim;

    [SerializeField] Slider staminaSlider;

    // Start is called before the first frame update
    void Start()
    {
        ogSpeed = speed;
        controller = GetComponent<CharacterController>();
        staminaSlider.maxValue = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        stamina = Mathf.Min(stamina, maxStamina, 100);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y <= -2f)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(speed * Time.deltaTime * move);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        } 


        if(Input.GetKey(KeyCode.LeftShift) && stamina > 1)
        {
            speed = runSpeed;
            stamina -= Time.deltaTime * 4f;
            anim.SetBool("Running", true);
        } else
        {
            speed = ogSpeed;
            stamina += 2f * Time.deltaTime;
            anim.SetBool("Running", false);
        }

        staminaSlider.value = stamina;

        #region Animation

        if(!isGrounded)
        {
            anim.SetBool("Jumping", true);
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            return;
        }
        else
        {
            anim.SetBool("Jumping", false);
        } 

        if(Input.GetKey(KeyCode.W))
        {
            anim.SetBool("Walking", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("Walking", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Walking", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("Walking", true);
        } else
        {
            anim.SetBool("Walking", false);
        }

        #endregion
    }
}
