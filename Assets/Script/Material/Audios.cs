using System.Collections.Generic;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace TerisGame
{
    /// <summary>
    /// 使用 QFramework 的 AudioManager
    /// </summary>
    public class Sound : StatefulWidget
    {
        public Widget Child;

        public Sound(Widget child)
        {
            Child = child;
        }

        public static string[] SOUNDS =
        {
            "clean",
            "drop",
            "explosion",
            "move",
            "rotate",
            "start"
        };

        public override State createState()
        {
            return new SoundState();
        }

        public static SoundState of(BuildContext context)
        {
            var state = context.ancestorStateOfType(new TypeMatcher<SoundState>()) as SoundState;
            D.assert(state != null, ()=>"can not find sound widght");
            return state;
        }
    }

    public class SoundState : State<Sound>
    {
        Dictionary<string, AudioClip> mSoundIds = new Dictionary<string, AudioClip>();

        private bool mMute = false;

        public void Play(string name)
        {
            Debug.Log(name);
            
            AudioClip clip = null;

            if (!mSoundIds.TryGetValue(name, out clip)) return;

            if (clip != null && !mMute)
            {
                AudioPlayer.Play(clip);
            }
        }


        public override void initState()
        {
            base.initState();

            foreach (var soundName in Sound.SOUNDS)
            {
                Window.instance.scheduleMicrotask(() =>
                {
                    var clip = Resources.Load<AudioClip>($"audios/{soundName}");
                    mSoundIds.Add(soundName, clip);
                });
            }
        }

        public override Widget build(BuildContext context)
        {
            return widget.Child;
        }

        public void Start()
        {
            Play("start");
        }

        public void Clear()
        {
            Play("clean");
        }

        public void Fall()
        {
            Play("drop");
        }

        public void Rotate()
        {
            Play("rotate");
        }

        public void Move()
        {
            Play("move");
        }
    }
}