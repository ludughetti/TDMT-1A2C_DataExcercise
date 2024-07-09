using UnityEngine;
using DataSource;

namespace Navigation
{
    [CreateAssetMenu(fileName = "MenuSource", menuName = "DataSource/MenuSource")]
    public class MenuDataSource : DataSource<Menu>
    {
        [SerializeField] private string _menuId;
        [SerializeField] private string _menuLabel;

        public string MenuId { get { return _menuId; } }
        public string MenuLabel { get { return _menuLabel; } }
    }
}