using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : Player
{
    [SerializeField] private Transform animTransform;

    private Player player;
    private Rigidbody rb;
    public bool IsCheering => player.PlayerAnimation.CurrentState == PlayerState.Cheer;

    void Awake()
    {
        OnInit();
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case GameTag.BRICK:
                {
                    TakeBrick(other.gameObject);
                    break; 
                }
            case GameTag.BRICK_TAKER:
                {
                    DropBrick(other.gameObject);
                    break; 
                }
            case GameTag.FINISH_LINE:
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
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
    }

    void TakeBrick(GameObject brick)
    {
        ChangeHighLevel(true);
        player.PlayerStack.AddBrick(brick);
    }

    void DropBrick(GameObject brickTaker)
    {
        if (player.PlayerStack.CollectedBrickCount == 0)
        {
            StopMovingAt(transform.position);
            bool isStopAtWinZone = brickTaker.transform.parent.CompareTag(GameTag.WINZONE);
            if (isStopAtWinZone)
            {
                UIManager.Instance.ActiveWonPanel();
            }
            else
            {
                LevelManager.Instance.RestartPlayingLevel();
            }
            return;
        }
        brickTaker.transform.GetChild(0).gameObject.SetActive(true);
        brickTaker.GetComponent<BoxCollider>().enabled = false;
        ChangeHighLevel(false);
        player.PlayerStack.RemoveBrick();
    }

    void ChangeHighLevel(bool isIncrease)
    {
        Vector3 oldPos = animTransform.position;
        Vector3 newPos = oldPos + new Vector3(0, GameConstant.Brick.THICKNESS * (isIncrease ? 1 : -1), 0);
        animTransform.position = newPos;
    }
    public void Idle()
    {
        if (player.PlayerAnimation.CurrentState == PlayerState.Cheer)
        {
            return;
        }
        player.PlayerAnimation.ChangeState(PlayerState.Idle);
    }
    public void Jump()
    {
        if (player.PlayerAnimation.CurrentState == PlayerState.Cheer)
        {
            return;
        }
        player.PlayerAnimation.ChangeState(PlayerState.Jump);
        Invoke(nameof(Idle), GameAnim.Duration.Player.JUMP);
    }
    public void Cheer()
    {
        player.PlayerAnimation.ChangeState(PlayerState.Cheer);
    }

    public void MoveInDirection(CardinalDirection direction, float moveSpeed)
    {
        Jump();

        Vector3 expectedMoveTargetPosition = player.PlayerMovement.DetectMoveTargetPosition(direction);
        if (VectorUtils.Approximately(expectedMoveTargetPosition, transform.position))
        {
            return;
        }

        player.PlayerMovement.MovingDirection = direction;
        player.PlayerMovement.MoveSpeed = moveSpeed;
        player.PlayerMovement.MoveTargetPosition = expectedMoveTargetPosition;
    }

    public void StopMovingAt(Vector3 stopPosition)
    {
        player.PlayerMovement.MovingDirection = CardinalDirection.Directionless;
        player.PlayerMovement.MoveTargetPosition = stopPosition;
        player.transform.position = stopPosition;
    }
}