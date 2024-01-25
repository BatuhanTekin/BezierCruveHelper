using System.Collections.Generic;
using UnityEngine;

namespace _game.Scripts.BezierHelper
{
    public static class BezierCurveCreator
    {
        private static List<Vector3> _list = new();
        
        private static Vector3 _pos;
        private static float _frame;

        // Works With 4 Points
        // Example:
        //     p2*  p3*
        // p1*          p4*

        // speed can not be bigger than 1

        public static Vector3[] GetBezierCurve(Vector3[] points, float speed, float totalFrame = 1)
        {
            if (speed > 1)
            {
                speed = 1;
            }

            if (speed <= 0)
            {
                speed = 0.001f;
            }

            _list.Clear();
            _frame = 0f;
            while (_frame < totalFrame)
            {
                _frame += speed;
                _pos = Mathf.Pow(1 - _frame, 3) * points[0] +
                       3 * Mathf.Pow(1 - _frame, 2) * _frame * points[1] +
                       3 * (1 - _frame) * Mathf.Pow(_frame, 2) * points[2] +
                       Mathf.Pow(_frame, 3) * points[3];
                _list.Add(_pos);
            }

            return _list.ToArray();
        }
    }
}