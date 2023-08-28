using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;
    public PoolManager pool;

    public float gameTime;
    private float maxGameTime = 2 * 60f;//추후 조정

    private void Awake()
    {

        instance = this;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        Debug.Log(gameTime);
        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;

        }
    }
}
