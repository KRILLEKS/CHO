using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
   // private static variables
   private static InputScheme _inputScheme;

   private void Awake()
   {
      _inputScheme = new InputScheme();
   }

   private void OnEnable()
   {
      _inputScheme.Enable();
   }

   private void Start()
   {
      _inputScheme.Player.Space.performed += _ => PauseController.InvertGameState();
   }

   private void OnDisable()
   {
      _inputScheme.Disable();
   }
}
