using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnRate;
    [SerializeField] private float _spawnAmount;
    private float _nextSpawnTime;
    private float _cameraDistance;
    private float _spawnOffset;

    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;

        //_cameraDistance = _camera.transform.position.y;
        _spawnOffset = 25;
    }

    void Update()
    {
        if (Time.time >= _nextSpawnTime)
        {
            SpawnAsteroid();
            _nextSpawnTime = Time.time + _spawnRate; 
        }
    }

    void SpawnAsteroid()
    {

        for (int i = 0; i < _spawnAmount; i++)
        {
            Vector3 spawnPosition = Vector3.zero;

            // Choose a random edge
            int randomEdge = Random.Range(0, 4);

            switch (randomEdge)
            {
                case 0: // Top edge
                    spawnPosition = new Vector3(Random.Range(0, Screen.width), Screen.height + _spawnOffset, 0);
                    break;
                case 1: // Bottom edge
                    spawnPosition = new Vector3(Random.Range(0, Screen.width), _spawnOffset * -1, 0);
                    break;
                case 2: // Left edge
                    spawnPosition = new Vector3(_spawnOffset * -1, Random.Range(0, Screen.height), 0);
                    break;
                case 3: // Right edge
                    spawnPosition = new Vector3(Screen.width + _spawnOffset, Random.Range(0, Screen.height), 0);
                    break;
            }


            spawnPosition = _camera.ScreenToWorldPoint(spawnPosition);
            spawnPosition.z = 0;

            //Asteroid clone = Instantiate(_asteroidPrefab, spawnPosition, Quaternion.identity);
            GameObject asteroid = PoolManager.Instance.RequestAsteroid();
            asteroid.transform.position = spawnPosition;
            asteroid.GetComponent<Asteroid>().SetDirection(GameInfo.Instance.player.transform.position);
            


        }
    }
}
