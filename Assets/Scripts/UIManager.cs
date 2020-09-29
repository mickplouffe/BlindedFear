using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _pointsText;
    [SerializeField]
    private GameObject _button;

    private void OnEnable()
    {
        GameManager.OnScoreChanged += ScoreChanged;
    }

    private void OnDisable()
    {
        GameManager.OnScoreChanged -= ScoreChanged;
    }



    private void ScoreChanged(float points)
    {
        _pointsText.text = points.ToString();
    }

    private void GameOver()
    {
        _button.SetActive(true);
    }
}
