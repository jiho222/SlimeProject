using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { HealthSlider, HealthText , Gem, DistanceSlider, DistnaceText }
    public InfoType infoType;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        switch (infoType)
        {
            case InfoType.HealthSlider:
                float curHp = GameManager.instance.playerHealth;
                float maxHp = GameManager.instance.maxPlayerHealth;
                mySlider.value = curHp / maxHp;
                break;
            case InfoType.HealthText:
                myText.text = string.Format("{0:F0}", GameManager.instance.playerHealth);
                break;

            case InfoType.Gem:
                myText.text = string.Format("{0}", GameManager.instance.gem);
                break;
            case InfoType.DistanceSlider:
                float curDistance = GameManager.instance.gameDistance;
                float maxDistance = GameManager.instance.maxGameDistance;
                mySlider.value = curDistance / maxDistance;
                break;
            case InfoType.DistnaceText:
                myText.text = string.Format("{0:F0}m", GameManager.instance.gameDistance);
                break;
        }
    }
}
