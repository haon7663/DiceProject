using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField] private TMP_Text description;

    public void Initialize(string log)
    {
        description.text = log;
    }
    public void SetTrasnparency(float value)
    {
        description.color = new Color(description.color.r, description.color.g, description.color.b, value);
    }
}