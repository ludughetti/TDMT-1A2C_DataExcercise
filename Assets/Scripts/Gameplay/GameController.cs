using UnityEngine;

namespace Gameplay 
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameControllerDataSource selfGameController;

        private void Awake()
        {
            if(selfGameController != null)
                selfGameController.DataInstance = this;
        }

        public void TriggerGameStart()
        {
            // Call SceneryController and change level
            Debug.Log("Start game");
        }

        public void TriggerExitGame()
        {
            // Quit Game
            Debug.Log("Exit game");
        }
    }
}

