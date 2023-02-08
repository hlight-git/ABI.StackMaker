using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public CardinalDirection MovingDirection { get; set; }
    public Vector3 MoveTargetPosition { get; set; }
    public Vector3 MovingDirectionVector => VectorUtils.CardinalDirectionVectorOf(MovingDirection);
    public bool IsMoving => MovingDirection != CardinalDirection.Directionless;

    private void Awake()
    {
        OnInit();
    }

    private void FixedUpdate()
    {
        if (MovingDirection == CardinalDirection.Directionless)
        {
            return;
        }
        MoveToTargetPosition();
    }
    void OnInit()
    {
        MoveTargetPosition = transform.position;
    }
    void MoveToTargetPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, MoveTargetPosition, Time.fixedDeltaTime * MoveSpeed);
        if (VectorUtils.Approximately(transform.position, MoveTargetPosition))
        {
            MovingDirection = CardinalDirection.Directionless;
        }
    }
    RaycastHit RaycastInDirection(Vector3 directionVector)
    {
        Vector3 raycastOrigin = transform.position + Vector3.up * 0.1f + directionVector * GameConstant.TILESIZE / 4;
        RaycastHit hit;

        Physics.Raycast(
            raycastOrigin,
            directionVector,
            out hit,
            GameConstant.Player.RAYCAST_MAX_RANGE,
            1 << LayerMask.NameToLayer(GameConstant.Brick.UnCollectable.LAYER_NAME)
        );

        return hit;
    }
    public Vector3 DetectMoveTargetPosition(CardinalDirection direction)
    {
        Vector3 directionVector = VectorUtils.CardinalDirectionVectorOf(direction);
        RaycastHit hit = RaycastInDirection(directionVector);

        return hit.transform.position - GameConstant.TILESIZE * directionVector;
    }
}
