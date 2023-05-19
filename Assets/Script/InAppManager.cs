using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using RevolutionGames;

public class InAppManager : MonoBehaviour, IStoreListener
{

    public UIManager uiManager;
    IStoreController m_StoreController;
    public bool purchaseButtonClicked = false;

#if UNITY_ANDROID
  IGooglePlayStoreExtensions m_GooglePlayStoreExtensions;
    private string subscription6months = "06_months_subscription";
    private string subscription12months = "12_months_subscription";
#else
    IAppleExtensions m_AppleExtensions;
    private string subscription6months = "TOHR06";
    private string subscription12months = "TOHR12";
  #endif  
    
    void Start()
    {
        //uiManager.purchaseExpired = true;

        InitializePurchasing();
    }

    void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(subscription6months, ProductType.Subscription);
        builder.AddProduct(subscription12months, ProductType.Subscription);


        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("In-App Purchasing successfully initialized");

        m_StoreController = controller;
#if UNITY_ANDROID
       m_GooglePlayStoreExtensions = extensions.GetExtension<IGooglePlayStoreExtensions>();
#else
       m_AppleExtensions = extensions.GetExtension<IAppleExtensions>();
#endif

        UpdateUI();
    }

    public void Restore()
    {
        if (uiManager.CheckInternet())
        {
            if (uiManager.apiManager.APIData.subcriptionDetails.Count > 0)
            {
                if (uiManager.apiManager.APIData.subcriptionDetails[0].plan_name != "Free" && uiManager.apiManager.APIData.subcriptionDetails[0].plan_name != "" && uiManager.apiManager.APIData.subcriptionDetails[0].plan_name != null)
                {

                    uiManager.loadingScreen.SetActive(true);
#if UNITY_ANDROID
                    m_GooglePlayStoreExtensions.RestoreTransactions(OnRestore);
#else
        m_AppleExtensions.RestoreTransactions(OnRestore);
#endif
                }
                else
                {
                    uiManager.homeScreen.transform.GetComponent<HomeScreen>().NotRestoreButtonCallBack();
                }
            }
            else
            {
                uiManager.homeScreen.transform.GetComponent<HomeScreen>().NotRestoreButtonCallBack();
            }
        }
    }

    public void BuySubscriptionFor6Months()
    {
        if (uiManager.CheckInternet())
        {
            purchaseButtonClicked = true;
            uiManager.loadingScreen.SetActive(true);
            m_StoreController.InitiatePurchase(subscription6months);
        }
    }
    public void BuySubscriptionFor12Months()
    {
        if (uiManager.CheckInternet())
        {
            purchaseButtonClicked = true;
            uiManager.loadingScreen.SetActive(true);
            m_StoreController.InitiatePurchase(subscription12months);
        }
       
    }

    void OnRestore(bool success)
    {
        var restoreMessage = "";
        if (success)
        {
            // This does not mean anything was restored,
            // merely that the restoration process succeeded.
            restoreMessage = "Restore Successful";
            uiManager.homeScreen.transform.GetComponent<HomeScreen>().OnRestoreButtonCallBack();
        }
        else
        {
            // Restoration failed.
            restoreMessage = "Restore Failed";
            uiManager.homeScreen.transform.GetComponent<HomeScreen>().OnRestoreButtonCallBack(restoreMessage);
        }

        Debug.Log(restoreMessage);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        var product = args.purchasedProduct; 

        Debug.Log($"Processing Purchase: {product.definition.id}");

        //UpdateUI();
        if (purchaseButtonClicked == true)
        {
            UpdatePurchases(product.definition.id);
        }
      

        return PurchaseProcessingResult.Complete;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {

        Debug.Log($"In-App Purchasing initialize failed: {error}");
        
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");

        if (purchaseButtonClicked == true)
        {
            uiManager.homeScreen.transform.GetComponent<HomeScreen>().PurchaseFailure();
        }
        else
        {
            uiManager.purchaseExpired = true;
            //uiManager.homeScreen.transform.GetComponent<HomeScreen>().PurchaseExpired();
        }
           
    }

    bool IsSubscribedTo(Product subscription)
    {
        // If the product doesn't have a receipt, then it wasn't purchased and the user is therefore not subscribed.
        if (subscription.receipt == null)
        {
            return false;
        }

        //The intro_json parameter is optional and is only used for the App Store to get introductory information.
        var subscriptionManager = new SubscriptionManager(subscription, null);

        // The SubscriptionInfo contains all of the information about the subscription.
        // Find out more: https://docs.unity3d.com/Packages/com.unity.purchasing@3.1/manual/UnityIAPSubscriptionProducts.html
        var info = subscriptionManager.getSubscriptionInfo();

        return info.isSubscribed() == Result.True;
    }

    void UpdateUI()
    {
        var subscriptionProduct = m_StoreController.products.WithID(subscription12months);

        try
        {
            var isSubscribed = IsSubscribedTo(subscriptionProduct);
            string message = isSubscribed ? "You are subscribed" : "You are not subscribed";
            print(message);
            print(subscriptionProduct.transactionID);
            
        }
        catch (StoreSubscriptionInfoNotSupportedException)
        {
            var receipt = (Dictionary<string, object>)MiniJson.JsonDecode(subscriptionProduct.receipt);
            var store = receipt["Store"];
            string message = 
                "Couldn't retrieve subscription information because your current store is not supported.\n" +
                $"Your store: \"{store}\"\n\n" +
                "You must use the App Store, Google Play Store or Amazon Store to be able to retrieve subscription information.\n\n" +
                "For more information, see README.md";
            print(message);
        }

        subscriptionProduct = m_StoreController.products.WithID(subscription6months);

        try
        {
            var isSubscribed = IsSubscribedTo(subscriptionProduct);
            string message = isSubscribed ? "You are subscribed" : "You are not subscribed";
            print(message);
            print(subscriptionProduct.transactionID);
        }
        catch (StoreSubscriptionInfoNotSupportedException)
        {
            var receipt = (Dictionary<string, object>)MiniJson.JsonDecode(subscriptionProduct.receipt);
            var store = receipt["Store"];
            string message =
                "Couldn't retrieve subscription information because your current store is not supported.\n" +
                $"Your store: \"{store}\"\n\n" +
                "You must use the App Store, Google Play Store or Amazon Store to be able to retrieve subscription information.\n\n" +
                "For more information, see README.md";
            print(message);
        }
    }
    void UpdatePurchases(string id)
    {
        var subscriptionProduct = m_StoreController.products.WithID(id);

        if (id == subscription12months)
        {
            subscriptionProduct = m_StoreController.products.WithID(subscription12months);

            try
            {
                var isSubscribed = IsSubscribedTo(subscriptionProduct);
                string message = isSubscribed ? "You are subscribed" : "You are not subscribed";
                print(message);
                print(subscriptionProduct.transactionID);
                uiManager.apiManager.APISubscriptionCode("12_months_subscription", 8f, subscriptionProduct.transactionID);
            }
            catch (StoreSubscriptionInfoNotSupportedException)
            {
                var receipt = (Dictionary<string, object>)MiniJson.JsonDecode(subscriptionProduct.receipt);
                var store = receipt["Store"];
                string message =
                    "Couldn't retrieve subscription information because your current store is not supported.\n" +
                    $"Your store: \"{store}\"\n\n" +
                    "You must use the App Store, Google Play Store or Amazon Store to be able to retrieve subscription information.\n\n" +
                    "For more information, see README.md";
                print(message);
                uiManager.homeScreen.transform.GetComponent<HomeScreen>().PurchaseNotAvailable();
            }
        }
        else if (id == subscription6months)
        {
            subscriptionProduct = m_StoreController.products.WithID(subscription6months);

            try
            {
                var isSubscribed = IsSubscribedTo(subscriptionProduct);
                string message = isSubscribed ? "You are subscribed" : "You are not subscribed";
                print(message);
                print(subscriptionProduct.transactionID);
                uiManager.apiManager.APISubscriptionCode("06_months_subscription", 5f, subscriptionProduct.transactionID);
            }
            catch (StoreSubscriptionInfoNotSupportedException)
            {
                var receipt = (Dictionary<string, object>)MiniJson.JsonDecode(subscriptionProduct.receipt);
                var store = receipt["Store"];
                string message =
                    "Couldn't retrieve subscription information because your current store is not supported.\n" +
                    $"Your store: \"{store}\"\n\n" +
                    "You must use the App Store, Google Play Store or Amazon Store to be able to retrieve subscription information.\n\n" +
                    "For more information, see README.md";
                print(message);
                uiManager.homeScreen.transform.GetComponent<HomeScreen>().PurchaseNotAvailable();
            }
        }
    }
}
