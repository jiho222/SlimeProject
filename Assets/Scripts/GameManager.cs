using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public static GameManager instance;
    public bool isAttack = false;
    public List<ScrollObject> scrollObjects; // 스크롤링 객체 목록

    void Awake()
    {
        instance = this;
    }

    public void SetAttackState(bool attackState)
    {
        isAttack = attackState;
        UpdateScrollObjects();
    }

    private void UpdateScrollObjects()
    {
        foreach (var scrollObject in scrollObjects)
        {
            scrollObject.SetScrolling(!isAttack);
        }
    }
}
