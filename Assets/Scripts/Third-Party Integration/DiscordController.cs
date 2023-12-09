using Discord;
using UnityEngine;

public class DiscordController : MonoBehaviour
{
    private long applicationID = 1105705733435170876;
    private string largeImage = "logo";
    private string largeText = "Night Light";
    private long time;
    private bool isInstanceExist;
    public Discord.Discord discord;

    private void Awake()
    {
        if (!isInstanceExist)
        {
            isInstanceExist = true;
            DontDestroyOnLoad(gameObject);
        }
        else if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        discord = new Discord.Discord(applicationID, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);

        time = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();

        UpdateDiscordStatus();
    }

    private void Update()
    {
        try
        {
            discord.RunCallbacks();
        }
        catch
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        UpdateDiscordStatus();
    }

    private void UpdateDiscordStatus()
    {
        // Update Status every frame
        try
        {
            var activityManager = discord.GetActivityManager();
            var activity = new Discord.Activity
            {
                Details = "",
                State = "",
                Assets =
                {
                    LargeImage = largeImage,
                    LargeText = largeText
                },
                Timestamps =
                {
                    Start = time
                }
            };

            activityManager.UpdateActivity(activity, (res) =>
            {
                if (res != Discord.Result.Ok) Debug.LogWarning("Failed connecting to Discord!");
            });
        }
        catch
        {
            // If updating the status fails, Destroy the GameObject
            Destroy(gameObject);
        }
    }
}
