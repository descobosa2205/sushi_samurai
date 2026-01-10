using UnityEngine;

public enum BackgroundType
{
    Wood,
    Rio,
    Cocina,
    Terraza
}

public static class PlayerData
{
    // ===============================
    // PlayerPrefs Keys
    // ===============================
    private const string CoinsKey = "Coins";
    private const string SelectedBgKey = "SelectedBackground";
    private const string BoughtRioKey = "Bought_Rio";
    private const string BoughtCocinaKey = "Bought_Cocina";
    private const string BoughtTerrazaKey = "Bought_Terraza";
    private const string LastRunPointsKey = "LastRunPoints";

    // ===============================
    // Prices
    // ===============================
    public const int PriceRio = 20;
    public const int PriceCocina = 50;
    public const int PriceTerraza = 100;

    // ===============================
    // Coins
    // ===============================
    public static int Coins
    {
        get => PlayerPrefs.GetInt(CoinsKey, 0);
        private set
        {
            PlayerPrefs.SetInt(CoinsKey, Mathf.Max(0, value));
            PlayerPrefs.Save();
        }
    }

    public static void AddCoins(int amount)
    {
        Coins += Mathf.Max(0, amount);
    }

    public static bool SpendCoins(int amount)
    {
        if (Coins < amount)
            return false;

        Coins -= amount;
        return true;
    }

    // ===============================
    // Last Run Points (GameOver)
    // ===============================
    public static int LastRunPoints
    {
        get => PlayerPrefs.GetInt(LastRunPointsKey, 0);
        set
        {
            PlayerPrefs.SetInt(LastRunPointsKey, Mathf.Max(0, value));
            PlayerPrefs.Save();
        }
    }

    // ===============================
    // Selected Background
    // ===============================
    public static BackgroundType SelectedBackground
    {
        get
        {
            int v = PlayerPrefs.GetInt(SelectedBgKey, (int)BackgroundType.Wood);
            return (BackgroundType)Mathf.Clamp(v, 0, (int)BackgroundType.Terraza);
        }
        set
        {
            PlayerPrefs.SetInt(SelectedBgKey, (int)value);
            PlayerPrefs.Save();
        }
    }

    // ===============================
    // Purchases
    // ===============================
    public static bool IsBought(BackgroundType bg)
    {
        return bg switch
        {
            BackgroundType.Wood => true,
            BackgroundType.Rio => PlayerPrefs.GetInt(BoughtRioKey, 0) == 1,
            BackgroundType.Cocina => PlayerPrefs.GetInt(BoughtCocinaKey, 0) == 1,
            BackgroundType.Terraza => PlayerPrefs.GetInt(BoughtTerrazaKey, 0) == 1,
            _ => false
        };
    }

    private static void SetBought(BackgroundType bg, bool bought)
    {
        int v = bought ? 1 : 0;

        switch (bg)
        {
            case BackgroundType.Rio:
                PlayerPrefs.SetInt(BoughtRioKey, v);
                break;
            case BackgroundType.Cocina:
                PlayerPrefs.SetInt(BoughtCocinaKey, v);
                break;
            case BackgroundType.Terraza:
                PlayerPrefs.SetInt(BoughtTerrazaKey, v);
                break;
        }

        PlayerPrefs.Save();
    }

    // ===============================
    // Prices helper
    // ===============================
    public static int GetPrice(BackgroundType bg)
    {
        return bg switch
        {
            BackgroundType.Rio => PriceRio,
            BackgroundType.Cocina => PriceCocina,
            BackgroundType.Terraza => PriceTerraza,
            _ => 0
        };
    }

    // ===============================
    // Buy + Select Logic
    // ===============================
    public static bool TryBuyAndSelect(BackgroundType bg)
    {
        // Wood is always available
        if (bg == BackgroundType.Wood)
        {
            SelectedBackground = bg;
            return true;
        }

        // Already bought → just select
        if (IsBought(bg))
        {
            SelectedBackground = bg;
            return true;
        }

        // Try to buy
        int price = GetPrice(bg);
        if (!SpendCoins(price))
        {
            Debug.LogWarning(
                $"Not enough coins for {bg}. Needed {price}, you have {Coins}."
            );
            return false;
        }

        // Purchase success
        SetBought(bg, true);
        SelectedBackground = bg;
        return true;
    }

    // ===============================
    // Reset All Progress
    // ===============================
    public static void ResetAllProgress()
    {
        PlayerPrefs.DeleteKey(CoinsKey);
        PlayerPrefs.DeleteKey(SelectedBgKey);
        PlayerPrefs.DeleteKey(LastRunPointsKey);

        PlayerPrefs.DeleteKey(BoughtRioKey);
        PlayerPrefs.DeleteKey(BoughtCocinaKey);
        PlayerPrefs.DeleteKey(BoughtTerrazaKey);

        PlayerPrefs.Save();
    }
}