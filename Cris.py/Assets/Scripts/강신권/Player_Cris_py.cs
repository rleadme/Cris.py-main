using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Cris_py : MonoBehaviour
{
    // @ Don't Use Public Variables @
    // SerializeField is used to make Unity Editor to show the private variable in the Inspector.
    [SerializeField]
    private float movePower = 5.0f;
    [SerializeField]
    private float jumpPower = 2.5f;
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
            RayShoot();
            PlayerMovingGFX();

            if(isJumping == false)
            {
                PlayerJumping();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isAlive == true)
        {
            PlayerMoving();

            if (isJumping == true)
            {
                CheckJumpingGround();
            }
            else // isJumping == false
            {
                PlayerJumpingGFX();
            }
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
    // �ε巯�� GFX �ִϸ��̼��� �����ϱ� ���� Update(������ ����)�� �ֽ��ϴ�.
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
    private void PlayerJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            Vector2 jumpVelocity = new Vector2(0, jumpPower);

            rigidBody.velocity = Vector2.zero;
            rigidBody.AddForce(jumpVelocity, ForceMode2D.Impulse);

            isJumping = true;
        }
    }
    private void PlayerJumpingGFX()
    {
        if(Input.GetKeyDown(KeyCode.Space) == true)
        {
            // animator.SetBool("isJumping", true);
        }
    }

    // �������ִ� �ٴ� �浹, ���� ����� ���� FixedUpdate(������ ����)�� �ֽ��ϴ�.
    private void CheckJumpingGround()
    {
        // Player and Platform distance, you have to check it yourself
        const float distancePlatformAndPlayer = 1.01f;


        Debug.DrawRay(rigidBody.position, new Vector3(0.0f, -5.0f, 0.0f), new Color(0.0f, 1.0f, 0.0f));

        // Only LayerMask "Platform" is checked... by LayerMask.GetMask(...);
        RaycastHit2D rayHit = Physics2D.Raycast(rigidBody.position, Vector3.down, 15.0f, LayerMask.GetMask("Platform"));
       
        
        Debug.Log(rayHit.collider);
        if(rayHit.collider != null)
        {
            Debug.Log(rayHit.distance);

            if (rayHit.distance < distancePlatformAndPlayer)
            {
                isJumping = false;
                // animator.SetBool("isJumping", false);
            }
        }
    }

    enum MouseButton
    {
        Left = 0,
        Right = 1,
        Wheel = 2
    }

    void RayShoot()
    {
        const float rayDistance = Mathf.Infinity;

        if (Input.GetMouseButtonDown((int)MouseButton.Left) == true)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 rayLine = mousePosition - transform.position;

            float angle = Mathf.Atan2(rayLine.y, rayLine.x) * Mathf.Rad2Deg;
            Vector3 rayDirection = Quaternion.Euler(0, 0, angle) * Vector3.right;

            // Ignore Player Layer, 2-Bit Opearation
            int ignoreLayerMask = ~(1 << LayerMask.NameToLayer("Player"));
            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, rayDirection, rayDistance, ignoreLayerMask); // Mathf.Infinity
            
            Debug.DrawRay(rigidBody.position, rayDirection, new Color(0.0f, 1.0f, 0.0f));

            if (rayHit.collider != null)
            {
                Debug.Log("Hit Collider : " + rayHit.collider.ToString());

                if (rayHit.collider.tag == "Enemy")
                {
                    // Destroy(rayHit.collider.gameObject);

                    TEMP_Enemy enemyObject = rayHit.collider.gameObject.GetComponent<TEMP_Enemy>();
                    enemyObject.TakeDamaged(10);
                }
            }
        }
    }
}