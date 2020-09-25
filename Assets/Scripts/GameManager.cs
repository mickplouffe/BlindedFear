using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public static event Action OnGameStart;
    public static event Action OnGameEnd;

    [SerializeField]
    private float _playerPoints = 0;


    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void BeginGame()
    {
        OnGameStart?.Invoke();
    }

    public void EndGame()
    {
        OnGameEnd?.Invoke();
    }

    private void AdjustPoints(float points)
    {
        _playerPoints += points;
    }
}
