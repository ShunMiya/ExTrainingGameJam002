using UnityEngine;
using Player;

namespace StageSystem
{
    public class NextStageSysmtem : MonoBehaviour
    {
        private AreaClear areaClear;
        private bool NextPoint = false;
        [SerializeField]private bool StageClear = false;

        // Start is called before the first frame update
        void Start()
        {
            areaClear = FindObjectOfType<AreaClear>();
        }

        // Update is called once per frame
        void Update()
        {
            if(NextPoint) areaClear.AreaClearPointUpdate(NextPoint,StageClear);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                // PlayerTagのオブジェクトが接触した場合、AllMapPointをtrueにする
                NextPoint = true;
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                // PlayerTagのオブジェクトが離れた場合、AllMapPointをfalseにする
                NextPoint = false;
            }
        }
    }
}