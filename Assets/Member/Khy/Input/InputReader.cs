using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, InputControlls.IPlayerMapActions
{
    public Action<bool> OnAttackEvent;
    public Action OnStartChargeAttackEvent;
    public Action OnEndChargeAttackEvent;
    public Action<int> OnWingEvent;

    public Action OnFirSkillEvent;
    public Action OnSecSkillEvent;
    public Action OnThrSkillEvent;
    public Vector2 InputDirection { get; private set; }
    private InputControlls _controlls;

    public LayerMask whatIShootPlace;

    public Vector2 MousePosition { get; private set; }
    private Vector3 _beforeMouseWorldPos;
    private Vector3 _worldPosition;

    private void OnEnable()
    {
        if (_controlls == null)
        {
            _controlls = new();
            _controlls.PlayerMap.SetCallbacks(this);
        }
        _controlls.PlayerMap.Enable();
    }

    private void OnDisable()
    {
        _controlls.PlayerMap.Disable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
            OnAttackEvent?.Invoke(true);
        if (context.canceled)
            OnAttackEvent?.Invoke(false);
    }

    public void OnLeftWing(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnWingEvent?.Invoke(-1);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        InputDirection = context.ReadValue<Vector2>();
    }

    public void OnRightWing(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnWingEvent?.Invoke(1);
    }

    public void OnChargeAttack(InputAction.CallbackContext context)
    {
        if (context.started)
            OnStartChargeAttackEvent?.Invoke();
        if (context.canceled)
            OnEndChargeAttackEvent?.Invoke();
    }

    public Vector3 GetWorldPosition(out RaycastHit hit)
    {
        Camera mainCam = Camera.main;
        Ray camRay = mainCam.ScreenPointToRay(MousePosition);
        bool isHit = Physics.Raycast(camRay, out hit, mainCam.farClipPlane,whatIShootPlace);
        if (isHit)
        {
            _worldPosition = hit.point;
        }
        return _worldPosition;
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
    }

    public void OnFirSkill(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnFirSkillEvent?.Invoke();
    }

    public void OnSecSkill(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnSecSkillEvent?.Invoke();
    }

    public void OnThrSkill(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnThrSkillEvent?.Invoke();
    }
}
