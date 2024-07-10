using TMPro;
using UnityEngine;

namespace Navigation
{
    public class EndgameResult : MonoBehaviour
    {
        [SerializeField] private EndgameResultDataSource selfSource;
        [SerializeField] private TMP_Text resultSection;
        [SerializeField] private string victoryText = "You won!";
        [SerializeField] private string defeatText = "You lost!";

        private void Awake()
        {
            selfSource.DataInstance = this;
        }

        public void HandleEndgameResult(bool isVictory)
        {
            resultSection.text = isVictory ? victoryText : defeatText;
        }
    }
}