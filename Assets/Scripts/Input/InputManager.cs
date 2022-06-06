using System;
using PlayerController1.Utility;
using UnityEngine;


public class InputManager : Singleton<InputManager>
{
    public event Action<Vector2> OnMoveInput;
    public event Action<Vector2> OnLookInput;
    public event Action<bool> OnRunInput;
    public event Action OnJumpInput;
    public event Action<int> OnWeaponChangeKeyInput; 
    public event Action<float> OnWeaponChangeScrollMouseInput;
    public event Action OnPrevWeaponInput; 
    
    

    private bool _run;
    
    public void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal"); // 1 0 -1 A = -1, D = 1, <- = -1, -> = 1
        var verticalInput = Input.GetAxis("Vertical");
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");

        var mouseInput = new Vector2(mouseX, mouseY);
        if(mouseInput.magnitude > 0)
        {
            OnLookInput?.Invoke(mouseInput);
            
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
            _run = true;
        else
        {
            _run = false; 
        }
        

        var moveInput = new Vector2(horizontalInput, verticalInput);
        OnMoveInput?.Invoke(moveInput);

        if (Input.GetButtonDown("Jump"))
        {
            OnJumpInput?.Invoke();
        }
        
        OnRunInput?.Invoke(_run);

        if(Input.GetKeyDown(KeyCode.Alpha1))
            OnWeaponChangeKeyInput?.Invoke(0);
        if(Input.GetKeyDown(KeyCode.Alpha2))
            OnWeaponChangeKeyInput?.Invoke(1);
        if(Input.GetKeyDown(KeyCode.Alpha3))
            OnWeaponChangeKeyInput?.Invoke(2);
        if (Input.GetKeyDown(KeyCode.Q))
            OnPrevWeaponInput?.Invoke();
        var mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");
        OnWeaponChangeScrollMouseInput?.Invoke(mouseScrollInput);
        
        
        
        
        

    }
}