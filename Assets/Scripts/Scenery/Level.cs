using System.Collections.Generic;
using UnityEngine;

namespace Scenery
{
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObject/Level")]
    public class Level : ScriptableObject
    {
        [SerializeField] private string levelName;
        [SerializeField] private List<Scene> scenes = new();

        public string LevelName { get { return levelName; } }
        public List<Scene> Scenes { get {  return scenes; } }
    }
}
