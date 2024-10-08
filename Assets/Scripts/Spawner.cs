using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float[] xPositions = { -4f, -2f, 0f, 2f, 4f };  // 스폰 가능한 x 좌표들
    public float spawnInterval;  // 적을 생성하는 간격 (초)
    public float shopSpawnDistanceInterval = 0.1f;  // 샵을 생성하는 거리 간격 (비율)
    public float specialObjectSpawnDistanceInterval = 0.7f;  // 특정 오브젝트를 생성하는 거리 간격 (비율)

    float timer;
    float shopSpawnThreshold;
    float specialObjectSpawnThreshold;
    float endLineSpawnThreshold;
    float lastShopSpawnDistance = 10f;
    float lastSpecialObjectSpawnDistance = 0f; // 초기 값 설정

    void Start()
    {
        // 게임 거리 기반으로 샵 생성 거리 간격 계산
        shopSpawnThreshold = GameManager.instance.maxGameDistance * shopSpawnDistanceInterval;
        specialObjectSpawnThreshold = GameManager.instance.maxGameDistance * specialObjectSpawnDistanceInterval;
        endLineSpawnThreshold = GameManager.instance.maxGameDistance * 0.905f; // 게임 거리의 97%
    }

    void Update()
    {
        if (!GameManager.instance.isLive || GameManager.instance.isAttack)
            return;

        // 타이머를 사용해 일정 시간마다 생성
        timer += Time.deltaTime;

        if (timer > spawnInterval)
        {
            float currentDistance = GameManager.instance.gameDistance;

            // 우선순위 1: EndLine 생성
            if (currentDistance >= endLineSpawnThreshold)
            {
                SpawnEndLine();
                endLineSpawnThreshold = float.MaxValue; // 이후 생성되지 않도록 무한대로 설정
            }
            // 우선순위 2: 특정 오브젝트 생성
            else if (currentDistance - lastSpecialObjectSpawnDistance >= specialObjectSpawnThreshold)
            {
                SpawnSpecialObject();
                lastSpecialObjectSpawnDistance = currentDistance;
            }
            // 우선순위 3: 샵 생성
            else if (currentDistance - lastShopSpawnDistance >= shopSpawnThreshold)
            {
                SpawnShop();
                lastShopSpawnDistance = currentDistance;
            }
            // 우선순위 4: 일반 몬스터 생성
            else
            {
                SpawnEnemies();  // 랜덤 적 생성
            }

            timer = 0;  // 타이머 초기화
        }
    }

    void SpawnEnemies()
    {
        // 랜덤하게 1~5마리 적 생성
        int enemyCount = Random.Range(1, 6);  // 1에서 5 사이의 랜덤 개수

        // 적을 생성할 x 좌표의 인덱스 셔플
        List<int> availablePositions = new List<int>() { 0, 1, 2, 3, 4 };
        for (int i = 0; i < enemyCount; i++)
        {
            // 랜덤으로 x 좌표 하나 선택
            int randomIndex = Random.Range(0, availablePositions.Count);
            float xPos = xPositions[availablePositions[randomIndex]];

            // 선택된 x 좌표는 사용 후 리스트에서 제거 (중복 방지)
            availablePositions.RemoveAt(randomIndex);

            // PoolManager의 인덱스에 따른 빈도수 비율로 적 생성
            int enemyIndex = GetRandomEnemyIndex();
            GameObject enemy = GameManager.instance.pool.Get(enemyIndex);  // 풀에서 선택된 인덱스의 프리팹 사용

            // 부모 오브젝트 기준으로 적을 x 좌표에 스폰, y 좌표는 고정
            enemy.transform.position = new Vector3(xPos, transform.position.y, 0);
        }
    }

    int GetRandomEnemyIndex()
    {
        // 확률에 따른 적 인덱스 선택
        int randomValue = Random.Range(0, 100);
        
        if (randomValue < 70) // 70% 확률로 인덱스 0
        {
            return 0;
        }
        else if (randomValue < 85) // 15% 확률로 인덱스 1
        {
            return 1;
        }
        else if (randomValue < 90) // 5% 확률로 인덱스 2
        {
            return 2;
        }
        else if (randomValue < 95) // 5% 확률로 인덱스 3
        {
            return 3;
        }
        else // 5% 확률로 인덱스 4
        {
            return 4;
        }
    }


    void SpawnShop()
    {
        // 샵 생성 로직 구현
        GameObject shop = GameManager.instance.pool.Get(6);  // 풀에서 6번째 프리팹 사용
        shop.transform.position = transform.position;  // Spawner의 위치를 (0,0)으로 설정
    }

    void SpawnSpecialObject()
    {
        // 특정 오브젝트 생성 로직 구현
        GameObject specialObject = GameManager.instance.pool.Get(5);  // 풀에서 2번째 프리팹 사용
        specialObject.transform.position = transform.position;  // Spawner의 위치를 (0,0)으로 설정
    }

    void SpawnEndLine()
    {
        // EndLine 생성 로직 구현
        GameObject endLine = GameManager.instance.pool.Get(7);  // 풀에서 7번째 프리팹 사용
        endLine.transform.position = transform.position;  // Spawner의 위치를 (0,0)으로 설정
    }
}
