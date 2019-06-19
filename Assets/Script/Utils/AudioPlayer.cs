using UnityEngine;

namespace TerisGame
{
    public class AudioPlayer : MonoBehaviour
    {
        public static void Play(AudioClip clip)
        {
            mInstance.mAudioSource.Stop();
            mInstance.mAudioSource.clip = clip;
            mInstance.mAudioSource.loop = false;
            mInstance.mAudioSource.Play();
        }
        
        private static AudioPlayer mInstance = null;

        private AudioSource mAudioSource;
        
        private void Awake()
        {
            mAudioSource = GetComponent<AudioSource>();
            
            mInstance = this;
        }


        private void OnDestroy()
        {
            mInstance = null;
        }
    }
}
