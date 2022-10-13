using UnityEngine;

namespace LevelTracker
{
    public class LevelTrackerPageSwitcher : MonoBehaviour
    {
        [SerializeField]
        private LevelTrackerUIFactory _uiFactory;
        private LevelHandler _levelHandler;

        private int _currentPageIndex;

        private void Start()
        {            
            //TODO: replace with singleton
            _levelHandler = FindObjectOfType<LevelHandler>();
            
            _currentPageIndex = 0;
            _uiFactory.GeneratePage(_currentPageIndex);
        }

        public void NextPage()
        {
            if(!_levelHandler.CanLevelSwitch())
                return;
            
            int maxIndex = LevelsDatabase.Subjects.Length - 1;
            _currentPageIndex = Mathf.Clamp(_currentPageIndex, 0, maxIndex);
            if (_currentPageIndex == maxIndex)
                _currentPageIndex = 0;
            else
                _currentPageIndex++;
            _uiFactory.GeneratePage(_currentPageIndex);
        }

        public void PreviousPage()
        {          
            if(!_levelHandler.CanLevelSwitch())
                return;
            
            int maxIndex = LevelsDatabase.Subjects.Length - 1;
            _currentPageIndex = Mathf.Clamp(_currentPageIndex, 0, maxIndex);
            if (_currentPageIndex == 0)
                _currentPageIndex = maxIndex;            
            else
                _currentPageIndex--;
            _uiFactory.GeneratePage(_currentPageIndex);
        }
    
    }
}
