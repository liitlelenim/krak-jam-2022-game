using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakedWall : MonoBehaviour
{
    [SerializeField] Animator weakedAnim_1;
    [SerializeField] Animator weakedAnim_2;
    [SerializeField] Animator weakedAnim_3;
    private float _timer = 0.5f;
    private bool _destroyed;
    private readonly int _destroyAnimBoolIndex =  Animator.StringToHash("destroy");
    private void Update()
    {
        if (_destroyed)
        {
            _timer -= Time.deltaTime;
        }

        if (_timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Rocket"))
        {
            weakedAnim_1.SetBool(_destroyAnimBoolIndex, true);
            weakedAnim_2.SetBool(_destroyAnimBoolIndex, true);
            weakedAnim_3.SetBool(_destroyAnimBoolIndex, true);
            _destroyed = true;
        }
    }
}