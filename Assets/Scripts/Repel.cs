using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Repel : MonoBehaviour
{
    [SerializeField] private InputActionProperty leftJoystick;
    [SerializeField] private Vector2 currentInput;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform Player;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        currentInput = leftJoystick.action.ReadValue<Vector2>();
        if (currentInput == Vector2.zero)
        {
            // at rest
        }
        else if (currentInput.y < 0)
        {
            // moving down
            MoveDown(currentInput.y);
        }
      
    }

    private void MoveDown(float yValue)
    {
        Vector3 downwardMovement = new Vector3(0, yValue * moveSpeed * Time.deltaTime, 0);
        Player.Translate(downwardMovement);
    }
}
