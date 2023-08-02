using System.Collections;
using UnityEngine;

namespace StageSystem
{
    public class GetHolyWater : MonoBehaviour
    {
        private bool Undetectable = false;
        private AudioSource auso;
        [SerializeField] private AudioClip Water;

        private void Start()
        {
            auso = GetComponent<AudioSource>();
        }

        public void UndetectableTime()
        {
            auso.PlayOneShot(Water);
            // 10•bŒã‚ÉUndetectableƒtƒ‰ƒO‚ðfalse‚É‚·‚é
            StartCoroutine(StartUndetectableTimer());
        }

        private IEnumerator StartUndetectableTimer()
        {
            Undetectable = true;

            yield return new WaitForSeconds(10f);

            Undetectable = false;
        }

        public bool GetUndetectable()
        { return Undetectable; }
    }
}