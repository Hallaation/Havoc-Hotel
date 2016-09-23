﻿using UnityEngine;
using System.Collections;

public class MovementTest : MonoBehaviour
{
    public float m_fMoveSpeed = 1.0f;
    const int _ROTATION_SPEED = 20; // Not used yet.
    const float m_f1FramePasses = 0.0170f;
   public float m_fJumpForce = 25.0f;
    public float m_fDoubleJumpMoveForce = 20f;
   public float m_fGravity = 2.0f;
    public Transform lookAt;
    bool wentThrough = false;
    bool disable = true;
    private bool m_bFlag = false;
    float timer = 0.0f;
    public Vector3 movementDirection;
    private float m_fJumpTimer;
    bool HasJumped;
    bool m_bJumpKeyReleased;
    public bool HasDoubleJumped;
    public int playerNumber;
    public float m_fGroundedTime;
    public bool m_bAllowDoubleJumpAlways;
    public float m_fMaxFallSpeed = 25.0f;
    
    //private Vector3 movementDirection = Vector3.zero;

    public GameObject platformController;
    void Start()
    {
       
    }
    //update every frame
    //void Update()
    //{
    //    CharacterController temp = GetComponent<CharacterController>();
    //    if (temp.isGrounded || disable)
    //    {
    //        this.transform.position -= new Vector3(Input.GetAxis(PlayerNumber +"_Horizontal") * Time.deltaTime * m_fm_fMoveSpeed , 0);
    //        if (temp.isGrounded)
    //        {
    //            if (Input.GetButton(PlayerNumber + "_Fire") && Input.GetAxis(PlayerNumber + "_Vertical") >= 0)
    //            {
    //                transform.position += new Vector3(0 , m_fJumpSpeed * Time.deltaTime);
    //                //movementDirection.y = m_fJumpSpeed;
    //                //Debug.Log("Jumping");
    //            }
    //        }
    //    }
    //    //timer += Time.deltaTime;
    //    
    //    movementDirection.y -= m_fGravity * Time.deltaTime;
    //    temp.Move(movementDirection * Time.deltaTime);
    //
    //    //if (wentThrough)
    //    //{
    //    //    foreach (Collider i in platformController.GetComponentsInChildren<Collider>())
    //    //    {
    //    //        Physics.IgnoreCollision(temp , i , false);
    //    //        Debug.Log(i.name);
    //    //    }
    //    //}
    //
    //    if (Input.GetButton(PlayerNumber + "_Fire") && Input.GetAxis(PlayerNumber + "_Vertical") < 0)
    //    {
    //        //JumpDown();
    //    }
    //}

    void OnTriggerEnter(Collider other)
    {
        CharacterController temp = GetComponent<CharacterController>();

        Debug.Log("TRIGGERED");
        Physics.IgnoreCollision(temp, other.GetComponentInParent<Collider>());
        Debug.Log(other.name);
        Debug.Log(GetComponent<Collider>().name);

    }
    //once exiting the trigger, the parent's collider will no longer ignore collisions
    void OnTriggerExit(Collider other)
    {
        CharacterController temp = GetComponent<CharacterController>();
        Physics.IgnoreCollision(temp, other.GetComponentInParent<Collider>(), false);
    }



    //update every frame

    void Update()
    {

        CharacterController temp = GetComponent<CharacterController>();
        if (temp.isGrounded)
        {
            // This is the Left/Right movement for X. always set Y to 0.
            HasJumped = false;
            HasDoubleJumped = false;
            m_fGroundedTime += Time.deltaTime;
            if (m_fGroundedTime >= m_f1FramePasses)
            {
                movementDirection.y = 0.01f;
            }

            UnityEngine.Debug.Log("IsGrounded"); // UnityEngine.Debug.Log just sends the developer a message when this line is reached. Displayed in the Unity Client.
            if (temp.isGrounded)
            {
                m_fJumpTimer = 0.0f;
                UnityEngine.Debug.Log("IsGrounded2");

                if (!HasJumped && Input.GetButton(playerNumber + "_Fire"))// if the players jump button is down
                {

                    movementDirection.y = m_fJumpForce;

                    UnityEngine.Debug.Log("Jumping");
                    HasJumped = true;
                }
            }
        }

        if (!temp.isGrounded)
        {
            UnityEngine.Debug.Log("HasJumped");
            m_fJumpTimer += Time.deltaTime;
            UnityEngine.Debug.Log(m_fJumpTimer.ToString());
            if (m_fJumpTimer > 0.017)
            {
                if (Input.GetButtonUp(playerNumber + "_Fire"))
                {
                    m_bJumpKeyReleased = true;
                }
                if (!HasDoubleJumped && m_bJumpKeyReleased && Input.GetButtonDown(playerNumber + "_Fire")) // if the players jump button is down
                {
                    movementDirection.y = m_fDoubleJumpMoveForce;
                    UnityEngine.Debug.Log("HasDoubleJumped");
                    HasDoubleJumped = true;
                }
            }
        }

        if (!HasDoubleJumped)
        {

        }
        m_fJumpTimer += Time.deltaTime;
        timer += Time.deltaTime;

        //if(m_fJumpTimer <= 150)
        //{

        //}

        //if (temp.isGrounded && Input.GetAxis(playerNumber + "_Horizontal") > 100)
        //{
        //    movementDirection.x = m_fMoveSpeed;
        //}
        movementDirection.y -= m_fGravity;
        if (movementDirection.y < -m_fMaxFallSpeed)
        {
            movementDirection.y = -m_fMaxFallSpeed;
        }
        movementDirection.x += ( m_fMoveSpeed * Input.GetAxis(playerNumber + "_Horizontal"));
        if (movementDirection.x > 0.0f)
        {
            movementDirection.x -= 0.5f;                // if momemntum x > 0, reduce it.
        }



        if (movementDirection.x > -0.26f && movementDirection.x < 0.26f && movementDirection.x != 0.0f)
        {
            movementDirection.x = 0.0f;                 // if momemntum within a range of .26 set it to 0;
        }
        if (movementDirection.x < 0.0f)
        {
            movementDirection.x += 0.5f;                // if momemntum x < 0, reduce it.
        }
        if (movementDirection.x > -0.26f && movementDirection.x < 0.26f && movementDirection.x != 0.0f)
        {
            movementDirection.x = 0.0f;                 // if momemntum within a range of .26 set it to 0;
        }

        if (movementDirection.x > 10)
        {
            movementDirection.x = 10;                   // Max speed settings
        }

        if (movementDirection.x < -10)
        {
            movementDirection.x = -10;                   // Max speed settings
        }
        //if (movementDirection.x < -m_fMoveSpeed)
        //{
        //    movementDirection.x = -m_fMoveSpeed;
        //}
        temp.Move(new Vector3(Time.deltaTime * movementDirection.x, movementDirection.y * Time.deltaTime));
        //temp.Move(movementDirection * Time.deltaTime);
        Debug.Log("FramesPassed");
        //if (movementDirection.y > 0.0f)
        //{
        //    UnityEngine.Debug.Log("movement dir y > 0");
        //    this.transform.position += new Vector3(0 , m_fm_fMoveSpeed * Time.deltaTime);

        //}
        //if (movementDirection.y == 0.0f && temp.isGrounded)    // Is always true. Does not work not sure why.
        //{
        //    this.transform.position += new Vector3(0 , m_fm_fMoveSpeed * Time.deltaTime);
        //    UnityEngine.Debug.Log("movement dir y == 0");

        //}
        //if (wentThrough)
        //{
        //    foreach (Collider i in platformController.GetComponentsInChildren<Collider>())
        //    {
        //        Physics.IgnoreCollision(temp , i , false);
        //        UnityEngine.Debug.Log(i.name);
        //    }
        //}
    }

    //this is where the collision is ignored. once the player hits a platform with the name "platform" in it the collisions for the player and this collider are ignored. which are re enabled later after the trigger exit
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        CharacterController temp = GetComponent<CharacterController>();

    }


    void JumpDown()
    {
        Debug.Log("Jumped down");

    }

}
