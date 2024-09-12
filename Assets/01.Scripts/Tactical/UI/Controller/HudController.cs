using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class HudController : Singleton<HudController>
{
    [SerializeField] private Transform canvas;
    
    [Header("Prefabs")]
    [SerializeField] private Hud avoidHud;
    [SerializeField] private Hud damageHud;
    [SerializeField] private Hud recoveryHud;
    [SerializeField] private Hud statusEffectHud;

    [Header("Values")]
    [SerializeField] private Vector2 offset;
    [SerializeField] private float randomRadius;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void PopAvoid(Vector2 pos)
    {
        var hud = Instantiate(avoidHud, canvas);
        hud.Initialize(_camera.WorldToScreenPoint(pos + offset + Random.insideUnitCircle * randomRadius));
    }

    public void PopDamage(Vector2 pos, int value)
    {
        var hud = Instantiate(damageHud, canvas);
        hud.Initialize(_camera.WorldToScreenPoint(pos + offset + Random.insideUnitCircle * randomRadius), value);
    }
    
    public void PopStatusEffect(Vector2 pos, int value, Sprite sprite)
    {
        var hud = Instantiate(statusEffectHud, canvas);
        hud.Initialize(_camera.WorldToScreenPoint(pos + offset + Random.insideUnitCircle * randomRadius), value, sprite);
    }
}
