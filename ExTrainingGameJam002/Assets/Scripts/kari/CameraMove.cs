using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StageSystem
{
    public class CameraMove : MonoBehaviour
    {
        public List<Transform> cameraPoints; // �J�������ړ�����|�C���g�̃��X�g
        public List<float> zoomValue;
        public int currentPointIndex = 0; // ���݂̃|�C���g�̃C���f�b�N�X

        public float moveDuration = 1.0f;
        private Camera cam;

        public bool AllMapPoint = false;
        public bool AllMap = false;

        private AudioSource auso;
        [SerializeField] private AudioClip SE;

        private void Start()
        {
            auso = GetComponent<AudioSource>();
            cam = GetComponent<Camera>();
            transform.position = cameraPoints[currentPointIndex].position;
            cam.orthographicSize = zoomValue[currentPointIndex];
        }

        // Update is called once per frame
        void Update()
        {
            if (!AllMapPoint)
            {
                AllMap = false;
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(AllMap == false)
                {
                    AllMap = true;
                    auso.PlayOneShot(SE);
                    StartCoroutine(MoveToNextStage());
                }
                else
                {
                    AllMap = false;
                }
            }
        }

        public IEnumerator MoveToNextStage()
        {
            if (cameraPoints == null || cameraPoints.Count == 0)
            {
                Debug.LogWarning("�J�����̈ړ��|�C���g���ݒ肳��Ă��܂���B");
                yield break;
            }

            // ���̃|�C���g�̃C���f�b�N�X���v�Z
            //currentPointIndex = (currentPointIndex + 1) % cameraPoints.Count;

            // ���݂̈ʒu
            Vector3 startPos = transform.position;

            // �ڕW�̈ʒu
            Vector3 targetPos = cameraPoints[currentPointIndex].position;

            // �ړ��J�n����
            float startTime = Time.time;

            // �ړ����I���܂ł̌o�ߎ���
            float elapsedTime = 0f;

            // �ړ�����
            while (elapsedTime < moveDuration)
            {
                elapsedTime = Time.time - startTime;

                // ���݂̈ʒu�ƖڕW�̈ʒu�̊Ԃ����炩�ɕ��
                transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / moveDuration);

                // �Y�[���l���Ԃ��ăJ�����ɓK�p
                cam.orthographicSize = Mathf.Lerp(zoomValue[currentPointIndex - 1], zoomValue[currentPointIndex], elapsedTime / moveDuration);

                // 1�t���[���ҋ@
                yield return null;
            }

            // �ڕW�ʒu�ɕ␳
            transform.position = targetPos;

            // �J�����̒����_�����̃|�C���g�ɍ��킹��
            transform.LookAt(cameraPoints[currentPointIndex]);
        }

        public void PointUpdate()
        {
            currentPointIndex += 1;
        }
        public void AllMapPointUpdate(bool data)
        {
            AllMapPoint = data;
        }
    }
}