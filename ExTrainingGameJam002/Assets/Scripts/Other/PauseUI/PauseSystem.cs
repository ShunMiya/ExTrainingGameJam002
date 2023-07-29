using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    public class PauseSystem : MonoBehaviour
    {
        [SerializeField] private GameObject pauseUI;
        [SerializeField] private GameObject GameOverUI;

        private void Start()
        {
            Time.timeScale = 1f;
            pauseUI.SetActive(false);
            GameOverUI.SetActive(false);
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
            Time.timeScale = 0f;
            GameOverUI.SetActive(true);
        }
    }
}