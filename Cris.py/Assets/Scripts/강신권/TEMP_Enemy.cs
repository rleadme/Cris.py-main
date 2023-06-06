using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MovementFlag
{
    Left = -1,
    Idle = 0,
    Right = 1
}


public class TEMP_Enemy : MonoBehaviour
{
    [SerializeField]
    private float movePower = 1.5f;
    [SerializeField]
    private EnemyType enemyType = EnemyType.TraceMob; // 0: Simple Moving Mob, 1: Trace Mob
    [SerializeField]
    private int health = 100;
    [SerializeField]
    private bool isDied = false;


    private Transform targetPlayer;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    // private Animator animator;


    private MovementFlag movementFlag = MovementFlag.Idle; // 0 : Idle, 1: Left, 2: Right
    private bool isTracing = false;
    private bool isChangeMovingDirection = false;

    // Start is called before the first frame update
    private void Start()
    {
        targetPlayer = FindObjectOfType<Player_Cris_py>().transform;
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // animator = GetComponent<Animator>();

        StartCoroutine("CheckPossibleChangeMovingDirection");
    }
    IEnumerator CheckPossibleChangeMovingDirection()
    {
        if (isTracing == true)
        {
            isChangeMovingDirection = false;
            yield return new WaitForSeconds(2.0f);
        }
        else
        {
            isChangeMovingDirection = true;
            yield return new WaitForSeconds(0.5f);
        }

        StartCoroutine("CheckPossibleChangeMovingDirection");
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDied == false)
        {
            MoveGFX();
        }
    }
    private void FixedUpdate()
    {
        if (isDied == false)
        {
            ChangingMovement();
            TracingPlayer();
            Move();
        }
    }


    void CheckFloor()
    {
        if (movementFlag == MovementFlag.Idle || isTracing == true)
        {
            return;
        }

        // Platform Check

        Vector3 frontVector = new Vector3(rigidBody.position.x + (float)movementFlag * 0.01f, rigidBody.velocity.y);

        Debug.DrawRay(frontVector, Vector3.down, new Color(0.0f, 1.0f, 0.0f));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVector, Vector3.down, 1.0f, LayerMask.GetMask("Platform"));

        if (rayHit.collider == null)
        {
            Debug.Log("¸ó½ºÅÍ´Â ³¶¶³¾îÁö¸¦ Å½ÁöÇß´Ù!");

            movementFlag = (MovementFlag)((int)movementFlag * -1);

            // ReStart!!
            StopCoroutine("CheckPossibleChangeMovingDirection");
            StartCoroutine("CheckPossibleChangeMovingDirection");
        }
    }


    void ChangingMovement()
    {
        if (isChangeMovingDirection == true)
        {
            isChangeMovingDirection = false;

            // Left is -1 / Idle is 0 / Right is 1
            movementFlag = (MovementFlag)Random.Range(-1, 2);
        }
    }

    void TracingPlayer()
    {
        float playerDistance = Vector2.Distance(transform.position, targetPlayer.position);
        // Debug.Log("Player Distance : " + playerDistance);


        if (enemyType == EnemyType.TraceMob)
        {
            // Very Close to Player
            if (playerDistance <= 8.0f)
            {
                isTracing = true;
                movementFlag = MovementFlag.Idle;

                // Player Attack


                // Show Player Direction...
                if (transform.position.x > targetPlayer.position.x)
                {
                    spriteRenderer.flipX = false;
                }
                else
                {
                    spriteRenderer.flipX = true;
                }
            }
            // Far from Player
            else if (playerDistance <= 15.0f)
            {
                isTracing = true;

                if (transform.position.x > targetPlayer.position.x)
                {
                    movementFlag = MovementFlag.Left;
                }
                else
                {
                    movementFlag = MovementFlag.Right;
                }
            }
            else
            {
                isTracing = false;
            }
        }
    }

    void MoveGFX()
    {
        if (movementFlag == MovementFlag.Idle)
        {
            // animator.SetBool("isMoving", false);
        }
        else
        {
            // animator.SetBool("isMoving", true);
        }
    }

    void Move()
    {
        Vector3 moveVelocity;

        if (movementFlag == MovementFlag.Left)
        {
            moveVelocity = Vector3.left;
            spriteRenderer.flipX = false;

            // transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movementFlag == MovementFlag.Right)
        {
            moveVelocity = Vector3.right;
            spriteRenderer.flipX = true;

            // transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            moveVelocity = Vector3.zero;
        }


        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    internal void TakeDamaged(int damagedHealth)
    {
        Debug.Log("Health : " + damagedHealth);
        // animator.SetTrigger("Damaged");
        // Knock-Back Movement

        EnemyOnDamaged();
        Invoke("EnemyOffDamaged", 1.5f);

        health -= damagedHealth;
        if (health <= 0)
        {
            EnermyDie();
        }
    }

    void EnemyOnDamaged()
    {
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.4f);

        float knockBackPower = 1.0f;

        float dir = transform.position.x - targetPlayer.position.x > 0 ? knockBackPower * 1.0f : knockBackPower * -1.0f;
        rigidBody.AddForce(new Vector2(dir, 1.0f), ForceMode2D.Impulse);
    }

    void EnemyOffDamaged()
    {
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    void EnermyDie()
    {
        isDied = true;
        rigidBody.velocity = Vector3.zero;
        spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.4f);

        // animator.SetTrigger("Die");

        Vector2 dieVelocity = new Vector2(0.0f, 5.0f);
        rigidBody.AddForce(dieVelocity, ForceMode2D.Impulse);

        Invoke("DestoryEnemy", 1.0f);
    }

    private void DestoryEnemy()
    {
        Destroy(this.gameObject);
    }
}
