using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    private Vector3 _direction;
    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * _movementSpeed * Time.deltaTime);
    }

    void OnEnable()
    {
        if (_player == null) _player = GameObject.FindGameObjectWithTag("Player");

        _direction = (transform.position - _player.transform.position).normalized;
    }

    public void SetDirection(Vector3 playerLocation)
    {
        _direction = (transform.position - playerLocation).normalized;
    }
}
