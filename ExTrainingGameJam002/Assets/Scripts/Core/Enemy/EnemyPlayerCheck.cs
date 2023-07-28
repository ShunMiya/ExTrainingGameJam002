using EnemyMove.PlayerFollow;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyMove.RoundTrip
{
    public class EnemyPlayerCheck : MonoBehaviour
    {
        [SerializeField] Transform _player; // �v���C���[
        [SerializeField] Transform _targetBeta; // ��ڂ̒ǔ��Ώۂ̈ʒu
        [SerializeField] Transform _targetGamma; // �O�ڂ̒ǔ��Ώۂ̈ʒu

        [SerializeField] float _moveSpeed = 1f; // �ړ����x
        [SerializeField] float _minDistance = 0.05f; // �ǂꂭ�炢�߂Â����玟�̃|�C���g�Ɉڂ邩
        [SerializeField] float _reCalcTime = 0.5f; // �v���C���[�ւ̌o�H�Čv�Z������Ԋu

        private Transform _playerTransform; //�v���C���[�̈ʒu
        NavMeshAgent2D agent;

        private Vector2 _currentTarget; // ���݂̒ǔ��Ώۂ̈ʒu                            
        private Vector2 _nextPoint;// ���̈ړ���
        private Transform _myTransform;
        private NavMeshPath _navMeshPath;
        private Queue<Vector3> _navMeshCorners = new();
        private Vector3 _calcedPlayerPos;
        private float _elapsed;
        private bool TargetBeta;

        public void Awake()
        {
            _myTransform = transform;
            _playerTransform = _player.transform;
            _currentTarget = _targetBeta.position; // �����ڕW�n�_�����ɐݒ�
            _nextPoint = _myTransform.position;
            _navMeshPath = new NavMeshPath();
            agent = GetComponent<NavMeshAgent2D>(); //agent��NavMeshAgent2D���擾
        }

        public void Update()
        {
            _elapsed += Time.deltaTime;
            if (_elapsed > _reCalcTime)
            {
                _elapsed = 0;

                //agentPlayer();
                _calcedPlayerPos = _playerTransform.localPosition;

                /*if(agent.GetTotalDistanceTraveled() <= 5f)
                {
                    _currentTarget = _calcedPlayerPos;
                }else _currentTarget = _targetBeta.position;*/

                NestStep();
            }


            Vector2 currentPos = _myTransform.localPosition;
            if (Vector2.Distance(_nextPoint, currentPos) < _minDistance)
            {
                if (_navMeshCorners.Count == 0)
                {
                    if(TargetBeta == true)
                    {
                        _currentTarget = _targetGamma.position;
                        _nextPoint = _myTransform.localPosition;
                        TargetBeta = false;
                    }else
                    {
                        _currentTarget = _targetBeta.position;
                        _nextPoint = _myTransform.localPosition;
                        TargetBeta = true;
                    }
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