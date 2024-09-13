using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deleter : MonoBehaviour
{
    public string[] tagsToDeactivate;  // 비활성화할 오브젝트의 태그 배열

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 태그가 tagsToDeactivate 배열에 포함된 오브젝트만 비활성화
        foreach (string tag in tagsToDeactivate)
        {
            if (collision.CompareTag(tag))
            {
                collision.gameObject.SetActive(false);
                break;
            }
        }
    }
}
