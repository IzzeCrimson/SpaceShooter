using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _fireRate = .5f;

    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                Instantiate(_projectile, _spawnPoint.position, _spawnPoint.rotation, null);
                _timer = _fireRate;
            }
        }

    }
}
