using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Navigation
{
    public class ButtonController : MonoBehaviour
    {
        public event Action<string> OnClick;

        private Button _button;
        private string _id;
        private TMP_Text _text;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _text = GetComponentInChildren<TMP_Text>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(TriggerOnClickEvent);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(TriggerOnClickEvent);
        }

        public void InitializeButton(string label, string id, Action<string> onClick)
        {
            name = label;
            _text.text = label;
            _id = id;
            OnClick = onClick;
        }

        private void TriggerOnClickEvent()
        {
            OnClick?.Invoke(_id);
        }
    }
}