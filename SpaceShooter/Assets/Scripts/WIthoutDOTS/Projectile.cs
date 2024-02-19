using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    private Camera _camera;
    private int _offset;


    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _offset = 50;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _movementSpeed * Time.deltaTime);
        CheckOffScreen();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            PoolManager.Instance.SendBackToPool(collision.gameObject);
        }
    }

    void CheckOffScreen()
    {
        // Check if the asteroid is off-screen and deactivate it
        Vector3 screenPos = _camera.WorldToScreenPoint(transform.position);

        if (screenPos.y > Screen.height + _offset || screenPos.y < 0 - _offset || screenPos.x > Screen.width + _offset || screenPos.x < 0 - _offset)
        {
            Destroy(this.gameObject);
        }
    }

}
//collision.gameObject.GetComponent<Asteroid>()