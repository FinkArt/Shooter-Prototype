using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

// public interface IInputManager
// {
//     event Action<Vector2> OnMoveInput;
//     event Action<Vector2> OnLookInput;
//     event Action OnJumpInput;
//     void OnUpdate();
// }
//
// public class PCInputManager : IInputManager
// {
    // public event Action<Vector2> OnMoveInput;
    // public event Action<Vector2> OnLookInput;
    // public event Action OnJumpInput; 
    //
    // public void OnUpdate()
    // {
    //     var horizontalInput = Input.GetAxis("Horizontal"); // 1 0 -1 A = -1, D = 1, <- = -1, -> = 1
    //     var verticalInput = Input.GetAxis("Vertical");
    //     var mouseX = Input.GetAxis("Mouse X");
    //     var mouseY = Input.GetAxis("Mouse Y");
    //
    //     var mouseInput = new Vector2(mouseX, mouseY);
    //     if(mouseInput.magnitude > 0)
    //     {
    //         OnLookInput?.Invoke(mouseInput);
    //     }
    //
    //     var dist = 4f * 4f;
    //     if (mouseInput.sqrMagnitude < dist)
    //     {
    //         
    //     }
    //     
    //     
    //
    //     var moveInput = new Vector2(horizontalInput, verticalInput);
    //     if (moveInput.magnitude > 0)
    //     {
    //         OnMoveInput?.Invoke(moveInput);
    //     }
    //
    //     if (Input.GetButtonDown("Jump"))
    //     {
    //         OnJumpInput?.Invoke();
    //     }
    // }
//}

// public class TouchInputManager : IInputManager
// {
//     public event Action<Vector2> OnMoveInput;
//     public event Action<Vector2> OnLookInput;
//     public event Action OnJumpInput;
//
//     public void OnUpdate()
//     {
//     
//     }
// }

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private HealthComponent _healthComponent;

    public HealthComponent Health => _healthComponent;

    public event Action<PlayerController> OnPlayerSpawned;

    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _camSenseX = 4f;
    [SerializeField] private float _camSenseY = 4f;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _maxAngle = 60f;
    [SerializeField] private float _minAngle = -60f;
    [SerializeField] private float _jumpSpeed = 5f;
    private float _currentAngle;
    [SerializeField] private float _gravityValue = -9.81f;
    
    
    private void Start()
    {
        InputManager.Instance.OnMoveInput += OnInputMove;
        InputManager.Instance.OnLookInput += OnInputLook;
        InputManager.Instance.OnJumpInput += OnInputJump;
       
        _currentAngle = _cam.transform.eulerAngles.x;
        OnPlayerSpawned?.Invoke(this);
    }

    private void OnInputLook(Vector2 lookInput)
    {
        var mouseX = lookInput.x;
        var mouseY = lookInput.y;
        transform.eulerAngles += new Vector3(0f, mouseX * _camSenseX, 0f);
        _currentAngle += -mouseY * _camSenseY;
        _currentAngle = Mathf.Clamp(_currentAngle, _minAngle, _maxAngle);
        _cam.transform.rotation = Quaternion.Euler(new Vector3(_currentAngle, _cam.transform.eulerAngles.y, _cam.transform.eulerAngles.z));
    }

    private void OnInputMove(Vector2 moveInput)
    {
        var horizontalMove = new Vector3(moveInput.x, 0f, moveInput.y);
        var moveDir = horizontalMove * _moveSpeed;
        _characterController.Move(transform.TransformDirection(moveDir));
    }

    private void OnInputJump()
    {
        var moveDir = new Vector3(0f, 0f, 0f);
        Debug.Log(_characterController.isGrounded);
        //if (_characterController.isGrounded)
        {
            moveDir.y += _jumpSpeed;
            
        }
        // moveDir.y += _gravityValue * Time.deltaTime;
        
        // if (!_characterController.isGrounded || moveDir.y < 0f)
        // {
        //     moveDir.y = 0f;
        // }
        _characterController.Move(transform.TransformDirection(moveDir));
    }  

   
}
