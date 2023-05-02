using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class BlueScreen : MonoBehaviour
    {
        public float timer;

        // Update is called once per frame
        void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
