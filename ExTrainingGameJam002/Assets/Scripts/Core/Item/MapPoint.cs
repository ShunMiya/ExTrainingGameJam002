using UnityEngine;

namespace StageSystem
{
    public class MapPoint : MonoBehaviour
    {
        public bool AllMapPoint = false;
        private CameraMove cameraMove;

        private void Start()
        {
            cameraMove = FindObjectOfType<CameraMove>();
        }

        public void Update()
        {
            cameraMove.AllMapPointUpdate(AllMapPoint);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                // PlayerTagのオブジェクトが接触した場合、AllMapPointをtrueにする
                AllMapPoint = true;
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                // PlayerTagのオブジェクトが離れた場合、AllMapPointをfalseにする
                AllMapPoint = false;
            }
        }
    }
}