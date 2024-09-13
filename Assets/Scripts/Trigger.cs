using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Player와 충돌 시 UI 활성화 및 오브젝트 비활성화
        if (collision.gameObject.CompareTag("Player"))
        {
            // if (uiElement != null)
            // {
            //     uiElement.SetActive(true); // UI 활성화
            // }
            gameObject.SetActive(false); // Trigger 오브젝트 비활성화
        }
    }
}
