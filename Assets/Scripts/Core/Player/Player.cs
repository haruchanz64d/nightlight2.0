using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class Player : MonoBehaviour
{
    #region Random Variables
    private Vector2 lastCheckpointPosition;

    public Vector2 GetLastCheckpointPosition
    {
        get
        {
            return lastCheckpointPosition;
        }

        set
        {
            lastCheckpointPosition = value;
        }
    }

    private bool isInteractedWithCheckpoint;

    public bool IsInteractedWithCheckpoint
    {
        get
        {
            return isInteractedWithCheckpoint;
        }
        set
        {
            isInteractedWithCheckpoint = value;
        }
    }
    #endregion

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
    private string herosTriumphKey = "HEROS_TRIUMPH";
    private string jumpMasterKey = "JUMP_MASTER";
    private string lightOrbCollectorKey = "LIGHT_ORB_COLLECTOR";
    private string noRushKey = "NO_RUSH";
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
    private float idleMaxTimer = 60f;
    private float lastInputTime;
    private float currentIdleTimer;
    #endregion

    #region Unity Components
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D box;
    [SerializeField] private TextMeshProUGUI lightOrbCount;
    [SerializeField] private TextMeshProUGUI scorePointText;
    #endregion

    #region Managers
    private GameManager gameManager;
    private AchievementManager achievementManager;
    private CheckpointSystem checkpointSystem;
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

    #region Chapter Progress
    private bool isPrologueCompleted;
    private bool isChapterOneCompleted;
    private bool isChapterTwoCompleted;
    private bool isChapterThreeCompleted;
    private bool isChapterFourCompleted;
    private bool isChapterFiveCompleted;
    private bool isEpilogueCompleted;
    private bool isGameCompleted;
    #endregion

    #region Chapter Getter And Setter
    public bool IsPrologueCompleted
    {
        get { return isPrologueCompleted; }
        set { isPrologueCompleted = value; }
    }

    public bool IsChapterOneCompleted
    {
        get { return isChapterOneCompleted; }
        set { isChapterOneCompleted = value; }
    }

    public bool IsChapterTwoCompleted
    {
        get { return isChapterTwoCompleted; }
        set { isChapterTwoCompleted = value; }
    }

    public bool IsChapterThreeCompleted
    {
        get { return isChapterThreeCompleted; }
        set { isChapterThreeCompleted = value; }
    }

    public bool IsChapterFourCompleted
    {
        get { return isChapterFourCompleted; }
        set { isChapterFourCompleted = value; }
    }

    public bool IsChapterFiveCompleted
    {
        get { return isChapterFiveCompleted; }
        set { isChapterFiveCompleted = value; }
    }

    public bool IsEpilogueCompleted
    {
        get { return isEpilogueCompleted; }
        set { isEpilogueCompleted = value; }
    }

    public bool IsGameCompleted
    {
        get { return isGameCompleted; }
        set
        {
            isGameCompleted = value;
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
        checkpointSystem = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointSystem>();
        CreatePlayerStatsJSON();

        int totalLightOrb = lightOrbCounter + hiddenLightOrbCounter;

        lightOrbCount.SetText($"x {totalLightOrb}");

        int scorePoint = totalLightOrb * 100;
        scorePointText.SetText($"Score: {scorePoint}");

        if (IsInteractedWithCheckpoint)
        {
            transform.position = lastCheckpointPosition;
        }
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
        lastInputTime = Time.time;
        currentIdleTimer = 0.0f;

        LoadPlayerStats();
    }
    private void Update()
    {
        if (gameManager.IsGamePaused == true) return;
        HandleInput();
        FlipAndAnimate();

        currentIdleTimer = Time.time - lastInputTime;

        int totalLightOrb = lightOrbCounter + hiddenLightOrbCounter;

        lightOrbCount.SetText($"x {totalLightOrb}");

        int scorePoint = totalLightOrb * 100;
        scorePointText.SetText($"Score: {scorePoint}");
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.OnPause();
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
            collision.gameObject.GetComponent<AudioSource>().Play();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Dark Orb"))
        {
            deathCount += 1;
            DestroyAndRespawn();
        }

        if (collision.gameObject.CompareTag("Hidden Light Orb"))
        {
            GetHiddenLightOrbCounter += 1;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Spike"))
        {
            deathCountFromTraps += 1;
            deathCount += 1;
            DestroyAndRespawn();
        }

        if (collision.gameObject.CompareTag("Kill Plane"))
        {
            deathCount += 1;
            DestroyAndRespawn();
        }

        if (collision.gameObject.CompareTag("SceneHandler"))
        {
            SavePlayerStats();
            collision.GetComponent<SceneHandler>().LoadScene();
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, 0.1f, layer);
    }

    public void DestroyAndRespawn()
    {
        SavePlayerStats();
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
        ShowHeroTriumphedAchievement();
        ShowJumpMasterAchievement();
        ShowLightOrbCollectorAchievement();
        ShowNoRushAchievement();
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
        if (IsGameCompleted == true && !PlayerPrefs.HasKey(adventurerKey))
        {
            achievementManager.ShowNotification(Achievements.ADVENTURER);
            PlayerPrefs.SetInt(adventurerKey, 1);
        }
    }

    private void ShowAscensionAchievement()
    {
        if (IsChapterFourCompleted == true && !PlayerPrefs.HasKey(ascensionKey))
        {
            achievementManager.ShowNotification(Achievements.ASCENSION);
            PlayerPrefs.SetInt(ascensionKey, 1);
        }
    }

    private void ShowAwakeningAchievement()
    {
        if (IsPrologueCompleted == true && !PlayerPrefs.HasKey(awakeningKey))
        {
            achievementManager.ShowNotification(Achievements.AWAKENING);
            PlayerPrefs.SetInt(awakeningKey, 1);
        }
    }

    private void ShowConfrontationAchievement()
    {
        if (IsChapterFiveCompleted == true && !PlayerPrefs.HasKey(confrontationKey))
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
        if (IsChapterTwoCompleted == true && !PlayerPrefs.HasKey(entrapmentKey))
        {
            achievementManager.ShowNotification(Achievements.ENTRAPMENT);
            PlayerPrefs.SetInt(entrapmentKey, 1);
        }
    }

    private void ShowEquilibriumAchievement()
    {
        if (IsEpilogueCompleted == true && !PlayerPrefs.HasKey(equilibriumKey))
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

    private void ShowHeroTriumphedAchievement()
    {
        if (IsMagusDefeated == true && !PlayerPrefs.HasKey(herosTriumphKey))
        {
            achievementManager.ShowNotification(Achievements.HEROS_TRIUMPH);
            PlayerPrefs.SetInt(herosTriumphKey, 1);
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
    private void ShowNoRushAchievement()
    {
        if (currentIdleTimer >= idleMaxTimer && !PlayerPrefs.HasKey(noRushKey))
        {
            achievementManager.ShowNotification(Achievements.NO_RUSH);
            PlayerPrefs.SetInt(noRushKey, 1);
        }
    }

    private void ShowPacifistAchievement()
    {
        if (GetEnemyKillCount <= 0 && IsChapterOneCompleted == true
            || IsChapterTwoCompleted == true || IsChapterThreeCompleted == true
            || IsChapterFourCompleted == true || IsChapterFiveCompleted == true && !PlayerPrefs.HasKey(pacifistKey))
        {
            achievementManager.ShowNotification(Achievements.PACIFIST);
            PlayerPrefs.SetInt(pacifistKey, 1);
        }
    }

    private void ShowPacifistRouteAchievement()
    {
        if (GetEnemyKillCount <= 0 && IsChapterOneCompleted == true
            || IsChapterTwoCompleted == true || IsChapterThreeCompleted == true
            || IsChapterFourCompleted == true || IsChapterFiveCompleted == true && !PlayerPrefs.HasKey(pacifistRouteKey))
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
        if (IsChapterThreeCompleted == true && !PlayerPrefs.HasKey(revelationKey))
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
            IsPrologueCompleted == true && IsChapterOneCompleted == true
            && IsChapterTwoCompleted == true && IsChapterThreeCompleted == true
            && IsChapterFourCompleted == true && IsChapterFiveCompleted == true
            && IsEpilogueCompleted == true
            && !PlayerPrefs.HasKey(speedrunnerKey))
        {
            achievementManager.ShowNotification(Achievements.SPEEDRUNNER);
            PlayerPrefs.SetInt(speedrunnerKey, 1);
        }
    }

    private void ShowStoryConquerorAchievement()
    {
        if (IsPrologueCompleted == true && IsChapterOneCompleted == true
            && IsChapterTwoCompleted == true && IsChapterThreeCompleted == true
            && IsChapterFourCompleted == true && IsChapterFiveCompleted == true
            && IsEpilogueCompleted == true && !PlayerPrefs.HasKey(storyConquerorKey))
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
            && IsChapterOneCompleted == true
            && IsChapterTwoCompleted == true && IsChapterThreeCompleted == true
            && IsChapterFourCompleted == true && IsChapterFiveCompleted == true
            && IsEpilogueCompleted == true && IsMagusDefeated == true
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
               PlayerPrefs.HasKey(herosTriumphKey) &&
               PlayerPrefs.HasKey(jumpMasterKey) &&
               PlayerPrefs.HasKey(lightOrbCollectorKey) &&
               PlayerPrefs.HasKey(noRushKey) &&
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
        if (IsChapterOneCompleted == true && !PlayerPrefs.HasKey(unyieldingKey))
        {
            achievementManager.ShowNotification(Achievements.UNYIELDING);
            PlayerPrefs.SetInt(unyieldingKey, 1);
        }
    }

    #endregion

    #region Create, Save and Load
    private void CreatePlayerStatsJSON()
    {
        string saveFilePath = Application.persistentDataPath + "/playerStats.json";
        if (!File.Exists(saveFilePath))
        {
            var playerStatsData = new PlayerStatsData();
            string jsonString = JsonUtility.ToJson(playerStatsData, true);
            File.WriteAllText(saveFilePath, jsonString);
        }
    }

    public void SavePlayerStats()
    {
        var dataToSave = new PlayerStatsData
        {
            enemyKillCount = enemyKillCount,
            deathCount = deathCount,
            deathCountFromTraps = deathCountFromTraps,
            isMagusDefeated = isMagusDefeated,
            isShadowLilaDefeated = isShadowLilaDefeated,
            lightOrbCounter = lightOrbCounter,
            hiddenLightOrbCounter = hiddenLightOrbCounter,
            isPrologueCompleted = isPrologueCompleted,
            isChapterOneCompleted = isChapterOneCompleted,
            isChapterTwoCompleted = isChapterTwoCompleted,
            isChapterThreeCompleted = isChapterThreeCompleted,
            isChapterFourCompleted = isChapterFourCompleted,
            isChapterFiveCompleted = isChapterFiveCompleted,
            isEpilogueCompleted = isEpilogueCompleted,
            isGameCompleted = isGameCompleted,
        };

        string jsonString = JsonUtility.ToJson(dataToSave, true);

        string saveFilePath = Application.persistentDataPath + "/playerStats.json";

        File.WriteAllText(saveFilePath, jsonString);
    }
    public void LoadPlayerStats()
    {
        string saveFilePath = Application.persistentDataPath + "/playerStats.json";

        if (!File.Exists(saveFilePath))
        {
            return;
        }

        string jsonString = File.ReadAllText(saveFilePath);
        var playerStats = JsonUtility.FromJson<PlayerStatsData>(jsonString);

        enemyKillCount = playerStats.enemyKillCount;
        deathCount = playerStats.deathCount;
        deathCountFromTraps = playerStats.deathCountFromTraps;
        isMagusDefeated = playerStats.isMagusDefeated;
        isShadowLilaDefeated = playerStats.isShadowLilaDefeated;
        lightOrbCounter = playerStats.lightOrbCounter;
        hiddenLightOrbCounter = playerStats.hiddenLightOrbCounter;
        isPrologueCompleted = playerStats.isPrologueCompleted;
        isChapterOneCompleted = playerStats.isChapterOneCompleted;
        isChapterTwoCompleted = playerStats.isChapterTwoCompleted;
        isChapterThreeCompleted = playerStats.isChapterThreeCompleted;
        isChapterFourCompleted = playerStats.isChapterFourCompleted;
        isChapterFiveCompleted = playerStats.isChapterFiveCompleted;
        isEpilogueCompleted = playerStats.isEpilogueCompleted;
        isGameCompleted = playerStats.isGameCompleted;
    }
    #endregion
}

[System.Serializable]
public class PlayerStatsData
{
    public int enemyKillCount;
    public int deathCount;
    public int deathCountFromTraps;
    public bool isMagusDefeated;
    public bool isShadowLilaDefeated;
    public int lightOrbCounter;
    public int hiddenLightOrbCounter;
    public bool isPrologueCompleted;
    public bool isChapterOneCompleted;
    public bool isChapterTwoCompleted;
    public bool isChapterThreeCompleted;
    public bool isChapterFourCompleted;
    public bool isChapterFiveCompleted;
    public bool isEpilogueCompleted;
    public bool isGameCompleted;
}