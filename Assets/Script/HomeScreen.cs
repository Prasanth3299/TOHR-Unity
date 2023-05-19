using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.Networking;
using System;
using System.Text.RegularExpressions;

namespace RevolutionGames
{

    public class HomeScreen : MonoBehaviour
    {
        #region Variables

        public UIManager uiManager;
        public GameObject homeScreen;
        public GameObject introScreen;
        public GameObject notificationScreen;
        public GameObject notificationpopupScreen;
        public RectTransform notificationContent;
        //public GameObject promoCodeScreen;
        public GameObject purchaseScreen;
        public Text purchaseText;
        public Text purchaseHeaderText;
        public Text welcomeText;
        public Text introductionText;
        public TMP_Text inclusionText;
        public Text subscribrPlanText, priceText;
        //public InputField promoField;
        public GameObject inclusionPrefab, emptyImage, wrongImage;
        public GameObject subscribeYearlyParent, subscribeHalfYearlyparent, onImage, offImage;
        private string promoCode;
        public GameObject notificationOnImage;
        public GameObject notificationOffImage;
        public Text emptyText;
        public Image profile;
        public Image setting;
        public Image textureImage;
        public GameObject renderImage;
        public Sprite avatar;
        public RectTransform homeContent;
        public GameObject trialImage;
        public Text timerText;
        public GameObject externalLinkPrefab;
        public GameObject restoreButton;
        public GameObject restoreScreen;
        public Text restoreText;
        public GameObject notificationHomePopup;
        public GameObject expertsScreen;
        public GameObject spinningTipsScreen;
        public GameObject freeButton;
        public GameObject paidButton;
        public Dropdown titleSelectDropdown;
        public InputField askField;
        public GameObject homeNotificationOnImage;
        public GameObject homeNotificationOffImage;
        public GameObject vipImage, nonVipImsge;
        public InputField askEmailField;
        public GameObject askEmailObject;
        public GameObject askemailText;
        public GameObject freeTrialOffImage;
        private string streamingAssetsPath;
        private List<string> purchaseDateAnnual = new List<string>();
        private List<string> tranIdAnnual = new List<string>();
        private List<string> linkUrl = new List<string>();
        private List<GameObject> externalLinkObject = new List<GameObject>();
        private bool flag = false;
        private string screenName = "";
        private string askTitle;
        private string gameStatus;
        private string askEmail;
        const string matchEmailPattern = @"^([a-zA-Z0-9_\-\+\.]+)@([a-zA-Z0-9_\-\+\.]+)\.([a-zA-Z]{2,5})$";
        #endregion

        #region Built in Methods
        // Start is called before the first frame update
        void Start()
        {
#if UNITY_STANDALONE_OSX || UNITY_IOS
            restoreButton.SetActive(true);
            streamingAssetsPath = Application.dataPath + "/StreamingAssets/Profile/";
#else
            streamingAssetsPath = Application.persistentDataPath + "/StreamingAssets/Profile/";
            restoreButton.SetActive(false);
#endif
            

            
            ProfilePictureUpdate();
            /*APIData.ExternaLink externaLink1 = new APIData.ExternaLink();
            externaLink1.logo = "https://tohruserdata.s3.ap-south-1.amazonaws.com/profile/37465004-43e6-4dd0-91db-d6a56f3315d2.png";
            externaLink1.url = "https://api.tohrlimited.com:3002/7042f01e-fa16-48e5-ad8d-58f4737888ad.html";
            externaLink1.enable_or_disable = "1";

            uiManager.apiManager.APIData.external_link.Add(externaLink1);
            uiManager.apiManager.APIData.external_link.Add(externaLink1);*/

            OnExternaLink();
        }

        // Update is called once per frame
        void Update()
        {
            if (trialImage.activeSelf&&homeScreen.activeSelf)
            {
                if (uiManager.apiManager.APIData.myProfile[0].paid_user == 1.ToString())
                {
                    if (uiManager.apiManager.APIData.subcriptionDetails.Count > 0)
                    {
                        if (uiManager.apiManager.APIData.subcriptionDetails[0].plan_name == "Free")
                        {
                            OnFreeTrialCountDownTimer();
                        }
                    }
                    
                   
                }
                    
            }
            OnPhoneBackButtonHandler();
        }

        public void OnPhoneBackButtonHandler()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (screenName == "notification")
                {
                    OnNotificationBackButtonClicked();
                }
                else if (screenName == "intro")
                {
                    OnIntroBackButtonClicked();
                }
                else
                {
                   // Application.Quit();
                }
            }
        }
        private void OnEnable()
        {
            trialImage.SetActive(false);
            freeTrialOffImage.SetActive(false);
            homeContent.anchoredPosition = new Vector2(0, 0);
            if (Directory.Exists(streamingAssetsPath))
            {
                //ProfilePictureUpdate();
            }
            if (uiManager.apiManager.APIData.myProfile[0].paid_user == 1.ToString())
            {
                freeButton.SetActive(false);
                paidButton.SetActive(true);
                if (uiManager.apiManager.APIData.subcriptionDetails.Count > 0)
                {
                    if (uiManager.apiManager.APIData.subcriptionDetails[0].plan_name == "Free")
                    {
                        OnFreeTrialCountDownTimer();
                    }
                    else if (uiManager.apiManager.APIData.subcriptionDetails[0].promo_code != null)
                    {
                        //promoParent.SetActive(false);
                        trialImage.SetActive(false);
                    }
                    else
                    {
                        //promoParent.SetActive(true);
                    }
                }
            }
            else
            {
                freeButton.SetActive(true);
                paidButton.SetActive(false);
                // api call
                uiManager.loadingScreen.SetActive(true);
                uiManager.apiManager.APIEmailValidation();
            }

            if (uiManager.imageFlag == false)
            {
               ProfilePictureUpdate();
            }

        }
        #endregion

        #region Custom Methods
        public void OnEmailRegisterWithLinkApiCallBack(int activate,int paidUser,int notification)
        {
            if (activate == 0)
            {
                freeTrialOffImage.SetActive(true);
                trialImage.SetActive(false);
            }
            else
            {
                freeTrialOffImage.SetActive(false);
                trialImage.SetActive(false);
            }
            if (paidUser==1)
            {
                // registerd
                // get subscription detail api
                uiManager.apiManager.APIData.myProfile[0].paid_user = paidUser.ToString();
                uiManager.apiManager.APIData.myProfile[0].notification = notification;
                uiManager.loadingScreen.SetActive(false);
                uiManager.apiManager.APISubcriptionDetailsHomeFirst();

            }
            else
            {
                uiManager.loadingScreen.SetActive(false);
            }
        }
        public void OnGetSubscriptionDetailsFirstCallback(string plan, int isExpired, string subscriptionToken = "")
        {
            if (plan == "Free")
            {
                InitialUpdate();
                OnFreeTrialCountDownTimer();
            }
            uiManager.loadingScreen.SetActive(false);
        }


        public void OnFreeTrialCountDownTimer()
        {
            try
            {
                string startDate = uiManager.apiManager.APIData.subcriptionDetails[0].expire_date;
                //print("startDate :" + startDate);
                string[] splitString = startDate.Split(char.Parse("/"));
                string finalString = splitString[1] + "-" + splitString[0] + "-" + splitString[2];
                //print("finalString :" + finalString);
                TimeSpan timeDifference;
                DateTime currentDate = DateTime.UtcNow;
                //print("currentDate :" + currentDate);
                DateTime tempTime = Convert.ToDateTime(finalString);
                long temp = Convert.ToInt64(tempTime.ToBinary());
                //long temp = long.Parse(uiManager.apiManager.APIData.subcriptionDetails[0].started_date);
                DateTime oldDate = DateTime.FromBinary(temp);
                //oldDate = oldDate.AddHours(24);
                timeDifference = oldDate.Subtract(currentDate);
                freeTrialOffImage.SetActive(false);
                trialImage.SetActive(true);
                //print("come");
                if (timeDifference.Days > 0)
                {
                    timerText.text = "Your Free Premium Feature Expires in " + timeDifference.Days + "d " + timeDifference.Hours + "h " + timeDifference.Minutes + "m " + timeDifference.Seconds + "s ";
                }
                else
                {
                    timerText.text = "Your Free Premium Feature Expires in " + timeDifference.Hours + "h " + timeDifference.Minutes + "m " + timeDifference.Seconds + "s ";
                }
                
                if (timeDifference.Minutes <= 0 && timeDifference.Seconds < 0)
                {
                    uiManager.apiManager.APIData.myProfile[0].paid_user = 0.ToString();
                    trialImage.SetActive(false);
                    freeTrialOffImage.SetActive(false);
                    InitialUpdate();
                    if (uiManager.CheckInternet())
                    {
                        uiManager.apiManager.APIUpdateSubcriptionDetails();
                    }

                }
            }
            
            catch (Exception e)
            {
                Checktime();
            }

        }
        public void Checktime()
        {
            string startDate = uiManager.apiManager.APIData.subcriptionDetails[0].expire_date;
            string[] splitString = startDate.Split(char.Parse("/"));
            string finalString = splitString[1] + "-" + splitString[0] + "-" + splitString[2];
            string[] split = finalString.Split(char.Parse(" "));
            string[] splitTime = split[1].Split(char.Parse(":"));
            string[] splitDate = split[0].Split(char.Parse("-"));

            DateTime currentDate = DateTime.UtcNow;
            DateTime startDate1 = new DateTime(int.Parse(splitDate[2]), int.Parse(splitDate[1]), int.Parse(splitDate[0]), int.Parse(splitTime[0]), int.Parse(splitTime[1]), int.Parse(splitTime[2]));
            //startDate1 = startDate1.AddHours(24);
            TimeSpan timeDifference1 = startDate1 - currentDate;
            
            trialImage.SetActive(true);
            if (timeDifference1.Days > 0)
            {
                timerText.text = "Your Free Premium Feature Expires in " + timeDifference1.Days + "d " + timeDifference1.Hours + "h " + timeDifference1.Minutes + "m " + timeDifference1.Seconds + "s ";
            }
            else
            {
                timerText.text = "Your Free Premium Feature Expires in " + timeDifference1.Hours + "h " + timeDifference1.Minutes + "m " + timeDifference1.Seconds + "s ";
            }
            if (timeDifference1.Minutes <= 0 && timeDifference1.Seconds < 0)
            {
                uiManager.apiManager.APIData.myProfile[0].paid_user = 0.ToString();
                trialImage.SetActive(false);
                InitialUpdate();
                if (uiManager.CheckInternet())
                {
                    uiManager.apiManager.APIUpdateSubcriptionDetails();
                }

            }
        }
        public void FacebookImageLoad()
        {
            uiManager.uploadImageLoadingScreen.SetActive(true);
            byte[] textureBytes = profile.sprite.texture.EncodeToPNG();
            //File.WriteAllBytes(streamingAssetsPath + "profile" + ".png", textureBytes);
            uiManager.apiManager.APIUploadDocument(textureBytes, "profile" + ".png","home");

        }
        public void UpdateImageCallBack()
        {
           
            ProfilePictureUpdate();
            
        }

        public void ProfilePictureUpdate()
        {
            // print("profile");
            uiManager.imageFlag = true;
            uiManager.uploadImageLoadingScreen.SetActive(true);
            uiManager.settingScreen.transform.GetComponent<SettingScreen>().imageFlag = true;
            uiManager.loadingScreen.SetActive(true);
            if (uiManager.apiManager.APIData.myProfile[0].profile_image_url != null)
            {
                uiManager.loadingScreen.SetActive(true);
                StartCoroutine(LoadTextureFromWeb(uiManager.apiManager.APIData.myProfile[0].profile_image_url,"profile"));
            }
            else
            {
                if (PlayerPrefs.GetInt("fblogin") != 1)
                {
                    profile.sprite = avatar;
                    setting.sprite = avatar;
                }
                uiManager.loadingScreen.SetActive(false);
                uiManager.uploadImageLoadingScreen.SetActive(false);
            }
          
            //uiManager.loadingScreen.SetActive(false);
        }

        IEnumerator LoadTextureFromWeb(string url,string name=null)
        {
            uiManager.settingScreen.transform.GetComponent<SettingScreen>().imageFlag = false;
            uiManager.loadingScreen.SetActive(true);
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            www.timeout = 200;
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                Texture2D loadedTexture = DownloadHandlerTexture.GetContent(www);
                if (name == "profile")
                {
                    profile.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
                    setting.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
                    //textureImage.SetNativeSize();
                }

                //print("down home");
            }
            //print("end");
            yield return new WaitForSeconds(0.5f);
            uiManager.loadingScreen.SetActive(false);
            uiManager.uploadImageLoadingScreen.SetActive(false);
        }

        public void InitialUpdate()
        {
            if (uiManager.apiManager.APIData.myProfile[0].paid_user == "0")
            {
                homeNotificationOnImage.SetActive(false);
                homeNotificationOffImage.SetActive(true);
                notificationOnImage.SetActive(false);
                notificationOffImage.SetActive(true);
                subscribeYearlyParent.SetActive(true);
                subscribeHalfYearlyparent.SetActive(true);
                freeButton.SetActive(true);
                paidButton.SetActive(false);
                //promoParent.SetActive(true);
            }
            else
            {
                freeButton.SetActive(false);
                paidButton.SetActive(true);
                if (uiManager.apiManager.APIData.myProfile[0].notification == 1)
                {
                    homeNotificationOnImage.SetActive(true);
                    homeNotificationOffImage.SetActive(false);
                    notificationOnImage.SetActive(true);
                    notificationOffImage.SetActive(false);
                }
                else
                {
                    homeNotificationOnImage.SetActive(true);
                    homeNotificationOffImage.SetActive(false);
                    notificationOnImage.SetActive(false);
                    notificationOffImage.SetActive(true);
                }

                //subscribeYearlyParent.SetActive(false);
                //subscribeHalfYearlyparent.SetActive(false);
                //promoParent.SetActive(false);
            }

            welcomeText.text = uiManager.apiManager.APIData.myProfile[0].first_name;

            
        }
        public void OnExternaLink()
        {
          

            if (uiManager.apiManager.APIData.external_link.Count > 0)
            {
                for (int i = 6; i < homeContent.childCount; i++)
                {
                    Destroy(homeContent.GetChild(i).gameObject);
                }

                externalLinkObject.Clear();
                linkUrl.Clear();
                for (int i = 0; i < uiManager.apiManager.APIData.external_link.Count; i++)
                {
                    if (uiManager.apiManager.APIData.external_link[i].enable_or_disable == "1")
                    {
                        int count = i;
                        GameObject link = Instantiate(externalLinkPrefab, homeContent);
                        link.transform.GetComponent<Button>().onClick.AddListener(() => externalButtonClicked(count));
                        externalLinkObject.Add(link);
                        linkUrl.Add(uiManager.apiManager.APIData.external_link[i].logo);
                    }

                    if (linkUrl.Count > 0)
                    {
                        OnLinkImageLoad(0);
                    }

                }
            }
        }
        public void OnLinkImageLoad(int count)
        {
            uiManager.loadingScreen.SetActive(true);
            StartCoroutine(LoadTextureFromWebLink(linkUrl[count],count));
        }
        IEnumerator LoadTextureFromWebLink(string url, int count)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            www.timeout = 200;
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                Texture2D loadedTexture = DownloadHandlerTexture.GetContent(www);
               
                   externalLinkObject[count].transform.GetChild(0).GetComponent<Image>().sprite= Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
                   
                //print("down home");
            }
            if (linkUrl.Count - 1 != count)
            {
                OnLinkImageLoad(count = count + 1);
            }
            uiManager.loadingScreen.SetActive(false);
        }
        public void externalButtonClicked(int index)
        {
            Application.OpenURL(uiManager.apiManager.APIData.external_link[index].url);
        }

        public void OnIntroButtonClicked()
        {
            if (uiManager.CheckInternet())
            {
                uiManager.apiManager.APIIntroduction();
                uiManager.loadingScreen.SetActive(true);
            }

        }
        public void IntoCallBack(List<APIData.Intoduction> intoductions)
        {
            uiManager.loadingScreen.SetActive(false);
            if (intoductions.Count > 0)
            {
                for (int i = 0; i < intoductions.Count; i++)
                {
                    introductionText.text =uiManager.TextFilter(intoductions[0].detail);
                }
                homeScreen.SetActive(false);
                introScreen.SetActive(true);
                screenName = "intro";
            }
           
            else
            {
                uiManager.noDataScreen.SetActive(true);
            }
            
        }
        public void OnBasicButtonClicked()
        {
            if (uiManager.CheckInternet())
            {
                this.gameObject.SetActive(false);
                uiManager.basicInfoScreen.SetActive(true);
                uiManager.screenName = "basichome";
                uiManager.screenSwitch = true;
            }

        }
        public void OnSpinningButtonClicked()
        {
            if (uiManager.CheckInternet())
            {
                this.gameObject.SetActive(false);
                uiManager.spinningPatternAndRewardScreen.SetActive(true);
                uiManager.screenName = "spinhome";
                uiManager.screenSwitch = true;
            }

        }
        public void OnVikingButtonClicked()
        {
            if (uiManager.CheckInternet())
            {
                this.gameObject.SetActive(false);
                uiManager.vikingScreen.SetActive(true);
                uiManager.screenName = "vikinghome";
                uiManager.screenSwitch = true;
            }

        }
        public void OnNotificationYesButtonCLicked()
        {
            notificationHomePopup.SetActive(false);
            //OnNotificationButtonClicked();
        }
        public void OnNotificationNoButtonClicked()
        {
            notificationHomePopup.SetActive(false);
        }
        public void OnExpertsButtonClicked()
        {
            if (uiManager.CheckInternet())
            {
                titleSelectDropdown.value = 0;
                DropdownButtonClicked();
                if (uiManager.apiManager.APIData.myProfile[0].email.Contains(SystemInfo.deviceUniqueIdentifier))
                {
                    askEmail = "";
                    askEmailField.text = "";
                }
                else
                {
                    askEmailField.text = uiManager.apiManager.APIData.myProfile[0].email;
                    askEmail = uiManager.apiManager.APIData.myProfile[0].email;
                }
                
                nonVipImsge.SetActive(true);
                vipImage.SetActive(false);
                gameStatus = "Non-VIP";
                //titleSelectDropdown.onValueChanged.AddListener(delegate { DropdownButtonClicked(titleSelectDropdown);
                notificationScreen.SetActive(false);
                expertsScreen.SetActive(true);
            }
        }
        public void OnAskEmailField(string email)
        {

            askEmail = email;
            askEmail = askEmailField.text;
        }
        public void OnVIPButtonClicked()
        {
            nonVipImsge.SetActive(false);
            vipImage.SetActive(true);
            gameStatus = "VIP";
        }
        public void OnNonVIPButtonClicked()
        {
            nonVipImsge.SetActive(true);
            vipImage.SetActive(false);
            gameStatus = "Non-VIP";
        }
        public void DropdownButtonClicked()
        {
            int index = titleSelectDropdown.value;
            askTitle = titleSelectDropdown.options[index].text;
            print(titleSelectDropdown.options[index].text);
        }
        public void OnAskSubmitBUttonClciked()
        {
            if (askEmail.Length > 0)
            {
                if (Regex.IsMatch(askEmail, matchEmailPattern))
                {
                    if (askField.text.Length <= 0)
                    {
                        emptyText.text = "Please enter your query";
                        emptyImage.SetActive(true);
                        StartCoroutine(FieldRequriedImageOff());
                    }
                    else
                    {
                        if (uiManager.CheckInternet())
                        {
                            uiManager.loadingScreen.SetActive(true);
                            uiManager.apiManager.APIAskExperts(askTitle, askField.text, gameStatus, askEmail);
                        }
                    }
                }
                else
                {
                    emptyText.text = "Please enter valid email";
                    emptyImage.SetActive(true);
                    StartCoroutine(FieldRequriedImageOff());
                }
                   
            }
            else
            {
                emptyText.text = "Please enter email";
                emptyImage.SetActive(true);
                StartCoroutine(FieldRequriedImageOff());
            }
            
        }
        public void AskSubmitCallBack()
        {
            uiManager.loadingScreen.SetActive(false);
            emptyText.text = "Successfully send your query";
            emptyImage.SetActive(true);
            StartCoroutine(FieldRequriedImageOff());
            
            notificationScreen.SetActive(true);
            expertsScreen.SetActive(false);
        }
        public void OnExpertsBackButtonClicked()
        {
            notificationScreen.SetActive(true);
            expertsScreen.SetActive(false);
        }
        public void OnSpinningTipsButtonClicked()
        {
            if (uiManager.CheckInternet())
            {
                notificationScreen.SetActive(false);
                spinningTipsScreen.SetActive(true);
            }
        }
        public void OnSpinningTipsBackButtonClicked()
        {
            notificationScreen.SetActive(true);
            spinningTipsScreen.SetActive(false);
        }

        public void OnNotificationButtonClicked()
        {
            if (uiManager.CheckInternet())
            {
                uiManager.loadingScreen.SetActive(true);
                //uiManager.apiManager.APINoticicationCheck();
                //promoField.text = "";
                notificationContent.anchoredPosition = new Vector2(0, 0);
                uiManager.apiManager.APIinclusion();
                for (int j = 4; j < notificationContent.childCount; j++)
                {
                    Destroy(notificationContent.GetChild(j).gameObject);
                }
            }
            if (uiManager.apiManager.APIData.myProfile[0].paid_user == "0")
            {
                //homeNotificationOnImage.SetActive(false);
                //homeNotificationOffImage.SetActive(true);
                notificationOnImage.SetActive(false);
                notificationOffImage.SetActive(true);
                subscribeYearlyParent.SetActive(true);
                subscribeHalfYearlyparent.SetActive(true);
                freeButton.SetActive(true);
                paidButton.SetActive(false);
                //promoParent.SetActive(true);
            }
            else
            {
                freeButton.SetActive(false);
                paidButton.SetActive(true);
                if (uiManager.apiManager.APIData.myProfile[0].notification == 1)
                {
                    //homeNotificationOnImage.SetActive(true);
                    //homeNotificationOffImage.SetActive(false);
                    notificationOnImage.SetActive(true);
                    notificationOffImage.SetActive(false);
                }
                else
                {
                    //homeNotificationOnImage.SetActive(false);
                    //homeNotificationOffImage.SetActive(true);
                    notificationOnImage.SetActive(false);
                    notificationOffImage.SetActive(true);
                }
                subscribeYearlyParent.SetActive(true);
                subscribeHalfYearlyparent.SetActive(true);
                //promoParent.SetActive(false);
            }

            /*homeScreen.SetActive(false);
            notificationScreen.SetActive(true);*/
        }

        public void CheckNotificationCallBack(string message = null)
        {

            if (message == null)
            {
                uiManager.apiManager.APIinclusion();
                //subscribeParent.SetActive(false);
                //promoParent.SetActive(false);
                //memberparent.SetActive(false);
                onImage.SetActive(true);
                offImage.SetActive(false);
            }
            else
            {
                uiManager.apiManager.APIGetAllSubscription();
                //subscribeParent.SetActive(true);
                //promoParent.SetActive(true);
                //memberparent.SetActive(true);
                onImage.SetActive(false);
                offImage.SetActive(true);
            }
        }

        public void GetAllSubscribeAPiCAllBack(List<APIData.GetAllSubscription> getAllSubscriptions)
        {
            for (int j = 0; j < notificationContent.childCount; j++)
            {
                Destroy(notificationContent.GetChild(j).gameObject);
            }
            uiManager.apiManager.APIinclusion();
            

        }
        public void InclusionAPICallBack(List<APIData.Inclusion> inclusions)
        {
            for (int j = 0; j < notificationContent.childCount; j++)
            {
                Destroy(notificationContent.GetChild(j).gameObject);
            }
            for (int i = 0; i < inclusions.Count; i++)
            {
                GameObject inclusion = Instantiate(inclusionPrefab, notificationContent);
                inclusion.GetComponent<Text>().text = inclusions[i].inclusion;
            }
            promoCode = "";



            /* homeScreen.SetActive(false);
             notificationScreen.SetActive(true);*/
            //uiManager.loadingScreen.SetActive(true);
            if (uiManager.apiManager.APIData.myProfile[0].paid_user == 1.ToString())
            {
                uiManager.loadingScreen.SetActive(true);
                uiManager.apiManager.APISubcriptionDetails();
            }
            else
            {
                homeScreen.SetActive(false);
                notificationScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
                screenName = "notification";
            }

        }
        public void PromoInputField(string code)
        {
            promoCode = code;
        }
        public void OnSubmitPromoButtonClicked()
        {
            if (promoCode.Length > 0)
            {
                if (uiManager.CheckInternet())
                {
                    uiManager.loadingScreen.SetActive(true);
                    uiManager.apiManager.APIPromoCode(promoCode);
                }
            }
            else
            {
                emptyText.text = "Promo code is empty";
                emptyImage.SetActive(true);
                StartCoroutine(FieldRequriedImageOff());
            }
        }
        public void PromoCodeAPICallBack(string message)
        {
            uiManager.loadingScreen.SetActive(false);
            if (message == "Promo Code not Exists")
            {
                print(message);
                emptyText.text = message;
                emptyImage.SetActive(true);
                StartCoroutine(FieldRequriedImageOff());
            }
            else
            {
                uiManager.loadingScreen.SetActive(true);
                PromoSuccessSubscribe();
                emptyText.text = "Successfully promo code applied";
                emptyImage.SetActive(true);
                //promoParent.SetActive(false);
                trialImage.SetActive(false);
                StartCoroutine(FieldRequriedImageOff());
            }
        }
        public void PromoSuccessSubscribe()
        {
            //homeNotificationOnImage.SetActive(true);
            //homeNotificationOffImage.SetActive(false);
            notificationOnImage.SetActive(true);
            notificationOffImage.SetActive(false);
            //subscribeYearlyParent.SetActive(false);
            //subscribeHalfYearlyparent.SetActive(false);
            //promoParent.SetActive(false);
            uiManager.loadingScreen.SetActive(false);
            //promoCodeScreen.SetActive(false);
        }
        public IEnumerator FieldRequriedImageOff()
        {
            yield return new WaitForSeconds(1);
            emptyImage.SetActive(false);
            wrongImage.SetActive(false);
        }
        public void OnSettingButtonClicked()
        {
            if (uiManager.CheckInternet())
            {
                this.gameObject.SetActive(false);
                uiManager.settingScreen.SetActive(true);
                uiManager.screenName = "settinghome";
                uiManager.screenSwitch = true;
            }

        }
        public void OnIntroBackButtonClicked()
        {
            introScreen.SetActive(false);
            homeScreen.SetActive(true);
            screenName = "";
        }
        public void OnNotificationBackButtonClicked()
        {
            notificationContent.anchoredPosition = new Vector2(0, 0);
            notificationScreen.SetActive(false);
            homeScreen.SetActive(true);
            screenName = "";
        }
        public void OnPromoCodeButtonCLicked()
        {
            // api call
            //promoField.text = "";
            promoCode = "";
            //promoCodeScreen.SetActive(true);
        }
        public void OnPromoCodeCloseButtonCLicked()
        {
            //promoCodeScreen.SetActive(false);
        }
        public void OnPurchaseBackButtonClicked()
        {
            purchaseScreen.SetActive(false);
        }
        public void OnNotificationEnableButtonClicked()
        {
            print(uiManager.apiManager.APIData.myProfile[0].paid_user);
            if (uiManager.apiManager.APIData.myProfile[0].paid_user == "1")
            {
                //notificationpopupScreen.SetActive(false);
                uiManager.loadingScreen.SetActive(true);
                if (notificationOnImage.activeSelf)
                {
                    uiManager.apiManager.APIUpdateNotification(false);
                }
                else
                {
                    uiManager.apiManager.APIUpdateNotification(true);
                }

            }
            else
            {
                notificationpopupScreen.SetActive(true);
            }
        }
        public void UpdateNotificatiionApiCallBack(string message = null)
        {
            InitialUpdate();
            uiManager.loadingScreen.SetActive(false);
        }
        public void OnNotificationPopupCloseButtonCLicked()
        {
            notificationpopupScreen.SetActive(false);
        }

        public void OnProfileButtonClicked()
        {
            if (uiManager.CheckInternet())
            {
                uiManager.loadingScreen.SetActive(true);
                this.gameObject.SetActive(false);
                //uiManager.settingScreen.SetActive(true);
                uiManager.settingScreen.transform.GetComponent<SettingScreen>().HomeProfileButtonClicked();
            }
            
            

        }
        public void SelectProfile()
        {
            PickImage(1);
        }
        private void PickImage(int maxSize)
        {
            
            NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
            {
                Debug.Log("Image path: " + path);
                if (path != null)
                {
                    if (!Directory.Exists(streamingAssetsPath))

                    {
                        Directory.CreateDirectory(streamingAssetsPath);
                    }
                    else
                    {
                        var hi = Directory.GetFiles(streamingAssetsPath);

                        for (int j = 0; j < hi.Length; j++)
                        {

                            File.Delete(hi[j]);
                        }

                        //Directory.Delete(streamingAssetsPath + "/Documents/");
                       
                    }
                    // Create Texture from selected image
                    string fileName = "";
                    fileName = Path.GetFileName(path);
                    File.Copy(path, streamingAssetsPath + Path.GetFileName(path));
                    Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
                    if (texture == null)
                    {
                        Debug.Log("Couldn't load texture from " + path);
                        return;
                    }

                    profile.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
                    uiManager.loadingScreen.SetActive(true);
                    //ProfilePictureUpdate();
                    //profiletext.SetActive(false);
                    // Assign texture to a temporary quad and destroy it after 5 seconds
                    /*GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
                    quad.transform.forward = Camera.main.transform.forward;
                    quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

                    Material material = quad.GetComponent<Renderer>().material;
                    if (!material.shader.isSupported) // happens when Standard shader is not included in the build
                        material.shader = Shader.Find("Legacy Shaders/Diffuse");

                    material.mainTexture = texture;

                    Destroy(quad, 5f);

                    // If a procedural texture is not destroyed manually, 
                    // it will only be freed after a scene change
                    Destroy(texture, 5f);*/
                }
            });

            Debug.Log("Permission result: " + permission);
        }

        // Subscription


        public void OnGetSubscriptionDetailsCallback(string plan, int isExpired, string subscriptionToken = "")
        {
            subscribeHalfYearlyparent.SetActive(true);
            subscribeYearlyParent.SetActive(true);

            print(subscriptionToken);
            if (uiManager.apiManager.APIData.subcription_Data.subscriptionToken != "None" && subscriptionToken != "")
            {
                uiManager.apiManager.APIData.subcription_Data.subscriptionToken = subscriptionToken;
            }
            if (plan == "Free" && isExpired == 0)
            {
                uiManager.apiManager.APIData.subcription_Data.subscriptionPlan = "Free";
                uiManager.apiManager.APIData.subcription_Data.subscriptionStatus = 0;
                //promoParent.SetActive(true);
                
                subscribeYearlyParent.SetActive(true);
                subscribeHalfYearlyparent.SetActive(true);
            }
            else if (plan == "Free" && isExpired == 1)
            {
                uiManager.apiManager.APIData.subcription_Data.subscriptionPlan = "FreeExpired";
                uiManager.apiManager.APIData.subcription_Data.subscriptionStatus = 1;
                //promoParent.SetActive(true);
                trialImage.SetActive(true);
                subscribeYearlyParent.SetActive(true);
                subscribeHalfYearlyparent.SetActive(true);
            }
           
            else if (plan == "06_months_subscription" && isExpired == 0)
            {
                uiManager.apiManager.APIData.subcription_Data.subscriptionPlan = "06_months_subscription";
                subscribeHalfYearlyparent.SetActive(true);
                subscribeYearlyParent.SetActive(true);
                //promoParent.SetActive(true);
            }
            else if (plan == "06_months_subscription" && isExpired == 1)
            {
                uiManager.apiManager.APIData.subcription_Data.subscriptionPlan = "06_months_subscription";
                subscribeHalfYearlyparent.SetActive(true);
                subscribeYearlyParent.SetActive(true);
                //promoParent.SetActive(false);
            }
            else if (plan == "12_months_subscription" && isExpired == 0)
            {
                uiManager.apiManager.APIData.subcription_Data.subscriptionPlan = "12_months_subscription";
                subscribeHalfYearlyparent.SetActive(true);
                subscribeYearlyParent.SetActive(true);
                //promoParent.SetActive(true);
            }
            else if (plan == "12_months_subscription" && isExpired == 1)
            {
                uiManager.apiManager.APIData.subcription_Data.subscriptionPlan = "12_months_subscription";
                subscribeHalfYearlyparent.SetActive(true);
                subscribeYearlyParent.SetActive(true);
                //promoParent.SetActive(false);
            }

            else if (uiManager.apiManager.APIData.subcriptionDetails[0].promo_code != null)
            {
                //promoParent.SetActive(false);
                trialImage.SetActive(false);
                subscribeHalfYearlyparent.SetActive(true);
                subscribeYearlyParent.SetActive(true);
            }

            else
            {
                uiManager.apiManager.APIData.subcription_Data.subscriptionPlan = "TrialExpired";
                subscribeHalfYearlyparent.SetActive(true);
                subscribeYearlyParent.SetActive(true);
                //promoParent.SetActive(true);
            }
            


            homeScreen.SetActive(false);
            notificationScreen.SetActive(true);
            uiManager.loadingScreen.SetActive(false);
            screenName = "notification";
        }

        public void OnRestorePurchasesButtonClicked()
        {
            uiManager.loadingScreen.SetActive(true);
            //uiManager.iapManager.RestorePurchases();
        }
        public void OnRestoreButtonCallBack(string message=null)
        {
            if (message == null)
            {
                purchaseText.text = "Your previously purchased products have been restored.";
               
            }
            else
            {
                purchaseText.text = "Your restored purchase has failed. Please try again later.";
            }
            purchaseScreen.SetActive(true);
            uiManager.loadingScreen.SetActive(false);

        }
        public void NotRestoreButtonCallBack()
        {
            purchaseText.text = "No purchases to restore.";
            purchaseScreen.SetActive(true);
            uiManager.loadingScreen.SetActive(false);
        }
        public void OnRestoreCloseButtonClicked()
        {
            restoreScreen.SetActive(false);
        }
      
      
        public void CreateSubscriptionCallback()
        {
            print("Purchase call back");
            uiManager.apiManager.APIData.myProfile[0].paid_user = "1";
            uiManager.apiManager.APIData.myProfile[0].notification = 1;
            trialImage.SetActive(false);
            freeButton.SetActive(false);
            paidButton.SetActive(true);
            InitialUpdate();
            purchaseText.text = "Your subscription is now active";
            purchaseScreen.SetActive(true);
            uiManager.loadingScreen.SetActive(false);
        }
        public void PurchaseFailure()
        {
            purchaseText.text = "Your subscription payment was not processed. Please try again later.";
            purchaseScreen.SetActive(true);
            uiManager.loadingScreen.SetActive(false);
        }
        
        public void PurchaseNotAvailable()
        {
            purchaseText.text = "Couldn't retrieve subscription information. Please try again later.";
            purchaseScreen.SetActive(true);
            uiManager.loadingScreen.SetActive(false);
        }

        #endregion
    }
}
