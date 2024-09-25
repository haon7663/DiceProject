using System;
using System.Collections;
using System.Collections.Generic;
using Bezier;
using UnityEngine;

public class DiceCountMovement : MonoBehaviour
{
    [SerializeField] private float setRadius;
    [SerializeField] private float getRadius;

    [SerializeField] private AnimationCurve speedCurve;
    [SerializeField] private float speed;
    
    private RandomBezier _bezier;

    public void Initialize(Vector2 target)
    {
        _bezier = new RandomBezier(transform.position, target, setRadius, getRadius);
        StartCoroutine(UpdateBezier());
    }

    private IEnumerator UpdateBezier()
    {
        for (float i = 0; i < 1; i += Time.deltaTime * speed)
        {
            transform.position = BezierMath.GetPoint(_bezier, speedCurve.Evaluate(i));
            yield return null;
        }
    }
}
