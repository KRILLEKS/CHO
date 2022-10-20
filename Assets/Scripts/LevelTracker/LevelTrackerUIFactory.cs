using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static LevelTracker.UILevelElement;

namespace LevelTracker
{
    public class LevelTrackerUIFactory : MonoBehaviour
    {
        [SerializeField]
        private Transform _uiLevelsRoot;
        
        [SerializeField]
        private Transform _averageElementRoot;

        [SerializeField] 
        private TextMeshProUGUI _uiTextOfReadyLevels;
        
        private LevelHandler _levelHandler;

        private List<UILevelElement> _instantiatedElements;

        private GameObject ElementPrefab => AssetProvider.ElementPrefab;
        
        private GameObject AverageElementPrefab => AssetProvider.AverageElementPrefab;

        private void Awake()
        {
            GetStartComponents();
            _instantiatedElements = new List<UILevelElement>();
        }

        private void GetStartComponents()
        {            
            //TODO: replace with singleton
            _levelHandler = FindObjectOfType<LevelHandler>();

        }

        public void GeneratePage(int subjectIndex)
        {
            _levelHandler.ChangeBackgroundThings(subjectIndex);
            ClearPage();
            for (int i = 0; i < LevelsDatabase.Subjects[subjectIndex].levelsAmount; i++)
                _instantiatedElements.Add(CreateElement(i, subjectIndex));
            
            CreateAverageElement(subjectIndex);
            SetReadyLevelsText(subjectIndex);
        }

        private void CreateAverageElement(int subjectIndex)
        {
            ElementParameters elementParameters = new(-1, false, subjectIndex);
            UILevelElement element = InstantiateElement(AverageElementPrefab, _averageElementRoot);
            ConfigureAverageElement(element, elementParameters);
        }

        private void SetReadyLevelsText(int subjectIndex)
        {
            int countOfReadyLevels = Data.Percentages[subjectIndex].Length;
            _uiTextOfReadyLevels.text = $"{countOfReadyLevels} / {LevelsDatabase.Subjects[subjectIndex].levelsAmount}";
        }
        
        private void ClearPage()
        {
            if(_instantiatedElements.Count == 0)
                return;
            
            foreach (UILevelElement element in _instantiatedElements) 
                Destroy(element.gameObject);
            _instantiatedElements.Clear();
            
            for (int i = 0; i < _averageElementRoot.childCount; i++)
                Destroy(_averageElementRoot.GetChild(i).gameObject);
        }
        
        private UILevelElement CreateElement(int levelIndex, int subjectIndex)
        {
            bool isLocked = Data.MaxLevels[subjectIndex] <= levelIndex;
            UILevelElement element = InstantiateElement(ElementPrefab, _uiLevelsRoot);

            ElementParameters parameters = new(levelIndex, isLocked, subjectIndex); 
            
            element.Construct(parameters);

            StartSettingPercentages(parameters, element.transform);
            return element;
        }

        private UILevelElement InstantiateElement(GameObject prefab, Transform root) => 
            Instantiate(prefab, root)
                .GetComponent<UILevelElement>();
        
        private void ConfigureAverageElement(UILevelElement element, ElementParameters parameters)
        {
            List<float> percentages = Data.Percentages[parameters.SubjectIndex].ToList();
            element.Construct(parameters, CalculateAverageValue(percentages));
            _levelHandler.StartCoroutine(_levelHandler
                .ChangePercentageOverTime(element.transform, CalculateAverageValue(percentages) / 100f));
        }

        private int CalculateAverageValue(IReadOnlyList<float> listOfValues)
        {
            int countOfListValues = listOfValues.Count;
            if (countOfListValues == 0)
                return 0;
            
            float averageValue = 0;
            for (int i = 0; i < countOfListValues; i++)
                averageValue += listOfValues[i];
            averageValue /= countOfListValues / 100f;
            
            return (int)averageValue;
        }
        private void StartSettingPercentages(ElementParameters parameters, Transform elementTransform)
        {
            List<float> percentages = Data.Percentages[parameters.SubjectIndex].ToList();
            if (!parameters.IsLocked
                && percentages.Count > parameters.LevelIndex
                && percentages[parameters.LevelIndex] >= 0)
                _levelHandler.StartCoroutine(_levelHandler
                    .ChangePercentageOverTime(elementTransform, percentages[parameters.LevelIndex]));
        }
        

    }
}
