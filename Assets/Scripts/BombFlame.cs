using UnityEngine;

public class BombFlame : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Monster"))
        {
            collider.gameObject.SetActive(false); // Monster 비활성화
        }
    }
}
