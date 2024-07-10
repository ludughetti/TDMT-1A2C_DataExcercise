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
        [SerializeField] private BoolEventChannel endgameEventChannel;
        [SerializeField] private Level endgameLevel;

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

            if (endgameEventChannel != null)
                endgameEventChannel.Subscribe(TriggerEndgame);
        }

        private void OnDisable()
        {
            if (levelEventChannel != null)
                levelEventChannel.Unsubscribe(TriggerNextLevel);

            if (endgameEventChannel != null)
                endgameEventChannel.Unsubscribe(TriggerEndgame);
        }

        private void Start()
        {
            if (sceneryController != null)
                _sceneryController = sceneryController.DataInstance;
        }

        public void TriggerNextLevel(string nextLevel)
        {
            // If it's endgame, invoke event
            if(endgameLevel != null && endgameLevel.LevelName == nextLevel
                && endgameEventChannel != null)
            {
                endgameEventChannel.Invoke(true);
                return;
            }

            // Call SceneryController and change level
            Debug.Log($"{name}: change level request received, loading next level ({nextLevel})");
            if (_sceneryController != null)
                _sceneryController.TriggerChangeLevel(nextLevel);
        }

        private void TriggerEndgame(bool isVictory)
        {
            Debug.Log($"isVictory: {isVictory}");
        }
    }
}

