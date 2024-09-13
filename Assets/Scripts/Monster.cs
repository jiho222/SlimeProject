using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int damageToPlayer; // 몬스터가 플레이어에게 주는 데미지
    public int damageToMonster; // 몬스터가 받는 데미지

    private bool isDamagingPlayer = false; // 플레이어와 접촉 중인지 여부
    private bool isDeactivatedByDeleter = false; // Deleter로 인해 비활성화되었는지 여부

    void OnEnable()
    {
        // 몬스터가 활성화될 때마다 체력을 최대 체력으로 초기화
        health = maxHealth;
        isDeactivatedByDeleter = false; // 초기화
    }

    void Update()
    {
        // GameManager의 isLive와 isAttack 값을 체크해서 스크롤링 여부를 결정
        if (GameManager.instance.isLive && !GameManager.instance.isAttack)
        {
            float scrollSpeed = GameManager.instance.GetScrollSpeed();
            // y축으로 일정 속도로 내려감
            transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
        }

        // 체력이 0 이하가 되면 오브젝트 비활성화
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }

        // 플레이어와 충돌 중일 때 데미지 주기
        if (isDamagingPlayer)
        {
            // 일정 간격으로 데미지를 주기
            if (Time.time % 1f < Time.deltaTime) // 1초마다
            {
                // 몬스터의 체력을 감소
                health -= damageToMonster;
                // GameManager를 통해 플레이어에게 데미지 전달
                GameManager.instance.TakeDamage(damageToPlayer); 
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDamagingPlayer = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDamagingPlayer = false;
        }
    }

    void OnDisable()
    {
        if (!isDeactivatedByDeleter)
        {
            // 몬스터가 Deleter로 인해 비활성화되지 않았을 때만 gem 증가
            GameManager.instance.IncreaseGem(1);
        }
    }

    public void SetDeactivatedByDeleter(bool value)
    {
        isDeactivatedByDeleter = value;
    }
}
