using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _movementSpeed = 5;
    [SerializeField] private float _rotationSpeed = 200;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        horizontal = vertical > -0.99 ? -horizontal : horizontal;

        transform.Translate(new Vector3(0, vertical * _movementSpeed * Time.deltaTime, 0));

        transform.Rotate(Vector3.forward * (horizontal * _rotationSpeed * Time.deltaTime));

    }
}
