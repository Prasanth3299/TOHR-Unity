using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Facebook.Unity;
using System;
using System.IO;


namespace RevolutionGames
{
    public class LoginScreen : MonoBehaviour
    {
        #region Variables
        //public IAPManager iapManager;
        public UIManager uiManager;
        public APIManager apiManager;
        public GameObject signinScreen, forgotPasswordPopup, signUpScreen, TandCScreen, countryScreen;

        public InputField emailSigninInputField, passwordSigninInputField, forgotPasswordSigninInputField;
        public GameObject fieldRequriedImage;
        public GameObject incorrectEmailImage;
        public Text messageText, termsErrorText;
        public GameObject passwordSigninShowButton, passwordSigninHideButton;
        public InputField signUpFirstNameInputField, signUpSurNameInputField, signUpEmailInputField, signUpPasswordInputField, signupPasswordShowField, passwordSigninShowField,
                          signUpVillageNumberInputField,referCodeField;
        public GameObject passwordSignUpShowButton, passwordSignUpHideButton;
        public GameObject vipImage, nonVipImage, temrsImage;
        public GameObject termsUncheckImage;
        public Text termstext;
        public RectTransform termsContent;
        public RectTransform privacyContent;
        public GameObject countryNamePrefab;
        public RectTransform countryContent;
        public Text countryNameText;
        public GameObject signupPasswordShow, signUpPasswordhide, passwordigninShow, passwordigninHide;
        public RectTransform sinupContent;
        public GameObject appleButton;
        public GameObject appleObject;

        //public UniWebView webViewTandC;
        //public UniWebView uniWebViewTandC;
        private string streamingAssetsPath;

        private List<string> countryNameList = new List<string>() { "Afghanistan", "Albania", "Algeria","American Samoa", "Andorra", "Angola","Anguilla","Antartica","Antigua and Barbuda", "Argentina", "Armenia","Aruba", "Australia", "	Austria", "Azerbaijan", "Bahamas The",
         "Bahrain","Bangladesh","Barbados","Belarus","Belgium","Belize","Benin","Bermuta","Bhutan","Bolivia","Bosnia and Herzegovina","Botswana","Bouvet Island","Brazil","British Indian Ocean Territiry","Brunei","Bulgaria","Burkina Faso","Burundi","Cambodia","Cameroon","Canada","Cape Verde",
         "Cayman Islands","Central African Republic","Chad","Chile","China","Chirtmas Island","Cocos(Keeling) Islands","Colombia","Comoros","Republic Of The Congo","Cook Island","Costa Rica","Cote D'lvoire (lvory Coast)","Croatia(Hrvatska)","Cuba","Cyprus","Czech Republic","Democratic Republic of the Congo","Denmark",
         "Djibouti","Dominica","Dominican Republic","East Timor","Ecuador","Egypt","El Salvador" ,"Equatorial Guinea","Eritrea","Estonia","Ethiopia","External Territories of Australia","Falkland Islands","Faroe Islands","Fiji Island","Finland","France","French Guiana","French Southern Territories","Gabon","Gambia The",
         "Georgia","Germany","Ghana","Gibraltar","Greece","Greenland","Grenada","Guadeloupe","Guam","Guatemala","Guernsey and Alderney","Guinea","Guinea-Bissau","Guyana","Haiti","Heard and McDonald Islands","Honduras","Hong Kong S.A.R","Hungary","Iceland","India","Indonesia","Iran","Iraq","Ireland","Israel","Italy","Jamaica","Japa","Jersy",
         "Jordan","Kazakhstan","Kenya","Kiribati","Korea North","Korea South","Kuwait","Kyrgyzstan","Laos","Latvia","Lebanon","Lesotho","Liberia","Libya","Liechtenstein","Lithuania","Luxembourg","Macau S.A.R","Macedonia","Madagascar","Malawi","Malaysia","Maldives","Mali","Malta","Man (lsle of)","Marshall Islands","Martinique",
         "Mauritania","Mauritius","Mexico","Micronesia","Moldova","Monaco","Mongolia","Montserrat","Morocco","Mozambique","Myanmar","Namibia","Nauru","Nepal","Netherlands Antilles","Netherlands The","New Caledonia","New Zealand","Nicaragua","Niger","Nigeria","Niue","Norfolk Island","Northern Mariana Islands","Norway","Oman","Pakistan","Palau",
         "Palestinian Territory Occupied","Panama","Papua New Guinea","Paraguay","Peru","Philippines","Pitcairn Island","Poland","Portugal","Puerton Rico","Qatar","Reunion","Romania","Russia","Rwanda","Saint Helena","Saint Kitts and Nevis","Saint Lucia","Saint Pierre and Miquelon","Saint Vincent And The Grenadines","Samoa","San Marino","Sao Tome and Principe",
         "Saudi Arabia","Senegal","Serbia","Seychelles","Sierra Leone","Singapore","Slovakia","Slovenia","Smaller Territories of the UK","Solomon Islands","Somalia","South Africa","South Georgia","South Sudan","Spain","Sri Lanka","Sudan","Suriname","Svalbard And Jan Mayen Islands","Swaziland","Sweden","Switzerland","Syria","Taiwan","Tajikistan","Tanzania",
         "Thailand","Togo","Tokkelau","Tonga","Trinidad and Tobago","Tunisia","Turkey","Turkmenistan","Truks And Caicos","Tuvalu","Uganda","Ukraine","United Arab Emirates","United Kingdom","United State","United States Minor Outlying Islands","Uruguay","Uzbekistan","Vanuatu","Vatican City State (Holy See)","Venezuela","Vietnam","Virgin Islands (US)",
         "Wallis And Futuna Islands","Western Sahara","Yemen","Zambia","Zimbabwe"};

        // fabcebook

        public Text FB_userName;
        public Image FB_useerDp;
        public Image settingProfile;
        public GameObject friendstxtprefab;
        public GameObject GetFriendsPos;
        private static readonly string EVENT_PARAM_SCORE = "score";
        private static readonly string EVENT_NAME_GAME_PLAYED = "game_played";

        private Color32 countryNameColorEnable = new Color32(225, 225, 225, 225);
        private Color32 countryNameColorDisable = new Color32(225, 225, 225, 128);
        private string signinEmail = "";
        private string signinPassword = "";
        private string forgotEmail = "";
        private string firstName = "", surName = "", signUpEmail = "", signUpPassword = "", villageNo = "", countryName = "", gameStatus = "";
        const string matchEmailPattern = @"^([a-zA-Z0-9_\-\+\.]+)@([a-zA-Z0-9_\-\+\.]+)\.([a-zA-Z]{2,5})$";
        private string screenName = "";
        //string incomingText = "<p>For those with Spins below 20k</p>\n\n<p>Pattern - 1</p>\n\n<p>&nbsp;</p>\n\n<p>1x - 90 Times</p>\n\n<p>10x - 20 Times</p>\n\n<p>30x - 10 Times</p>\n\n<p>50x - 15 Times</p>\n\n<p>100x - 15 Times</p>\n\n<p>250x - 6 Times</p>\n\n<p>If you Still do not get 3 Symbols, then continue using 100x &amp; 250x mix Bets until you hit the 3 Symbols.</p>\n\n<p>&nbsp;</p>\n\n<p>Please Follow the Instructions for the Following 3 Symbols:</p>\n\n<p>&nbsp;</p>\n\n<p>1. If you Hit the 3 Symbols between 1 to 100 Bets, then you should be repeating the above given pattern.</p>\n\n<p>&nbsp;</p>\n\n<p>2. If you Hit the 3 Symbols between 101 to 130 Bets, then for the next 3 Symbols, you should start from 10x bet and all other bets remains the same as above.</p>\n\n<p>&nbsp;</p>\n\n<p>3. If you Hit the 3 Symbols between 131 to 150 Bets, then for the next 3 Symbols, you should start from 30x bet and all other bets remains the same as above.</p>\n\n<p>&nbsp;</p>\n\n<p>4. If you Hit the 3 Symbols after 150 Bets, then for the next 3 Symbols, you should use directly from 50x Bet and all other bets after 50x remains the same as above.</p>\n\n<p>&nbsp;</p>\n\n<p>Note:</p>\n\n<p>Whenever during the above pattern, if you hit 1 or 2 symbols on High Bets, then for next 1 or 2 bets use a lower bet, for example &ndash;</p>\n\n<ol>\n\t<li>If you hit 1 or 2 symbols on 1st bet of 100x, then for next 1 or 2 bets use 50x bet</li>\n\t<li>If you hit 1 or 2 symbols on 1st bet of 250x, then for next 1 or 2 bets use 100x bet</li>\n</ol>\n\n<p>&nbsp;</p>\n\n<p>&nbsp;</p>\n\n<p>&nbsp;</p>\n\n<p>Pattern &ndash; 2(Secret Symbol Pattern)</p>\n\n<p>&nbsp;</p>\n\n<p>1x - 100 Times</p>\n\n<p>Step: 1</p>\n\n<p>After 100 Bets, if you see 1 or 2 Symbols/Raid/Attack/3 Shields/3 Spin Capsules on the Winning Line (Middle Line) then follow the following Steps:</p>\n\n<p>&nbsp;</p>\n\n<p>A) Between 101 to 130 Bets, once you see Step 1, raise your Bet to 100x and keep spinning on 100x until you see Step 1 on the Winning Line. If You do not see any Step 1, then drop your Bet to 30x and continue on it until you see Step 1 again and then raise your Bet to 100x</p>\n\n<p>&nbsp;</p>\n\n<p>B) After 130 Bets, once you see Step 1, raise your Bet to 250x and keep spinning on 250x until you see Step 1 on the Winning Line. If you do not see Step 1, then drop your Bet to 100x and continue on it until you see Step 1 again and then raise your bet to 250x</p>\n\n<p>&nbsp;</p>\n\n<p>Restart the Pattern from 1x after Hitting 3 Symbols</p>\n\n<p>&nbsp;</p>\n\n<p>&nbsp;</p>\n\n<p>&nbsp;</p>\n\n<p>For those with Spins above 20k</p>\n\n<p>Pattern - 1</p>\n\n<p>&nbsp;</p>\n\n<p>1x - 90 Times</p>\n\n<p>10x - 10 Times</p>\n\n<p>30x - 10 Times</p>\n\n<p>50x - 10 Times</p>\n\n<p>100x - 10 Times</p>\n\n<p>250x - 10 Times</p>\n\n<p>600x - 10 Times</p>\n\n<p>800x - 6 Times</p>\n\n<p>(1000x - 4 Times) During Bet Blast</p>\n\n<p>If you Still do not get 3 Symbols, then continue using 600x &amp; 800x mix Bets until you hit the 3 Symbols and during Bet Blast mix your bets 600x &amp; 1000x until you hit 3 Symbols.</p>\n\n<p>&nbsp;</p>\n\n<p>Please Follow the Instructions for the Following 3 Symbols:</p>\n\n<p>&nbsp;</p>\n\n<p>1. If you Hit the 3 Symbols between 1 to 100 Bets, then you should be repeating the above given pattern.</p>\n\n<p>&nbsp;</p>\n\n<p>2. If you Hit the 3 Symbols between 101 to 130 Bets, then for the next 3 Symbols, you should start from 10x bet and all other bets remains the same as above.</p>\n\n<p>&nbsp;</p>\n\n<p>3. If you Hit the 3 Symbols between 131 to 150 Bets, then for the next 3 Symbols, you should start from 30x bet and all other bets remains the same as above.</p>\n\n<p>&nbsp;</p>\n\n<p>4. If you Hit the 3 Symbols after 150 Bets, then for the next 3 Symbols, you should use directly from 100x Bet and all other bets after 100x remains the same as above.</p>\n\n<p>&nbsp;</p>\n\n<p>Note:</p>\n\n<p>Whenever during the above pattern, if you hit 1 or 2 symbols on High Bets, then for next 1 or 2 bets use a lower bet, for example &ndash;</p>\n\n<ol>\n\t<li>If you hit 1 or 2 symbols on 1st bet of 250x, then for next 1 or 2 bets use 100x bet</li>\n\t<li>If you hit 1 or 2 symbols on 1st bet of 600x, then for next 1 or 2 bets use 250x bet</li>\n\t<li>If you hit 1 or 2 symbols on 1st bet of 800x or 1000x, then for next 1 or 2 bets use 600x bet</li>\n</ol>\n\n<p>&nbsp;</p>\n\n<p>&nbsp;</p>\n\n<p>&nbsp;</p>\n\n<p>Pattern &ndash; 2(Secret Symbol Pattern)</p>\n\n<p>&nbsp;</p>\n\n<p>1x - 100 Times</p>\n\n<p>Step: 1</p>\n\n<p>After 100 Bets, if you see 1 or 2 Symbols/Raid/Attack/3 Shields/3 Spin Capsules on the Winning Line (Middle Line) then follow the following Steps:</p>\n\n<p>&nbsp;</p>\n\n<p>A) Between 101 to 130 Bets, once you see Step 1, raise your Bet to 250x and keep spinning on 250x until you see Step 1 on the Winning Line. If You do not see any Step 1, then drop your Bet to 50x and continue on it until you see Step 1 again and then raise your Bet to 250x</p>\n\n<p>&nbsp;</p>\n\n<p>B) Between 131 to 150 Bets, once you see Step 1, raise your Bet to 600x and keep spinning on 600x until you see Step 1 on the Winning Line. If you do not see Step 1, then drop your Bet to 100x and continue on it until you see Step 1 again and then raise your bet to 600x</p>\n\n<p>&nbsp;</p>\n\n<p>C) After 150 bets, if you see Step 1, raise your Bet to 800x(or 1000x during Bet Blast) and keep spinning on 800x(or 1000x during Bet Blast) until you see Step 1 on the Winning Line. If you do not see Step 1 then drop your Bet to 250x and continue on it until you see Step 1 again and then raise your bet to 800x(or 1000x during Bet Blast)</p>\n\n<p>&nbsp;</p>\n\n<p>Restart the Pattern from 1x after Hitting 3 Symbols</p>\n\n<p>&nbsp;</p>\n\n<p>&nbsp;</p>\n";
        //string outText="";
        #endregion

        #region Built in Methods
        // Start is called before the first frame update
        void Start()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                appleButton.SetActive(false);
                appleObject.SetActive(false);
            }

            else
            {
                appleButton.SetActive(true);
                appleObject.SetActive(true);
            }
            print("Device : " + SystemInfo.deviceUniqueIdentifier);


#if UNITY_STANDALONE_OSX
            streamingAssetsPath = Application.dataPath + "/StreamingAssets/Profile/";
#else
            streamingAssetsPath = Application.persistentDataPath + "/StreamingAssets/Profile/";
#endif
            print("data path :" + streamingAssetsPath);

            if (!Directory.Exists(streamingAssetsPath))
            {
                Directory.CreateDirectory(streamingAssetsPath);
            }
            else
            {
                /* var hi = Directory.GetFiles(streamingAssetsPath);

                 for (int i = 0; i < hi.Length; i++)
                 {

                     File.Delete(hi[i]);
                 }

                 //Directory.Delete(streamingAssetsPath);
                 Directory.CreateDirectory(streamingAssetsPath);*/
            }

            //TextFilter();
        }

        // Update is called once per frame
        void Update()
        {
            /*if (Input.GetKey(KeyCode
             * .Escape))
            {
                print(Input.GetKeyUp(KeyCode.Escape));
                OnTermsBackButtonClicked();
            }*/
            OnPhoneBackButtonHandler();
        }
        public void OnPhoneBackButtonHandler()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (screenName == "forgot")
                {
                    OnForgotPasswordBackButtonClicked();
                }
                else if (screenName == "signup")
                {
                    OnSignUpBackButtonClicked();
                }
                else
                {
                    // Application.Quit();
                }
            }
        }
        private void OnEnable()
        {
            OnResetDetails();


        }
        #endregion

        #region Custom Methods


        public void OnShareButtonPress()
        {

            if (!Application.isEditor)
            {
                NativeShare native = new NativeShare();

#if UNITY_ANDROID
                native.SetSubject("Refer code");
                native.SetTitle("TOHR");
                native.SetText("Refer code : prasanth \n TOHR \n https://play.google.com/store/apps/details?id=com.rg.stickergame");
#elif UNITY_IOS
                native.SetSubject("Refer code");
            native.SetTitle("TOHR");
            native.SetText("Refer code : prasanth \n TOHR \n https://play.google.com/store/apps/details?id=com.rg.stickergame");
#endif
                native.Share();
            }
        }

        public void OnResetDetails()
        {
            signinScreen.SetActive(true);
            signUpScreen.SetActive(false);
            forgotPasswordPopup.SetActive(false);
            TandCScreen.SetActive(false);
            signinEmail = ""; signinPassword = ""; forgotEmail = "";
            firstName = ""; surName = ""; signUpEmail = ""; signUpPassword = ""; villageNo = "";
            emailSigninInputField.text = ""; passwordSigninInputField.text = ""; passwordSigninShowField.text = ""; forgotPasswordSigninInputField.text = "";
            signUpFirstNameInputField.text = ""; signUpSurNameInputField.text = ""; signUpEmailInputField.text = ""; signUpPasswordInputField.text = ""; signupPasswordShowField.text = "";
            signUpVillageNumberInputField.text = "";
            referCodeField.text = "";
            nonVipImage.SetActive(true);
            vipImage.SetActive(false);
            temrsImage.SetActive(false);
            passwordSigninInputField.contentType = InputField.ContentType.Password;
            passwordSigninShowButton.SetActive(true);
            passwordSigninHideButton.SetActive(false);
            signUpPasswordInputField.contentType = InputField.ContentType.Password;
            passwordSignUpShowButton.SetActive(true);
            passwordSignUpHideButton.SetActive(false);
            gameStatus = "Non-VIP";
            countryNameText.text = "Country Name";
            countryNameText.color = countryNameColorDisable;
            signupPasswordShow.SetActive(false);
            signUpPasswordhide.SetActive(true);
            passwordigninShow.SetActive(false);
            passwordigninHide.SetActive(true);
            sinupContent.anchoredPosition = new Vector2(0, 0);
        }

        public void OnSigninEmailInputField(string email)
        {
            signinEmail = email;

        }
        public void OnSigninEmailShowField(string password)
        {
            signinPassword = password;
            passwordSigninInputField.text = passwordSigninShowField.text;
        }
        public void OnSigninPasswordInputField(string password)
        {
            signinPassword = password;
            passwordSigninShowField.text = passwordSigninInputField.text;
        }

        public void OnSigninButtonClicked()
        {
            if (uiManager.CheckInternet())
            {
                if (signinEmail.Length > 0)
                {
                    if (signinPassword.Length > 0)
                    {
                        PlayerPrefs.SetInt("fblogin", 0);
                        uiManager.loadingScreen.SetActive(true);
                        uiManager.loginFlag = false;
                        apiManager.APIUserLogin(signinEmail, signinPassword);
                    }
                    else {
                        messageText.text = "Enter your password";
                        incorrectEmailImage.SetActive(true);
                        StartCoroutine(FieldRequriedImageOff());
                    }
                    /*this.gameObject.SetActive(false);
                    uiManager.homeScreen.SetActive(true);*/
                    // Api call

                    //SigninButtonCallBack();
                }
                else
                {
                    messageText.text = "Enter your email";
                    incorrectEmailImage.SetActive(true);
                    StartCoroutine(FieldRequriedImageOff());
                }
            }

        }

        public IEnumerator FieldRequriedImageOff()
        {
            yield return new WaitForSeconds(1);
            fieldRequriedImage.SetActive(false);
            termsUncheckImage.SetActive(false);
            incorrectEmailImage.SetActive(false);
        }

        public void OnSigninPasswordShowButtonClicked()
        {
            /*passwordSigninInputField.contentType = InputField.ContentType.Standard;
            passwordSigninShowButton.SetActive(false);
            passwordSigninHideButton.SetActive(true);*/
            passwordigninShow.SetActive(true);
            passwordigninHide.SetActive(false);
        }

        public void OnSigninPasswordHideButtonClicked()
        {
            /*passwordSigninInputField.contentType = InputField.ContentType.Password;
            passwordSigninShowButton.SetActive(true);
            passwordSigninHideButton.SetActive(false);*/
            passwordigninShow.SetActive(false);
            passwordigninHide.SetActive(true);
        }

        public void SigninButtonCallBack(string message = null)
        {
            // Api call back
            // sucess
            // 

            if (message != null)
            {
                uiManager.homeScreen.SetActive(false);
                uiManager.loginScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
                messageText.text = "Incorrect email id or password";
                incorrectEmailImage.SetActive(true);
                StartCoroutine(FieldRequriedImageOff());
            }
            else
            {

                uiManager.noDataLogin = true;
                if (uiManager.apiManager.APIData.myProfile.Count > 0)
                {
                    if (uiManager.apiManager.APIData.myProfile[0].paid_user == 1.ToString())
                    {
                        if (uiManager.CheckInternet())
                        {
                            if (uiManager.purchaseExpired == true)
                            {
                                uiManager.loadingScreen.SetActive(true);
                                PurchaseExpired();
                            }
                            else
                            {
                                uiManager.apiManager.APISubcriptionDetailsLogin();
                            }

                        }
                        else
                        {
                            uiManager.loadingScreen.SetActive(false);
                            uiManager.noNetworkScreen.SetActive(true);
                        }
                    }
                    else
                    {


                        uiManager.homeScreen.SetActive(true);
                        uiManager.homeScreen.GetComponent<HomeScreen>().InitialUpdate();
                        //uiManager.homeScreen.GetComponent<HomeScreen>().ProfilePictureUpdate();
                        this.gameObject.SetActive(false);

                        uiManager.loadingScreen.SetActive(false);
                        if (PlayerPrefs.GetInt("fblogin") == 1)
                        {
                            if (uiManager.apiManager.APIData.myProfile[0].profile_image_url == null || uiManager.apiManager.APIData.myProfile[0].profile_image_url == "")
                            {
                                uiManager.transform.GetComponent<HomeScreen>().FacebookImageLoad();
                            }
                        }


                    }
                    PlayerPrefs.SetInt("login", 1);

                    
                }
                else
                {
                    uiManager.loginScreen.SetActive(true);
                    uiManager.homeScreen.SetActive(false);
                    uiManager.loadingScreen.SetActive(false);
                    PlayerPrefs.SetInt("login", 0);
                }

                if (uiManager.loginFlag == false)
                {
                    if (PlayerPrefs.GetInt("fblogin") == 0)
                    {
                        print("login");
                        PlayerPrefs.SetString("email", signinEmail);
                        PlayerPrefs.SetString("password", signinPassword);
                    }

                }


            }


        }
        public void OnGetSubscriptionDetailsCallback(string plan, int isExpired, string subscriptionToken = "")
        {

            if (plan != null && plan != "Free" && uiManager.apiManager.APIData.subcriptionDetails[0].promo_code == null || uiManager.apiManager.APIData.subcriptionDetails[0].promo_code == "")
            {
                if (uiManager.CheckInternet())
                {
                    //iapManager.CheckSubscription();
                    uiManager.homeScreen.SetActive(true);
                    uiManager.homeScreen.GetComponent<HomeScreen>().InitialUpdate();
                    //uiManager.homeScreen.GetComponent<HomeScreen>().ProfilePictureUpdate();
                    this.gameObject.SetActive(false);

                    uiManager.loadingScreen.SetActive(false);
                }
                else
                {
                    uiManager.loadingScreen.SetActive(false);
                    uiManager.noNetworkScreen.SetActive(true);
                }
            }
            else
            {
                //timerText.text = "";
                //trialImage.SetActive(true);
                if (uiManager.apiManager.APIData.subcriptionDetails.Count > 0)
                {
                    if (plan == "Free")
                    {
                        uiManager.homeScreen.GetComponent<HomeScreen>().OnFreeTrialCountDownTimer();
                    }

                }
                uiManager.homeScreen.SetActive(true);
                uiManager.homeScreen.GetComponent<HomeScreen>().InitialUpdate();
                //uiManager.homeScreen.GetComponent<HomeScreen>().ProfilePictureUpdate();
                this.gameObject.SetActive(false);

                uiManager.loadingScreen.SetActive(false);
            }
            if (PlayerPrefs.GetInt("fblogin") == 1)
            {
                if (uiManager.apiManager.APIData.myProfile[0].profile_image_url == null || uiManager.apiManager.APIData.myProfile[0].profile_image_url == "")
                {
                    uiManager.transform.GetComponent<HomeScreen>().FacebookImageLoad();
                }
            }
        }
       
        public void OnForgotPasswordButtonClicked()
        {
           
            if (uiManager.CheckInternet())
            {
                signinScreen.SetActive(false);
                forgotPasswordPopup.SetActive(true);
                forgotPasswordSigninInputField.text = "";
                screenName = "forgot";
            }
            
        }

        public void OnForgotEmailInputField(string email)
        {
            forgotEmail = email;
        }

        public void OnForgotPasswordSubmitButtonClicked()
        {
            if (uiManager.CheckInternet())
            {
                if (forgotEmail.Length > 0)
                {
                    // APi call
                    uiManager.loadingScreen.SetActive(true);
                    apiManager.APIUserForgotPassword(forgotEmail);
                }
                else
                {
                    messageText.text = "Enter registered email address";
                    incorrectEmailImage.SetActive(true);
                    StartCoroutine(FieldRequriedImageOff());
                }
            }
            
        }

        public void ForgotPasswordSubmitButtonCallBack(string message=null)
        {
            // Api call back
            // sucess messageText.text="password sent to your email address"
            // fali messageText.text="Somthing wrong"
            uiManager.loadingScreen.SetActive(false);
            if (message == null)
            {
                messageText.text = "Password has been sent to your email address";
                incorrectEmailImage.SetActive(true);
                StartCoroutine(FieldRequriedImageOff());
                forgotPasswordPopup.SetActive(false);
                signinScreen.SetActive(true);
            }
            else
            {
                messageText.text = "Email id is not registered";
                incorrectEmailImage.SetActive(true);
                StartCoroutine(FieldRequriedImageOff());
            }
            
        }

        public void OnForgotPasswordBackButtonClicked()
        {
            forgotPasswordPopup.SetActive(false);
            signinScreen.SetActive(true);
            screenName = "";
        }

        // sign up screen

        public void OnSignUpButtonCLicked()
        {
            if (uiManager.CheckInternet())
            {
                OnResetDetails();
                signinScreen.SetActive(false);
                signUpScreen.SetActive(true);
                screenName = "signup";
            }
            
           
        }

        public void OnSignUpBackButtonClicked()
        {
            signUpScreen.SetActive(false);
            signinScreen.SetActive(true);
            screenName = "";
        }

        public void OnSignUpEmailInputField(string email)
        {
            signUpEmail = email;
        }

        public void OnSignUpPasswordInputField(string password)
        {
            signUpPassword = password;
            signupPasswordShowField.text = signUpPasswordInputField.text;
        }
        public void OnSignUpPasswordShowField(string password)
        {
            signUpPassword = password;
            signUpPasswordInputField.text = signupPasswordShowField.text;
        }

        public void OnFirstNameInputField(string name)
        {
            firstName = name;
        }
        
        public void OnSurNameInputField(string name)
        {
            surName = name;
        }

        public void OnVillageNumberInputField(string number)
        {
            villageNo = number;
        }

        public void OnSignUpPasswordShowButtonClicked()
        {
           /* signUpPasswordInputField.contentType = InputField.ContentType.Standard;
            passwordSignUpShowButton.SetActive(false);
            passwordSignUpHideButton.SetActive(true);*/
            signUpPasswordhide.SetActive(false);
            signupPasswordShow.SetActive(true);
        }

        public void OnSignUpPasswordHideButtonClicked()
        {
           /* signUpPasswordInputField.contentType = InputField.ContentType.Password;
            passwordSignUpShowButton.SetActive(true);
            passwordSignUpHideButton.SetActive(false);*/
            signUpPasswordhide.SetActive(true);
            signupPasswordShow.SetActive(false);
        }

        public void OnRegisterButtonClicked()
        {
            // APi call
            if (uiManager.CheckInternet())
            {
                if (!temrsImage.activeSelf)
                {
                    termsErrorText.text = "Please check the terms and conditions";
                    termsUncheckImage.SetActive(true);
                    StartCoroutine(FieldRequriedImageOff());
                }
                else if (firstName.Length > 0)
                {
                    if (surName.Length > 0)
                    {
                        if (signUpEmail.Length > 0)
                        {
                            if (Regex.IsMatch(signUpEmail, matchEmailPattern))
                            {
                                // api call
                                if (signUpPassword.Length > 0)
                                {
                                    if (signUpPassword.Length > 3 && signUpPassword.Length < 30)
                                    {
                                        if (signUpPassword.Contains(" "))
                                        {
                                            termsErrorText.text = "Space cannot be used in password";
                                            termsUncheckImage.SetActive(true);
                                            StartCoroutine(FieldRequriedImageOff());
                                        }
                                        else
                                        {
                                            if (villageNo.Length > 0)
                                            {
                                                print(referCodeField.text);
                                                uiManager.loadingScreen.SetActive(true);
                                                apiManager.APIUserRegister(firstName, surName, signUpEmail, signUpPassword, villageNo, gameStatus,referCodeField.text);
                                            }
                                            else
                                            {
                                                termsErrorText.text = "Please enter village number";
                                                termsUncheckImage.SetActive(true);
                                                StartCoroutine(FieldRequriedImageOff());
                                            }
                                        }


                                    }
                                    else
                                    {
                                        termsErrorText.text = "Password should be between 4 and 30 letters";
                                        termsUncheckImage.SetActive(true);
                                        StartCoroutine(FieldRequriedImageOff());
                                    }

                                }
                                else
                                {
                                    termsErrorText.text = "Please enter password";
                                    termsUncheckImage.SetActive(true);
                                    StartCoroutine(FieldRequriedImageOff());
                                }

                            }
                            else
                            {
                                termsErrorText.text = "Please enter valid email address";
                                termsUncheckImage.SetActive(true);
                                StartCoroutine(FieldRequriedImageOff());
                            }
                        }
                        else
                        {
                            termsErrorText.text = "Please enter email";
                            termsUncheckImage.SetActive(true);
                            StartCoroutine(FieldRequriedImageOff());
                        }
                    }
                    else
                    {
                        termsErrorText.text = "Please enter surname";
                        termsUncheckImage.SetActive(true);
                        StartCoroutine(FieldRequriedImageOff());
                    }
                }
                else
                {
                    termsErrorText.text = "Please enter first name";
                    termsUncheckImage.SetActive(true);
                    StartCoroutine(FieldRequriedImageOff());
                }
                
               
            }
            
        }

        public void OnRegisterButtonCallBack(string message = null)
        {
            // api call back
            uiManager.loadingScreen.SetActive(false);
            if (message == null)
            {
                //signUpScreen.SetActive(false);
                //signinScreen.SetActive(true);
                PlayerPrefs.SetInt("fblogin", 0);
                PlayerPrefs.SetString("email", signUpEmail);
                PlayerPrefs.SetString("password", signUpPassword);
                uiManager.loginFlag = true;
                uiManager.loadingScreen.SetActive(true);
                apiManager.APIUserLogin(signUpEmail, signUpPassword);
            }
            else
            {
                termsErrorText.text = message;
                termsUncheckImage.SetActive(true);
                StartCoroutine(FieldRequriedImageOff());
            }
            
        }

        public void OnTermsButtonClicked()
        {

            termsContent.anchoredPosition = new Vector2(0, 0);
            uiManager.termsScreen.SetActive(true);
            //TandCScreen.SetActive(true);
            //Application.OpenURL("http://tohradminpanel.us-east-1.elasticbeanstalk.com/#/adminpanel/termsandcondition");
               
           
        }
        public void OnPrivacyButtonCLicked()
        {
            privacyContent.anchoredPosition = new Vector2(0, 0);
            uiManager.privacyScreen.SetActive(true);
        }
        public void TermsCallBack(List<APIData.Terms> terms )
        {
            if (terms.Count > 0)
            {
                for (int i = 0; i < terms.Count; i++)
                {

                    termstext.text = uiManager.TextFilter(terms[0].details);
                    
                    //webViewTandC.gameObject.SetActive(true); 
                    //webViewTandC.LoadWebView("http://docs.uniwebview.com/guide/");
                }
            }
            
            else
            {
                uiManager.noDataScreen.SetActive(true);
            }
            uiManager.loadingScreen.SetActive(false);
            
        }
        
        public void OnTermsBackButtonClicked()
        {
            //webViewTandC.gameObject.SetActive(false);
            TandCScreen.SetActive(false);
        }

        public void OnCheckBoxButtonClicked()
        {
            if (temrsImage.activeSelf)
            {
                temrsImage.SetActive(false);
            }
            else
            {
                temrsImage.SetActive(true);
            }
        }

        public void OnVipButtonClicked()
        {
            vipImage.SetActive(true);
            nonVipImage.SetActive(false);
            gameStatus = "VIP";
        }
        
        public void OnNonVipButtonClicked()
        {
            vipImage.SetActive(false);
            nonVipImage.SetActive(true);
            gameStatus = "Non-VIP";
        }

        public void OnCountryNameClicked()
        {
            uiManager.loadingScreen.SetActive(true);
            for (int i = 0; i < countryContent.childCount; i++)
            {
                Destroy(countryContent.GetChild(i).gameObject);
            }
            countryContent.anchoredPosition = new Vector2(0, 0);
            for (int i = 0; i < countryNameList.Count; i++)
            {
                GameObject name = Instantiate(countryNamePrefab, countryContent);
                name.transform.GetChild(0).GetComponent<Text>().text = countryNameList[i];
                int count = i;
                name.transform.GetComponent<Button>().onClick.AddListener(() => CountryNameButtonClicked(count));
            }
            uiManager.loadingScreen.SetActive(false);
            countryScreen.SetActive(true);
        }
        public void CountryNameButtonClicked(int index)
        {
            countryNameText.text = countryNameList[index];
            countryName = countryNameText.text;
            countryNameText.color = countryNameColorEnable;
            countryScreen.SetActive(false);
        }
        public void OnCountryBackButtonClicked()
        {
            countryScreen.SetActive(false);
        }
        public void PurchaseExpired()
        {
            if (uiManager.CheckInternet())
            {
                uiManager.apiManager.APIData.myProfile[0].paid_user = "0";
                uiManager.apiManager.APIData.myProfile[0].notification = 0;
                //InitialUpdate();
                uiManager.homeScreen.SetActive(true);
                uiManager.homeScreen.GetComponent<HomeScreen>().InitialUpdate();
                //uiManager.homeScreen.GetComponent<HomeScreen>().ProfilePictureUpdate();
                this.gameObject.SetActive(false);

                uiManager.loadingScreen.SetActive(false);
                uiManager.apiManager.APIUpdateSubcriptionDetails();
            }
        }

        #region Facebook Login

        private void Awake()

        {

            // FB.Init(SetInit, onHidenUnity);

            // Panel_Add.SetActive(false);





            if (!FB.IsInitialized)

            {

                FB.Init(() =>

                {

                    if (FB.IsInitialized)

                        FB.ActivateApp();

                    else

                        Debug.LogError("Couldn't initialize");

                },

                isGameShown =>

                {

                    if (!isGameShown)

                        Time.timeScale = 0;

                    else

                        Time.timeScale = 1;

                });

            }

            else

                FB.ActivateApp();

        }

        void SetInit()

        {

            if (FB.IsLoggedIn)

            {

                Debug.Log("Facebook is Login!");

            }

            else

            {

                Debug.Log("Facebook is not Logged in!");

            }

            DealWithFbMenus(FB.IsLoggedIn);

        }



        void onHidenUnity(bool isGameShown)

        {

            if (!isGameShown)

            {

                Time.timeScale = 0;

            }

            else

            {

                Time.timeScale = 1;

            }

        }

        public void FBLogin()

        {
            if (uiManager.CheckInternet())
            {
                List<string> permissions = new List<string>();

                permissions.Add("public_profile");

                //permissions.Add("user_friends");
                permissions.Add("email");

                FB.LogInWithReadPermissions(permissions, AuthCallBack);
            }
            

        }



        public void CallLogout()

        {

            StartCoroutine("FBLogout");

        }

        public void FBLogout()

        {

            FB.LogOut();

            while (FB.IsLoggedIn)

            {

                print("Logging Out");

                //yield return null;

            }

            print("Logout Successful");

            FB_useerDp.sprite = null;

            FB_userName.text = "";

        }





        public void GetFriendsPlayingThisGame()

        {

            string query = "/me/friends";

            FB.API(query, HttpMethod.GET, result =>

            {

                Debug.Log("the raw" + result.RawResult);

                var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);

                var friendsList = (List<object>)dictionary["data"];









                foreach (var dict in friendsList)

                {



                    GameObject go = Instantiate(friendstxtprefab);

                    go.GetComponent<Text>().text = ((Dictionary<string, object>)dict)["name"].ToString();

                    go.transform.SetParent(GetFriendsPos.transform, false);



                    //  FriendsText[1].text += ((Dictionary<string, object>)dict)["name"];

                }







            });



        }

        public void FacebookSharefeed()

        {

            string url = "https://developers.facebook.com/docs/unity/reference/current/FB.ShareLink";

            FB.ShareLink(

                new Uri(url),

                "Checkout unity3d teacher channel",

                "I just watched " + "22" + " times of this channel",

                null,

                ShareCallback);



        }

        private static void ShareCallback(IShareResult result)

        {

            Debug.Log("ShareCallback");

            SpentCoins(2, "sharelink");

            if (result.Error != null)

            {

                Debug.LogError(result.Error);

                return;

            }

            Debug.Log(result.RawResult);

        }

        // Start is called before the first frame update

        void AuthCallBack(IResult result)

        {


            if (result.Error != null)

            {

                Debug.Log(result.Error);

            }

            else

            {

                if (FB.IsLoggedIn)

                {

                    Debug.Log("Facebook is Login!");
                    FetchFBProfile(FB.IsLoggedIn);
                    // Panel_Add.SetActive(true);

                }

                else

                {

                    Debug.Log("Facebook is not Logged in!");

                }

                //DealWithFbMenus(FB.IsLoggedIn);
                

            }

        }



        void DealWithFbMenus(bool isLoggedIn)

        {

            if (isLoggedIn)

            {
                
                FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);

                //FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);

            }

            else

            {



            }

        }
        private void FetchFBProfile(bool isLoggedIn)
        {
            if (isLoggedIn)
            {
                if (uiManager.CheckInternet())
                {
                    uiManager.loadingScreen.SetActive(true);
                    this.gameObject.SetActive(false);
                    //FB.API("/me?fields=first_name,last_name,email", HttpMethod.GET, FetchProfileCallback, new Dictionary<string, string>() { });
                    FB.API("/me?fields=id,first_name,last_name,email", HttpMethod.GET, GetFacebookInfo, new Dictionary<string, string>() { });
                    FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
                }
                
            }
            else
            {
                
            }
               
        }

        private void FetchProfileCallback(IGraphResult result)
        {

            Debug.Log(result.RawResult);
            Dictionary<string, object> FBUserDetails = (Dictionary<string, object>)result.ResultDictionary;
            //StartCoroutine(FetchFBProfilePicture());
            Debug.Log("Profile: first name: " + FBUserDetails["first_name"]);
            Debug.Log("Profile: last name: " + FBUserDetails["last_name"]);
            //Debug.Log("Profile: id: " + FBUserDetails["id"]);
            Debug.Log("Profile: email: " + FBUserDetails["email"]);

        }

        public void GetFacebookInfo(IResult result)
        {
            if (result.Error == null)
            {
                Debug.Log(result.ResultDictionary["id"].ToString());
                Debug.Log(result.ResultDictionary["first_name"].ToString());
                
                print("email out");
                PlayerPrefs.SetInt("fblogin", 1);
                PlayerPrefs.SetString("fbid", result.ResultDictionary["id"].ToString());
                PlayerPrefs.SetString("fbname", result.ResultDictionary["first_name"].ToString());
                PlayerPrefs.SetString("mode", "Facebook");
                try
                {
                    Debug.Log(result.ResultDictionary["email"].ToString());
                    PlayerPrefs.SetString("fbmail", result.ResultDictionary["email"].ToString());
                }
                catch (Exception e)
                {
                    print("catch");
                    PlayerPrefs.SetString("fbmail", "");

                }
                try
                {
                    Debug.Log(result.ResultDictionary["last_name"].ToString());
                    PlayerPrefs.SetString("fbnamelast", result.ResultDictionary["last_name"].ToString());
                }
                catch (Exception e)
                {
                    print("catch");
                    PlayerPrefs.SetString("fbnamelast", "");

                }
                if (uiManager.CheckInternet())
                {
                    uiManager.loadingScreen.SetActive(true);
                    uiManager.apiManager.APIFacebookLogin(PlayerPrefs.GetString("fbmail"), result.ResultDictionary["first_name"].ToString(), PlayerPrefs.GetString("fbnamelast"), result.ResultDictionary["id"].ToString(), "Facebook");
                }

            }
            else
            {
                Debug.Log(result.Error);
            }
        }

        void DisplayUsername(IResult result)

        {

            if (result.Error == null)

            {

                string name = "" + result.ResultDictionary["first_name"];

                FB_userName.text = "Welcome, "+name;

                this.gameObject.SetActive(false);
                uiManager.homeScreen.SetActive(true);

                Debug.Log("" + name);

            }

            else

            {

                Debug.Log(result.Error);

            }

        }



        void DisplayProfilePic(IGraphResult result)

        {

            if (result.Texture != null)

            {

                Debug.Log("Profile Pic");

                FB_useerDp.sprite = Sprite.Create(result.Texture, new Rect(0, 0, result.Texture.width, result.Texture.height), new Vector2());
                settingProfile.sprite = FB_useerDp.sprite;
                //uiManager.homeScreen.transform.GetComponent<HomeScreen>().FacebookImageLoad();

            }

            else

            {

                Debug.Log(result.Error);

            }

        }

        public static void SpentCoins(int coins, string item)

        {

            // setup parameters

            var param = new Dictionary<string, object>();

            param[AppEventParameterName.ContentID] = item;

            // log event

            FB.LogAppEvent(AppEventName.SpentCredits, (float)coins, param);

        }

        #endregion

        #endregion
    }
}
