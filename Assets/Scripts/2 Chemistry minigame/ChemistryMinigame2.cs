using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ChemistryMinigame2 : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI formulaTMPSerializable;

   // private static variables
   private static TextMeshProUGUI _formulaTMP;
   private static string _startString;
   private static string _currentText;
   private static bool _previousCharIsLetter = false;
   private static bool _nextCharIsUpper = false;

   private void Awake()
   {
      _formulaTMP = formulaTMPSerializable;

      _startString = _formulaTMP.text;
   }

   public static void AddLetter(char inputChar)
   {
      string input = inputChar.ToString();
      Debug.Log(_formulaTMP.GetRenderedValues(true).x);
      // if we press backspace (or some other keys) we can get strange chars also we don't need 0
      if (Regex.IsMatch(input, @"[^a-zA-Z1-9+=]") || _formulaTMP.GetRenderedValues(true).x >= Constants.MaxFormulaLength)
         return;

      // we will add plus even u pressed on = without shift
      if (inputChar == '+' || inputChar == '=')
      {
         _currentText += " + ";

         _previousCharIsLetter = false;
         _nextCharIsUpper = true;
      }
      // it's value
      else if (Regex.IsMatch(input, @"\d"))
      {
         // we will write coefficients after. And indexes (bottom small numbers) must be 1 digital 
         if (_previousCharIsLetter == false)
            return;

         _currentText += $"<size={Constants.IndexNumSize}>{input}</size>";
         _previousCharIsLetter = false;
      }
      // it's number
      else
      {
         // for things like Na
         if (_previousCharIsLetter == false || _nextCharIsUpper)
         {
            _currentText += input.ToUpper();
            _nextCharIsUpper = false;
         }
         else
         {
            _currentText += input.ToLower();
            _nextCharIsUpper = true;
         }

         _previousCharIsLetter = true;
      }

      _formulaTMP.text = _startString + _currentText;
   }

   // we could save our string and just render text after each text but I chose a hard way but more optimized
   // Render text is not expensive at all although I want to delete symbol in real time not to render hole text after change
   // The main problem that this code may be hard to understand but I think it's pretty native (insanely native I would say)
   public static void DeleteLetter()
   {
      // we have nothing to delete so we return
      if (_currentText.Length == 0)
         return;

      // if we have > it means that we must delete constructions <size=40>4</size> which contains 17 symbols
      // if we have 'space' it means that we must delete " + " which contains 3 symbols
      _currentText = _currentText.Substring(0, _currentText.Length - (_currentText[^1] == '>' ? 17 : (_currentText[^1] == ' ' ? 3 : 1)));
      _formulaTMP.text = _startString + _currentText;

      if (Regex.IsMatch(_currentText, @"[a-z]$"))
      {
         _nextCharIsUpper = true;
         _previousCharIsLetter = true;
      }
      else if (Regex.IsMatch(_currentText, @"[A-Z]$"))
      {
         _nextCharIsUpper = false;
         _previousCharIsLetter = true;
      }
      // number or plus or empty field
      else
      {
         _nextCharIsUpper = true;
         _previousCharIsLetter = false;
      }
   }
}