using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Espale.Utilities
{
    public class RandomUtilities
    {
        /// <summary>
        /// Returns a random value between <c>a</c> and <c>b</c>.
        /// </summary>
        /// <param name="a">The start value</param>
        /// <param name="b">The end value</param>
        public static float RandomLerp(float a, float b) => Mathf.Lerp(a, b, Random.Range(0f, 1f));
                
        /// <summary>
        /// Returns a random value between <c>a</c> and <c>b</c>.
        /// </summary>
        /// <param name="range">A Vector2 in the form of <c>(a, b)</c> where <c>a</c> is the start and <c>b</c> is the end value</param>
        public static float RandomLerp(Vector2 range) => RandomLerp(range.x, range.y);
        
        /// <summary>
        /// Returns a random value between <c>a</c> and <c>b</c>.
        /// </summary>
        /// <param name="a">The start value</param>
        /// <param name="b">The end value</param>
        public static Vector2 RandomLerp(Vector2 a, Vector2 b) => Vector2.Lerp(a, b, Random.Range(0f, 1f));
        
        /// <summary>
        /// Returns a random value between <c>a</c> and <c>b</c>.
        /// </summary>
        /// <param name="a">The start value</param>
        /// <param name="b">The end value</param>
        public static Color RandomLerp(Color a, Color b) => Color.Lerp(a, b, Random.Range(0f, 1f));

        /// <summary>
        /// Returns a random value between <c>a</c> and <c>b</c>.
        /// </summary>
        /// <param name="a">The start value</param>
        /// <param name="b">The end value</param>
        public static Vector3 RandomLerp(Vector3 a, Vector3 b) => Vector3.Lerp(a, b, Random.Range(0f, 1f));
        
        /// <summary>
        /// Returns the <c>true</c> if the randomly picked number between 0 and denominator is less than or equal to the given maximum numerator.
        /// </summary>
        /// <param name="numerator">Max Numerator</param>
        /// <param name="denominator">Denominator</param>
        /// <returns></returns>
        public static bool RandomChance(float numerator, float denominator)
        {
            var randNum = Random.Range(0, denominator);
            return randNum <= numerator;
        }

        /// <summary>
        /// Returns <c>true</c>, <c>percentage</c>% of the time.
        /// </summary>
        /// <param name="percentage">Percentage at which this function will return <c>true</c></param>
        /// <returns>Random value</returns>
        public static bool RandomPercentageChance(float percentage) => RandomChance(percentage, 100f);

        /// <summary>
        /// Returns a random <c>Vector2</c> with unit-magnitude
        /// </summary>
        public static Vector2 RandomVector2() => new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized;

        /// <summary>
        /// Returns a random <c>Vector3</c> with unit-magnitude
        /// </summary>
        public static Vector3 RandomVector3() => new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)).normalized;

        /// <summary>
        /// Returns a random <c>Vector4</c> with unit-magnitude
        /// </summary>
        public static Vector4 RandomVector4() => new Vector4(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)).normalized;

        /// <summary>
        /// Returns an <c>array</c> of given length which contains random <c>floats</c> between the given min and max values.
        /// </summary>
        /// <param name="length">Length of the output <c>array</c> </param>
        /// <param name="minVal">Minimum value for each of the <c>floats</c> (Inclusive)</param>
        /// <param name="maxVal">Maximum value for each of the <c>floats</c> (Inclusive)</param>
        /// <returns><c>Array</c> with the random <c>floats</c></returns>
        public static float[] RandomFloatArray(int length, float minVal, float maxVal)
        {
            var array = new float[length];
            for (var i = 0; i < length; i++)
                array[i] = Random.Range(minVal, maxVal);

            return array;
        }

        /// <summary>
        /// Returns an <c>array</c> of given length which contains random <c>ints</c> between the given min and max values.
        /// </summary>
        /// <param name="length">Length of the output <c>array</c> </param>
        /// <param name="minVal">Minimum value for each of the <c>ints</c> (Inclusive)</param>
        /// <param name="maxVal">Maximum value for each of the <c>ints</c> (Inclusive)</param>
        /// <returns><c>Array</c> with the random <c>ints</c></returns>
        public static int[] RandomIntArray(int length, int minVal, int maxVal)
        {
            var array = new int[length];
            for (var i = 0; i < length; i++)
                array[i] = Random.Range(minVal, maxVal);

            return array;
        }

        public static T PickRandomItemFrom<T>(List<T> collection) => collection[Random.Range(0, collection.Count)];
        public static T PickRandomItemFrom<T>(T[] collection) => collection[Random.Range(0, collection.Length)];
    }
}
