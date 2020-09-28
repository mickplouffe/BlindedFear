using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private Transform _raycastPoint;
    [SerializeField]
    private Transform _bulletSpawn;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private float _fireDelay = 0.25f;
    [SerializeField]
    private float _fireDistance = 250f;
    [SerializeField]
    private float _damageAmount = 1f;
    private float _newFireTime;

    [SerializeField]
    private bool _isGameActive = false;


    private void OnEnable()
    {
        GameManager.OnGameStart += GameStart;
        MonsterEndGoal.OnEnemyReached += EndGame;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= GameStart;
        MonsterEndGoal.OnEnemyReached -= EndGame;
    }

    private void GameStart()
    {
        _isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameActive)
        {
            if (Input.GetAxis("Fire1") != 0 && Time.time >= _newFireTime)
            {
                _newFireTime = Time.time + _fireDelay;
                Debug.Log("Fire!");
                Instantiate(_bulletPrefab, _bulletSpawn);

                Ray ray = new Ray(_raycastPoint.position, _raycastPoint.forward);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, _fireDistance))
                {
                    Debug.Log("We hit " + hitInfo.collider.name);
                    IDamagable damagable = hitInfo.collider.gameObject.GetComponent<IDamagable>();

                    if (damagable != null)
                    {
                        damagable.Damage(_damageAmount);

                    }
                }
            }
        }
    }

    private void EndGame()
    {
        _isGameActive = false;
    }

    private Transform ReturnPosition()
    {
        return this.transform;
    }
}
