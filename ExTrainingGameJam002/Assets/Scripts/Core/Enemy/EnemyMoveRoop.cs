using EnemyMove.PlayerFollow;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StageSystem;

namespace EnemyMove.RoundTrip
{
    public class EnemyMoveRoop : MonoBehaviour
    {
        [SerializeField] Transform _player; // �v���C���[
        [SerializeField] List<Transform> _targetList; // �ǔ��Ώۂ̈ʒu���X�g

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
            _currentTarget = _targetList[0].position; // �����ڕW�n�_��_targetList�̐擪�ɐݒ�
            _nextPoint = _myTransform.position;
            _navMeshPath = new NavMeshPath();
            agent = GetComponent<NavMeshTotalDistanceCheck>(); //agent��NavMeshAgent2D���擾
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
                        frameCount = 0; // �t���[���������Z�b�g
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