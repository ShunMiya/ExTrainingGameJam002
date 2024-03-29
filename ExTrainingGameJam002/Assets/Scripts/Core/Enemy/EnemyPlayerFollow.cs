using StageSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyMove.PlayerFollow
{
    public class EnemyPlayerFollow : MonoBehaviour
    {
        [SerializeField] Transform _player; // プレイヤー

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
        private GetHolyWater getHolyWater;


        public void Awake()
        {
            _myTransform = transform;
            _playerTransform = _player.transform;
            _currentTarget = _playerTransform.position;
            _nextPoint = _myTransform.position;
            _navMeshPath = new NavMeshPath();
            agent = GetComponent<NavMeshTotalDistanceCheck>(); //agentにNavMeshAgent2Dを取得
            getHolyWater = FindObjectOfType<GetHolyWater>();
        }

        public void Update()
        {
            if (getHolyWater.GetUndetectable() == true) return;
            
            _elapsed += Time.deltaTime;
            if (_elapsed > _reCalcTime)
            {
                _elapsed = 0;
                
                agentPlayer();
                _calcedPlayerPos = _playerTransform.localPosition;

                _currentTarget = agent.GetTotalDistance() <= 1000f ? _calcedPlayerPos : _currentTarget;

                NestStep();
            }

            Vector2 currentPos = _myTransform.localPosition;
            if (Vector2.Distance(_nextPoint, currentPos) < _minDistance)
            {
                if (_navMeshCorners.Count == 0)
                {
                    _nextPoint = _myTransform.localPosition;
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

    /// <summary>
    /// <see cref="Queue{T}"/> の拡張機能を定義します。
    /// </summary>
    public static class QueueExtension
    {
        public static void EnqueueRange<T>(this Queue<T> self, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                self.Enqueue(item);
            }
        }
    }
}