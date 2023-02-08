using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public PlayerState CurrentState { get; private set; }

    private void Awake()
    {
        CurrentState = PlayerState.Idle;
    }

    public void ChangeState(PlayerState state)
    {
        if (CurrentState != state)
        {
            animator.SetInteger("state", (int)state);
            CurrentState = state;
        }
    }
}
