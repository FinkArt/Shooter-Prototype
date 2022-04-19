using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private List<ButtonView> _buttonViews = new List<ButtonView>();

        
        [SerializeField] private Color _enterColor = Color.green;
        [SerializeField] private Color _normalColor = Color.white;
        
        private void Awake()
        {
            _buttonViews = FindObjectsOfType<ButtonView>().ToList();
            foreach (var buttonView in _buttonViews)
            {
                buttonView.OnMouseStateChanged += OnMouseStateChanged;
            }
        }
        
        public void OnMouseStateChanged(MeshRenderer meshRenderer, bool seleted)
        {
            var color = seleted ? _enterColor : _normalColor;
            meshRenderer.material.color = color;
        }
    }
}

public delegate void MyDelegate(AuthStatus status);
public delegate bool AuthDelegate(string login, string password);

public class GameLoader
{
    public Action<AuthStatus> _del2;
    public Func<string, string, bool> _del3;

    public MyDelegate _del;
    
    private AuthManager _authManager;
    
    private void StartGame()
    {
        _authManager = new AuthManager();
        _authManager.Auth("TestUser", "12345", OnAuthStatus);
    }

    private void OnAuthStatus(AuthStatus status)
    {
        
    }

}

public enum AuthStatus
{
    Success,
    FailWrongData,
    FailConnection
}

public class AuthManager
{
    private const string _login = "TestUser";
    private const string _password = "12345";
    public bool Auth(string login, string password, MyDelegate onAuth)
    {
        string test = "0";
        string test2 = "0.1";
        int num = int.Parse(test);
        float num2 = float.Parse(test);
        var isSuccess = login == _login && _password == password;
        var status = AuthStatus.FailConnection;
        //check connection
        status = isSuccess ? AuthStatus.Success : AuthStatus.FailWrongData;
        onAuth?.Invoke(status);
        return isSuccess;
    }

}