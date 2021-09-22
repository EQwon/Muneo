using UnityEngine;
using MuneoCrepe;

namespace MuneoCrepe.Sound
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClip bgmClip;
        [SerializeField] private AudioClip correctSEClip;
        [SerializeField] private AudioClip wrongSEClip;
        
        private static SoundManager _instance;
        public static SoundManager Instance => _instance;

        private AudioSource _bgmSource;
        private AudioSource _seSource;
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            Initialize();
        }

        private void Initialize()
        {
            _bgmSource = gameObject.AddComponent<AudioSource>();
            _bgmSource.loop = true;
            _bgmSource.volume = 0.6f;
            
            _seSource = gameObject.AddComponent<AudioSource>();
            _seSource.loop = false;
        }

        public void PlayBGM()
        {
            if (_bgmSource == null) Initialize();
            if (_bgmSource.isPlaying) return;

            _bgmSource.clip = bgmClip;
            _bgmSource.Play();
        }

        public void StopBGM()
        {
            if(_bgmSource == null) Initialize();
            if (!_bgmSource.isPlaying) return;
            
            _bgmSource.Stop();
        }

        public void PlaySE(bool isCorrect)
        {
            if (_seSource == null) Initialize();

            _seSource.clip = isCorrect ? correctSEClip : wrongSEClip;
            _seSource.Play();
        }
    }
}
