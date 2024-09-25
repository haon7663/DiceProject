using System;

[Serializable]
public class TutorialData
{
    public bool isPlayed;
    public int tutorialIndex;

    public TutorialData(bool isPlayed)
    {
        this.isPlayed = isPlayed;
    }
}