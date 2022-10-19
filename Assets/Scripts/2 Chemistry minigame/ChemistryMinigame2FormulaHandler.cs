using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

// can be optimized with object pooling
// but it's optimized enough. I don't think that object pooling is needed
public class ChemistryMinigame2FormulaHandler : MonoBehaviour
{
   [Header("Essentials")]
   [SerializeField] private TextMeshProUGUI formulaTMPSerializable;
   [SerializeField] private Transform formulaFolderSerializable;
   [SerializeField] private GameObject bigConfirmButtonSerializable;
   [SerializeField] private GameObject smallConfirmButtonSerializable;
   [SerializeField] private GameObject smallEditButtonSerializable;
   [Header("Values")] // For me logically correct not to place them in constants cause there will be a lot of mini games
   [SerializeField] private int maxFormulaLengthSerializable = 15;
   [SerializeField] private float indexSizeSerializable = 40;

   // private static variables
   private static GameObject _bigConfirmButton;
   private static GameObject _smallConfirmButton;
   private static GameObject _smallEditButton;

   private static int _maxFormulaLength;
   private static float _indexSize;
   private static TextMeshProUGUI _holeUserFormulaTMP;
   private static Transform _formulaFolder;

   private static GameObject _formulaPiecePrefab;
   private static GameObject _formulaInputFieldPrefab;
   private static string _startText;
   private static string _renderedStartText;
   private static string _currentText = String.Empty;

   private static List<TMP_InputField> _inputFieldList;
   private static string[] _inputFieldIndexes;

   private void Awake()
   {
      _bigConfirmButton = bigConfirmButtonSerializable;
      _smallConfirmButton = smallConfirmButtonSerializable;
      _smallEditButton = smallEditButtonSerializable;

      _maxFormulaLength = maxFormulaLengthSerializable;
      _indexSize = indexSizeSerializable;
      _holeUserFormulaTMP = formulaTMPSerializable;
      _formulaFolder = formulaFolderSerializable;

      _formulaPiecePrefab = Resources.Load<GameObject>($"ChemistryMinigame2/Formula");
      _formulaInputFieldPrefab = Resources.Load<GameObject>($"ChemistryMinigame2/InputField");

      _holeUserFormulaTMP.text = _renderedStartText;
   }


   public void Edit()
   {
      _inputFieldIndexes = _inputFieldList.Select(x => x.text).ToArray();
      _inputFieldList = null;

      IsEditState(false);
   }

   public void ConfirmIndexes()
   {
      ChemistryMinigame2ExercisesHandler.NextExercise();
   }

   public static void AddLetter(char inputChar)
   {
      // we in edit mode (set indexes)
      if (_holeUserFormulaTMP != null && _holeUserFormulaTMP.gameObject.activeSelf == false)
         return;

      string input = inputChar.ToString();

      // we add value if
      // 1) we won't have more symbols than limit
      // 2) It's a letter
      // 3) It's number after letter
      // 4) It's symbol(plus or equal (same as plus)) after letter or number
      if (_currentText.Length + 1 <= _maxFormulaLength &&
          (Regex.IsMatch(inputChar.ToString(), @"[a-zA-Z]") ||
           Regex.IsMatch(_currentText, @"[a-zA-Z]$") && Regex.IsMatch(inputChar.ToString(), @"[1-9]") ||
           Regex.IsMatch(_currentText, @"[a-zA-Z1-9]$") && Regex.IsMatch(inputChar.ToString(), @"[+=]")))
      {
         _currentText += input;
         RenderFormula(_holeUserFormulaTMP, _currentText, true);
      }
   }

   public static void DeleteLetter()
   {
      // we in edit mode (set indexes)
      if (_holeUserFormulaTMP != null && _holeUserFormulaTMP.gameObject.activeSelf == false)
         return;
      
      if (_currentText.Length == 0)
         return;

      _currentText = _currentText.Substring(0, _currentText.Length - 1);

      RenderFormula(_holeUserFormulaTMP, _currentText, true);
   }

   public static void RenderFormula(TextMeshProUGUI tmp2Render, string formulaText, bool toUseStartString = false)
   {
      string actualText = String.Empty;
      for (int i = 0; i < formulaText.Length; i++)
      {
         // if it's number set size and add number
         if (Regex.IsMatch(formulaText[i].ToString(), @"[1-9]"))
         {
            // if we have index before letter (in start formula) we shouldn't render it cause player will write it by himself
            if (i != 0 && Regex.IsMatch(formulaText[i - 1].ToString(), @"[a-zA-Z()]"))
               actualText += $"<size={_indexSize}>{formulaText[i]}</size>";
         }
         // if it's '+' or '=' we add '+' with spaces
         else if (Regex.IsMatch(formulaText[i].ToString(), @"[+=]"))
            actualText += " + ";
         // if it's symbol just add symbol
         else
            // if (Regex.IsMatch(formulaText[i].ToString(), @"[a-zA-Z]"))
            actualText += formulaText[i];
      }

      tmp2Render.text = (toUseStartString ? _renderedStartText : null) + actualText;
   }

   // invokes on button press
   // add input fields and split formula
   public static void MakeCompositeFormula()
   {
      string holeFormulaText = _startText + _currentText;
      _inputFieldList = new List<TMP_InputField>();

      IsEditState(true);

      // if composite formula have something in it we must remove it
      for (int i = 0; i < _formulaFolder.childCount; i++)
         Destroy(_formulaFolder.GetChild(i).gameObject);

      // spawn first IF
      SpawnIF(0);

      int pieceStartIndex = 0;
      int IFIndex = 1;
      for (int i = 1; i < holeFormulaText.Length - 1; i++)
      {
         // IF can be instantiated only after symbol (space) and before element
         if (Regex.IsMatch(holeFormulaText[i].ToString(), @"[1-9a-zA-Z()]") && Regex.IsMatch(holeFormulaText[i - 1].ToString(), @"[^a-zA-Z0-9()]"))
         {
            // spawn text which is before IF
            InstantiateFormulaPiece(holeFormulaText.Substring(pieceStartIndex, i - pieceStartIndex));

            // spawn IF
            SpawnIF(IFIndex++);

            pieceStartIndex = i;
         }
      }

      // spawn last piece
      InstantiateFormulaPiece(holeFormulaText.Substring(pieceStartIndex, holeFormulaText.Length - pieceStartIndex));
      
      void InstantiateFormulaPiece(string text)
      {
         var pieceGO = Instantiate(_formulaPiecePrefab, _formulaFolder);
         var pieceTMP = pieceGO.GetComponent<TextMeshProUGUI>();
         pieceTMP.text = text;
         RenderFormula(pieceTMP, pieceTMP.text);
         pieceTMP.rectTransform.sizeDelta = new Vector2(LayoutUtility.GetPreferredWidth(pieceTMP.rectTransform), pieceTMP.rectTransform.sizeDelta.y);
      }
      void SpawnIF(int index)
      {
         _inputFieldList.Add(Instantiate(_formulaInputFieldPrefab, _formulaFolder).transform.GetChild(0).GetComponent<TMP_InputField>());
         if (_inputFieldIndexes != null && _inputFieldIndexes.Length - 1 >= index)
         {
            _inputFieldList[index].text = _inputFieldIndexes[index];
         }
      }
   }

   public static void LoadExercise(ChemistryMinigame2ExercisesHandler.ExerciseData exerciseData)
   {
      _startText = exerciseData.StartFormula;
      RenderFormula(_holeUserFormulaTMP, _startText);
      _renderedStartText = _holeUserFormulaTMP.text;

      _inputFieldList = null;
      
      // if we not in edit mode it'll be equal to null
      _inputFieldIndexes = exerciseData.Indexes;

      if (exerciseData.UserFormula != null)
      {
         _currentText = exerciseData.UserFormula;
         RenderFormula(_holeUserFormulaTMP, _currentText, true);
      }
      else
      {
         _currentText = String.Empty;
      }

      if (exerciseData.InEditState)
      {
         IsEditState(true);

         MakeCompositeFormula();
      }
      else
      {
         IsEditState(false);
      }
   }

   public static ChemistryMinigame2ExercisesHandler.ExerciseData GetExerciseData()
   {
      ChemistryMinigame2ExercisesHandler.ExerciseData exerciseData = new ChemistryMinigame2ExercisesHandler.ExerciseData();

      exerciseData.StartFormula = _startText;
      exerciseData.UserFormula = _currentText;

      if (_inputFieldList != null)
      {
         exerciseData.Indexes = _inputFieldList.Select(x => x.text).ToArray();
         exerciseData.InEditState = true;
      }
      else
      {
         exerciseData.InEditState = false;
      }

      return exerciseData;
   }

   public static void DisableAllElements()
   {
      _bigConfirmButton.SetActive(false);
      _holeUserFormulaTMP.gameObject.SetActive(false);

      _smallConfirmButton.SetActive(false);
      _smallEditButton.SetActive(false);
      _formulaFolder.gameObject.SetActive(false);
   }
   
   public static void IsEditState(bool state)
   {
      _bigConfirmButton.SetActive(state == false);
      _holeUserFormulaTMP.gameObject.SetActive(state == false);

      _smallConfirmButton.SetActive(state);
      _smallEditButton.SetActive(state);
      _formulaFolder.gameObject.SetActive(state);

      if (state == false)
         // if composite formula have something in it we must remove it
         for (int i = 0; i < _formulaFolder.childCount; i++)
            Destroy(_formulaFolder.GetChild(i).gameObject);
   }
}