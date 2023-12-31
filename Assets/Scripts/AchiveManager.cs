using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject uiNotice;

    enum Achive
    {
        UnlockPotato,
        UnlockSweetPotato,
    }

    Achive[] achives;
    WaitForSecondsRealtime wait;

    private async void Awake()
    {
        achives = (Achive[])System.Enum.GetValues(typeof(Achive));
        wait = new WaitForSecondsRealtime(5);
        if (!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    private void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach (var achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);

        }
    }

    private void Start()
    {
        UnlockCharacter();
    }

    private void UnlockCharacter()
    {
        for (int i = 0; i < lockCharacter.Length; i++)
        {
            string achiveName = achives[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            lockCharacter[i].SetActive(!isUnlock);
            unlockCharacter[i].SetActive(isUnlock);
        }
    }

    private void LateUpdate()
    {
        foreach (var achive in achives)
        {
            CheckAchive(achive);
        }
    }

    private void CheckAchive(Achive achive)
    {
        if (GameManager.instance == null) return;

        bool isAchive = false;

        switch (achive)
        {
            case Achive.UnlockPotato:
                isAchive = GameManager.instance.kill >= 10;
                break;
            case Achive.UnlockSweetPotato:
                isAchive = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;
            default:
                break;
        }

        if (isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);

            for (int i = 0; i < uiNotice.transform.childCount; i++)
            {
                bool isActive = i == (int)achive;
                uiNotice.transform.GetChild(i).gameObject.SetActive(isActive);
            }

            StartCoroutine(NoticeRoutine());
        }
    }

    private IEnumerator NoticeRoutine()
    {
        uiNotice.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);

        yield return wait;

        uiNotice.SetActive(false);

    }
}
