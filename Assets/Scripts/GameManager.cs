using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    public bool isAttack;

    [Header("# Game Control")]
    public bool isLive;
    public float gameDistance;
    public float maxGameDistance;
    public float scrollSpeed = 8f;
    [Header("# Player Info")]
    public int health;
    public int maxHealth = 300;
    public int gem;
    [Header("# GameObject")]
    // public PoolManager pool;
    public Player player;
    // public LevelUp uiLevelUp;
    // public Result uiResult;
    // public Transform uiJoy;

    // [Header("# Boss")]
    // public GameObject boss1;
    // public GameObject boss2;
    // private bool boss1Spawned = false;
    // private bool boss2Spawned = false;

    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (!isLive)
            return;
            UpdateGameDistance(); // 게임 거리 업데이트
            CheckMaxDistance();   // 최대 거리 체크
    }

    // 게임 거리 업데이트 (스크롤링 중일 때 거리 증가)
    private void UpdateGameDistance()
    {
        // 스크롤링 중일 때만 거리가 증가하도록 처리
        if (!isAttack)
        {
            gameDistance += scrollSpeed * Time.deltaTime; // 스크롤 속도에 비례해 거리 증가
        }
    }

    // 최대 거리에 도달하면 게임 종료 처리
    private void CheckMaxDistance()
    {
        if (gameDistance >= maxGameDistance)
        {
            isLive = false;
        }
    }

    public void SetAttackState(bool attackState)
    {
        isAttack = attackState;
    }

    public float GetScrollSpeed()
    {
        return scrollSpeed;
    }
}