using UnityEngine;

namespace Game
{
    public class SoundsManager : MonoBehaviour
    {
        [SerializeField] private AudioClip[] sounds;

        public void PlaySound(int soundIndex)
        {
            HandleSound(sounds[soundIndex]);
        }

        public void PlaySound(int soundIndex, float volumeFactor)
        {
            HandleSound(sounds[soundIndex], volumeFactor);
        }

        private void HandleSound(AudioClip clip, float volumeFactor = 0.3f)
        {
            GameObject sound = new GameObject();
            AudioSource source = sound.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = volumeFactor;
            source.Play();
            Destroy(sound, source.clip.length + 2f);
        }
    }
}