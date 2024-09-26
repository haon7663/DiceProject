using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TMP_Text description;

    public void Initialize(string log, TextAlignmentOptions textAlignmentOptions)
    {
        description.text = log;
        descriptionText.alignment = textAlignmentOptions;
    }
    public void SetTransparency(float value)
    {
        description.color = new Color(description.color.r, description.color.g, description.color.b, value);
    }
}