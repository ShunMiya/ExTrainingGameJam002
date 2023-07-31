using UnityEngine;
using Player;

namespace StageSystem
{
    public class NextStageSysmtem : MonoBehaviour
    {
        private AreaClear areaClear;
        public bool NextPoint = false;

        // Start is called before the first frame update
        void Start()
        {
            areaClear = FindObjectOfType<AreaClear>();
        }

        // Update is called once per frame
        void Update()
        {
            areaClear.AreaClearPointUpdate(NextPoint);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                // PlayerTag�̃I�u�W�F�N�g���ڐG�����ꍇ�AAllMapPoint��true�ɂ���
                NextPoint = true;
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                // PlayerTag�̃I�u�W�F�N�g�����ꂽ�ꍇ�AAllMapPoint��false�ɂ���
                NextPoint = false;
            }
        }
    }
}