using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerStateManager : MonoBehaviour
{
    PlayerInputHandler _input;
    PlayerMovement _movement;

    PlayerState _currentState;
    IdleState _idleState = new IdleState();
    WalkingState _walkingState = new WalkingState();


    public PlayerInputHandler Input => _input;
    public PlayerMovement Movement => _movement;

    public IdleState Idle => _idleState;
    public WalkingState Walking => _walkingState;

    void Start()
    {
        _input = GetComponent<PlayerInputHandler>();
        _movement = GetComponent<PlayerMovement>();


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
