using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandle : MonoBehaviour
{
    private Player player;
    private Vector3 oldMousePosition;

    void Awake()
    {
        player = GetComponent<Player>();
    }
    void Update()
    {
        if (
            player.PlayerMovement.IsMoving ||
            player.PlayerAction.IsCheering ||
            LevelManager.Instance.IsTransitioning)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            oldMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 moveDirection = currentMousePosition - oldMousePosition;
            if (moveDirection.magnitude > GameConfig.Player.Input.SENSITIVITY)
            {
                oldMousePosition = currentMousePosition;
                PlayerMoveNavigate(moveDirection);
            }
        }
    }

    void PlayerMoveNavigate(Vector3 moveDirectionVector)
    {
        CardinalDirection moveDirection;
        if (Mathf.Abs(moveDirectionVector.x) > Mathf.Abs(moveDirectionVector.y))
        {
            if (moveDirectionVector.x < 0)
            {
                moveDirection = CardinalDirection.Left;
            }
            else
            {
                moveDirection = CardinalDirection.Right;
            }
        }
        else
        {
            if (moveDirectionVector.y < 0)
            {
                moveDirection = CardinalDirection.Backward;
            }
            else
            {
                moveDirection = CardinalDirection.Forward;
            }
        }
        player.PlayerAction.MoveInDirection(moveDirection, player.PlayerMovement.MoveSpeed);
    }
}
