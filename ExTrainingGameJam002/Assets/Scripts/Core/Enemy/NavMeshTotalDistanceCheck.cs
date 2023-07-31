using UnityEngine;
using UnityEngine.AI;

public class NavMeshTotalDistanceCheck : MonoBehaviour
{
    [HideInInspector]//常にUnityエディタから非表示
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
    // プロパティで直接destinationを設定した場合にも必要な移動距離を計算して格納する
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

    // 目的地を外部から設定するためのメソッド
    public void SetDestination(Vector2 target)
    {
        destination = target;
    }

    public float GetTotalDistance()
    {
        return totalDistance;
    }
}