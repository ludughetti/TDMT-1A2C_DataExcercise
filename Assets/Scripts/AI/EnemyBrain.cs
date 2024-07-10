using Core.Interactions;
using Events;
using UnityEngine;

namespace AI
{
    public class EnemyBrain : MonoBehaviour
    {
        [SerializeField] private BoolEventChannel endgameEventChannel;
        [SerializeField] private ITargetDataSource targetSource;
        [SerializeField] private float attackDistance;

        private ITarget _target;
        private ISteerable _steerable;

        private void Awake()
        {
            _steerable = GetComponent<ISteerable>();
            if( _steerable == null)
            {
                Debug.LogError($"{name}: cannot find a {nameof(ISteerable)} component!");
                enabled = false;
            }

            if (targetSource != null)
                _target = targetSource.DataInstance;
        }

        private void OnEnable()
        {
            if (endgameEventChannel != null)
                endgameEventChannel.Subscribe(DisableTargetOnLevelEnd);
        }

        private void OnDisable()
        {
            if (endgameEventChannel != null)
                endgameEventChannel.Unsubscribe(DisableTargetOnLevelEnd);
        }

        private void Update()
        {
            //TODO: [Done] Add logic to get the target from a source/reference system
            if (_target == null)
                return;
            //          AB        =         B        -          A
            var directionToTarget = _target.transform.position - transform.position;
            var distanceToTarget = directionToTarget.magnitude;
            if (distanceToTarget < attackDistance)
            {
                _target.ReceiveAttack();
            }
            else
            {
                Debug.DrawRay(transform.position, directionToTarget.normalized, Color.red);
                _steerable.SetDirection(directionToTarget.normalized);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }

        private void DisableTargetOnLevelEnd(bool isVictory)
        {
            Debug.Log($"{name}: Disabling EnemyBrain on level end");
            gameObject.SetActive(false);
        }
    }
}
