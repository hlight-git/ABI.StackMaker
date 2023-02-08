using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private PlayerStack playerStack;
    public PlayerAction PlayerAction { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerAnimation PlayerAnimation { get; private set; }
    public PlayerInputHandle PlayerInputHandle { get; private set; }
    public PlayerStack PlayerStack { get => playerStack;}
    private void Awake()
    {
        PlayerInputHandle = GetComponent<PlayerInputHandle>();
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerAnimation = GetComponent<PlayerAnimation>();
        PlayerAction = GetComponent<PlayerAction>();
    }
}
