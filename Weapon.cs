using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId; //무기 프리팹 ID
    public float damage; //무기의 데미지
    public int count; //무기의 개수
    public float Speed; //무기의 공격 속도
    PlayerControll player; // 플레이어 컨트롤러 참조
    float Timer; // 타이머 (필요시 사용)

    private void Awake()
    {
        player = GetComponentInParent<PlayerControll>(); // 부모 오브젝트에서 PlayerControll 컴포넌트 찾기
    }
    private void Start()
    {
        // 초기화 메소드 호출
        Init();
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * Speed * Time.deltaTime);
                break;
            default:
                Timer += Time.deltaTime; // 타이머 업데이트
                if(Timer >= Speed) // 타이머가 속도보다 크거나 같으면
                {
                    Timer = 0f; // 타이머 초기화
                    FIre(); // 총알 발사 메소드 호출
                }
                break;

        }
        //..test code..
        if(Input.GetKeyDown(KeyCode.Space))
        {
            LevelUp(damage + 1, count += 1,Speed + 10);
        }
    }
    
    public void LevelUp(float damage, int count,float Speed)
    {
        this.damage = damage;
        this.count = count;
        this.Speed = Speed; 

        if(id == 0) // 무기가 회전하는 경우
        {
            Batch();
        }
    }
    public void Init() 
    {
        switch (id)
        {
            case 0:
                Speed = 100;
                Batch();
                break;
            default:
                Speed = 0.3f; // 연사 속도 초기화
                break;
        }
    }

    void Batch()
    {
        for(int index = 0; index<count; index++)
        {
            Transform bullet;
            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index); // 기존 자식 오브젝트 재사용
            }
            else
            {
                bullet = GameManager.Instance.Pool.GetObject(prefabId).transform; // 새로 생성된 총알 오브젝트
                bullet.SetParent(transform);// 부모 설정    
            }

            bullet.localPosition = Vector3.zero; // 총알의 위치를 무기 중앙으로 설정
            bullet.localRotation = Quaternion.identity; // 총알의 회전 초기화

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec); // 총알의 회전 설정
            bullet.Translate(bullet.up * 2.0f, Space.World); // 총알의 위치 설정
            bullet.GetComponent<Bullet>().Init(damage, -1,Vector3.zero);//-1은 무기 무한으로 관통하는 근접공격
        }
    }
    void FIre()
    {
        if (!player.scaner.nearestTarget)
            return;

        Vector3 targetpos = player.scaner.nearestTarget.position;
        Vector3 dir = (targetpos - transform.position).normalized; // 타겟 방향 계산
        Transform bullet = GameManager.Instance.Pool.GetObject(prefabId).transform; // 새로 생성된 총알 오브젝트
        bullet.position = transform.position; // 총알의 위치를 무기 중앙으로 설정
        bullet.rotation = Quaternion.FromToRotation(Vector3.up,dir); // 총알의 회전 초기화
        bullet.GetComponent<Bullet>().Init(damage, count, dir);// 총알 초기화

    }
}
