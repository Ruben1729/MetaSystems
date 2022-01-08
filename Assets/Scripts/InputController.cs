using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private Vector2 _mouse;
    private Vector2 _movement;

    public Vector2 MouseInput => _mouse;
    public Vector2 MovementInput => _movement;

    public void OnMouse(InputAction.CallbackContext callbackContext)
    {
        _mouse = callbackContext.ReadValue<Vector2>();
    }

    public void OnMovement(InputAction.CallbackContext callbackContext)
    {
        _movement = callbackContext.ReadValue<Vector2>();
    }
}
