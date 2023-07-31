using UnityEngine;

namespace TimerSystem
{
    public class TimerOn : MonoBehaviour
    {
        public TimerController tc;

        private void Start()
        {
            tc = FindObjectOfType<TimerController>();
        }
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                tc.TimerStart();
            }
        }
    }
}