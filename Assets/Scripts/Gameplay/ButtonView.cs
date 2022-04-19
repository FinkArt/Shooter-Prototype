using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[RequireComponent(typeof(Rigidbody))]
public class ButtonView : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    public event Action<MeshRenderer, bool> OnMouseStateChanged;
    
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        OnMouseStateChanged?.Invoke(_meshRenderer, false);
    }

    public void OnMouseEnter()
    {
        OnMouseStateChanged?.Invoke(_meshRenderer, true);
    }

    public void OnMouseExit()
    {
        OnMouseStateChanged?.Invoke(_meshRenderer, false);
    }
}
