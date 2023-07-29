using UnityEngine;
using UnityEngine.AI;

public class NavMeshTotalDistanceCheck : MonoBehaviour
{
    [HideInInspector]//���Unity�G�f�B�^�����\��
    private Vector2 trace_area = Vector2.zero;

    private float totalDistance = 0f;
    public Vector2 destination
    {
        get { return trace_area; }
        set
        {
            trace_area = value;
            CalculateTotalDistance();
        }
    }
    // �v���p�e�B�Œ���destination��ݒ肵���ꍇ�ɂ��K�v�Ȉړ��������v�Z���Ċi�[����
    private void CalculateTotalDistance()
    {
        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);

        totalDistance = 0f;
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            totalDistance += Vector2.Distance(path.corners[i], path.corners[i + 1]);
        }
    }

    // �ړI�n���O������ݒ肷�邽�߂̃��\�b�h
    public void SetDestination(Vector2 target)
    {
        destination = target;
    }

    public float GetTotalDistance()
    {
        return totalDistance;
    }
}