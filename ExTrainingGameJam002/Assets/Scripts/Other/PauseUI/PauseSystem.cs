using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    public class PauseSystem : MonoBehaviour
    {
        [SerializeField] private GameObject pauseUI;

        private void Start()
        {
            pauseUI.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown("p"))
            {
                pauseUI.SetActive(!pauseUI.activeSelf);

                Time.timeScale = pauseUI.activeSelf ? 0f : 1f;
            }
        }
    }
}