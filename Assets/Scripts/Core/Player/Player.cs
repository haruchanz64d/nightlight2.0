using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    #region Singleton
    private Player instance;
    #endregion

    #region PlayerPrefs Keys
    private string adventurerKey = "ADVENTURER";
    private string ascensionKey = "ASCENSION";
    private string awakeningKey = "AWAKENING";
    private string confrontationKey = "CONFRONTATION";
    private string eagleEyeKey = "EAGLE_EYE";
    private string enemyEradicatorKey = "ENEMY_ERADICATOR";
    private string entrapmentKey = "ENTRAPMENT";
    private string equilibriumKey = "EQUILIBRIUM";
    private string firstStrikeKey = "FIRST_STRIKE";
    private string flawlessVictoryKey = "FLAWLESS_VICTORY";
    private string heroTriumphKey = "HEROS_TRIUMPH";
    private string jumpMasterKey = "JUMP_MASTER";
    private string lightOrbCollectorKey = "LIGHT_ORB_COLLECTOR";
    private string nemesisVanquishedKey = "NEMESIS_VANQUISHED";
    private string pacifistKey = "PACIFIST";
    private string pacifistRouteKey = "PACIFIST_ROUTE";
    private string peakClimberKey = "PEAK_CLIMBER";
    private string revelationKey = "REVELATION";
    private string seekerOfHiddenLightKey = "SEEKER_OF_HIDDEN_LIGHT";
    private string speedrunnerKey = "SPEEDRUNNER";
    private string storyConquerorKey = "STORY_CONQUEROR";
    private string trappedNoviceKey = "TRAPPED_NOVICE";
    private string ultimateChallengeKey = "ULTIMATE_CHALLENGE";
    private string ultimateMasterKey = "ULTIMATE_MASTER";
    private string unyieldingKey = "UNYIELDING";
    #endregion

    #region Player Stats
    private int enemyKillCount = 0;
    private int deathCount = 0;
    private int deathCountFromTraps = 0;
    private bool isMagusDefeated;
    private bool isShadowLilaDefeated;
    private int lightOrbCounter = 0;
    private int hiddenLightOrbCounter = 0;
    #endregion

    #region Unity Components
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D box;
    #endregion

    #region Managers
    private GameManager gameManager;
    private AchievementManager achievementManager;
    #endregion

    #region Movement Variables
    [SerializeField] private LayerMask layer;
    [SerializeField] private bool isFacingRight = true;
    private float movement;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float movementSpeed = 7.5f;
    private int jumpCount = 0;
    #endregion

    #region Getters & Setters
    public int GetJumpCount
    {
        get
        {
            return jumpCount;
        }
        set
        {
            jumpCount = value;
        }
    }

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
    #endregion

    #region Unity MonoBehaviour functions
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        achievementManager = FindObjectOfType<AchievementManager>();
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        HandleInput();
        FlipAndAnimate();
    }

    private void LateUpdate()
    {
       AchivementCollection();
    }
    #endregion

    #region Movement
    private void HandleInput()
    {
        movement = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movement * movementSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.C) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            jumpCount++;
        }
    }
    #endregion

    #region Animation
    private void FlipAndAnimate()
    {
        if (isFacingRight && movement < 0f || !isFacingRight && movement > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        animator.SetBool("isRunning", movement != 0f);
    }
    #endregion

    #region Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Light Orb"))
        {
            GetLightOrbCounter += 1;
            DestroyAndRespawn();
        }

        if (collision.gameObject.CompareTag("Hidden Light Orb"))
        {
            GetHiddenLightOrbCounter += 1;
            DestroyAndRespawn();
        }

        if (collision.gameObject.CompareTag("Spike"))
        {
            deathCountFromTraps += 1;
            deathCount += 1;
            DestroyAndRespawn();
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, 0.1f, layer);
    }

    public void DestroyAndRespawn()
    {
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("isDead");
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

    #region Achievement Collection
    private void AchivementCollection()
    {
        ShowAdventurerAchievement();
        ShowAscensionAchievement();
        ShowAwakeningAchievement();
        ShowConfrontationAchievement();
        ShowEagleEyeAchievement();
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
        ShowRevelationAchievement();
        ShowSeekerOfHiddenLightAchievement();
        ShowSpeedrunnerAchievement();
        ShowStoryConquerorAchievement();
        ShowTrappedNoviceAchievement();
        ShowUltimateChallengeAchievement();
        ShowUltimateMasterAchievement();
        ShowUnyieldingAchievement();
    }
    #endregion

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

    private void ShowEagleEyeAchievement()
    {
        if (GetHiddenLightOrbCounter >= 1 && !PlayerPrefs.HasKey(eagleEyeKey))
        {
            achievementManager.ShowNotification(Achievements.EAGLE_EYE);
            PlayerPrefs.SetInt(eagleEyeKey, 1);
        }
    }

    private void ShowEnemyEradicatorAchievement()
    {
        if (GetEnemyKillCount >= 30 && !PlayerPrefs.HasKey(enemyEradicatorKey))
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
        if (GetEnemyKillCount >= 1 && !PlayerPrefs.HasKey(firstStrikeKey))
        {
            achievementManager.ShowNotification(Achievements.FIRST_STRIKE);
            PlayerPrefs.SetInt(firstStrikeKey, 1);
        }
    }

    private void ShowFlawlessVictoryAchievement()
    {
        if (IsMagusDefeated == true && GetDeathCount == 0 && !PlayerPrefs.HasKey(flawlessVictoryKey))
        {
            achievementManager.ShowNotification(Achievements.FLAWLESS_VICTORY);
            PlayerPrefs.SetInt(flawlessVictoryKey, 1);
        }
    }

    private void ShowHeroTriumphAchievement()
    {
        if (IsMagusDefeated == true && !PlayerPrefs.HasKey(heroTriumphKey))
        {
            achievementManager.ShowNotification(Achievements.HEROS_TRIUMPH);
            PlayerPrefs.SetInt(heroTriumphKey, 1);
        }
    }

    private void ShowJumpMasterAchievement()
    {
        if (GetJumpCount >= 300 && !PlayerPrefs.HasKey(jumpMasterKey))
        {
            achievementManager.ShowNotification(Achievements.JUMP_MASTER);
            PlayerPrefs.SetInt(jumpMasterKey, 1);
        }
    }

    private void ShowLightOrbCollectorAchievement()
    {
        if (GetLightOrbCounter > 36 && GetHiddenLightOrbCounter > 4 && !PlayerPrefs.HasKey(lightOrbCollectorKey))
        {
            achievementManager.ShowNotification(Achievements.LIGHT_ORB_COLLECTOR);
            PlayerPrefs.SetInt(lightOrbCollectorKey, 1);
        }
    }

    private void ShowNemesisVanquishedAchievement()
    {
        if (IsShadowLilaDefeated == true && !PlayerPrefs.HasKey(nemesisVanquishedKey))
        {
            achievementManager.ShowNotification(Achievements.NEMESIS_VANQUISHED);
            PlayerPrefs.SetInt(nemesisVanquishedKey, 1);
        }
    }

    private void ShowPacifistAchievement()
    {
        if (GetEnemyKillCount <= 0 && gameManager.IsChapterOneCompleted == true
            || gameManager.IsChapterTwoCompleted == true || gameManager.IsChapterThreeCompleted == true
            || gameManager.IsChapterFourCompleted == true || gameManager.IsChapterFiveCompleted == true && !PlayerPrefs.HasKey(pacifistKey))
        {
            achievementManager.ShowNotification(Achievements.PACIFIST);
            PlayerPrefs.SetInt(pacifistKey, 1);
        }
    }

    private void ShowPacifistRouteAchievement()
    {
        if (GetEnemyKillCount <= 0 && gameManager.IsChapterOneCompleted == true
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

    private void ShowRevelationAchievement()
    {
        if (gameManager.IsChapterThreeCompleted == true && !PlayerPrefs.HasKey(revelationKey))
        {
            achievementManager.ShowNotification(Achievements.REVELATION);
            PlayerPrefs.SetInt(revelationKey, 1);
        }
    }

    private void ShowSeekerOfHiddenLightAchievement()
    {
        if (GetHiddenLightOrbCounter >= 1 && !PlayerPrefs.HasKey(seekerOfHiddenLightKey))
        {
            achievementManager.ShowNotification(Achievements.SEEKER_OF_HIDDEN__LIGHT);
            PlayerPrefs.SetInt(seekerOfHiddenLightKey, 1);
        }
    }

    private void ShowSpeedrunnerAchievement()
    {
        if (GetHiddenLightOrbCounter < 0 && GetLightOrbCounter < 0 &&
            gameManager.IsPrologueCompleted == true && gameManager.IsChapterOneCompleted == true
            && gameManager.IsChapterTwoCompleted == true && gameManager.IsChapterThreeCompleted == true
            && gameManager.IsChapterFourCompleted == true && gameManager.IsChapterFiveCompleted == true
            && gameManager.IsEpilogueCompleted == true
            && !PlayerPrefs.HasKey(speedrunnerKey))
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
        if (GetDeathCountFromTraps >= 1 && !PlayerPrefs.HasKey(trappedNoviceKey))
        {
            achievementManager.ShowNotification(Achievements.TRAPPED_NOVICE);
            PlayerPrefs.SetInt(trappedNoviceKey, 1);
        }
    }

    private void ShowUltimateChallengeAchievement()
    {
        if (GetDeathCount < 0 && GetDeathCountFromTraps < 0
            && GetLightOrbCounter > 36 && GetHiddenLightOrbCounter > 4
            && gameManager.IsChapterOneCompleted == true
            && gameManager.IsChapterTwoCompleted == true && gameManager.IsChapterThreeCompleted == true
            && gameManager.IsChapterFourCompleted == true && gameManager.IsChapterFiveCompleted == true
            && gameManager.IsEpilogueCompleted == true && IsMagusDefeated == true
            && IsShadowLilaDefeated == true && !PlayerPrefs.HasKey(ultimateChallengeKey))
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
               PlayerPrefs.HasKey(eagleEyeKey) &&
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
               PlayerPrefs.HasKey(revelationKey) &&
               PlayerPrefs.HasKey(seekerOfHiddenLightKey) &&
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