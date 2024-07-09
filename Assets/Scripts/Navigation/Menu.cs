using System;
using System.Collections.Generic;
using UnityEngine;

namespace Navigation
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private MenuDataSource selfMenu;
        [SerializeField] private List<MenuDataSource> subMenus;
        [SerializeField] private ButtonController buttonPrefab;
        [SerializeField] private Transform buttons;

        public event Action<string> OnMenuChange;

        private void Awake()
        {
            if (selfMenu != null)
            selfMenu.DataInstance = this;
        }

        public void InitializeMenu()
        {
            // For each subMenu in the list, create a button to redirect to that screen
            foreach (MenuDataSource subMenu in subMenus)
            {
                ButtonController subMenuButton = Instantiate(buttonPrefab, buttons);

                subMenuButton.InitializeButton(subMenu.MenuLabel, subMenu.MenuId, HandleButtonClick);
            }
        }

        private void HandleButtonClick(string id)
        {
            OnMenuChange?.Invoke(id);
        }
    }
}
