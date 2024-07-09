using UnityEngine;
using DataSource;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "GameControllerSource", menuName = "DataSource/GameControllerSource")]
    public class GameControllerDataSource : DataSource<GameController> { }
}