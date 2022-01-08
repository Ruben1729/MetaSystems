using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEditorInternal;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private InputController _inputController;

    public float mSpeed = 10;
    public float mRotationSpeed = 25;

    void Reset()
    {
        _inputController = GetComponent<InputController>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 eulerDelta = new Vector3(-_inputController.MouseInput.y, _inputController.MouseInput.x, 0);
        eulerDelta *= mRotationSpeed;
        transform.localEulerAngles += eulerDelta * Time.deltaTime;
        
        Vector3 direction = _inputController.MovementInput.x * transform.right + _inputController.MovementInput.y * transform.forward;
        transform.localPosition += direction * mSpeed * Time.deltaTime;

    }
}
