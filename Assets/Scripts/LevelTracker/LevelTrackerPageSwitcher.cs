using UnityEngine;

namespace LevelTracker
{
    public class LevelTrackerPageSwitcher : MonoBehaviour
    {
        [SerializeField]
        private LevelTrackerUIFactory _uiFactory;
        private LevelHandler _levelHandler;

        private int _currentPageIndex => LevelsDatabase.CurrentSubjectIndex;

        private void Start()
        {            
            //TODO: replace with singleton
            _levelHandler = FindObjectOfType<LevelHandler>();
            
            _uiFactory.GeneratePage(_currentPageIndex);
        }

        public void NextPage()
        {
            if(!_levelHandler.CanLevelSwitch())
                return;
            
            int maxIndex = LevelsDatabase.Subjects.Length - 1;
            int newIndex = Mathf.Clamp(_currentPageIndex, 0, maxIndex);
            
            if (newIndex == maxIndex)
                newIndex = 0;
            else
                newIndex++;
            LevelsDatabase.SetSubjectIndex(newIndex);
            _uiFactory.GeneratePage(newIndex);
        }

        public void PreviousPage()
        {          
            if(!_levelHandler.CanLevelSwitch())
                return;
            
            int maxIndex = LevelsDatabase.Subjects.Length - 1;
            int newIndex = Mathf.Clamp(_currentPageIndex, 0, maxIndex);
            if (newIndex == 0)
                newIndex = maxIndex;            
            else
                newIndex--;
            LevelsDatabase.SetSubjectIndex(newIndex);
            _uiFactory.GeneratePage(newIndex);
        }
    
    }
}
