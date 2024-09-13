using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int damageAmount;   // 한번에 입는 데미지

    private bool isDamaging = false;   // 플레이어와 접촉 중인지 여부

    void Start()
    {
        // 몬스터 생성 시 체력을 최대 체력으로 초기화
        health = maxHealth;
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

        // 체력이 0 이하가 되면 오브젝트 삭제
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어와 충돌 시 데미지를 입기 시작
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isDamaging)
            {
                StartCoroutine(DamageOverTime());
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // 플레이어와의 충돌이 끝나면 데미지 입는 코루틴 종료
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isDamaging)
            {
                StopCoroutine(DamageOverTime());
                isDamaging = false;
            }
        }
    }

    private IEnumerator DamageOverTime()
    {
        isDamaging = true;
        while (true)
        {
            health -= damageAmount;
            yield return new WaitForSeconds(1f);
        }
    }
}
