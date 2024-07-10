using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenery
{
    public class SceneryController : MonoBehaviour
    {
        [SerializeField] private SceneryControllerDataSource selfSceneryController;
        [SerializeField] private List<Level> levels;
        [SerializeField] private Level defaultLevel;
        [SerializeField] private BoolEventChannel endgameEventChannel;

        private Dictionary<string, Level> _levelsByName = new();
        private string _currentLevelName;

        private void Awake()
        {
            if(selfSceneryController != null)
                selfSceneryController.DataInstance = this;

            foreach(Level level in levels)
            {
                _levelsByName.TryAdd(level.LevelName, level);

                if (level.LevelName == defaultLevel.LevelName)
                    _currentLevelName = level.LevelName;

                Debug.Log($"{name}: Scene {level.LevelName} was saved in the scenes dictionary.");
            }

            TriggerChangeLevel(_currentLevelName);
        }

        private void OnEnable()
        {
            if (endgameEventChannel != null)
                endgameEventChannel.Subscribe(TriggerEndgame);
        }

        private void OnDisable()
        {
            if (endgameEventChannel != null)
                endgameEventChannel.Unsubscribe(TriggerEndgame);
        }

        public void TriggerChangeLevel(string nextLevelName)
        {
            Debug.Log($"{name}: Triggering ChangeLevel from {_currentLevelName} to {nextLevelName}.");
            Level currentLevel = _levelsByName[_currentLevelName];
            Level nextLevel = _levelsByName[nextLevelName];

            if(currentLevel != null && nextLevel != null)
                StartCoroutine(ChangeLevel(currentLevel, nextLevel));
            else
                Debug.Log($"{name}: Scenes {_currentLevelName} or {nextLevelName} were not found in the scenes dictionary.");
        }

        private void TriggerEndgame(bool isVictory)
        {
            StartCoroutine(HandleEndgame(isVictory));
        }

        private IEnumerator HandleEndgame(bool isVictory)
        {
            // Unload what we don't need
            Level currentLevel = _levelsByName[_currentLevelName];
            yield return Unload(currentLevel);

            _currentLevelName = defaultLevel.LevelName;
        }

        private IEnumerator ChangeLevel(Level currentLevel, Level nextLevel)
        {
            yield return Unload(currentLevel);

            yield return Load(nextLevel);
            _currentLevelName = nextLevel.LevelName;
        }

        private IEnumerator Load(Level level)
        {
            foreach(Scene scene in level.Scenes)
            {
                AsyncOperation loadOperation = SceneManager.LoadSceneAsync(scene.SceneId, LoadSceneMode.Additive);
                yield return new WaitUntil(() => loadOperation.isDone);
            }
        }

        private IEnumerator Unload(Level level)
        {
            foreach(Scene scene in level.Scenes)
            {
                if(!scene.IsUnloadable)
                    continue;

                TryUnloadScene(scene.SceneId, out AsyncOperation unloadOperation);

                if(unloadOperation != null)
                    yield return new WaitUntil(() => unloadOperation.isDone);
            }
        }

        private void TryUnloadScene(string sceneName, out AsyncOperation unloadOperation)
        {
            unloadOperation = null;

            if(SceneManager.GetSceneByName(sceneName).IsValid())
                unloadOperation = SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
