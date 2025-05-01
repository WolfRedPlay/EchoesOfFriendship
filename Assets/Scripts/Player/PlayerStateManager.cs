using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] Animator _animator;


    PlayerInputHandler _input;
    PlayerMovement _movement;
    SoundController _soundController;

    PlayerState _currentState;
    IdleState _idleState = new IdleState();
    WalkingState _walkingState = new WalkingState();
    JumpingState _jumpingState = new JumpingState();


    public Animator Animator => _animator;
    public PlayerInputHandler Input => _input;
    public PlayerMovement Movement => _movement;
    public SoundController Sound => _soundController;

    public IdleState Idle => _idleState;
    public WalkingState Walking => _walkingState;
    public JumpingState Jumping => _jumpingState;

    void Start()
    {
        _input = GetComponent<PlayerInputHandler>();
        _movement = GetComponent<PlayerMovement>();
        _soundController = GetComponent<SoundController>();

        Cursor.visible = false;
        SwitchState(_idleState);
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(PlayerState newState)
    {
        _currentState = newState;
        _currentState.EnterState(this);
    }
}
