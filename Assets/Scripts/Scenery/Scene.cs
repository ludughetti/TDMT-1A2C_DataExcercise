using System;
using UnityEngine;

namespace Scenery
{
    [CreateAssetMenu(fileName = "Scene", menuName = "ScriptableObject/Scene")]
    public class Scene : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private bool isUnloadable;

        public string SceneId { get { return id; } }
        public bool IsUnloadable { get { return isUnloadable; } }
    }
}
