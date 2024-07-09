using UnityEngine;
using DataSource;

namespace Navigation
{
    [CreateAssetMenu(fileName = "MenuSource", menuName = "DataSource/MenuSource")]
    public class MenuDataSource : DataSource<Menu>
    {
        [SerializeField] private string menuId;
        [SerializeField] private string menuLabel;

        public string MenuId { get { return menuId; } }
        public string MenuLabel { get { return menuLabel; } }
    }
}