using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElasticPush : MonoBehaviour
{
    private const int PUSH_FORCE = 12;
    private const float BOUNCE_ANIM_DURATION = 0.5f;
    private static readonly Vector3 ELASTIC_PLANE_NORMAL = new Vector3(-1, 0, -1);

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            Vector3 playerMoveDirection = playerRb.velocity.normalized;
            Player player = other.GetComponent<Player>();

            playerRb.velocity = Vector3.zero;
            player.transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);

            player.Move(transform.rotation * ELASTIC_PLANE_NORMAL + playerMoveDirection, PUSH_FORCE);
            Bounce();
        }
    }

    // Animations
    public void Bounce()
    {
        animator.SetBool("bounce", true);
        Invoke(nameof(Idle), BOUNCE_ANIM_DURATION);
    }

    public void Idle()
    {
        animator.SetBool("bounce", false);
    }
}
