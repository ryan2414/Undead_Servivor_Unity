using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;
    public PoolManager pool;

    private void Awake()
    {

        instance = this;
    }
}
