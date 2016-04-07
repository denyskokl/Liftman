using Soomla.Store;
using System.Collections.Generic;

public class StoreAssets : IStoreAssets
{

    public const string COIN_CURRENCY_ITEM_ID = "currency_coin";
    public const string LIVE_GOOD_ID = "live_good";
    public const string COINS_PACK_1_PRODUCT_ID = "coins_pack_1";
    public static VirtualCurrency COIN_CURRENCY = new VirtualCurrency("COINS", "", COIN_CURRENCY_ITEM_ID);

    public static VirtualGood LIVES_GOOD = new SingleUseVG("Live", "", LIVE_GOOD_ID, new PurchaseWithVirtualItem(COIN_CURRENCY_ITEM_ID, 1));


    public static VirtualCurrencyPack COINS_PACK1 =
       new VirtualCurrencyPack("30 Coins", "Test refund of an item", COINS_PACK_1_PRODUCT_ID, 0, COIN_CURRENCY_ITEM_ID, new PurchaseWithMarket(COINS_PACK_1_PRODUCT_ID, 0.99f));


    public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory("General", GetAllGoodIds());

    public VirtualCategory[] GetCategories()
    {
        return new VirtualCategory[] { GENERAL_CATEGORY };
    }

    public static List<string> GetAllGoodIds()
    {
        List<string> ids = new List<string>();
        ids.Add(LIVE_GOOD_ID);
        return ids;
    }

    public VirtualCurrency[] GetCurrencies()
    {
        return new VirtualCurrency[] { COIN_CURRENCY };
    }

    public VirtualCurrencyPack[] GetCurrencyPacks()
    {
        return new VirtualCurrencyPack[] { COINS_PACK1 };
    }

    public VirtualGood[] GetGoods()
    {
        return new VirtualGood[] { LIVES_GOOD };
    }

    public int GetVersion()
    {
        return 1;
    }
}
