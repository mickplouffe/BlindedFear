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
    private Transform _monsterParent;

    [SerializeField]
    private List<GameObject> _spawnPoints = new List<GameObject>();

    [SerializeField]
    private int _maxEnemies = 4;
    private int _activeEnemies;
    [SerializeField]
    private float _spawnDelay = 3f;
    private WaitForSeconds _spawnYield;
    private bool _isGameActive = false;
    private Coroutine _spawnRoutine;

    private void OnEnable()
    {
        GameManager.OnGameStart += BeginGame;
        GameManager.OnGameEnd += EndGame;
        MonsterController.OnMonsterDisable += ReturnToPool;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= BeginGame;
        GameManager.OnGameEnd -= EndGame;
        MonsterController.OnMonsterDisable -= ReturnToPool;
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
            if(_activeEnemies <= _maxEnemies)
            {
                //grab random spawn point
                Transform randomSpawn = _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform;
                //spawn enemy
                MonsterController monster = Spawn(_monsters, _monsterPrefab, _monsterParent);
                monster.transform.position = randomSpawn.position;
                _activeEnemies++;
            }
        }

    }

    private MonsterController Spawn(List<MonsterController> list, MonsterController spawningItem, Transform parent)
    {
        if (list.Count == 0)
            PoolManager.Instance.GeneratePooledObjects(list, spawningItem, parent);

        //Pick a random item from the pool
        var itemToSpawn = PoolManager.Instance.RequestPooledObject(list, true);

        itemToSpawn.gameObject.SetActive(true);
        return itemToSpawn;

    }

    private void ReturnToPool(MonsterController monster)
    {
        //Prevent the event from registering multiple times
        if (!_monsters.Contains(monster))
        {
            _monsters.Add(monster);
            _activeEnemies--;
        }
    }

}
