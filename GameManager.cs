using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float GameTime = 0f; // ���� �ð�
    public float maxTime = 30f * 60f; // �ִ� ���� �ð�

    public PoolMnager Pool;
    public PlayerControll player;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        // ���� �ð��� �ִ� �ð��� �����ϸ� ���� ����
        if (GameTime >= maxTime)
        {
            Debug.Log("Game End");
            // ���� ���� ���� �߰�
        }
        else
        {
            GameTime += Time.deltaTime; // ���� �ð� ������Ʈ
        }
    }
}

