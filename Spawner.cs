using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnDatas; // 몬스터의 스폰 데이터 배열

    int Level; // Level of the game, can be used to increase difficulty
    float spawnTime;
    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        spawnTime += Time.deltaTime;
        Level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.GameTime /10f),spawnDatas.Length - 1);//최대 30분 까지

        if (spawnTime > spawnDatas[Level].spawnTime) // 스폰 시간 체크
        {
            spawnTime = 0;
            Spawn();
        }
        else if (GameManager.Instance.GameTime > 0 && spawnTime <= 0) // 게임이 시작되었고, 스폰 시간이 0 이하일 때
        {
            spawnTime = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.Instance.Pool.GetObject(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;  
        enemy.GetComponent<Enemy>().Init(spawnDatas[Level]); // 몬스터 초기화
    }
}

[System.Serializable]
public class SpawnData
{
    public int spriteType; // 몬스터의 스프라이트 타입
    public float spawnTime;
    public int health; // 몬스터의 체력
    public float speed; // 몬스터의 이동 속도
}
