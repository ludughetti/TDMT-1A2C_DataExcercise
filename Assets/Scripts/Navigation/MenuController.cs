using System.Collections.Generic;
using UnityEngine;

namespace Navigation
{
    // This class handles each menu's creation and setup
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private List<MenuDataSource> menus;
        [SerializeField] private string defaultMenu;
        [SerializeField] private string playMenu;
        [SerializeField] private string exitMenu;
        //[SerializeField] private GameManagerDataSource gameManagerDataSource;

        private Dictionary<string, MenuDataSource> _menusById;
        string _currentMenuId;

        private void Start()
        {
            SetupMenusById();
        }

        private void SetupMenusById()
        {
            foreach (MenuDataSource menu in menus)
            {
                menu.DataInstance.InitializeMenu();
                menu.DataInstance.OnMenuChange += HandleMenuChange;
                menu.DataInstance.gameObject.SetActive(false);

                _menusById.TryAdd(menu.MenuId, menu);
            }
        }

        private void HandleMenuChange(string nextMenuId)
        {
            // Check if play or exit, else turn on/off
                // If play/exit -> call game manager

            // Turn off current menu and turn on next menu only if both exist
            if (_menusById.TryGetValue(_currentMenuId, out MenuDataSource currentMenu)
                && _menusById.TryGetValue(nextMenuId, out MenuDataSource nextMenu))
            {
                currentMenu.DataInstance.gameObject.SetActive(false);
                nextMenu.DataInstance.gameObject.SetActive(true);

                _currentMenuId = nextMenuId;
            } else
            {
                Debug.Log($"{name}: Menus {_currentMenuId} or {nextMenuId} were not found in the menu dictionary.");
            }
        }
    }
}

