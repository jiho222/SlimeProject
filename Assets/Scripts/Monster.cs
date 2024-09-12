using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float scrollSpeed = 8f;

    void Update()
    {
        if (GameManager.instance != null && !GameManager.instance.isAttack)
        {
            // y축으로 일정 속도로 내려감
            transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // 스크롤 상태는 GameManager가 관리하므로 여기서는 필요 없음
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // 스크롤 상태는 GameManager가 관리하므로 여기서는 필요 없음
        }
    }
}
