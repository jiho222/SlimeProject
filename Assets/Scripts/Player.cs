using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;

    Rigidbody2D rigid;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // 이동할 벡터 계산
        Vector2 nextVec = new Vector2(inputVec.x, 0) * speed * Time.fixedDeltaTime;

        // 목표 위치 계산 (이동 벡터 적용)
        Vector2 targetPosition = rigid.position + nextVec;

        // X축 좌표를 중앙 기준으로 -0.1f ~ 0.1f 범위로 제한
        targetPosition.x = Mathf.Clamp(targetPosition.x, -4f, 4f);

        // 제한된 목표 위치로 캐릭터 이동
        rigid.MovePosition(targetPosition);
    }


    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
}
