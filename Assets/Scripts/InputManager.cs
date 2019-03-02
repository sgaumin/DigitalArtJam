using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Singleton
    public static InputManager instance;

    // Action
    public event Action OnPause = delegate { };

    // Inputs Code
    [SerializeField] private KeyCode pauseCode;

    // Initialize Singleton
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKey(pauseCode))
            OnPause();
    }
}