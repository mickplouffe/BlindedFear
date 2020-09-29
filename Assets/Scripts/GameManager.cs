using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public static event Action OnGameStart;
    public static event Action<float> OnScoreChanged;

    [SerializeField]
    private float _playerPoints = 0;


    private void OnEnable()
    {
        MonsterEndGoal.OnEnemyReached += EndGame;
        MonsterController.OnMonsterDeath += AdjustScore;
        Building.OnBuildingDamaged += AdjustScore;

        AdjustScore(100);
    }

    private void OnDisable()
    {
        MonsterEndGoal.OnEnemyReached -= EndGame;
        MonsterController.OnMonsterDeath += AdjustScore;
        Building.OnBuildingDamaged -= AdjustScore;
    }

    public void BeginGame()
    {
        OnGameStart?.Invoke();
    }

    public void EndGame()
    {

    }

    private void AdjustScore(float points)
    {
        _playerPoints += points;
        OnScoreChanged?.Invoke(_playerPoints);
    }

}
