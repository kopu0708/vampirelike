using UnityEngine;

public class Scaner : MonoBehaviour
{
    public float ScanRadius = 5f; // ��ĵ �ݰ�
    public LayerMask TargetLayer; // ��ĵ�� ��� ���̾�
    public RaycastHit2D[] targets; // ��ĵ�� ������ ������ �迭
    public Transform nearestTarget; // ���� ����� ���

    private void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, ScanRadius, Vector2.zero, 0,TargetLayer);// ��ĵ ��� ���̾ �ش��ϴ� ��� ������Ʈ�� ��ĵ�մϴ�. ���� ����� ����� `nearestTarget`�� ����˴ϴ�.
        nearestTarget = GetNearest(); // ���� ����� ��� ������Ʈ
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

            if(distance < diff) // ���� ����� ���� ����� ��󺸴� ����� ���
            {
                diff = distance; // ���� ����� �Ÿ� ������Ʈ
                result = target.transform; // ���� ����� ��� ������Ʈ
                
            }
        }
        return result;
    }
}

