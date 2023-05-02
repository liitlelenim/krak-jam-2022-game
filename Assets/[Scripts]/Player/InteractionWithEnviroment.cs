using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithEnviroment : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _springboardJumpForce;
    private void Awake()
    {
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Springboard")
        {
            _rigidbody2D.AddForce(Vector2.up * _springboardJumpForce);
        }
    }
}
