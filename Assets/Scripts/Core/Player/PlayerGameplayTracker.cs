using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameplayTracker : MonoBehaviour
{
    private int enemyKillCount = 0;
    private int deathCount = 0;
    private int deathCountFromTraps = 0;
    private bool isMagusDefeated;
    private bool isShadowLilaDefeated;
    private int lightOrbCounter = 0;
    private int hiddenLightOrbCounter = 0;
    private int puzzleSolvedCounter = 0;

    public int GetDeathCountFromTraps
    {
        get
        {
            return deathCountFromTraps;
        }
        set
        {
            deathCountFromTraps = value;
        }
    }
    public int GetPuzzleSolvedCounter
    {
        get
        {
            return puzzleSolvedCounter;
        }
        set
        {
            puzzleSolvedCounter = value;
        }
    }

    public int GetLightOrbCounter
    {
        get
        {
            return lightOrbCounter;
        }
        set
        {
            lightOrbCounter = value;
        }
    }

    public int GetHiddenLightOrbCounter
    {
        get
        {
            return hiddenLightOrbCounter;
        }
        set
        {
            hiddenLightOrbCounter = value;
        }
    }
    public bool IsMagusDefeated
    {
        get
        {
            return isMagusDefeated;
        }
        set
        {
            isMagusDefeated = value;
        }
    }

    public bool IsShadowLilaDefeated
    {
        get
        {
            return isShadowLilaDefeated;
        }
        set
        {
            isShadowLilaDefeated = value;
        }
    }
    public int GetEnemyKillCount
    {
        get
        {
            return enemyKillCount;
        }
        set
        {
            enemyKillCount = value;
        }
    }

    public int GetDeathCount
    {
        get
        {
            return deathCount;
        }
        set
        {
            deathCount = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
