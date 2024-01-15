using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    private Vector3 _direction;
    private Camera _camera;
    private int _offset;

    // Start is called before the first frame update
    void Start()
    {
        _offset = 50;
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * _movementSpeed * Time.deltaTime);
        CheckOffScreen();
    }

    //void OnEnable()
    //{
    //    SetDirection(GameInfo.Instance.player.transform.position);
    //    Debug.Log("Enabled");

    //}

    public void SetDirection(Vector3 playerLocation)
    {
        _direction = (playerLocation - transform.position).normalized;
    }
    void CheckOffScreen()
    {
        // Check if the asteroid is off-screen and deactivate it
        Vector3 screenPos = _camera.WorldToScreenPoint(transform.position);

        if (screenPos.y > Screen.height + _offset || screenPos.y < 0 - _offset || screenPos.x > Screen.width + _offset || screenPos.x < 0 - _offset)
        {
            PoolManager.Instance.SendBackToPool(this.gameObject);
        }
    }
}
