using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

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
      _inputScheme.Player.Backspace.performed += _ => ChemistryMinigame2.DeleteLetter();
   }

   private void Update()
   {
      Keyboard.current.onTextInput += ChemistryMinigame2.AddLetter;
   }

   private void OnDisable()
   {
      _inputScheme.Disable();
   }
}