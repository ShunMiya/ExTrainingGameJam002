using StageSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyMove.PlayerFollow
{
    public class EnemyPlayerFollow : MonoBehaviour
    {
        [SerializeField] Transform _player; // �v���C���[

        [SerializeField] float _moveSpeed = 1f; // �ړ����x
        [SerializeField] float _minDistance = 0.05f; // �ǂꂭ�炢�߂Â����玟�̃|�C���g�Ɉڂ邩
        [SerializeField] float _reCalcTime = 0.1f; // �v���C���[�ւ̌o�H�Čv�Z������Ԋu

        private Transform _playerTransform; //�v���C���[�̈ʒu
        NavMeshTotalDistanceCheck agent;

        private Vector2 _currentTarget; // ���݂̒ǔ��Ώۂ̈ʒu                            
        private Vector2 _nextPoint;// ���̈ړ���
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
            agent = GetComponent<NavMeshTotalDistanceCheck>(); //agent��NavMeshAgent2D���擾
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
    /// <see cref="Queue{T}"/> �̊g���@�\���`���܂��B
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