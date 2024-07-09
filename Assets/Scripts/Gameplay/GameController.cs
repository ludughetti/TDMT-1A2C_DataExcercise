using Scenery;
using UnityEngine;

namespace Gameplay 
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameControllerDataSource selfGameController;
        [SerializeField] private SceneryControllerDataSource sceneryController;

        private SceneryController _sceneryController;

        private void Awake()
        {
            if(selfGameController != null)
                selfGameController.DataInstance = this;
        }

        private void Start()
        {
            if (sceneryController != null)
                _sceneryController = sceneryController.DataInstance;
        }

        public void TriggerGameStart(string startMenu)
        {
            // Call SceneryController and change level
            Debug.Log("Start game");
            _sceneryController.TriggerChangeLevel(startMenu);
        }

        public void TriggerExitGame(string exitMenu)
        {
            // Quit Game
            Debug.Log("Exit game");
            _sceneryController.TriggerChangeLevel(exitMenu);
        }
    }
}

