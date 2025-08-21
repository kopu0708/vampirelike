using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnDatas; // ������ ���� ������ �迭

    int Level; // Level of the game, can be used to increase difficulty
    float spawnTime;
    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        spawnTime += Time.deltaTime;
        Level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.GameTime /10f),spawnDatas.Length - 1);//�ִ� 30�� ����

        if (spawnTime > spawnDatas[Level].spawnTime) // ���� �ð� üũ
        {
            spawnTime = 0;
            Spawn();
        }
        else if (GameManager.Instance.GameTime > 0 && spawnTime <= 0) // ������ ���۵Ǿ���, ���� �ð��� 0 ������ ��
        {
            spawnTime = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.Instance.Pool.GetObject(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;  
        enemy.GetComponent<Enemy>().Init(spawnDatas[Level]); // ���� �ʱ�ȭ
    }
}

[System.Serializable]
public class SpawnData
{
    public int spriteType; // ������ ��������Ʈ Ÿ��
    public float spawnTime;
    public int health; // ������ ü��
    public float speed; // ������ �̵� �ӵ�
}
