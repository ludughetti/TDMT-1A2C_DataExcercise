using System.Collections.Generic;
using Gameplay;
using UnityEngine;

namespace Navigation
{
    // This class handles each menu's creation and setup
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameControllerDataSource gameControllerDataSource;
        [SerializeField] private List<MenuDataSource> menus;
        [SerializeField] private string defaultMenu;
        [SerializeField] private string playMenu;
        [SerializeField] private string exitMenu;

        private Dictionary<string, MenuDataSource> _menusById = new();
        private GameController _gameController;
        private string _currentMenuId;

        private void Start()
        {
            SetupMenusById();

            if (gameControllerDataSource != null)
                _gameController = gameControllerDataSource.DataInstance; 
        }

        private void SetupMenusById()
        {
            foreach (MenuDataSource menu in menus)
            {
                menu.DataInstance.InitializeMenu();
                menu.DataInstance.OnMenuChange += HandleMenuChange;

                if(menu.MenuId != defaultMenu)
                    menu.DataInstance.gameObject.SetActive(false);
                else
                    _currentMenuId = menu.MenuId;

                Debug.Log($"MenuDataSource: id->{menu.MenuId}, label->{menu.MenuLabel}, isActive->{menu.DataInstance.gameObject.activeSelf}");

                _menusById.TryAdd(menu.MenuId, menu);
            }
        }

        private void HandleMenuChange(string nextMenuId)
        {
            MenuDataSource currentMenu;

            // Check if play or exit
            if (nextMenuId == playMenu)
            {
                // Trigger Coroutine to load next level
                _gameController.TriggerNextLevel(nextMenuId);

                // Turn off current menu so it's not displayed as the game loads up
                _menusById.TryGetValue(_currentMenuId, out currentMenu);
                currentMenu.DataInstance.gameObject.SetActive(false);

                return;
            }
            else if (nextMenuId == exitMenu)
            {
                _gameController.TriggerNextLevel(nextMenuId);
                return;
            }

            // Turn off current menu and turn on next menu only if both exist
            if (_menusById.TryGetValue(_currentMenuId, out currentMenu)
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

