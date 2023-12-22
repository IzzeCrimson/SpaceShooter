using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    public static PoolManager Instance { get; set; }

    [Header("Asteroid")]
    [SerializeField] private GameObject _asteroidPrefab;
    private Queue<GameObject> asteroidPool = new Queue<GameObject>();
    [SerializeField] private int _asteroidPoolCount;

    [Header("Container")]
    [SerializeField] private GameObject _asteroidContainer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        GeneratePool(_asteroidPoolCount);
    }

    private void GeneratePool(int poolAmount)
    {
        
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject asteroid = Instantiate(_asteroidPrefab, transform);
            asteroid.transform.parent = _asteroidContainer.transform;
            asteroid.SetActive(false);
            asteroidPool.Enqueue(asteroid);

        }

    }

    public GameObject RequestAsteroid()
    {
        if (asteroidPool.Count == 0)
        {
            GeneratePool(_asteroidPoolCount);
        }

        GameObject asteroid = asteroidPool.Dequeue();
        asteroid.SetActive(true);

        return asteroid;
    }

    public void SendBackToPool(GameObject asteroid)
    {
        asteroid.SetActive(false);
        asteroidPool.Enqueue(asteroid);
    }

}
