using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerAchievementTracker : MonoBehaviour
{
    private AchievementManager achievementManager;
    private GameManager gameManager;
    private Player player;
    private PlayerGameplayTracker playerGameplayTracker;

    #region PlayerPrefs Keys
    private string adventurerKey = "AdventurerAchievement";
    private string ascensionKey = "AscensionAchievement";
    private string awakeningKey = "AwakeningAchievement";
    private string confrontationKey = "ConfrontationAchievement";
    private string enemyEradicatorKey = "EnemyEradicatorAchievement";
    private string entrapmentKey = "EntrapmentAchievement";
    private string equilibriumKey = "EquilibriumAchievement";
    private string firstStrikeKey = "FirstStrikeAchievement";
    private string flawlessVictoryKey = "FlawlessVictoryAchievement";
    private string heroTriumphKey = "HeroTriumphAchievement";
    private string jumpMasterKey = "JumpMasterAchievement";
    private string lightOrbCollectorKey = "LightOrbCollectorAchievement";
    private string nemesisVanquishedKey = "NemesisVanquishedAchievement";
    private string pacifistKey = "PacifistAchievement";
    private string pacifistRouteKey = "PacifistRouteAchievement";
    private string peakClimberKey = "PeakClimberAchievement";
    private string puzzleMasterKey = "PuzzleMasterAchievement";
    private string revelationKey = "RevelationAchievement";
    private string seekerOfLightKey = "SeekerOfLightAchievement";
    private string speedrunnerKey = "SpeedrunnerAchievement";
    private string storyConquerorKey = "StoryConquerorAchievement";
    private string trappedNoviceKey = "TrappedNoviceAchievement";
    private string ultimateChallengeKey = "UltimateChallengeAchievement";
    private string ultimateMasterKey = "UltimateMasterAchievement";
    private string unyieldingKey = "UnyieldingAchievement";
    #endregion

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerGameplayTracker = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGameplayTracker>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        achievementManager = FindObjectOfType<AchievementManager>();
    }

    void Update()
    {
        ShowAdventurerAchievement();
        ShowAscensionAchievement();
        ShowAwakeningAchievement();
        ShowConfrontationAchievement();
        ShowEnemyEradicatorAchievement();
        ShowEntrapmentAchievement();
        ShowEquilibriumAchievement();
        ShowFirstStrikeAchievement();
        ShowFlawlessVictoryAchievement();
        ShowHeroTriumphAchievement();
        ShowJumpMasterAchievement();
        ShowLightOrbCollectorAchievement();
        ShowNemesisVanquishedAchievement();
        ShowPacifistAchievement();
        ShowPacifistRouteAchievement();
        ShowPeakClimberAchievement();
        ShowPuzzleMasterAchievement();
        ShowRevelationAchievement();
        ShowSeekerOfLightAchievement();
        ShowSpeedrunnerAchievement();
        ShowStoryConquerorAchievement();
        ShowTrappedNoviceAchievement();
        ShowUltimateChallengeAchievement();
        ShowUltimateMasterAchievement();
        ShowUnyieldingAchievement();
    }

    #region Achievement Functions

    private void ShowAdventurerAchievement()
    {
        if (gameManager.IsGameCompleted == true && !PlayerPrefs.HasKey(adventurerKey))
        {
            achievementManager.ShowNotification(Achievements.ADVENTURER);
            PlayerPrefs.SetInt(adventurerKey, 1);
        }
    }

    private void ShowAscensionAchievement()
    {
        if (gameManager.IsChapterFourCompleted == true && !PlayerPrefs.HasKey(ascensionKey))
        {
            achievementManager.ShowNotification(Achievements.ASCENSION);
            PlayerPrefs.SetInt(ascensionKey, 1);
        }
    }

    private void ShowAwakeningAchievement()
    {
        if (gameManager.IsPrologueCompleted == true && !PlayerPrefs.HasKey(awakeningKey))
        {
            achievementManager.ShowNotification(Achievements.AWAKENING);
            PlayerPrefs.SetInt(awakeningKey, 1);
        }
    }

    private void ShowConfrontationAchievement()
    {
        if (gameManager.IsChapterFiveCompleted == true && !PlayerPrefs.HasKey(confrontationKey))
        {
            achievementManager.ShowNotification(Achievements.CONFRONTATION);
            PlayerPrefs.SetInt(confrontationKey, 1);
        }
    }

    private void ShowEnemyEradicatorAchievement()
    {
        if (playerGameplayTracker.GetEnemyKillCount >= 300 && !PlayerPrefs.HasKey(enemyEradicatorKey))
        {
            achievementManager.ShowNotification(Achievements.ENEMY_ERADICATOR);
            PlayerPrefs.SetInt(enemyEradicatorKey, 1);
        }
    }

    private void ShowEntrapmentAchievement()
    {
        if (gameManager.IsChapterTwoCompleted == true && !PlayerPrefs.HasKey(entrapmentKey))
        {
            achievementManager.ShowNotification(Achievements.ENTRAPMENT);
            PlayerPrefs.SetInt(entrapmentKey, 1);
        }
    }

    private void ShowEquilibriumAchievement()
    {
        if (gameManager.IsEpilogueCompleted == true && !PlayerPrefs.HasKey(equilibriumKey))
        {
            achievementManager.ShowNotification(Achievements.EQUILIBRIUM);
            PlayerPrefs.SetInt(equilibriumKey, 1);
        }
    }

    private void ShowFirstStrikeAchievement()
    {
        if (playerGameplayTracker.GetEnemyKillCount >= 1 && !PlayerPrefs.HasKey(firstStrikeKey))
        {
            achievementManager.ShowNotification(Achievements.FIRST_STRIKE);
            PlayerPrefs.SetInt(firstStrikeKey, 1);
        }
    }

    private void ShowFlawlessVictoryAchievement()
    {
        if (playerGameplayTracker.IsMagusDefeated == true && playerGameplayTracker.GetDeathCount == 0 && !PlayerPrefs.HasKey(flawlessVictoryKey))
        {
            achievementManager.ShowNotification(Achievements.FLAWLESS_VICTORY);
            PlayerPrefs.SetInt(flawlessVictoryKey, 1);
        }
    }

    private void ShowHeroTriumphAchievement()
    {
        if (playerGameplayTracker.IsMagusDefeated == true && !PlayerPrefs.HasKey(heroTriumphKey))
        {
            achievementManager.ShowNotification(Achievements.HEROS_TRIUMPH);
            PlayerPrefs.SetInt(heroTriumphKey, 1);
        }
    }

    private void ShowJumpMasterAchievement()
    {
        if (player.GetJumpCount >= 10 && !PlayerPrefs.HasKey(jumpMasterKey))
        {
            achievementManager.ShowNotification(Achievements.JUMP_MASTER);
            PlayerPrefs.SetInt(jumpMasterKey, 1);
        }
    }

    private void ShowLightOrbCollectorAchievement()
    {
        if (playerGameplayTracker.GetLightOrbCounter > 36 && playerGameplayTracker.GetHiddenLightOrbCounter > 4 && !PlayerPrefs.HasKey(lightOrbCollectorKey))
        {
            achievementManager.ShowNotification(Achievements.LIGHT_ORB_COLLECTOR);
            PlayerPrefs.SetInt(lightOrbCollectorKey, 1);
        }
    }

    private void ShowNemesisVanquishedAchievement()
    {
        if (playerGameplayTracker.IsShadowLilaDefeated == true && !PlayerPrefs.HasKey(nemesisVanquishedKey))
        {
            achievementManager.ShowNotification(Achievements.NEMESIS_VANQUISHED);
            PlayerPrefs.SetInt(nemesisVanquishedKey, 1);
        }
    }

    private void ShowPacifistAchievement()
    {
        if (playerGameplayTracker.GetEnemyKillCount <= 0 && gameManager.IsChapterOneCompleted == true
            || gameManager.IsChapterTwoCompleted == true || gameManager.IsChapterThreeCompleted == true
            || gameManager.IsChapterFourCompleted == true || gameManager.IsChapterFiveCompleted == true && !PlayerPrefs.HasKey(pacifistKey))
        {
            achievementManager.ShowNotification(Achievements.PACIFIST);
            PlayerPrefs.SetInt(pacifistKey, 1);
        }
    }

    private void ShowPacifistRouteAchievement()
    {
        if (playerGameplayTracker.GetEnemyKillCount <= 0 && gameManager.IsChapterOneCompleted == true
            || gameManager.IsChapterTwoCompleted == true || gameManager.IsChapterThreeCompleted == true
            || gameManager.IsChapterFourCompleted == true || gameManager.IsChapterFiveCompleted == true && !PlayerPrefs.HasKey(pacifistRouteKey))
        {
            achievementManager.ShowNotification(Achievements.PACIFIST_ROUTE);
            PlayerPrefs.SetInt(pacifistRouteKey, 1);
        }
    }

    private void ShowPeakClimberAchievement()
    {
        if (SceneManager.GetActiveScene().name == "Chapter 5" && !PlayerPrefs.HasKey(peakClimberKey))
        {
            achievementManager.ShowNotification(Achievements.PEAK_CLIMBER);
            PlayerPrefs.SetInt(peakClimberKey, 1);
        }
    }

    private void ShowPuzzleMasterAchievement()
    {
        if (playerGameplayTracker.GetPuzzleSolvedCounter >= 5 && !PlayerPrefs.HasKey(puzzleMasterKey))
        {
            achievementManager.ShowNotification(Achievements.PUZZLE_MASTER);
            PlayerPrefs.SetInt(puzzleMasterKey, 1);
        }
    }

    private void ShowRevelationAchievement()
    {
        if (gameManager.IsChapterThreeCompleted == true && !PlayerPrefs.HasKey(revelationKey))
        {
            achievementManager.ShowNotification(Achievements.REVELATION);
            PlayerPrefs.SetInt(revelationKey, 1);
        }
    }

    private void ShowSeekerOfLightAchievement()
    {
        if (playerGameplayTracker.GetHiddenLightOrbCounter >= 1 && !PlayerPrefs.HasKey(seekerOfLightKey))
        {
            achievementManager.ShowNotification(Achievements.SEEKER_OF_LIGHT);
            PlayerPrefs.SetInt(seekerOfLightKey, 1);
        }
    }

    private void ShowSpeedrunnerAchievement()
    {
        if (playerGameplayTracker.GetHiddenLightOrbCounter < 0 && playerGameplayTracker.GetLightOrbCounter < 0 && !PlayerPrefs.HasKey(speedrunnerKey))
        {
            achievementManager.ShowNotification(Achievements.SPEEDRUNNER);
            PlayerPrefs.SetInt(speedrunnerKey, 1);
        }
    }

    private void ShowStoryConquerorAchievement()
    {
        if (gameManager.IsPrologueCompleted == true && gameManager.IsChapterOneCompleted == true
            && gameManager.IsChapterTwoCompleted == true && gameManager.IsChapterThreeCompleted == true
            && gameManager.IsChapterFourCompleted == true && gameManager.IsChapterFiveCompleted == true
            && gameManager.IsEpilogueCompleted == true && !PlayerPrefs.HasKey(storyConquerorKey))
        {
            achievementManager.ShowNotification(Achievements.STORY_CONQUEROR);
            PlayerPrefs.SetInt(storyConquerorKey, 1);
        }
    }

    private void ShowTrappedNoviceAchievement()
    {
        if (playerGameplayTracker.GetDeathCountFromTraps >= 1 && !PlayerPrefs.HasKey(trappedNoviceKey))
        {
            achievementManager.ShowNotification(Achievements.TRAPPED_NOVICE);
            PlayerPrefs.SetInt(trappedNoviceKey, 1);
        }
    }

    private void ShowUltimateChallengeAchievement()
    {
        if (playerGameplayTracker.GetDeathCount < 0 && playerGameplayTracker.GetDeathCountFromTraps < 0
            && playerGameplayTracker.GetLightOrbCounter > 36 && playerGameplayTracker.GetHiddenLightOrbCounter > 4
            && playerGameplayTracker.GetPuzzleSolvedCounter > 4 && gameManager.IsPrologueCompleted == true
            && gameManager.IsChapterOneCompleted == true
            && gameManager.IsChapterTwoCompleted == true && gameManager.IsChapterThreeCompleted == true
            && gameManager.IsChapterFourCompleted == true && gameManager.IsChapterFiveCompleted == true
            && gameManager.IsEpilogueCompleted == true && playerGameplayTracker.IsMagusDefeated == true
            && playerGameplayTracker.IsShadowLilaDefeated == true && !PlayerPrefs.HasKey(ultimateChallengeKey))
        {
            achievementManager.ShowNotification(Achievements.ULTIMATE_CHALLENGE);
            PlayerPrefs.SetInt(ultimateChallengeKey, 1);
        }
    }

    private void ShowUltimateMasterAchievement()
    {
        if (AreAllAchievementsObtained() && !PlayerPrefs.HasKey(ultimateMasterKey))
        {
            achievementManager.ShowNotification(Achievements.ULTIMATE_MASTER);
            PlayerPrefs.SetInt(ultimateMasterKey, 1);
        }
    }

    private bool AreAllAchievementsObtained()
    {
        return PlayerPrefs.HasKey(adventurerKey) &&
               PlayerPrefs.HasKey(ascensionKey) &&
               PlayerPrefs.HasKey(awakeningKey) &&
               PlayerPrefs.HasKey(confrontationKey) &&
               PlayerPrefs.HasKey(enemyEradicatorKey) &&
               PlayerPrefs.HasKey(entrapmentKey) &&
               PlayerPrefs.HasKey(equilibriumKey) &&
               PlayerPrefs.HasKey(firstStrikeKey) &&
               PlayerPrefs.HasKey(flawlessVictoryKey) &&
               PlayerPrefs.HasKey(heroTriumphKey) &&
               PlayerPrefs.HasKey(jumpMasterKey) &&
               PlayerPrefs.HasKey(lightOrbCollectorKey) &&
               PlayerPrefs.HasKey(nemesisVanquishedKey) &&
               PlayerPrefs.HasKey(pacifistKey) &&
               PlayerPrefs.HasKey(pacifistRouteKey) &&
               PlayerPrefs.HasKey(peakClimberKey) &&
               PlayerPrefs.HasKey(puzzleMasterKey) &&
               PlayerPrefs.HasKey(revelationKey) &&
               PlayerPrefs.HasKey(seekerOfLightKey) &&
               PlayerPrefs.HasKey(speedrunnerKey) &&
               PlayerPrefs.HasKey(storyConquerorKey) &&
               PlayerPrefs.HasKey(trappedNoviceKey) &&
               PlayerPrefs.HasKey(ultimateChallengeKey);
    }

    private void ShowUnyieldingAchievement()
    {
        if (gameManager.IsChapterOneCompleted == true && !PlayerPrefs.HasKey(unyieldingKey))
        {
            achievementManager.ShowNotification(Achievements.UNYIELDING);
            PlayerPrefs.SetInt(unyieldingKey, 1);
        }
    }

    #endregion
}