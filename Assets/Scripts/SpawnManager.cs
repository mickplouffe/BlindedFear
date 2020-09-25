using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{

    [SerializeField]
    private MonsterController _monsterPrefab;
    [SerializeField]
    private List<MonsterController> _monsters = new List<MonsterController>();

    [SerializeField]
    private List<GameObject> _spawnPoints = new List<GameObject>();

    [SerializeField]
    private float _spawnDelay = 3f;
    private WaitForSeconds _spawnYield;
    private bool _isGameActive = false;
    private Coroutine _spawnRoutine;

    private void OnEnable()
    {
        GameManager.OnGameStart += BeginGame;
        GameManager.OnGameEnd += EndGame;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= BeginGame;
        GameManager.OnGameEnd -= EndGame;
    }

    private void Start()
    {
        _spawnYield = new WaitForSeconds(_spawnDelay);
    }

    private void BeginGame()
    {
        _isGameActive = true;
        _spawnRoutine = StartCoroutine(EnemySpawning());
    }

    private void EndGame()
    {
        _isGameActive = false;
        StopCoroutine(_spawnRoutine);
    }

    IEnumerator EnemySpawning()
    {
        while (_isGameActive)
        {
            yield return _spawnYield;
        }

    }

    private void Spawn(List<GameObject> list, GameObject spawningItem, Transform parent)
    {
        if (list.Count == 0)
            PoolManager.Instance.GeneratePooledObjects(list, spawningItem, parent);

        //Pick a random item from the pool
        var itemToSpawn = PoolManager.Instance.RequestPooledObject(list, true);

        itemToSpawn.SetActive(true);

    }

}
