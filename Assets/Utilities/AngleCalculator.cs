using System;
using UnityEngine;

namespace Assets.Utilities
{
    public static class AngleCalculator
    {
        private const float Epsilon = 0.000001f;

        /// <summary>
        ///     Requires input vectors to be normalized and with y=0
        /// </summary>
        /// <returns>Signed angle [-180, 180]</returns>
        public static float CalculateLogicAngle(Vector3 from, Vector3 to)
        {
            var angle = Vector3.Angle(from, to);
            var referenceRight = Vector3.Cross(Vector3.up, from);
            return Mathf.Sign(Vector3.Dot(to, referenceRight)) * angle;
        }

        /// <summary>
        ///     Method rotates vector around the Vector.up axis
        /// </summary>
        /// <param name="vectorToRotateAroundVerticalAxis"></param>
        /// <param name="angleToRotate"></param>
        /// <returns></returns>
        public static Vector3 RotateLogicVector(Vector3 vectorToRotateAroundVerticalAxis, float angleToRotate)
        {
            return Quaternion.AngleAxis(angleToRotate, Vector3.up) * vectorToRotateAroundVerticalAxis;
        }

        public static bool CheckIfVectorIsLogic(Vector3 v)
        {
            if (1 - v.magnitude > Epsilon || v.y > Epsilon)
            {
                throw new ApplicationException("Vector is not logic.");
            }

            return true;
        }
    }
}