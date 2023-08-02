using UnityEngine;
using SoundSystem;

namespace TimerSystem
{
    public class TimerOn : MonoBehaviour
    {
        public TimerController tc;
        private BGMControl bg;
        private SaveCreate sv;

        private void Start()
        {
            bg = FindObjectOfType<BGMControl>();
            tc = FindObjectOfType<TimerController>();
            sv = FindObjectOfType<SaveCreate>();
        }
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                bg.AreaStart();
                tc.TimerStart();
                sv.SaveGame();

                Destroy(this);
            }
        }
    }
}