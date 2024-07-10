using System.Collections.Generic;
using Events;
using Gameplay;
using UnityEngine;

namespace Navigation
{
    // This class handles each menu's creation and setup
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameControllerDataSource gameControllerDataSource;
        [SerializeField] private List<MenuDataSource> menus;
        [SerializeField] private MenuDataSource defaultMenu;
        [SerializeField] private MenuDataSource playMenu;
        [SerializeField] private MenuDataSource exitMenu;
        [SerializeField] private BoolEventChannel endgameEventChannel;
        [SerializeField] private MenuDataSource endgameMenu;
        [SerializeField] private EndgameResultDataSource endgameResultDataSource;

        private Dictionary<string, MenuDataSource> _menusById = new();
        private GameController _gameController;
        private string _currentMenuId;

        private void OnEnable()
        {
            if (endgameEventChannel != null)
                endgameEventChannel.Subscribe(HandleEndgameMenu);
        }

        private void OnDisable()
        {
            if (endgameEventChannel != null)
                endgameEventChannel.Unsubscribe(HandleEndgameMenu);
        }

        private void Start()
        {
            SetupMenusById();

            if (gameControllerDataSource != null)
                _gameController = gameControllerDataSource.DataInstance; 

            if(endgameMenu != null)
                endgameMenu.DataInstance.gameObject.SetActive(false);
        }

        private void SetupMenusById()
        {
            foreach (MenuDataSource menu in menus)
            {
                menu.DataInstance.OnMenuChange += HandleMenuChange;

                if(menu.MenuId != defaultMenu.MenuId)
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
            if (nextMenuId == playMenu.MenuId)
            {
                // Trigger Coroutine to load next level
                _gameController.TriggerNextLevel(nextMenuId);

                // Turn off current menu so it's not displayed as the game loads up
                _menusById.TryGetValue(_currentMenuId, out currentMenu);
                currentMenu.DataInstance.gameObject.SetActive(false);

                return;
            }
            else if (nextMenuId == exitMenu.MenuId)
            {
                _gameController.QuitGame();
                return;
            }

            // Turn off current menu and turn on next menu only if both exist
            if (_menusById.TryGetValue(_currentMenuId, out currentMenu)
                && _menusById.TryGetValue(nextMenuId, out MenuDataSource nextMenu))
            {
                //currentMenu.DataInstance.gameObject.SetActive(false);
                nextMenu.DataInstance.gameObject.SetActive(true);

                _currentMenuId = nextMenuId;
            } else
            {
                Debug.Log($"{name}: Menus {_currentMenuId} or {nextMenuId} were not found in the menu dictionary.");
            }
        }

        private void HandleEndgameMenu(bool isVictory)
        {
            Debug.Log($"MenuDataSource: id->{endgameMenu.MenuId}, label->{endgameMenu.MenuLabel}, isActive->{endgameMenu.DataInstance.gameObject.activeSelf}");
            endgameMenu.DataInstance.gameObject.SetActive(true);
            endgameMenu.DataInstance.OnMenuChange += HandleMenuChange;
            endgameResultDataSource.DataInstance.HandleEndgameResult(isVictory);
        }
    }
}

