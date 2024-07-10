using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "BoolEventChannel", menuName = "ScriptableObject/BoolEventChannel")]
    public class BoolEventChannel : EventChannel<bool> { }
}
