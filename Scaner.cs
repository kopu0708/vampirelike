using UnityEngine;

public class Scaner : MonoBehaviour
{
    public float ScanRadius = 5f; // 스캔 반경
    public LayerMask TargetLayer; // 스캔할 대상 레이어
    public RaycastHit2D[] targets; // 스캔된 대상들을 저장할 배열
    public Transform nearestTarget; // 가장 가까운 대상

    private void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, ScanRadius, Vector2.zero, 0,TargetLayer);// 스캔 대상 레이어에 해당하는 모든 오브젝트를 스캔합니다. 가장 가까운 대상은 `nearestTarget`에 저장됩니다.
        nearestTarget = GetNearest(); // 가장 가까운 대상 업데이트
    }
    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;
        foreach (RaycastHit2D target in targets) 
        {
            Vector3 Mypos = transform.position;
            Vector3 targetPos = target.transform.position;
            float distance = Vector3.Distance(Mypos, targetPos);

            if(distance < diff) // 현재 대상이 가장 가까운 대상보다 가까운 경우
            {
                diff = distance; // 가장 가까운 거리 업데이트
                result = target.transform; // 가장 가까운 대상 업데이트
                
            }
        }
        return result;
    }
}

