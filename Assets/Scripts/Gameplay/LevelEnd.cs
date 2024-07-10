using Events;
using Scenery;
using UnityEngine;

namespace Gameplay
{
    public class LevelEnd : MonoBehaviour
    {
        [SerializeField] private StringEventChannel levelEventChannel;
        [SerializeField] private Level nextLevel;

        private void OnTriggerEnter(Collider other)
        {
            //TODO: [Done] Raise event through event system telling the game to show the win sequence.
            Debug.Log($"{name}: Player touched the flag!");
            if(levelEventChannel != null ) 
                levelEventChannel.Invoke(nextLevel.LevelName);
        }
    }
}
