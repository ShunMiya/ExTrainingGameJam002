using UnityEngine;
using TMPro;
using System.Collections;
using UISystem;

namespace TimerSystem
{
    public class TimerController : MonoBehaviour
    {
        public float totalTime; // 制限時間（秒）
        public float currentTime; // 現在の残り時間
        private TextMeshProUGUI textMeshPro;
        [SerializeField]private PauseSystem pauseSystem;
        [SerializeField] private ResultTime resultText;
        private string timetext;

        void Start()
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        public void TimerStart()
        {
            // シーン読み込み後、1秒待機してからカウントダウン開始
            StartCountdownAfterDelay();
        }

        public void StartCountdownAfterDelay()
        {
            currentTime = totalTime; // カウントダウン開始
            StartCoroutine(UpdateTimer());
        }

        IEnumerator UpdateTimer()
        {
            while (currentTime > 0f)
            {
                // タイマーをカウントダウンする
                currentTime -= Time.deltaTime;

                // 残り時間を分、秒、ミリ秒に変換して桁数を調整して表示
                int minutes = Mathf.FloorToInt(currentTime / 60f);
                int seconds = Mathf.FloorToInt(currentTime % 60f);
                int milliseconds = Mathf.FloorToInt((currentTime * 100) % 100);

                string timeText = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
                textMeshPro.text = timeText;

                timetext = timeText;

                yield return null;
            }

            // タイムアップ時の処理
            textMeshPro.text = "00:00:00";
            pauseSystem.GameOver();
        }

        public void GoalTime()
        {
            resultText.SetText(timetext);
        }
    }
}