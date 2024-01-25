using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _game.Scripts.BezierHelper
{
    public class BezierCurveToTarget : MonoBehaviour
    {
        [SerializeField] private Transform mTarget;
        [SerializeField] private float mSpeed = 0.01f;
        [SerializeField] private float mTopPointOffset = 2f;
        
        private readonly WaitForFixedUpdate _fixedUpdate = new();
        private Vector3[] _bezierDots;
        private Transform _transform;

        private void Start()
        {
            _transform = transform;
        }

        public void FollowCurve()
        {
            _bezierDots = BezierCurveCreator.GetBezierCurve(GetBezier(), mSpeed);
            StartCoroutine(FollowPath());
        }

        private IEnumerator FollowPath()
        {
            foreach (var dot in _bezierDots)
            {
                _transform.position  = dot;
                yield return new WaitForFixedUpdate();
            }

            yield return _fixedUpdate;
        }
        
        private Vector3[] GetBezier()
        {
            var position = _transform.position;
            var finalPoint = mTarget.position;
            
            var list = new Vector3[4];

            var midPoint = new Vector3((position.x + finalPoint.x) / 2, position.y + finalPoint.y + mTopPointOffset,
                (position.z + finalPoint.z) / 2);
            
            list[0] = position;
            list[1] = midPoint;
            list[2] = midPoint;
            list[3] = finalPoint;

            return list;
        }
        
        
#if UNITY_EDITOR

        [ContextMenu("Test Movement")]
        public void Test_FollowCurve()
        {
            _bezierDots = BezierCurveCreator.GetBezierCurve(GetBezier(), mSpeed);
            StartCoroutine(FollowPath());
        }
        private void OnDrawGizmosSelected()
        {
            _transform = transform;
            _bezierDots = BezierCurveCreator.GetBezierCurve(GetBezier(), mSpeed);
            if (_bezierDots.Length > 0)
            {
                foreach (var dot in _bezierDots)
                {
                    Gizmos.DrawSphere(dot, 0.1f);
                }
            }
        }
#endif
    }
}