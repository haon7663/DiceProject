using UnityEngine;

namespace Bezier
{
    public struct RandomBezier
    {
        public Vector2 P0, P1, P2, P3;

        public RandomBezier(Vector2 setter, Vector2 getter, float setRadius, float getRadius)
        {
            P0 = setter;
            P3 = getter;

            var setterDegree = Random.Range(0, 360f);
            var setterRad = setterDegree * Mathf.Deg2Rad;
            var setterVec = new Vector2(Mathf.Cos(setterRad), Mathf.Sin(setterRad));
            P1 = setter + setterVec * setRadius;
            
            var getterDegree = Random.Range(0, 360f);
            var getterRad = getterDegree * Mathf.Deg2Rad;
            var getterVec = new Vector2(Mathf.Cos(getterRad), Mathf.Sin(getterRad));
            P2 = getter + getterVec * getRadius;
        }
    }

    public static class BezierMath
    {
        public static Vector2 GetPoint(Vector2 p0, Vector2 p1, Vector2 p2, float t)
        {
            t = Mathf.Clamp01(t);
            var oneMinusT = 1 - t;

            return Mathf.Pow(oneMinusT, 2) * p0 +
                   2 * oneMinusT * t * p1 +
                   Mathf.Pow(t, 2) * p2;
        }
    
        public static Vector2 GetPoint(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
        {
            t = Mathf.Clamp01(t);
            var oneMinusT = 1 - t;

            return Mathf.Pow(oneMinusT, 3) * p0 +
                   3 * Mathf.Pow(oneMinusT, 2) * t * p1 +
                   3 * oneMinusT * Mathf.Pow(t, 2) * p2 +
                   Mathf.Pow(t, 3) * p3;
        }
    
        public static Vector2 GetPoint(RandomBezier randomBezier, float t)
        {
            return GetPoint(randomBezier.P0, randomBezier.P1, randomBezier.P2, randomBezier.P3, t);
        }
    }
}