using UnityEngine;

namespace TetrisApp
{
    public class AudioPlayer
    {
        public static bool Mute { get; set; } = false;

        public static void Play(string audioName)
        {
            if (Mute) return;
            
            var audioClip = Resources.Load<AudioClip>(audioName);

            AudioSource.PlayClipAtPoint(audioClip, Vector3.zero);
        } 
        
        public static void Clean()
        {
            Play("audios/clean");
        }

        public static void Drop()
        {
            Play("audios/drop");
        }
        
        public static void Explosion()
        {
            Play("audios/explosion");
        }
        
        public static void Move()
        {
            Play("audios/move");
        }
        
        public static void Rotate()
        {
            Play("audios/rotate");
        }
        
        public static void Start()
        {
            Play("audios/start");
        }
    }
}