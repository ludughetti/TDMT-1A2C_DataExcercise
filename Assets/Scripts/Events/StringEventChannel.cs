using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "LevelEventChannel", menuName = "ScriptableObject/LevelEventChannel")]
    public class StringEventChannel : EventChannel<string> { }
}