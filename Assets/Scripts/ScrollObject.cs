using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    public float scrollSpeed = 10f;
    private bool isScrolling = true;

    void Update()
    {
        if (isScrolling)
        {
            // y축으로 일정 속도로 내려감
            transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
        }
    }

    public void SetScrolling(bool shouldScroll)
    {
        isScrolling = shouldScroll;
    }
}
