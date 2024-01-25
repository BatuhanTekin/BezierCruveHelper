using System.Collections;
using UnityEngine;

namespace _game.Scripts.BezierHelper
{
    public class BezierCurveFollower : MonoBehaviour
    {
        [SerializeField] private Vector3[] mPoints = new Vector3[4];
        [SerializeField] private float mSpeed = 0.01f;
        
        private readonly WaitForFixedUpdate _fixedUpdate = new();
        
        private Transform _follower;
        private Vector3[] _bezierDots;

        private void Start()
        {
            _bezierDots = BezierCurveCreator.GetBezierCurve(mPoints, mSpeed);
        }

        public void FollowCurve(Transform follower)
        {
            _follower = follower;
            StartCoroutine(FollowPath());
        }

        private IEnumerator FollowPath()
        {
            foreach (var dot in _bezierDots)
            {
                _follower.position  = dot;
                yield return new WaitForFixedUpdate();
            }

            yield return _fixedUpdate;
        }
        
        
#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
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