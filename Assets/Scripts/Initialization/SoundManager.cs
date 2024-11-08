using System.Collections.Generic;
using UnityEngine;

namespace App.Initialization
{
    public class SoundManager : MonoBehaviour
    {
        // 싱글톤 인스턴스
        private static SoundManager _instance;
        public static SoundManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("@SoundManager");
                    _instance = go.AddComponent<SoundManager>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        // Sound 종류를 정의하는 enum
        public enum SoundType
        {
            BGM,              // BGM용 사운드 (Loop)
            Effect,
            PlayerTimer,      // Player용 Timer 사운드 (Loop)
            PlayerBoom,       // Player용 Boom 사운드 (One-shot)
            OreCollected,     // 광물 획득용 사운드 (One-shot)
            CurrencyCollected, // 재화 획득용 사운드 (One-shot)
            MaxCount,         // 사운드 종류의 최대값
        }

        // 오디오 소스 배열과 클립 캐싱을 위한 딕셔너리
        private AudioSource[] _audioSources = new AudioSource[(int)SoundType.MaxCount];
        private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

        private void Awake()
        {
            Init();
        }

        // SoundManager 초기화
        private void Init()
        {
            // SoundType에 맞게 각 AudioSource를 초기화
            for (int i = 0; i < (int)SoundType.MaxCount; i++)
            {
                GameObject go = new GameObject(((SoundType)i).ToString());
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = this.transform;

                // PlayerTimer는 Loop가 되도록 설정
                if ((SoundType)i == SoundType.BGM || (SoundType)i == SoundType.PlayerTimer)
                {
                    _audioSources[i].loop = true;
                }
            }
        }

        // 사운드 클리어 함수
        public void Clear()
        {
            foreach (AudioSource audioSource in _audioSources)
            {
                audioSource.Stop();
                audioSource.clip = null;
            }
            _audioClips.Clear();
        }

        // 경로에 따라 사운드를 재생 (One-shot 및 Loop를 구분), 볼륨 조절 가능
        public void Play(string path, SoundType type, float volume = 1.0f, float pitch = 1.0f)
        {
            AudioClip audioClip = GetOrAddAudioClip(path);
            Play(audioClip, type, volume, pitch);
        }

        // AudioClip을 받아서 재생하는 함수, 볼륨 조절 가능
        public void Play(AudioClip audioClip, SoundType type, float volume = 1.0f, float pitch = 1.0f)
        {
            if (audioClip == null) return;

            AudioSource audioSource = _audioSources[(int)type];
            audioSource.pitch = pitch;
            audioSource.volume = Mathf.Clamp01(volume); // 볼륨을 0~1 사이로 제한

            if (type == SoundType.BGM || type == SoundType.PlayerTimer)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = audioClip;
                    audioSource.Play();
                }
            }
            else
            {
                audioSource.PlayOneShot(audioClip, volume);
            }
        }

        // 특정 SoundType의 오디오를 멈추는 함수
        public void Stop(SoundType type)
        {
            AudioSource audioSource = _audioSources[(int)type];
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        // 오디오 클립을 가져오거나 딕셔너리에 추가하는 함수
        private AudioClip GetOrAddAudioClip(string path)
        {
            if (_audioClips.TryGetValue(path, out var audioClip)) return audioClip;

            audioClip = Resources.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.LogWarning($"AudioClip not found at path: {path}");
                return null;
            }

            _audioClips.Add(path, audioClip);
            return audioClip;
        }
    }
}