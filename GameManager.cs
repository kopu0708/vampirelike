using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float GameTime = 0f; // 게임 시간
    public float maxTime = 30f * 60f; // 최대 게임 시간

    public PoolMnager Pool;
    public PlayerControll player;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        // 게임 시간이 최대 시간에 도달하면 게임 종료
        if (GameTime >= maxTime)
        {
            Debug.Log("Game End");
            // 게임 종료 로직 추가
        }
        else
        {
            GameTime += Time.deltaTime; // 게임 시간 업데이트
        }
    }
}

