using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public bool isAttack = false;
    public bool isDie = false; // 사망 여부

    Rigidbody2D rigid;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // 사망 시에는 움직이지 않음
        if (isDie)
            return;

        // 이동할 벡터 계산
        Vector2 nextVec = new Vector2(inputVec.x, 0) * speed * Time.fixedDeltaTime;

        // 목표 위치 계산 (이동 벡터 적용)
        Vector2 targetPosition = rigid.position + nextVec;

        // X축 좌표를 중앙 기준으로 -4f ~ 4f 범위로 제한
        targetPosition.x = Mathf.Clamp(targetPosition.x, -4f, 4f);

        // 제한된 목표 위치로 캐릭터 이동
        rigid.MovePosition(targetPosition);
    }

    void Update()
    {
        // 체력이 0이 되었을 때 사망 처리
        if (GameManager.instance.playerHealth <= 0 && !isDie)
        {
            Die(); // 사망 처리
        }
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Monster"))
        {
            if (!isAttack)
            {
                isAttack = true;
                GameManager.instance.SetAttackState(true); // 공격 상태를 GameManager에 전달
                anim.SetBool("isAttack", true);

                AudioManager.instance.PlaySfx("SwordSound");
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Monster"))
        {
            isAttack = false;
            GameManager.instance.SetAttackState(false); // 공격 상태를 GameManager에 전달
            anim.SetBool("isAttack", false);
        }
    }

    public void Die()
    {
        isDie = true;
        anim.SetTrigger("isDie"); // 사망 애니메이션 실행
        GameManager.instance.isLive = false; // 게임 종료
    }
}
