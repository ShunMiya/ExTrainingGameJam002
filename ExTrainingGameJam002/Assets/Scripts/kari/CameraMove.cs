using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StageSystem
{
    public class CameraMove : MonoBehaviour
    {
        public List<Transform> cameraPoints; // カメラが移動するポイントのリスト
        public List<float> zoomValue;
        public int currentPointIndex = 0; // 現在のポイントのインデックス

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
                Debug.LogWarning("カメラの移動ポイントが設定されていません。");
                yield break;
            }

            // 次のポイントのインデックスを計算
            //currentPointIndex = (currentPointIndex + 1) % cameraPoints.Count;

            // 現在の位置
            Vector3 startPos = transform.position;

            // 目標の位置
            Vector3 targetPos = cameraPoints[currentPointIndex].position;

            // 移動開始時刻
            float startTime = Time.time;

            // 移動が終わるまでの経過時間
            float elapsedTime = 0f;

            // 移動処理
            while (elapsedTime < moveDuration)
            {
                elapsedTime = Time.time - startTime;

                // 現在の位置と目標の位置の間を滑らかに補間
                transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / moveDuration);

                // ズーム値を補間してカメラに適用
                cam.orthographicSize = Mathf.Lerp(zoomValue[currentPointIndex - 1], zoomValue[currentPointIndex], elapsedTime / moveDuration);

                // 1フレーム待機
                yield return null;
            }

            // 目標位置に補正
            transform.position = targetPos;

            // カメラの注視点を次のポイントに合わせる
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