using NaughtyAttributes;
using UnityEngine;

namespace Espale.Utilities.Components.Randomizers
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceRandomizer : MonoBehaviour
    {
        public bool randomizeOnStart = true;
        
        [Header("Randomization")]
        public bool randomizeClip;
        [ShowIf(nameof(randomizeClip))] public AudioClip[] clipOptions = new AudioClip[2];
        
        public bool randomizeVolume;
        [ShowIf(nameof(randomizeVolume)), MinMaxSlider(0f, 1f)] public Vector2 volumeRange = new (0f, 1f);
        
        public bool randomizePitch;
        [ShowIf(nameof(randomizePitch)), MinMaxSlider(-1f, 1f)] public Vector2 pitchRange = new (0f, 1f);
        
        private AudioSource source;

        private void Awake() => source = GetComponent<AudioSource>();

        private void Start()
        {
            if (randomizeOnStart) Randomize();
        }

        public void Randomize()
        {
            if (randomizeClip) source.clip = CollectionUtilities.GetRandomItemFrom(clipOptions);
            if (randomizePitch) source.pitch = RandomUtilities.RandomLerp(pitchRange);
            if (randomizeVolume) source.volume = RandomUtilities.RandomLerp(volumeRange);
        }
    } 
}
