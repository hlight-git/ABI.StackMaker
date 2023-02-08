using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        OnInit();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameTag.PLAYER))
        {
            Player player = other.GetComponent<Player>();
            Vector3 pushDirection = transform.rotation * GameConstant.Pusher.ELASTIC_PLANE_NORMAL + player.PlayerMovement.MovingDirectionVector;

            player.PlayerAction.StopMovingAt(transform.position);

            Push(player, VectorUtils.CardinalDirectionOf(pushDirection));
            
        }
    }
    void OnInit()
    {
        animator = GetComponent<Animator>();
    }
    void Push(Player player, CardinalDirection direction)
    {
        player.PlayerAction.MoveInDirection(direction, GameConstant.Pusher.PUSH_FORCE);
        Bounce();
    }
    // Animations
    void Bounce()
    {
        animator.SetBool("bounce", true);
        Invoke(nameof(Idle), GameAnim.Duration.Pusher.BOUNCE);
    }

    void Idle()
    {
        animator.SetBool("bounce", false);
    }
}
