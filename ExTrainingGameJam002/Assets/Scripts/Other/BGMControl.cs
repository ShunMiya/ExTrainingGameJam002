using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem
{
    public class BGMControl : MonoBehaviour
    {
        [SerializeField] private AudioClip AreaBGM;
        [SerializeField] private AudioClip ClearBGM;
        [SerializeField] private AudioClip DeadBGM;
        [SerializeField] private float AreaVolume;
        [SerializeField] private float ClearVolume;
        [SerializeField] private float DeadVolume;

        public float backSpeed = 1.0f; // 再生速度を指定
        public float AreaSpeed = 1.0f; // 再生速度を指定

        private AudioSource audioSource;
        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void AreaStart()
        {
            audioSource.pitch = AreaSpeed;
            audioSource.Stop();
            audioSource.PlayOneShot(AreaBGM, AreaVolume);
        }

        public void GameClear()
        {
            audioSource.pitch = backSpeed;
            audioSource.Stop();
            audioSource.PlayOneShot(ClearBGM, ClearVolume);
        }

        public void GameDead()
        {
            audioSource.pitch = backSpeed;
            audioSource.Stop();
            audioSource.PlayOneShot(DeadBGM, DeadVolume);
        }
    }
}