using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor;

namespace TitleScene
{
    public class FadeScene : MonoBehaviour
    {
        public Image fadeImage;
        public Image PauseFadeImage;
        public float fadeOutDuration;
        public float fadeInDuration;
        public float waitTime = 0.2f;
        public string scene;

        private void Start()
        {
            fadeImage.gameObject.SetActive(true);
            StartCoroutine(FadeIn());
        }

        public void SceneJump()
        {
            StartCoroutine(TransitionSeq(1));
        }

        public void SceneJumpButtonClick()
        {
            StartCoroutine(TransitionSeq(2));
        }

        public void GameCloseButtonClick()
        {
            StartCoroutine(GameClose());
        }

        private IEnumerator TransitionSeq(int a)
        {
            if(a == 1) yield return StartCoroutine(FadeOut());
            else if(a == 2) yield return StartCoroutine(FadeOutButton());
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
                st += Time.unscaledDeltaTime / waitTime;
                yield return null;
            }

            float t = 0f;
            while (t < 1f)
            {
                t += Time.unscaledDeltaTime / fadeInDuration;
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
                t += Time.unscaledDeltaTime / fadeOutDuration;
                fadeImage.color = Color.Lerp(startColor, endColor, t);
                yield return null;
            }
        }

        private IEnumerator FadeOutButton()
        {
            PauseFadeImage.gameObject.SetActive(true);

            float t = 0f;
            Color startColor = Color.clear;
            Color endColor = Color.black;

            while (t < 1f)
            {
                t += Time.unscaledDeltaTime / fadeOutDuration;
                PauseFadeImage.color = Color.Lerp(startColor, endColor, t);
                yield return null;
            }
        }
    }
}