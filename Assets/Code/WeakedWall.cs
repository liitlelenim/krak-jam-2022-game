using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakedWall : MonoBehaviour
{
    [SerializeField] Animator weakedAnim_1;
    [SerializeField] Animator weakedAnim_2;
    [SerializeField] Animator weakedAnim_3;
    float timer = 0.5f;
    bool destroy;
    private void Update()
    {
        if (destroy)
            timer -= Time.deltaTime;
        if (timer <= 0)
            Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Rocket"))
        {
            weakedAnim_1.SetBool("destroy", true);
            weakedAnim_2.SetBool("destroy", true);
            weakedAnim_3.SetBool("destroy", true);
            destroy = true;
        }

    }
}
