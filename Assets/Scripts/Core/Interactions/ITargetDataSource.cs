using DataSource;
using UnityEngine;

namespace Core.Interactions
{
    [CreateAssetMenu(fileName = "ITargetDataSource", menuName = "DataSource/ITargetDataSource")]
    public class ITargetDataSource : DataSource<ITarget> { }
}
