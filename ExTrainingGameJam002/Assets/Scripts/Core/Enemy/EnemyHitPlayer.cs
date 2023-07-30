using UnityEngine;
using UISystem;

namespace Enemy
{
    public class EnemyHitPlayer : MonoBehaviour
    {
        [SerializeField] private PauseSystem pauseSystem;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                pauseSystem.GameOver();
            }
        }
    }
}
