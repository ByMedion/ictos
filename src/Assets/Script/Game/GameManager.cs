using Script.Game.SwipeDetection;
using TMPro;
using UnityEngine;

namespace Script.Game
{
    public class GameManager : MonoBehaviour
    {
        private int score;

        [SerializeField] private TextMeshProUGUI scoreText;
        public static GameManager instance { get; private set; }

        [field: SerializeField] public PlayerController playerController { get; }

        [field: SerializeField] public WallController wallController { get; }

        [field: SerializeField] public BackgroundController backgroundController { get; }

        [field: SerializeField] public SwipeDetector swipeDetector { get; }

        private void Awake()
        {
            instance = this;

            wallController.wallsGenerated += OnWallsGenerated;
            playerController.onGameOver += OnGameOver;
        }

        private void OnGameOver()
        {
            Global.IsAlive = false;
            if (score > Global.Record)
            {
                Global.Record = score;
                PlayerPrefs.SetInt("BestScore", score);
            }
        }

        private void OnWallsGenerated()
        {
            backgroundController.SetRandomColor();
            score++;
            scoreText.text = "Score: " + score;
        }
    }
}