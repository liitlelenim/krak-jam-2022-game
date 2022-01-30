using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlueScreen : MonoBehaviour
{
    public float timer;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        if (timer <= 0)
            SceneManager.LoadScene(1);
    }
}
