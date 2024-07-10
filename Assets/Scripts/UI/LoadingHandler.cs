using UnityEngine;
using UnityEngine.UI;

namespace Scenery
{
    public class LoadingHandler : MonoBehaviour
    {
        [SerializeField] private SceneryController sceneryController;
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private Slider barFill;

        private void OnEnable()
        {
            barFill.value = 0;

            sceneryController.OnLoadingScreenToggle += ToggleLoadingScreen;
            sceneryController.OnLoadingUpdate += UpdateFill;
        }

        private void OnDisable()
        {
            sceneryController.OnLoadingScreenToggle -= ToggleLoadingScreen;
            sceneryController.OnLoadingUpdate -= UpdateFill;
        }

        public void UpdateFill(float fillAmount)
        {
            Debug.Log($"Loading amount: {fillAmount}");
            barFill.value = fillAmount;
        }

        public void ToggleLoadingScreen(bool turnOn)
        {
            loadingScreen.SetActive(turnOn);
            if (!turnOn)
                barFill.value = 0;
        }
    }
}
