using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Espale.Utilities
{
    public static class MathUtilities
    {
#region Mean
        /// <summary>
        /// Returns the arithmetic mean of the elements of given enumerable
        /// </summary>
        /// <returns> Arithmetic mean of the given enumerable</returns>
        public static float Mean(IEnumerable<int> numbers)
        {
            var arr = numbers as int[] ?? numbers.ToArray();
            if (arr.Length == 0) return 0;

            return arr.Sum() / (float) arr.Length;
        }

        /// <summary>
        /// Returns the arithmetic mean of the elements of given enumerable
        /// </summary>
        /// <returns> Arithmetic mean of the given enumerable</returns>
        public static float Mean(IEnumerable<float> numbers)
        {
            var arr = numbers as float[] ?? numbers.ToArray();
            if (arr.Length == 0) return 0;

            return arr.Sum() / arr.Length;
        }
#endregion

#region UnitCirclePosition
        /// <summary>
        /// returns a position as Vector2(r*cos(theta), r*sin(theta))
        /// </summary>
        /// <param name="theta">angle, in radians</param>
        /// <param name="r">radius</param>
        /// <returns>Vector2(r*cos(theta), r*sin(theta))</returns>
        public static Vector2 UnitCirclePosRadians(float theta, float r = 1f)
        {
            var rawPos = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
            return rawPos * r;
        }

        /// <summary>
        /// returns a position as Vector2(r*cos(theta), r*sin(theta))
        /// </summary>
        /// <param name="theta">angle, in degrees</param>
        /// <param name="r">radius</param>
        /// <returns>Vector2(r*cos(theta), r*sin(theta))</returns>
        public static Vector2 UnitCirclePosDegrees(float theta, float r = 1f) => UnitCirclePosRadians(theta * Mathf.Deg2Rad, r);

#endregion

        /// <summary>
        /// Order of the bounds does not matter. However
        /// if both of them are equal to each other,
        /// the function will return true if the number
        ///  is equal to the bounds
        /// </summary>
        /// <returns>a <c>boolean</c> value indicating if the given number is in range.</returns>
        public static bool IsInRange(float num, float bound1, float bound2)
        {
            if (bound1 > bound2)
                return bound1 > num && num > bound2;
            if (bound2 > bound1)
                return bound2 > num && num > bound1;

            // if the boundary is a number instead
            // of a range, check if the number is
            // equal to it
            return Math.Abs(num - bound1) < Mathf.Epsilon;
        }

#region Absolute Vectors
        /// <summary>
        /// Applies absolute value to all axes of the given Vector
        /// </summary>
        /// <param name="vec">Original vector</param>
        /// <returns>Vector with absolute axes</returns>
        public static Vector2 AbsVector(Vector2 vec) => new (Mathf.Abs(vec.x), Mathf.Abs(vec.y));

        /// <summary>
        /// Applies absolute value to all axes of the given Vector
        /// </summary>
        /// <param name="vec">Original vector</param>
        /// <returns>Vector with absolute axes</returns>
        public static Vector3 AbsVector(Vector3 vec) => new (Mathf.Abs(vec.x), Mathf.Abs(vec.y), Mathf.Abs(vec.z));
#endregion

#region Clamp Vectors
        /// <summary>
        /// Clamps all axis of the given vector between <c>min</c> and <c>max</c> values.
        /// </summary>
        /// <param name="v">original vector</param>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <returns>vector with all axis clamped.</returns>
        public static Vector2 ClampVector(Vector2 v, float min, float max)
            => new (Mathf.Clamp(v.x, min, max), Mathf.Clamp(v.y, min, max));
                
        /// <summary>
        /// Clamps all axis of the given vector between <c>min</c> and <c>max</c> values.
        /// </summary>
        /// <param name="v">original vector</param>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <returns>vector with all axis clamped.</returns>
        public static Vector3 ClampVector(Vector3 v, float min, float max)
            => new (Mathf.Clamp(v.x, min, max), Mathf.Clamp(v.y, min, max), Mathf.Clamp(v.z, min, max));
#endregion

#region Round Vectors
        /// <summary>
        /// Rounds all axes of the given vector.
        /// </summary>
        /// <param name="v">original vector</param>
        /// <returns>rounded vector.</returns>
        public static Vector2 RoundVector(Vector2 v) => new (Mathf.Round(v.x), Mathf.Round(v.y));
        
        /// <summary>
        /// Rounds all axes of the given vector.
        /// </summary>
        /// <param name="v">original vector</param>
        /// <returns>rounded vector.</returns>
        public static Vector3 RoundVector(Vector3 v) => new (Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
#endregion

#region Min Vectors
        /// <summary>
        /// Takes the min value for all axes with the given float value.
        /// </summary>
        /// <param name="v">original vector</param>
        /// <param name="val">value to take the min of each axis.</param>
        /// <returns>rounded vector.</returns>
        public static Vector2 MinVector(Vector2 v, float val) => new (Mathf.Min(v.x, val), Mathf.Min(v.y, val));

        /// <summary>
        /// Takes the min value for all axes with the given float value.
        /// </summary>
        /// <param name="v">original vector</param>
        /// <param name="val">value to take the min of each axis.</param>
        /// <returns>rounded vector.</returns>
        public static Vector3 MinVector(Vector3 v, float val) => new (Mathf.Min(v.x, val), Mathf.Min(v.y, val), Mathf.Min(v.z, val));
#endregion

#region Max Vectors
        /// <summary>
        /// Takes the max value for all axes with the given float value.
        /// </summary>
        /// <param name="v">original vector</param>
        /// <param name="val">value to take the max of each axis.</param>
        /// <returns>rounded vector.</returns>
        public static Vector2 MaxVector(Vector2 v, float val) => new (Mathf.Max(v.x, val), Mathf.Max(v.y, val));

        /// <summary>
        /// Takes the max value for all axes with the given float value.
        /// </summary>
        /// <param name="v">original vector</param>
        /// <param name="val">value to take the max of each axis.</param>
        /// <returns>rounded vector.</returns>
        public static Vector3 MaxVector(Vector3 v, float val) => new (Mathf.Max(v.x, val), Mathf.Max(v.y, val), Mathf.Max(v.z, val));
#endregion

        /// <summary>
        /// Returns the result of a non-clamping linear remapping of the given value from source range [a, b] to the destination range [c, d]
        /// </summary>
        /// <param name="value">The value to remap from [a, b] to [c, d]</param>
        /// <param name="a">The first endpoint of the source range [a,b]</param>
        /// <param name="b">The second endpoint of the source range [a, b]</param>
        /// <param name="c">The first endpoint of the destination range [c, d]</param>
        /// <param name="d">The second endpoint of the destination range [c, d]</param>
        /// <returns>The remapped value in the range [c, d]</returns>
        public static float Remap(float value, float a,  float b, float c, float d)
        {
            if (a == b || c == d) return 0;
            value = Mathf.Clamp(value, a, b);

            return (value - a) / (b - a) * (d - c) + c;
        }

        /// <summary>
        /// Returns the result of a linear remapping of the given value from source range [a, b] to the destination range [c, d]. The value can be outside the source range.
        /// </summary>
        /// <param name="value">The value to remap from [a, b] to [c, d]</param>
        /// <param name="a">The first endpoint of the source range [a,b]</param>
        /// <param name="b">The second endpoint of the source range [a, b]</param>
        /// <param name="c">The first endpoint of the destination range [c, d]</param>
        /// <param name="d">The second endpoint of the destination range [c, d]</param>
        /// <returns>The remapped value in the range [c, d]</returns>
        public static float RemapUnclamped(float value, float a, float b, float c, float d)
        {
            if (a == b || c == d) return 0;

            return (value - a) / (b - a) * (d - c) + c;
        }

#region Direction
        /// <summary>
        /// Returns the direction between the given points
        /// </summary>
        /// <param name="from">start of the direction vector</param>
        /// <param name="to">tip of the direction vector</param>
        /// <param name="normalized">if set to <c>true</c>, the output will be normalized, else it will have a magnitude equal to the distance between from and to.</param>
        /// <returns>The direction vector</returns>
        public static Vector3 Direction(Vector3 from, Vector3 to, bool normalized=true)
        {
            var diff = to - from;

            return normalized ? diff.normalized : diff;
        }

        /// <summary>
        /// Returns the direction between the given points
        /// </summary>
        /// <param name="from">start of the direction vector</param>
        /// <param name="to">tip of the direction vector</param>
        /// <param name="normalized">if set to <c>true</c>, the output will be normalized, else it will have a magnitude equal to the distance between from and to.</param>
        /// <returns>The direction vector</returns>
        public static Vector2 Direction(Vector2 from, Vector2 to, bool normalized = true)
        {
            var diff = to - from;

            return normalized ? diff.normalized : diff;
        }
#endregion

#region Bezier Lerp
        /// <summary>
        /// Return the point at <c>t</c> of a Bezier curve which starts from <c>p1</c> and ends at <code>p2</code>.
        /// https://en.wikipedia.org/wiki/B%C3%A9zier_curve 
        /// </summary>
        /// <param name="p1">Starting point of the Bezier Curve</param>
        /// <param name="p2">Ending point of the Bezier Curve</param>
        /// <param name="t">Interpolation amount, between 0 and 1. Note: t get's clamped to the range.</param>
        /// <param name="orderedControlPoints">Control points of the Bezier Curve.</param>
        /// <returns>Interpolated point.</returns>
        public static Vector3 BezierInterpolation(Vector3 p1, Vector3 p2, float t, Vector3[] orderedControlPoints = null)
        {
            t = Mathf.Clamp01(t);
            if (orderedControlPoints is null || orderedControlPoints.Length == 0) return Vector3.Lerp(p1, p2, t);

            // Create a points array = [p1, orderedControlPoints, p2]
            var points = new Vector3[orderedControlPoints.Length + 2];
            points[0] = p1;
            points[^1] = p2;
            for (var i = 0; i < orderedControlPoints.Length; i++) points[i + 1] = orderedControlPoints[i];

            var maxIterCount = points.Length - 1;
            for (var _ = 0; _ < maxIterCount; _++)
            {
                var subPoints = new Vector3[points.Length - 1];
                for (var i = 0; i < points.Length - 1; i++)
                    subPoints[i] = Vector3.Lerp(points[i], points[i + 1], t);

                points = subPoints;
            }

            return points[0];
        }

        /// <summary>
        /// Return the point at <c>t</c> of a Bezier curve which starts from <c>p1</c> and ends at <code>p2</code>.
        /// https://en.wikipedia.org/wiki/B%C3%A9zier_curve
        /// </summary>
        /// <param name="p1">Starting point of the Bezier Curve</param>
        /// <param name="p2">Ending point of the Bezier Curve</param>
        /// <param name="t">Interpolation amount, between 0 and 1. Note: t get's clamped to the range.</param>
        /// <param name="orderedControlPoints">Control points of the Bezier Curve.</param>
        /// <returns>Interpolated point.</returns>
        public static Vector2 BezierInterpolation(Vector2 p1, Vector2 p2, float t, Vector2[] orderedControlPoints = null)
        {
            t = Mathf.Clamp01(t);
            if (orderedControlPoints is null || orderedControlPoints.Length == 0) return Vector2.Lerp(p1, p2, t);

            // Create a points array = [p1, orderedControlPoints, p2]
            var points = new Vector2[orderedControlPoints.Length + 2];
            points[0] = p1;
            points[^1] = p2;
            for (var i = 0; i < orderedControlPoints.Length; i++) points[i + 1] = orderedControlPoints[i];

            var maxIterCount = points.Length - 1;
            for (var _ = 0; _ < maxIterCount; _++)
            {
                var subPoints = new Vector2[points.Length - 1];
                for (var i = 0; i < points.Length - 1; i++)
                    subPoints[i] = Vector2.Lerp(points[i], points[i + 1], t);

                points = subPoints;
            }

            return points[0];
        }
#endregion

        /// <summary>
        /// Returns the given <c>Vector2</c> and rotates it by the angle given in radians.
        /// </summary>
        /// <param name="vector"> The original  <c>Vector2</c></param>
        /// <param name="angle"> The angle to rotate in radians</param>
        /// <returns></returns>
        public static Vector2 RotateVector2(Vector2 vector, float angle)
        {
            var cos = Mathf.Cos(angle);
            var sin = Mathf.Sin(angle);

            return new Vector2(
                cos * vector.x - sin * vector.y,
                sin * vector.x + cos * vector.y
            );
        }

        /// <summary>
        /// Clamps the given angle between the given min and max angles.
        /// </summary>
        /// <param name="angle">The angle value to restrict inside the range defined by the minimum and maximum values.</param>
        /// <param name="min">The minimum angle value to compare against.</param>
        /// <param name="max">The maximum angle value to compare against.</param>
        /// <returns>The angle result clamped between the minimum and maximum values.</returns>
        /// Source: https://gist.github.com/johnsoncodehk/2ecb0136304d4badbb92bd0c1dbd8bae
        public static float ClampAngle(float angle, float min, float max)
        {
            var start = (min + max) * 0.5f - 180;
            var floor = Mathf.FloorToInt((angle - start) / 360) * 360;
            return Mathf.Clamp(angle, min + floor, max + floor);
        }
    }
}
