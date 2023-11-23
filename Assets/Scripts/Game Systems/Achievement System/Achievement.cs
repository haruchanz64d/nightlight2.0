[System.Serializable]
public class Achievement
{
    public string achievementID;
    public string title;
    public string description;
    public AchievementDifficulty difficulty;
    public AchievementCondition condition;

    public int currentValue;
    public int maxValue;

    public bool trigger;

    public bool hidden;

    public enum AchievementCondition
    {
        Trigger,
        Numerical
    }

    public enum AchievementDifficulty
    {
        Bronze,
        Silver,
        Gold,
        Platinum
    }
}
