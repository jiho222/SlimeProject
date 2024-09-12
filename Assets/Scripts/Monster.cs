using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
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
}
