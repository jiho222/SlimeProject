using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Health, Gem, Distance }
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
            case InfoType.Health:
                myText.text = GameManager.instance.playerHealth.ToString();
                float curHp = GameManager.instance.playerHealth;
                float maxHp = GameManager.instance.maxPlayerHealth;
                mySlider.value = curHp / maxHp;

                break;
            case InfoType.Gem:
                myText.text = GameManager.instance.gem.ToString();
                break;
            case InfoType.Distance:
                // myText.text = GameManager.instance.gameDistance.ToString("F0");
                float curDistance = GameManager.instance.gameDistance;
                float maxDistance = GameManager.instance.maxGameDistance;
                mySlider.value = curDistance / maxDistance;
                break;
        }
    }
}
