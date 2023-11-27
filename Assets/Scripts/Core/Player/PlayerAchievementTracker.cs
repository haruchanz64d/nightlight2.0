using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAchievementTracker : MonoBehaviour
{
    private AchievementManager achievementManager;

    private void Awake()
    {
        achievementManager = FindObjectOfType<AchievementManager>();
    }

    public void GetAllAchievementCompleted()
    {
        achievementManager.ShowNotification(Achievements.Achievement_Junkie);
    }

    public void AchievementCheckpointWhisperer()
    {
        achievementManager.ShowNotification(Achievements.Checkpoint_Whisperer);
    }

    public void AchievementCursedNoMore()
    {
        achievementManager.ShowNotification(Achievements.Cursed_No_More);
    }

    public void AchievementDoubleDasher()
    {
        achievementManager.ShowNotification(Achievements.Double_Dasher);
    }

    public void AchievementEagleEyed()
    {
        achievementManager.ShowNotification(Achievements.Eagle_Eyed);
    }

    public void AchievementEnemyHunter()
    {
        achievementManager.ShowNotification(Achievements.Enemy_Hunter);
    }

    public void AchievementFiftyShadesOfAirborne()
    {
        achievementManager.ShowNotification(Achievements.Fifty_Shades_of_Airborne);
    }

    public void AchievementFirstBlood()
    {
        achievementManager.ShowNotification(Achievements.First_Blood);
    }

    public void AchievementFlawlessVictory()
    {
        achievementManager.ShowNotification(Achievements.Flawless_Victory);
    }

    public void AchievementGameVeteran()
    {
        achievementManager.ShowNotification(Achievements.Game_Veteran);
    }

    public void AchievementGravitysPlaything()
    {
        achievementManager.ShowNotification(Achievements.Gravitys_Plaything);
    }

    public void AchievementHighOnVictory()
    {
        achievementManager.ShowNotification(Achievements.High_On_Victory);
    }

    public void AchievementInTheBeginning()
    {
        achievementManager.ShowNotification(Achievements.In_The_Beginning);
    }

    public void AchievementItemHoarder()
    {
        achievementManager.ShowNotification(Achievements.Item_Hoarder);
    }

    public void AchievementJourneysEnd()
    {
        achievementManager.ShowNotification(Achievements.Journeys_End);
    }

    public void AchievementKeepGoing()
    {
        achievementManager.ShowNotification(Achievements.Keep_Going);
    }

    public void AchievementLightFooted()
    {
        achievementManager.ShowNotification(Achievements.Light_Footed);
    }

    public void AchievementMasterJumper()
    {
        achievementManager.ShowNotification(Achievements.Master_Jumper);
    }

    public void AchievementMasterOfShadows()
    {
        achievementManager.ShowNotification(Achievements.Master_of_Shadows);
    }

    public void AchievementMemoryLane()
    {
        achievementManager.ShowNotification(Achievements.Memory_Lane);
    }

    public void AchievementNightmaresEnd()
    {
        achievementManager.ShowNotification(Achievements.Nightmares_End);
    }

    public void AchievementNoMoreHints()
    {
        achievementManager.ShowNotification(Achievements.No_More_Hints);
    }

    public void AchievementNoRush()
    {
        achievementManager.ShowNotification(Achievements.No_Rush);
    }

    public void AchievementPerfectionist()
    {
        achievementManager.ShowNotification(Achievements.Perfectionist);
    }

    public void AchievementPuzzleSolver()
    {
        achievementManager.ShowNotification(Achievements.Puzzle_Solver);
    }

    public void AchievementRaceAgainstTheClock()
    {
        achievementManager.ShowNotification(Achievements.Race_Against_The_Clock);
    }

    public void AchievementRelentlessPursuit()
    {
        achievementManager.ShowNotification(Achievements.Relentless_Pursuit);
    }

    public void AchievementSecretsRevealed()
    {
        achievementManager.ShowNotification(Achievements.Secrets_Revealed);
    }

    public void AchievementShadowWalker()
    {
        achievementManager.ShowNotification(Achievements.Shadow_Walker);
    }

    public void AchievementSparklingDiscovery()
    {
        achievementManager.ShowNotification(Achievements.Sparkling_Discovery);
    }

    public void AchievementSpireTrailblazer()
    {
        achievementManager.ShowNotification(Achievements.Spire_Trailblazer);
    }

    public void AchievementStealthyNegotiator()
    {
        achievementManager.ShowNotification(Achievements.Stealthy_Negotiator);
    }

    public void AchievementSubterraneanResurgence()
    {
        achievementManager.ShowNotification(Achievements.Subterranean_Resurgence);
    }

    public void AchievementTheEquilibrium()
    {
        achievementManager.ShowNotification(Achievements.The_Equilibrium);
    }

    public void AchievementTheRevelation()
    {
        achievementManager.ShowNotification(Achievements.The_Revelation);
    }

    public void AchievementThisGameSeemsFamiliar()
    {
        achievementManager.ShowNotification(Achievements.This_Game_Seems_Familiar);
    }

    public void AchievementUltimateCollector()
    {
        achievementManager.ShowNotification(Achievements.Ultimate_Collector);
    }

    public void AchievementVerdantOdyssey()
    {
        achievementManager.ShowNotification(Achievements.Verdant_Odyssey);
    }

    public void AchievementVisibleSavior()
    {
        achievementManager.ShowNotification(Achievements.Visible_Savior);
    }

    public void AchievementWatchOut()
    {
        achievementManager.ShowNotification(Achievements.Watch_Out);
    }
}
