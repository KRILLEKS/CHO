using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelTracker
{
    public class UILevelElement : MonoBehaviour
    {
        public class ElementParameters
        {
            public int LevelIndex { get; }
            public bool IsLocked { get; }
            public int SubjectIndex { get; }

            public ElementParameters(int levelIndex, bool isLocked, int subjectIndex)
            {
                SubjectIndex = subjectIndex;
                IsLocked = isLocked;
                LevelIndex = levelIndex;
            } 
        }

        [SerializeField]
        private RawImage _openedImage;
        
        [SerializeField]
        private RawImage _closedImage;
        
        [SerializeField]
        private Image _fillImage;
        
        [SerializeField]
        private TextMeshProUGUI _textOfPersantages;
        
        [SerializeField]
        private TextMeshProUGUI _textOfNumber;

        public void Construct(ElementParameters parameters, int percentages = 0)
        {
            LevelsDatabase.Subject currentSubject = LevelsDatabase.Subjects[parameters.SubjectIndex];
            
            SetPercentages(percentages);
            SetNumber(parameters.LevelIndex);
            SetLocked(parameters.IsLocked);
            SetImages(currentSubject);
        }

        private void SetImages(LevelsDatabase.Subject subject)
        {
            _openedImage.texture = subject.subjectIcon;
            _openedImage.GetComponent<RectTransform>().sizeDelta = subject.unlockedTextureSize;
            _closedImage.texture = subject.lockedTexture;
            _closedImage.GetComponent<RectTransform>().sizeDelta = subject.lockedTextureSize;
        }

        private void SetPercentages(int value)
        {
            _textOfPersantages.text = $"{value}%";
            FillImage(value);
        }
        private void FillImage(float value) =>
            _fillImage.fillAmount = value;

        private void SetNumber(int number) =>
            _textOfNumber.text = number == -1 ? "" : (number+1).ToString();
        private void SetLocked(bool isLocked)
        {            
            _openedImage.gameObject.SetActive(!isLocked);
            _closedImage.gameObject.SetActive(isLocked);
            _textOfPersantages.gameObject.SetActive(!isLocked);
        }
        
    }
}
