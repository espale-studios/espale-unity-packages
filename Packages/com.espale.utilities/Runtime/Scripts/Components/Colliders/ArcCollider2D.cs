using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Espale.Utilities.Components.Colliders
{
    [ExecuteInEditMode, RequireComponent(typeof(EdgeCollider2D))]
    public class ArcCollider2D : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private EdgeCollider2D edgeCollider;

        [Header("Arc Setting")] 
        public Vector2 arcOffset = Vector2.zero;
        // Anything below .0127f will make the polygon collider exclude some vertices.
        [Range(.0127f, 1f)] public float distanceBetweenPoints = .1f;
        [Space]
        [Range(0f, 360f)]public float startAngle; 
        [Range(-360f, 360f)]public float arcAngle = 90f; 
        [Space]
        [MinValue(.01f)] public float arcRadius = 1f;
        [MinValue(.012f)] public float thickness = 1f;

        private void Update()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                UpdateCollider();
#endif
        }

        public void UpdateCollider()
        {
            if (!edgeCollider) edgeCollider = GetComponent<EdgeCollider2D>();

            var points = new List<Vector2>();
            var unitVertexCount = Mathf.Abs(arcAngle) * Mathf.Deg2Rad / distanceBetweenPoints + 1;
        
            // We need segment count + 1 vertices for each part.
            // ex: here we have 3 segments (--) with 4 vertices (|)
            // |--|--|--|
        
            // Inner Vertices
            var innerSegmentCount = Mathf.CeilToInt(unitVertexCount * arcRadius);
            for (float i = 0; i <= innerSegmentCount; i++)
            {
                var angle = startAngle + arcAngle * (i / innerSegmentCount);
                var dir = MathUtilities.UnitCirclePosDegrees(angle);
            
                points.Add(arcOffset + dir * (arcRadius + thickness * .5f));
            }
        
            edgeCollider.SetPoints(points);
            edgeCollider.edgeRadius = thickness * .5f;
        }
    }
}
