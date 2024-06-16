using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Relic : MonoBehaviour
{
    private RelicSO _relicSO;

    [SerializeField] private Image image;

    public void Setup(RelicSO relicSO)
    {
        image.sprite = relicSO.sprite;
    }
}
