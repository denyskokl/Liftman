using UnityEngine;
using System.Collections;
using System;
using Soomla.Store;

public class CreditsManager : MonoBehaviour {


    public static CreditsManager Instance { get; private set; }
    Action _callback = null;

    public static Action OnVirtualPufchaseFail = delegate { };
    public static Action OnSilverExchangeFail = delegate { };
    public StoreAssets Assets;

    public int Coins
    {
        get { return StoreInventory.GetItemBalance(StoreAssets.COIN_CURRENCY_ITEM_ID); }
    }
    public int Lives
    {
        get { return StoreInventory.GetItemBalance(StoreAssets.LIVE_GOOD_ID); }
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            Instance = null;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitSoomlaStore();
    }

    public void InitSoomlaStore()
    {
        StoreEvents.OnUnexpectedStoreError = (i) => { Debug.Log("!!SOOMLA ERROR: " + i.ToString()); };

        StoreEvents.OnCurrencyBalanceChanged += (a, b, c) =>
        {
           
        };

        if (!SoomlaStore.Initialized)
        {
            Assets = new StoreAssets();
            SoomlaStore.Initialize(Assets);
#if UNITY_ANDROID
            SoomlaStore.StartIabServiceInBg();
#endif
        }

    }

    public void GiveCurrency(int amount, bool playSound = true)
    {
        StoreInventory.GiveItem(StoreAssets.COIN_CURRENCY_ITEM_ID, amount);
    }
}
