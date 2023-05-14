using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Cris_py : MonoBehaviour
{
    // @ Don't Use Public Variables @
    // SerializeField is used to make Unity Editor to show the private variable in the Inspector.
    [SerializeField]
    private float movePower = 2.0f;
    [SerializeField]
    private float jumpPower = 1.5f;
    [SerializeField]
    private int health = 5;
    [SerializeField]
    private bool isAlive = true;


    private bool isJumping = false;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;


    // For Game Restart
    // ex) Call Func -> sceneLoader.LoadScene(GameSceneCrisPy.GameMenu);
    private Game_Scene_Loader sceneLoader;
    

    private void Start() 
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        sceneLoader = GameObject.FindObjectOfType<Game_Scene_Loader>();
        if (sceneLoader == null)
        {
            Debug.LogError("Game_Scene_Loader component not found in the scene.");
        }
    }

    private void Update()
    {
        if(isAlive == true)
        {
            PlayerMovingGFX();
        }
    }

    private void FixedUpdate()
    {
        if (isAlive == true)
        {
            PlayerMoving();
        }
    }

    private void PlayerMoving()
    {
        Vector3 moveVelocity;

        switch (Input.GetAxisRaw("Horizontal"))
        {
            case -1.0f: // LEFT Move
                moveVelocity = Vector3.left;
                break;

            case 1.0f: // RIGHT Move
                moveVelocity = Vector3.right;
                break;

            default: // 0 or ETC... is STOP
                moveVelocity = Vector3.zero;
                break;
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }


    // 부드러운 애니메이션을 제공하기 위해 Update(프레임 마다)에 넣습니다.
    private void PlayerMovingGFX()
    {
        switch (Input.GetAxisRaw("Horizontal"))
        {
            case -1.0f: // LEFT Move
                spriteRenderer.flipX = true;

                // animator.SetInteger("Direction", -1);
                // animator.SetBool("isMoving", true);
                break;

            case 1.0f: // RIGHT Move
                spriteRenderer.flipX = false;

                // animator.SetInteger("Direction", 1);
                // animator.SetBool("isMoving", true);
                break;

            default: // 0 or ETC... is STOP
                // animator.SetBool("isMoving", false);
                break;
        }
    }
}
