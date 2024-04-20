using UnityEngine;

namespace Espale.Utilities
{
    public static class GizmoUtilities
    {
        /// <summary>
        /// Draws an arrow
        /// </summary>
        /// <param name="origin">starting position of the arrow</param>
        /// <param name="normal">normal vector of the arrow, used for determining the direction on arrow's head</param>
        /// <param name="tip">ending position of the arrow</param>
        /// <param name="arrowHeadLength">length of the arrow's head</param>
        public static void DrawArrow(Vector3 origin, Vector3 tip, Vector3 normal, float arrowHeadLength)
        {
            var arrowDir = MathUtilities.Direction(origin, tip);
            var perpendicularArrowDir = Vector3.Cross(arrowDir, normal).normalized;
            
            var tipMultiplier = arrowHeadLength * Constants.INVERSE_SQRT_OF_TWO;

            // Arrow Drawing
            Gizmos.DrawLine(origin,  tip);
            Gizmos.DrawLine(tip,  tip - (arrowDir + perpendicularArrowDir) * tipMultiplier);
            Gizmos.DrawLine(tip,  tip - (arrowDir - perpendicularArrowDir) *tipMultiplier);
        }

        /// <summary>
        /// Draws a 2D arrow
        /// </summary>
        /// <param name="origin">starting position of the arrow</param>
        /// <param name="tip">ending position of the arrow</param>
        /// <param name="arrowHeadLength">length of the arrow's head</param>
        public static void DrawArrow2D(Vector2 origin, Vector2 tip, float arrowHeadLength) =>
            DrawArrow(origin, tip, Vector3.forward, arrowHeadLength);

        /// <summary>
        /// Draws a cross with the given arguments
        /// </summary>
        /// <param name="center">Center position</param>
        /// <param name="normal">Normal vector of the cross</param>
        /// <param name="up">Vector in the direction to the top of the cross</param>
        /// <param name="length">Length of a side (assume that a cross has 4 sides)</param>
        /// <param name="angle">Rotation around the normal in degrees</param>
        public static void DrawCross(Vector3 center, Vector3 normal, Vector3 up, float length, float angle=0f)
        {
            var centerVector = Vector3.Cross(normal, up).normalized;
            
            var crossVector1 = Quaternion.AngleAxis(45f, normal) * centerVector * length;
            var crossVector2 = Quaternion.AngleAxis(-45f, normal) * centerVector * length;
            
            Gizmos.DrawLine(center - crossVector1, center + crossVector1);
            Gizmos.DrawLine(center - crossVector2, center + crossVector2);
        }
        
        /// <summary>
        /// Draws a cross in 2D with the given arguments
        /// </summary>
        /// <param name="center">Center position</param>
        /// <param name="length">Length of a side (assume that a cross has 4 sides)</param>
        /// <param name="angle">Rotation in degrees</param>
        public static void DrawCross2D(Vector3 center, float length, float angle=0f) =>
            DrawCross(center, Vector3.forward, Vector3.up, length, angle);
    }
}
