using UnityEngine;
using UnityEngine.Audio;

namespace Espale.UI
{
    public class UIAudioManager : MonoBehaviour
    {
        public static AudioSource PlayOneShotAudio(AudioClip clip, float volume, AudioMixerGroup mixerGroup)
        {
            if (!clip) return null;
            
            var obj = new GameObject {name = "Temp. AudioSource"};
            var audioSource = obj.AddComponent<AudioSource>();

            audioSource.loop = false;
            audioSource.clip = clip;
            audioSource.volume = volume;
            if (mixerGroup)
                audioSource.outputAudioMixerGroup = mixerGroup;

            audioSource.Play();
            Destroy(obj, clip.length);

            return audioSource;
        }
    }
}
