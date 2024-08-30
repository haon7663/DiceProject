using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HudController : Singleton<HudController>
{
    [SerializeField] private Transform canvas;
    
    [Header("Prefabs")]
    [SerializeField] private Hud damageHud;
    [SerializeField] private Hud recoveryHud;

    [Header("Values")]
    [SerializeField] private Vector2 offset;
    [SerializeField] private float randomRadius;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void PopDamage(Vector2 pos, int value)
    {
        var hud = Instantiate(damageHud, canvas);
        hud.Initialize(_camera.WorldToScreenPoint(pos + offset + Random.insideUnitCircle * randomRadius), value);
    }
}
