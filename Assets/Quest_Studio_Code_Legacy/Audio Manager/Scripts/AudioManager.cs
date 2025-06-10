using System;
using UnityEngine;
using Quest_Studio;

namespace Quest_Studio
{
    public class AudioManager : MonoBehaviour
    {
        // Instance
        #region
        private static AudioManager instance;
        public static AudioManager GetInstance()
        {
            return instance;
        }
        #endregion

        // Variables
        #region
        [Header("Variables")]
        public Sound[] sounds;

        private AudioSourceComponent[] audioSourceComponents = new AudioSourceComponent[0];
        #endregion

        // Method
        #region
        public void SetSound(string name, AudioClip audioClip)
        {
            Sound s = Array.Find(sounds, sounds => sounds.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            s.source.clip = audioClip;
        }

        public void Stop(string name)
        {
            Sound s = Array.Find(sounds, sounds => sounds.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            s.source.Stop();
        }

        public void Play(string name)
        {
            Sound s = Array.Find(sounds, sounds => sounds.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            s.source.Play();
        }

        public void SetVolume(string name, float volumeLevel)
        {
            Sound s = Array.Find(sounds, sounds => sounds.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            AudioSourceComponent component = Array.Find(audioSourceComponents, audioSourceComponents => audioSourceComponents.name == name);
            AudioSource audioSource = component.audioSource;
            if (audioSource == null)
            {
                Debug.Log("Audio Source not found!");
                return;
            }

            //Change Volume
            audioSource.volume = s.volume * volumeLevel;
        }

        public void SetPitch(string name, float pitchLevel)
        {
            Sound s = Array.Find(sounds, sounds => sounds.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            AudioSourceComponent component = Array.Find(audioSourceComponents, audioSourceComponents => audioSourceComponents.name == name);
            AudioSource audioSource = component.audioSource;
            if (audioSource == null)
            {
                Debug.Log("Audio Source not found!");
                return;
            }

            audioSource.pitch = s.pitch * pitchLevel;
        }

        // - Destroy Self -
        #region
        public virtual void DestroyAudioManager()
        {
            Destroy(this.gameObject);
        }
        #endregion

        #endregion

        // Audio Souce Component Class
        #region
        [Serializable]
        public class AudioSourceComponent
        {
            public string name;
            public AudioSource audioSource;

            public AudioSourceComponent(string name, AudioSource audioSource)
            {
                this.name = name;
                this.audioSource = audioSource;
            }
        }
        #endregion

        public virtual void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;

                AudioSourceComponent audioSourceComponent = new AudioSourceComponent(s.name, s.source);
                ArrayExtension.AddElement(ref audioSourceComponents, ref audioSourceComponent);
            }

            //DontDestroyOnLoad(gameObject);
        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }
    }
}