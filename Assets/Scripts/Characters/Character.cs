using Events;
using UnityEngine;
using Core.Interactions;

namespace Characters
{
    public class Character : MonoBehaviour, ISteerable, ITarget
    {
        [SerializeField] private float speed = 2.5f;
        [SerializeField] private float runningSpeed = 5;
        [SerializeField] private BoolEventChannel endgameEventChannel;

        private Vector3 _currentDirection = Vector3.zero;
        private bool _isRunning = false;

        private void Update()
        {
            var currentSpeed = _isRunning ? runningSpeed : speed;
            transform.Translate(_currentDirection * (Time.deltaTime * currentSpeed), Space.World);
        }

        public void SetDirection(Vector3 direction)
        {
            _currentDirection = direction;
        }

        public void StartRunning() => _isRunning = true;

        public void StopRunning() => _isRunning = false;

        public void ReceiveAttack()
        {
            //TODO: [Done] Raise event through event system telling the game to show the defeat sequence.
            Debug.Log($"{name}: received an attack!");
            if (endgameEventChannel != null)
                endgameEventChannel.Invoke(false);
        }
    }
}