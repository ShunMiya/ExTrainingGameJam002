using System.Collections;
using UnityEngine;
using StageSystem;

namespace Player
{
    public class AreaClear : MonoBehaviour
    {
        public bool HaveKey = false;
        private bool AreaClearPoint = false;
        private CameraMove cameraMove;

        private SpriteRenderer playerSpriteRenderer;

        public float moveDuration = 1f;
        public float yOffset = 2f;

        public void Start()
        {
            cameraMove = FindObjectOfType<CameraMove>();
            playerSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!AreaClearPoint) return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!HaveKey) return;

                cameraMove.PointUpdate();

                // �v���C���[����Ɉړ�������R���[�`�����J�n
                StartCoroutine(MovePlayer());
            }

        }

        private IEnumerator MovePlayer()
        {
            // �ړ��J�n���̓����x��ۑ�
            float startAlpha = playerSpriteRenderer.color.a;
            Vector3 startPosition = transform.position;

            // �ړ����͓����x��0�ɂ���
            Color startColor = playerSpriteRenderer.color;
            playerSpriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, 0f);

            // �v���C���[����Ɉړ�������
            Vector3 targetPosition = transform.position + Vector3.up * yOffset;
            float elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                float t = elapsedTime / moveDuration;
                transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // �ړ����I������瓧���x�����ɖ߂�
            playerSpriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, startAlpha);
            HaveKey = false;
        }

        public void AreaClearPointUpdate(bool data)
        {
            AreaClearPoint = data;
        }

        public void HaveKeyUpdate()
        {
            HaveKey = true;
        }
    }
}
