using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Espale.Utilities.Components
{
    public class ObjectDetector : MonoBehaviour
    {
        [Header("Detection")]
        public DetectionShape shape;
        public Transform target;
        
        [Header("Events")]
        public UnityEvent onEnter;
        public UnityEvent onExit;

        private bool isTargetInside;

        [Header("Circle Details")]
        [EnableIf("IsCircle")]
        [SerializeField] private float radius = 1f;

        [Header("Rectangle Details")]
        [EnableIf("IsRectangle")]
        [SerializeField] private float width = 1f;
        [SerializeField] private float height = 1f;

        private bool IsCircle() => shape == DetectionShape.Circle;
        private bool IsRectangle() => shape == DetectionShape.Rectangle;

        private void Update()
        {
#if UNITY_EDITOR
            if (!target)
            {
                BetterDebug.LogError($"Object Detector has no Target at {gameObject.name}");
                return;
            }
#endif
            
            var detected = false;
            switch (shape)
            {
                case DetectionShape.Circle:
                    detected = Vector3.Distance(transform.position, target.position) <= radius;
                    break;
                case DetectionShape.Rectangle:
                    var diff = MathUtilities.AbsVector(transform.position - target.position);
                    detected = diff.x <= width * .5f && diff.y <= height * .5f;
                    break;
            }

            if (detected && !isTargetInside) onEnter.Invoke();
            else if (!detected && isTargetInside) onExit.Invoke();
            
            isTargetInside = detected;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            switch (shape)
            {
                case DetectionShape.Circle:
                    Gizmos.DrawWireSphere(transform.position, radius);
                    break;
                case DetectionShape.Rectangle:
                    Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 1f));
                    break;
            }
        }

        public enum DetectionShape
        {
            Circle = 0,
            Rectangle = 1,
        }
    }
}
