using UnityEngine;
using UnityEngine.SceneManagement;
using SoundSystem;
using TimerSystem;

namespace UISystem
{
    public class PauseSystem : MonoBehaviour
    {
        private BGMControl bg;
        [SerializeField] private GameObject pauseUI;
        [SerializeField] private GameObject GameOverUI;
        [SerializeField] private GameObject GameClearUI;
        private TimerController tmc;

        private void Start()
        {
            tmc = FindObjectOfType<TimerController>();
            bg = FindObjectOfType<BGMControl>();
            Time.timeScale = 1f;
            pauseUI.SetActive(false);
            GameOverUI.SetActive(false);
            GameClearUI.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (GameOverUI.activeSelf) return;

            if(Input.GetKeyDown("p"))
            {
                pauseUI.SetActive(!pauseUI.activeSelf);

                Time.timeScale = pauseUI.activeSelf ? 0f : 1f;
            }
        }

        public void GameOver()
        {
            bg.GameDead();
            Time.timeScale = 0f;
            GameOverUI.SetActive(true);
        }

        public void GameClear()
        {
            bg.GameClear();
            Time.timeScale = 0f;
            GameClearUI.SetActive(true);
            tmc.GoalTime();
        }

        public void LoadStart()
        {
            Time.timeScale = 1f;
            pauseUI.SetActive(false);
            GameOverUI.SetActive(false);
            GameClearUI.SetActive(false);

        }

        public void RetryGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}