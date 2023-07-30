using UnityEngine;

namespace StageSystem
{
    public class MapPoint : MonoBehaviour
    {
        public bool AllMapPoint = false;
        public CameraMove cameraMove;

        public void Update()
        {
            cameraMove.AllMapPointUpdate(AllMapPoint);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                // PlayerTag�̃I�u�W�F�N�g���ڐG�����ꍇ�AAllMapPoint��true�ɂ���
                AllMapPoint = true;
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                // PlayerTag�̃I�u�W�F�N�g�����ꂽ�ꍇ�AAllMapPoint��false�ɂ���
                AllMapPoint = false;
            }
        }
    }
}