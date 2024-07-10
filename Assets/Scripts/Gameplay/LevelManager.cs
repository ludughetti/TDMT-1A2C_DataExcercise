using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform levelStart;
        [SerializeField] private PlayerControllerDataSource playerControllerSource;
        
        private PlayerController _playerController;

        private IEnumerator Start()
        {
            while (_playerController == null)
            {
                //TODO: [Done] Get reference to player controller from ReferenceManager/DataSource
                if (playerControllerSource != null && playerControllerSource.DataInstance != null)
                    _playerController = playerControllerSource.DataInstance;

                yield return null;
            }
            _playerController.SetPlayerAtLevelStartAndEnable(levelStart.position);
        }
    }
}
