using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public int damageToPlayer = 50; // 플레이어에게 줄 데미지

    private Collider2D spinCollider; // 스핀의 콜라이더

    void OnEnable()
    {
        // 스핀의 콜라이더를 가져옴
        spinCollider = GetComponent<Collider2D>();
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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어와 충돌했는지 확인
        if (other.CompareTag("Player"))
        {
            // 플레이어에게 데미지 주기
            GameManager.instance.TakeDamage(damageToPlayer);

            // Collider를 비활성화하여 더 이상 충돌하지 않도록 설정
            if (spinCollider != null)
            {
                spinCollider.enabled = false;
            }
        }
    }
}
