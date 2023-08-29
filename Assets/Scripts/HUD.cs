using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType
    {
        Exp,
        Level,
        Kill,
        Time,
        Health,
    }

    public InfoType type;

    Text myText;
    Slider mySlider;
    GameManager gm;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void Start()
    {
        gm = GameManager.instance;
        if (!gm) return;
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = gm.exp;
                float maxExp = gm.nextExp[gm.level];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", gm.level);
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", gm.kill);
                break;
            case InfoType.Time:
                float remainTime = gm.maxGameTime - gm.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            case InfoType.Health:
                float curHp = gm.health;
                float maxHp = gm.maxHealth;
                mySlider.value = curHp / maxHp;
                break;
            
        }
    }
}
