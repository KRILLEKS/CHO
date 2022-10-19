using System;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LevelTracker
{
    [RequireComponent(typeof(Button))]
    public class ButtonSceneSwitcher : MonoBehaviour
    {
        [SerializeField]
        private ButtonType _type;
        private enum ButtonType { ToSelection, ToTracker}
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            SetActivity(_type);
        }

        private void SetActivity(ButtonType type)
        {
            switch (type)
            {
                case ButtonType.ToSelection:
                    _button.onClick.AddListener(SceneSwitcher.SwitchSceneToSelection);
                    break;
                case ButtonType.ToTracker:
                    _button.onClick.AddListener(SceneSwitcher.SwitchSceneToTracker);
                    break;
                default:
                    Debug.LogError("ButtonType error");
                    break;
            }
        }



    }
}
