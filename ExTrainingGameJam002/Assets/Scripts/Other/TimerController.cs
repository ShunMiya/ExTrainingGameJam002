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

        void Start()
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();

            // シーン読み込み後、1秒待機してからカウントダウン開始
            StartCoroutine(StartCountdownAfterDelay());
        }

        IEnumerator StartCountdownAfterDelay()
        {
            yield return new WaitForSeconds(0.5f); // 1秒待機
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

                yield return null;
            }

            textMeshPro.text = "00:00:00";
            // タイムアップ時の処理
            pauseSystem.GameOver();
        }
    }
}