using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Jump,
    Cheer,
    Lose
}
public class Player : MonoBehaviour
{
    private const float IDLE_ANIM_DURATION = 1.1f;
    private const float JUMP_ANIM_DURATION = 0.17f;
    private const float CHEER_ANIM_DURATION = 6.14f;

    private static readonly Vector3 LEFT_COLLIDER_CENTER = Vector3.left * 0.45f;
    private static readonly Vector3 RIGHT_COLLIDER_CENTER = Vector3.right * 0.45f;
    private static readonly Vector3 FORWARD_COLLIDER_CENTER = Vector3.forward * 0.45f;
    private static readonly Vector3 BACKWARD_COLLIDER_CENTER = Vector3.back * 0.45f;

    [SerializeField] private PlayerStack playerStack;
    [SerializeField] private Transform animTransform;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;

    private PlayerState currentState;
    private Rigidbody rb;
    private BoxCollider boxCollider;

    private bool IsMoving => rb.velocity != Vector3.zero;
    public bool IsCheering => currentState == PlayerState.Cheer;
    public int StackCount => playerStack.Count;

    void Start()
    {
        OnInit();
    }
    void Update()
    {
        if (IsMoving || IsCheering || LevelManager.instance.IsTransitioning)
        {
            return;
        }
        InputHandle();
    }
    // If game object "Anim" use Rigidbody:
    ////private void LateUpdate()
    ////{
    ////    animTransform.position = new Vector3(transform.position.x, animTransform.position.y, transform.position.z);
    ////}

    private void InputHandle()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveFoward(moveSpeed);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft(moveSpeed);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MoveBackward(moveSpeed);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight(moveSpeed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "PositiveBrick":
                {
                    TakeBrick(other.gameObject);
                    break; 
                }
            case "NegativeBrick": case "FinishZone":
                {
                    DropBrick(other.gameObject);
                    break; 
                }
            case "Caro":
                {
                    Cheer();
                    break;
                }
            default:
                {
                    //Debug.Log(other.gameObject);
                    break;
                }
        }
    }
    void OnInit()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void TakeBrick(GameObject positiveBrick)
    {
        ChangeHighLevel(true);
        playerStack.AddBrick(positiveBrick);
    }

    void DropBrick(GameObject other)
    {
        bool isNegativeBrick = other.CompareTag("NegativeBrick");
        if (isNegativeBrick)
        {
            other.GetComponent<BoxCollider>().enabled = false;
            other.GetComponentInChildren<MeshRenderer>().enabled = true;
        }
        if (playerStack.Count == 1)
        {
            rb.velocity = Vector3.zero;

            Vector3 otherPos = other.transform.position;
            transform.position = new Vector3(otherPos.x, transform.position.y, otherPos.z);
            if (isNegativeBrick)
            {
                LevelManager.instance.RestartPlayingLevel();
            } else
            {
                UIManager.instance.ActiveWonPanel();
            }
            return;
        }
        ChangeHighLevel(false);
        playerStack.RemoveBrick();
    }

    void ChangeHighLevel(bool isIncrease)
    {
        // If game object "Anim" use Rigidbody:
        ////if (isIncrease)
        ////{
        ////    animTransform.position += Vector3.up / 2;
        ////} else
        ////{
        ////    animTransform.position -= Vector3.up / 2;
        ////}

        if (isIncrease)
        {
            Vector3 oldPos = animTransform.position;
            animTransform.position = new Vector3(oldPos.x, oldPos.y + PlayerStack.BRICK_HEIGHT, oldPos.z);
        }
        else
        {
            Vector3 oldPos = animTransform.position;
            animTransform.position = new Vector3(oldPos.x, oldPos.y - PlayerStack.BRICK_HEIGHT, oldPos.z);
        }
    }
    void Jump()
    {
        if (IsCheering)
        {
            return;
        }
        ChangeState(PlayerState.Jump);
        Invoke(nameof(Idle), JUMP_ANIM_DURATION);
    }

    void Cheer()
    {
        ChangeState(PlayerState.Cheer);
    }

    void Idle()
    {
        if (IsCheering)
        {
            return;
        }
        ChangeState(PlayerState.Idle);
    }

    void ChangeState(PlayerState state)
    {
        if (currentState != state)
        {
            animator.SetInteger("state", (int) state);
            currentState = state;
        }
    }
    void MoveLeft(float moveSpeed)
    {
        Jump();
        boxCollider.center = LEFT_COLLIDER_CENTER;
        rb.velocity = Vector3.left * moveSpeed;
    }

    void MoveRight(float moveSpeed)
    {
        Jump();
        boxCollider.center = RIGHT_COLLIDER_CENTER;
        rb.velocity = Vector3.right * moveSpeed;
    }

    void MoveFoward(float moveSpeed)
    {
        Jump();
        boxCollider.center = FORWARD_COLLIDER_CENTER;
        rb.velocity = Vector3.forward * moveSpeed;
    }

    void MoveBackward(float moveSpeed)
    {
        Jump();
        boxCollider.center = BACKWARD_COLLIDER_CENTER;
        rb.velocity = Vector3.back * moveSpeed;
    }
    public void Move(Vector3 direction, float moveSpeed)
    {
        Jump();
        boxCollider.center = direction * 0.45f;
        rb.velocity = direction * moveSpeed;
    }

}
