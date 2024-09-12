using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    void Update()
    {
        // GameManager의 isAttack 값을 체크해서 스크롤링 여부를 결정
        if (!GameManager.instance.isAttack)
        {
            float scrollSpeed = GameManager.instance.GetScrollSpeed();
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