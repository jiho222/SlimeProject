using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    public float scrollSpeed = 10f;

    void Update()
    {
        // y축으로 일정 속도로 내려감
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Area"))
            transform.Translate(Vector3.up * 160); // Area 밖으로 나가면 위로 만큼 이동
    }
}
