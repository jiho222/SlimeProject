using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isAttack;
    public bool isLive;
    public float gameDistance;
    public float maxGameDistance;
    public float scrollSpeed = 8f;

    [Header("# Player Info")]
    public int playerHealth;
    public int maxPlayerHealth = 300;

    [Header("# GameObject")]
    public PoolManager pool;
    public Player player;

    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
        playerHealth = maxPlayerHealth; // 게임 시작 시 플레이어 체력을 최대 체력으로 설정
    }

    void Update()
    {
        if (!isLive)
            return;

        UpdateGameDistance(); // 게임 거리 업데이트
        CheckMaxDistance();   // 최대 거리 체크
    }

    private void UpdateGameDistance()
    {
        if (!isAttack)
        {
            gameDistance += scrollSpeed * Time.deltaTime;
        }
    }

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

    public void TakeDamage(int damageAmount)
    {
        if (isLive)
        {
            playerHealth -= damageAmount;
            if (playerHealth <= 0)
            {
                isLive = false; // 체력이 0 이하가 되면 게임 종료
            }
        }
    }
}
