using EnemyMove.PlayerFollow;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StageSystem;

namespace EnemyMove.RoundTrip
{
    public class EnemyMoveRoop : MonoBehaviour
    {
        [SerializeField] Transform _player; // プレイヤー
        [SerializeField] List<Transform> _targetList; // 追尾対象の位置リスト

        [SerializeField] float _moveSpeed = 1f; // 移動速度
        [SerializeField] float _minDistance = 0.05f; // どれくらい近づいたら次のポイントに移るか
        [SerializeField] float _reCalcTime = 0.1f; // プレイヤーへの経路再計算をする間隔

        private Transform _playerTransform; //プレイヤーの位置
        NavMeshTotalDistanceCheck agent;

        private Vector2 _currentTarget; // 現在の追尾対象の位置                            
        private Vector2 _nextPoint;// 次の移動先
        private Transform _myTransform;
        private NavMeshPath _navMeshPath;
        private Queue<Vector3> _navMeshCorners = new();
        private Vector3 _calcedPlayerPos;
        private float _elapsed;
        private int _currentTargetIndex = 0;
        private GetHolyWater getHolyWater;
        private AudioSource auso;
        [SerializeField] private AudioClip SE;
        private int frameCount = 0;
        [SerializeField] private int framesBetweenSounds;
        [SerializeField] private int SErange;


        public void Awake()
        {
            _myTransform = transform;
            _playerTransform = _player.transform;
            _currentTarget = _targetList[0].position; // 初期目標地点を_targetListの先頭に設定
            _nextPoint = _myTransform.position;
            _navMeshPath = new NavMeshPath();
            agent = GetComponent<NavMeshTotalDistanceCheck>(); //agentにNavMeshAgent2Dを取得
            getHolyWater = FindObjectOfType<GetHolyWater>();
            auso = GetComponent<AudioSource>();
        }

        public void Update()
        {
            _elapsed += Time.deltaTime;
            if (_elapsed > _reCalcTime)
            {
                _elapsed = 0;

                agentPlayer();
                _calcedPlayerPos = _playerTransform.localPosition;

                if (getHolyWater.GetUndetectable() == false)
                _currentTarget = agent.GetTotalDistance() <= 5f ? _calcedPlayerPos : _currentTarget;

                if (agent.GetTotalDistance() <= SErange)
                {
                    frameCount++;

                    if (frameCount >= framesBetweenSounds)
                    {
                        auso.PlayOneShot(SE);
                        frameCount = 0; // フレーム数をリセット
                    }
                }

                NestStep();
            }


            Vector2 currentPos = _myTransform.localPosition;
            if (Vector2.Distance(_nextPoint, currentPos) < _minDistance)
            {
                if (_navMeshCorners.Count == 0)
                {
                    _currentTargetIndex++;
                    if (_currentTargetIndex > _targetList.Count) _currentTargetIndex = 0;

                    _currentTarget = _targetList[_currentTargetIndex].position;
                    _nextPoint = _myTransform.localPosition;
                    _elapsed = _reCalcTime;
                    return;
                }
                _nextPoint = _navMeshCorners.Dequeue();
            }

            Vector2 diff = _nextPoint - currentPos;
            if (diff == Vector2.zero)
            {
                return;
            }

            Vector2 step = _moveSpeed * Time.deltaTime * diff.normalized;
            _myTransform.Translate(step);
        }

        private void agentPlayer()
        {
            agent.SetDestination(_player.position);
        }

        private void NestStep()
        {
            bool isOk = NavMesh.CalculatePath(_myTransform.position,
                _currentTarget, NavMesh.AllAreas, _navMeshPath);
            if (!isOk)
            {
                Debug.LogWarning("Failed to NavMesh.CalculatePath.", this);
                return;
            }

            _navMeshCorners.Clear();
            _navMeshCorners.EnqueueRange(_navMeshPath.corners);
            _nextPoint = _myTransform.localPosition;
        }
    }
}