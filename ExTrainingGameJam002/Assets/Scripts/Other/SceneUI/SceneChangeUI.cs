using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor;

namespace TitleScene
{
    public class FadeOutScene : MonoBehaviour
    {
        public Image fadeImage;
        public float fadeOutDuration;
        public float fadeInDuration;
        public float waitTime = 0.2f;
        public string scene;

        private void Start()
        {
            fadeImage.gameObject.SetActive(true);
            StartCoroutine(FadeIn());
        }

        public void SceneJumpButtonClick()
        {
            StartCoroutine(TransitionSeq());
        }

        public void GameCloseButtonClick()
        {
            StartCoroutine(GameClose());
        }

        private IEnumerator TransitionSeq()
        {
            yield return StartCoroutine(FadeOut());
            SceneManager.LoadScene(scene);
        }

        private IEnumerator GameClose()
        {
            yield return StartCoroutine(FadeOut());

            EditorApplication.isPlaying = false; // UnityEditorClose NotAplicationClose
        }

        private IEnumerator FadeIn()
        {
            float st = 0f;
            Color startColor = Color.black;
            Color endColor = Color.clear;

            while (st < 1f)
            {
                st += Time.deltaTime / waitTime;
                yield return null;
            }

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / fadeInDuration;
                fadeImage.color = Color.Lerp(startColor, endColor, t);
                yield return null;
            }
            fadeImage.gameObject.SetActive(false);
        }

        private IEnumerator FadeOut()
        {
            fadeImage.gameObject.SetActive(true);

            float t = 0f;
            Color startColor = Color.clear;
            Color endColor = Color.black;

            while (t < 1f)
            {
                t += Time.deltaTime / fadeOutDuration;
                fadeImage.color = Color.Lerp(startColor, endColor, t);
                yield return null;
            }
        }
    }
}