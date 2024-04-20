using System;
using UnityEngine;

namespace Espale.Utilities
{
    public class AudioUtilities
    { 
        /// <summary>
        /// /// Converts the given normalized float to decibels using the formula:
        /// decibel = log_10(float_val) * 20
        /// </summary>
        /// <param name="f">float value, ranged in [0, 1] (both are inclusive)</param>
        /// <returns>decibel value</returns>
        public static float FloatToDecibel(float f)
        {
            f = Mathf.Clamp01(f);
            if (f < .00001f) return -80f;
            return Mathf.Log10(f) * 20f;
        }

        /// <summary>
        /// Converts the given decibel to a normalized float ranged in [0, 1] (both inclusive)
        /// </summary>
        /// <param name="decibel">decibel value</param>
        /// <returns>normalized float value</returns>
        public static float DecibelToFloat(float decibel)
        {
            // float_val =  10^(decibel / 20)
            return Mathf.Pow(10, decibel * .05f);
        }

        /// <summary>
        /// Returns an <c>AudioData</c> from the random clip options defined in the given <c>RandomAudioData</c>
        /// </summary>
        /// <param name="randomData">Original <c>RandomAudioData</c></param>
        /// <returns><c>AudioData</c> with a random clip chosen from the given <c>RandomAudioSource</c></returns>
        public static AudioData ChooseRandomAudioData(RandomAudioData randomData)
        {
            return new AudioData
            {
                clip = RandomUtilities.PickRandomItemFrom(randomData.options),
                volume = randomData.volume
            };
        }
        
        [Serializable]
        public struct RandomAudioData
        {
            public AudioClip[] options;
            [Range(0f, 1f)] public float volume;
        }

        [Serializable]
        public struct AudioData
        {
            public AudioClip clip;
            [Range(0f, 1f)] public float volume;
        }
    }
}
