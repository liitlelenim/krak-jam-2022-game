using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioClip[] musicClips;
        [SerializeField] private int levelToLoad;
        private AudioSource _audioSource;
        private int _currentMusic = -1;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(this);
            PlayNewSong();
            SceneManager.LoadScene(levelToLoad);
        }

        private IEnumerator WaitUntilEndOfTheSong(int songLength)
        {
            yield return new WaitForSeconds(songLength);
            PlayNewSong();
        }

        private void PlayNewSong()
        {
            _currentMusic++;
            if (_currentMusic > musicClips.Length - 1)
            {
                _currentMusic = 0;
            }

            _audioSource.clip = musicClips[_currentMusic];
            _audioSource.volume = 0.05f;
            _audioSource.PlayDelayed(3f);
            StartCoroutine(WaitUntilEndOfTheSong((int) _audioSource.clip.length));
        }
    }
}