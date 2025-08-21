using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId; //���� ������ ID
    public float damage; //������ ������
    public int count; //������ ����
    public float Speed; //������ ���� �ӵ�
    PlayerControll player; // �÷��̾� ��Ʈ�ѷ� ����
    float Timer; // Ÿ�̸� (�ʿ�� ���)

    private void Awake()
    {
        player = GetComponentInParent<PlayerControll>(); // �θ� ������Ʈ���� PlayerControll ������Ʈ ã��
    }
    private void Start()
    {
        // �ʱ�ȭ �޼ҵ� ȣ��
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
                Timer += Time.deltaTime; // Ÿ�̸� ������Ʈ
                if(Timer >= Speed) // Ÿ�̸Ӱ� �ӵ����� ũ�ų� ������
                {
                    Timer = 0f; // Ÿ�̸� �ʱ�ȭ
                    FIre(); // �Ѿ� �߻� �޼ҵ� ȣ��
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

        if(id == 0) // ���Ⱑ ȸ���ϴ� ���
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
                Speed = 0.3f; // ���� �ӵ� �ʱ�ȭ
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
                bullet = transform.GetChild(index); // ���� �ڽ� ������Ʈ ����
            }
            else
            {
                bullet = GameManager.Instance.Pool.GetObject(prefabId).transform; // ���� ������ �Ѿ� ������Ʈ
                bullet.SetParent(transform);// �θ� ����    
            }

            bullet.localPosition = Vector3.zero; // �Ѿ��� ��ġ�� ���� �߾����� ����
            bullet.localRotation = Quaternion.identity; // �Ѿ��� ȸ�� �ʱ�ȭ

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec); // �Ѿ��� ȸ�� ����
            bullet.Translate(bullet.up * 2.0f, Space.World); // �Ѿ��� ��ġ ����
            bullet.GetComponent<Bullet>().Init(damage, -1,Vector3.zero);//-1�� ���� �������� �����ϴ� ��������
        }
    }
    void FIre()
    {
        if (!player.scaner.nearestTarget)
            return;

        Vector3 targetpos = player.scaner.nearestTarget.position;
        Vector3 dir = (targetpos - transform.position).normalized; // Ÿ�� ���� ���
        Transform bullet = GameManager.Instance.Pool.GetObject(prefabId).transform; // ���� ������ �Ѿ� ������Ʈ
        bullet.position = transform.position; // �Ѿ��� ��ġ�� ���� �߾����� ����
        bullet.rotation = Quaternion.FromToRotation(Vector3.up,dir); // �Ѿ��� ȸ�� �ʱ�ȭ
        bullet.GetComponent<Bullet>().Init(damage, count, dir);// �Ѿ� �ʱ�ȭ

    }
}
