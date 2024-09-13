using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int damageToBomb; // 폭탄이 플레이어에게 주는 데미지

    public GameObject bombFlame; // 폭탄 폭발 시 활성화할 BombFlame Prefab

    private bool isDamagingPlayer = false; // 플레이어와의 충돌 여부
    private Collider2D bombCollider;

    void OnEnable()
    {
        health = maxHealth;
        bombCollider = GetComponent<Collider2D>();
        bombCollider.enabled = true;
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

        // 체력이 0 이하가 되면 폭발
        if (health <= 0)
        {
            
            StartCoroutine(Explode());
        }

        if (isDamagingPlayer)
        {
            // 일정 간격으로 데미지를 주기
            if (Time.time % 1f < Time.deltaTime) // 1초마다
            {
                // 몬스터의 체력을 감소
                health -= damageToBomb;
                // 폭탄 소리 재생
                AudioManager.instance.PlaySfx("BombSound");
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

    private IEnumerator Explode()
    {
        // 폭탄의 콜라이더 비활성화
        if (bombCollider != null)
        {
            bombCollider.enabled = false;
        }

        if (bombFlame != null)
        {
            // BombFlame을 자식 객체에서 활성화
            bombFlame.SetActive(true);

            // 1초 대기
            yield return new WaitForSeconds(1f);

            // BombFlame 비활성화
            bombFlame.SetActive(false);
        }
        
        // 폭탄 비활성화
        gameObject.SetActive(false);
    }


}
