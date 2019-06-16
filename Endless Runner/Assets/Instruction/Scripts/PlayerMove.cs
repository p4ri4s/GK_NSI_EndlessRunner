using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller;
    private Animator anim;
    private Vector3 moveVector;
    private float verticalVelocity;
    private float gravity = 0.35f;
    private float jumpForce = 0.1f;
    public GameObject jumpSideEffect;
    private bool canJumpSide = true;
    private bool canJumpUp = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        moveVector = Vector3.zero;

        //Jump controller
        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow)) && canJumpUp)
            {
                canJumpUp = false;
                verticalVelocity = jumpForce;
                anim.SetTrigger("isJumping");
                StartCoroutine(timeBetweenJumpUp());
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        //  Left/Right move
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown("a")) && transform.position.x >= (-1.88f) && controller.isGrounded && canJumpSide)
        {
            jumpSideEffect.transform.position = transform.position;
            jumpSideEffect.GetComponent<ParticleSystem>().Play();
            moveVector.x = moveVector.x - 1.5f;
            canJumpSide = false;
            StartCoroutine(timeBetweenJumpSide());
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown("d")) && transform.position.x <= (-1.88f) && controller.isGrounded && canJumpSide)
        {
            jumpSideEffect.transform.position = transform.position;
            jumpSideEffect.GetComponent<ParticleSystem>().Play();
            moveVector.x = moveVector.x + 1.5f;
            canJumpSide = false;
            StartCoroutine(timeBetweenJumpSide());
        }


        // Move Forward
        moveVector.z = 0;

        //  Up/Down move
        moveVector.y = verticalVelocity;

        //Move Player
        controller.Move(moveVector);
    }

    IEnumerator timeBetweenJumpSide()
    {
        yield return new WaitForSeconds(0.1f);
        canJumpSide = true;
    }
    IEnumerator timeBetweenJumpUp()
    {
        yield return new WaitForSeconds(1f);
        canJumpUp = true;
    }
}
