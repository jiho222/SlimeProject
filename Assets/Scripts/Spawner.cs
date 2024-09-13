using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float[] xPositions = { -4f, -2f, 0f, 2f, 4f };  // 스폰 가능한 x 좌표들
    public float spawnInterval;  // 적을 생성하는 간격 (초)
    
    float timer;

    void Update()
    {
        // 게임이 진행 중이 아니거나 플레이어가 공격 중이면 스폰 중지
        if (!GameManager.instance.isLive || GameManager.instance.isAttack)
            return;

        // 타이머를 사용해 일정 시간마다 적 생성
        timer += Time.deltaTime;
        if (timer > spawnInterval)
        {
            SpawnEnemies();  // 랜덤 적 생성
            timer = 0;  // 타이머 초기화
        }
    }

    // 적을 랜덤한 x 좌표에 랜덤 개수로 생성
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

            // 적 생성 (PoolManager에서 가져옴)
            GameObject enemy = GameManager.instance.pool.Get(0);  // 풀에서 0번째 프리팹 사용

            // 부모 오브젝트 기준으로 적을 x 좌표에 스폰, y 좌표는 고정
            enemy.transform.position = new Vector3(xPos, transform.position.y, 0);
        }
    }
}
