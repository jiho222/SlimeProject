using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    public float scrollSpeed = 10f;

    void Update()
    {
        // GameManager에서 플레이어의 공격 상태를 가져온다.
        bool playerIsAttack = GameManager.instance.player.isAttack;

        // 플레이어가 공격 중이 아니면 스크롤링을 한다.
        if (!playerIsAttack)
        {
            // y축으로 일정 속도로 내려감
            transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Area"))
        {
            // Area 밖으로 나가면 위로 만큼 이동
            transform.Translate(Vector3.up * 160);
        }
    }
}
