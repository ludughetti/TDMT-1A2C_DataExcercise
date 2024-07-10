using Characters;
using Core.Interactions;
using Events;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Character))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerControllerDataSource playerControllerSource;
        [SerializeField] private ITargetDataSource targetSource;
        [SerializeField] private Vector2EventChannel moveEventChannel;
        [SerializeField] private BoolEventChannel runEventChannel;

        private Character _character;

        private void Reset()
        {
            _character = GetComponent<Character>();
        }

        private void Awake()
        {
            _character ??= GetComponent<Character>();
            if (_character)
            {
                _character.enabled = false;
            }

            if(targetSource != null && _character != null)
            {
                targetSource.DataInstance = _character.GetComponent<ITarget>();
            }
        }

        private void OnEnable()
        {
            //TODO: [Done] Subscribe to inputs via event manager/event channel
            if (moveEventChannel != null)
                moveEventChannel.Subscribe(HandleMove);

            if (runEventChannel != null)
                runEventChannel.Subscribe(HandleRun);

            //TODO: [Done] Set itself as player reference via ReferenceManager/DataSource
            if (playerControllerSource != null)
                playerControllerSource.DataInstance = this;
        }

        private void OnDisable()
        {
            //TODO: [Done] Unsubscribe from all inputs via event manager/event channel
            if (moveEventChannel != null)
                moveEventChannel.Unsubscribe(HandleMove);

            if (runEventChannel != null)
                runEventChannel.Unsubscribe(HandleRun);

            //TODO: [Done] Remove itself as player reference via reference manager/dataSource
            if (playerControllerSource != null)
                playerControllerSource.DataInstance = null;
        }

        public void SetPlayerAtLevelStartAndEnable(Vector3 levelStartPosition)
        {
            transform.position = levelStartPosition;
            _character.enabled = true;
        }
        
        private void HandleMove(Vector2 direction)
        {
            _character.SetDirection(new Vector3(direction.x, 0, direction.y));
        }

        private void HandleRun(bool shouldRun)
        {
            if (shouldRun)
                _character.StartRunning();
            else
                _character.StopRunning();
        }
    }
}
