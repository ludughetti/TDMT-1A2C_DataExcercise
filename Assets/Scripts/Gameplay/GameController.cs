using Events;
using Scenery;
using UnityEngine;

namespace Gameplay 
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameControllerDataSource selfGameController;
        [SerializeField] private SceneryControllerDataSource sceneryController;
        [SerializeField] private StringEventChannel levelEventChannel;
        [SerializeField] private Level endgameLevel;
        [SerializeField] private BoolEventChannel endgameEventChannel;

        private SceneryController _sceneryController;

        private void Awake()
        {
            if (selfGameController != null)
                selfGameController.DataInstance = this;
        }

        private void OnEnable()
        {
            if (levelEventChannel != null)
                levelEventChannel.Subscribe(TriggerNextLevel);
        }

        private void OnDisable()
        {
            if (levelEventChannel != null)
                levelEventChannel.Unsubscribe(TriggerNextLevel);
        }

        private void Start()
        {
            if (sceneryController != null)
                _sceneryController = sceneryController.DataInstance;
        }

        public void TriggerNextLevel(string nextLevel)
        {
            // Call SceneryController to change level
            Debug.Log($"{name}: change level request received, loading next level ({nextLevel})");

            // If it's endgame and everything's set up, trigger endgame. 
            if (_sceneryController != null && endgameLevel.LevelName == nextLevel
                    && endgameEventChannel != null)
            {
                endgameEventChannel.Invoke(true);
                return;
            }

            // Else, change level.
            if (_sceneryController != null)
                _sceneryController.TriggerChangeLevel(nextLevel);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}

