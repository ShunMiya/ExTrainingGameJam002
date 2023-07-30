using UnityEngine;
using TMPro;
using System.Collections;
using UISystem;

namespace TimerSystem
{
    public class TimerController : MonoBehaviour
    {
        public float totalTime; // �������ԁi�b�j
        private float currentTime; // ���݂̎c�莞��
        private TextMeshProUGUI textMeshPro;
        [SerializeField]private PauseSystem pauseSystem;

        void Start()
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();

            // �V�[���ǂݍ��݌�A1�b�ҋ@���Ă���J�E���g�_�E���J�n
            StartCoroutine(StartCountdownAfterDelay());
        }

        IEnumerator StartCountdownAfterDelay()
        {
            yield return new WaitForSeconds(0.5f); // 1�b�ҋ@
            currentTime = totalTime; // �J�E���g�_�E���J�n
            StartCoroutine(UpdateTimer());
        }

        IEnumerator UpdateTimer()
        {
            while (currentTime > 0f)
            {
                // �^�C�}�[���J�E���g�_�E������
                currentTime -= Time.deltaTime;

                // �c�莞�Ԃ𕪁A�b�A�~���b�ɕϊ����Č����𒲐����ĕ\��
                int minutes = Mathf.FloorToInt(currentTime / 60f);
                int seconds = Mathf.FloorToInt(currentTime % 60f);
                int milliseconds = Mathf.FloorToInt((currentTime * 100) % 100);

                string timeText = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
                textMeshPro.text = timeText;

                yield return null;
            }

            textMeshPro.text = "00:00:00";
            // �^�C���A�b�v���̏���
            pauseSystem.GameOver();
        }
    }
}