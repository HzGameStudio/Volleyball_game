using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float normalSpeed = 15f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float sprintTime;
    public float sprintSpeed;

    private float speed;

    public float currentSprintTime = 0f;

    private bool isSprintRefilling = false;

    public float horizontalMovementMultiplier;

    public GameObject handsModel;
    public GameObject handsBottomModel;

    Vector3 velocity;
    bool isGrounded;


    private void Awake()
    {
        float speed = normalSpeed;
        currentSprintTime = sprintTime;
    }
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x * horizontalMovementMultiplier + transform.forward * z;

        if (move != Vector3.zero)
        {
            if(ChangeHandsPosition.positionFlag)
            {
                handsBottomModel.GetComponent<TriggerR>().TriggerKick();
            }
            else
            {
                handsModel.GetComponent<TriggerR>().TriggerKick();
            }
        }
        else
        {
            if (ChangeHandsPosition.positionFlag)
            {
                handsBottomModel.GetComponent<TriggerR>().Stop();
            }
            else
            {
                handsModel.GetComponent<TriggerR>().Stop();
            }
        }

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);



        if (Input.GetKey(KeyCode.LeftShift) && currentSprintTime > 0 && !isSprintRefilling)
        {
            if(currentSprintTime >0f)
            {
                speed = sprintSpeed;
                currentSprintTime -= Time.deltaTime;
                if(currentSprintTime< 0f)
                {
                    currentSprintTime = 0f;
                    isSprintRefilling = true;
                }
            }
            

        }
        else
        {
            speed = normalSpeed;
            currentSprintTime += Time.deltaTime / 2f;
            if (currentSprintTime > sprintTime)
            {
                currentSprintTime = sprintTime;
            }

            if(currentSprintTime > sprintTime * 0.2f)
            {
                isSprintRefilling = false;
            }
        }
    }

    

    
}
