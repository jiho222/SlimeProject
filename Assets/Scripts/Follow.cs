using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Vector2 offset; // 오프셋을 지정할 수 있는 변수
    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update() 
    {
        // 플레이어 위치를 스크린 좌표로 변환
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
        
        // 오프셋을 적용해서 살짝 아래쪽으로 위치 조정
        rect.position = playerScreenPos + new Vector3(offset.x, offset.y, 0);
    }
}
