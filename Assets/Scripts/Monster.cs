using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float scrollSpeed = 10f;

    void Update()
    {
        // y축으로 일정 속도로 내려감
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
    }
}
