using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

namespace RevolutionGames
{

    public class SettingScreen : MonoBehaviour
    {
        #region  Variables

        public UIManager uiManager;
        public GameObject settingMainScreen;
        public GameObject passwordChangeScreen;
        public GameObject myProfileScreen;
        public GameObject contactUsScreen;
        public GameObject logoutScreen;
        public GameObject countryScreen;
        public InputField currentPasswordField, newPasswordField, newPasswordShowField, confirmPasswordField, confirmPasswordShowField, firstNameField, surNameField, villageNoField;
        public Text customerIdText, emailText,countryNameText;
        public GameObject vipImage, nonVipImage;
        public RectTransform profileContent,contactContent,countryContent;
        public GameObject contactVipImage, contactNonVipImage;
        public Text contactCustomerIdText, contactEmailText;
        public InputField contactVillaheNoField, contactAddNoteField;
        public GameObject fieldRequriedImage, updateImage;
        public Text updateText;
        public Text passwordText;
        public GameObject paawordErrorImage;
        public Text passwotdText;
        public GameObject countryNamePrefab;
        public GameObject changePasswordButton;
        public Sprite avatar;
        public Image profile;
        public Image homeProfile;
        public Text homeNameText;
        public GameObject newPasswordShow, newPasswordHide, confirmPasswordShow, confirmPasswordHide;
        public InputField contactEmailField;
        public RectTransform termsContent;
        public RectTransform privacyContent;
        private string streamingAssetsPath;
        private string screenSwitch = "";
        private string currentPassword = "", newPassword = "", confirmPassword = "",firstName="",surName="",villageNo="",gameStatus="",addNote="";
        private bool profileUpdateFlag = false;
        public bool imageFlag = false;
        private string contactEmail;
        const string matchEmailPattern = @"^([a-zA-Z0-9_\-\+\.]+)@([a-zA-Z0-9_\-\+\.]+)\.([a-zA-Z]{2,5})$";

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

        private Color32 countryNameColorEnable = new Color32(225, 225, 225, 225);
        private Color32 countryNameColorDisable = new Color32(225, 225, 225, 128);
        private string screenName = "";

        #endregion

        #region  Built in Methods
        // Start is called before the first frame update
        void Start()
        {
#if UNITY_STANDALONE_OSX
            streamingAssetsPath = Application.dataPath + "/StreamingAssets/Profile/";
#else
            streamingAssetsPath = Application.persistentDataPath + "/StreamingAssets/Profile/";
#endif
            print("data path :" + streamingAssetsPath);

            imageFlag = true;
            // ProfilePictureUpdate();
        }

    

    // Update is called once per frame
    void Update()
        {
            OnPhoneBackButtonHandler();
        }
        private void OnEnable()
        {
            if (Directory.Exists(streamingAssetsPath))
            {
                //ProfilePictureUpdate();
            }
            if (PlayerPrefs.GetInt("fblogin")==1)
            {
                changePasswordButton.SetActive(false);
            }
            else
            {
                changePasswordButton.SetActive(true);
            }
            OnResetDetais();
            fieldRequriedImage.SetActive(false);
            updateImage.SetActive(false);
            paawordErrorImage.SetActive(false);
            imageFlag = true;
            if (imageFlag == false)
            {
                //ProfilePictureUpdate();
            }
        }
        #endregion

        #region Custom Methods

        public void OnShareButtonPress()
        {

            if (!Application.isEditor)
            {
                NativeShare native = new NativeShare();

#if UNITY_ANDROID
                native.SetSubject("Referral code");
                native.SetTitle("TOHR");
                native.SetText("Referral code : "+ uiManager.apiManager.APIData.myProfile[0].reference_uuid +" \n https://play.google.com/store/apps/details?id=com.rg.stickergame");
#elif UNITY_IOS
                native.SetSubject("Referral code");
            native.SetTitle("TOHR");
            native.SetText("Referral code : "+ uiManager.apiManager.APIData.myProfile[0].reference_uuid +" \n https://play.google.com/store/apps/details?id=com.rg.stickergame");
#endif
                native.Share();
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
        public void OnPhoneBackButtonHandler()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (screenName == "profile")
                {
                    OnBackButtonClicked();
                }
                else if (screenName == "logout")
                {
                    OnNoButtonClicked();
                }else if (screenName == "home")
                {
                    OnBackButtonClicked();
                }

            }
        }

        public void ProfilePictureUpdate()
        {
            print("profile");
            imageFlag = true;
            if (uiManager.apiManager.APIData.myProfile[0].profile_image_url != null)
            {
                uiManager.loadingScreen.SetActive(true);
                StartCoroutine(LoadTextureFromWeb(uiManager.apiManager.APIData.myProfile[0].profile_image_url));
                //uiManager.apiManager.APIDownloadImage(uiManager.apiManager.APIData.myProfile[0].profile_image_url);
            }
            else
            {
                profile.sprite = avatar;
            }
        }

        public void OnImageDownloadApiCallBack(Texture2D texture)
        {
            profile.sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero);
            homeProfile.sprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero); 
            uiManager.loadingScreen.SetActive(false);
        }

        IEnumerator LoadTextureFromWeb(string url)
        {

            //GameObject image = Instantiate(renderImage, documentViewParent);
            //textureImage = image.GetComponent<Image>();
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            www.timeout = 200;
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError("Error: " + www.error); 
                updateText.text = "Profile picture not updated";
                updateImage.SetActive(true);
                StartCoroutine(FieldRequriedImageOff());
            }
            else
            {
                Texture2D loadedTexture = DownloadHandlerTexture.GetContent(www);
                profile.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
                homeProfile.sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
                print("rotation :" +profile.gameObject.transform.rotation);
                //textureImage.SetNativeSize();
                print("downn");
            }
            uiManager.loadingScreen.SetActive(false);
            //OnLoadImageFromDiskButtonClick(count);
        }
        public void SelectProfile()
        {
            if(uiManager.CheckInternet())
            {
                PickImage(1);
            }
           
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

                    byte[] data = File.ReadAllBytes(streamingAssetsPath + Path.GetFileName(path));
                    //uiManager.apiManager.APIUploadImage(data);
                    

                    Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
                    if (texture != null)
                    {

                    }
                        if (texture == null)
                    {
                        Debug.Log("Couldn't load texture from " + path);
                        return;
                    }

                    //profile.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                    //homeProfile.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                    //uiManager.loadingScreen.SetActive(true);
                    //ProfilePictureUpdate();
                    uiManager.loadingScreen.SetActive(true);
                    uiManager.apiManager.APIUploadDocument(data, Path.GetFileName(path),"setting");
                   // uiManager.apiManager.APIUploadImage(data);

                    print("Path.GetFileName(path)"+ Path.GetFileName(path));
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

        public void OnUploadImageCallBack()
        {
            print("upload");
            ProfilePictureUpdate();
            uiManager.loadingScreen.SetActive(true);
        }
        public void OnUploadImageFailure()
        {
            uiManager.loadingScreen.SetActive(false);
            updateText.text = "Profile picture not updated";
            updateImage.SetActive(true);
            StartCoroutine(FieldRequriedImageOff());
        }
        public void OnResetDetais()
        {
            settingMainScreen.SetActive(true);
            logoutScreen.SetActive(false);
            passwordChangeScreen.SetActive(false);
            contactUsScreen.SetActive(false);
            myProfileScreen.SetActive(false);
        }




        public void OnSeetingBackButtonClicked()
        {
            this.gameObject.SetActive(false);
            uiManager.homeScreen.SetActive(true);
            uiManager.screenName = "";
        }

        public void OnChangePasswordButtonClicked()
        {
            screenSwitch = "";
            currentPasswordField.text = ""; newPasswordField.text = ""; confirmPasswordField.text = "";
            currentPassword = ""; newPassword = ""; confirmPassword = "";
            currentPasswordField.text = ""; newPasswordField.text = ""; confirmPasswordField.text = ""; newPasswordShowField.text = ""; confirmPasswordShowField.text = "";
            currentPassword = ""; newPassword = ""; confirmPassword = "";
            newPasswordShow.SetActive(false);
            newPasswordHide.SetActive(true);
            confirmPasswordShow.SetActive(false);
            confirmPasswordHide.SetActive(true);
            settingMainScreen.SetActive(false);
            passwordChangeScreen.SetActive(true);
            screenName = "profile";
            uiManager.screenSwitch = false;
        }
        public void HomeProfileButtonClicked()
        {
            screenSwitch = "home";
            if (uiManager.CheckInternet())
            {
                uiManager.loadingScreen.SetActive(true);
                uiManager.apiManager.APIGetProfile();
            }
        }
        public void OnMyProfileButtonClicked()
        {
            screenSwitch = "";
            if (uiManager.CheckInternet())
            {
                uiManager.loadingScreen.SetActive(true);
                uiManager.apiManager.APIGetProfile();
            }
            
        }
        public void OnMyProfileAPICallBack()
        {
            customerIdText.text = uiManager.apiManager.APIData.playerData.player_id.ToString();
            firstNameField.text = uiManager.apiManager.APIData.playerData.first_name;
            firstName = firstNameField.text;
            surNameField.text = uiManager.apiManager.APIData.playerData.sur_name;
            surName = surNameField.text;
            emailText.text = uiManager.apiManager.APIData.playerData.email;
            villageNoField.text = "";
            if (uiManager.apiManager.APIData.playerData.village_id != null)
            {
                villageNoField.text = uiManager.apiManager.APIData.playerData.village_id;
                villageNo = villageNoField.text;
            }
            if (uiManager.apiManager.APIData.playerData.country_name != null)
            {
                countryNameText.text = uiManager.apiManager.APIData.playerData.country_name;
            }
            
            if (uiManager.apiManager.APIData.playerData.game_status == "Non-VIP")
            {
                nonVipImage.SetActive(true);
                vipImage.SetActive(false);
                gameStatus = "Non-VIP";
            }
            else
            {
                nonVipImage.SetActive(false);
                vipImage.SetActive(true);
                gameStatus = "VIP";
            }
            this.gameObject.SetActive(true);
            profileContent.anchoredPosition = new Vector2(0, 0);
            uiManager.loadingScreen.SetActive(false);
            settingMainScreen.SetActive(false);
            myProfileScreen.SetActive(true);
            profileUpdateFlag = false;
            screenName = "profile";
            uiManager.screenSwitch = false;
        }

        public void OnContactsButtonClicked()
        {
            if (uiManager.apiManager.APIData.myProfile[0].email.Contains(SystemInfo.deviceUniqueIdentifier))
            {
                contactEmail= "";
                contactEmailField.text = "";
            }
            else
            {
                contactEmailField.text = uiManager.apiManager.APIData.myProfile[0].email;
                contactEmail = uiManager.apiManager.APIData.myProfile[0].email;
            }
            screenSwitch = "";
            addNote = "";
            villageNo = "";
            contactVillaheNoField.text = "";contactAddNoteField.text = "";
            contactCustomerIdText.text = uiManager.apiManager.APIData.myProfile[0].player_id.ToString();
            if (uiManager.apiManager.APIData.myProfile[0].game_status == "VIP")
            {
                contactVipImage.SetActive(true);
                contactNonVipImage.SetActive(false);
                gameStatus = "VIP";
            }
            else
            {
                contactVipImage.SetActive(false);
                contactNonVipImage.SetActive(true);
                gameStatus = "Non-VIP";
            }
            contactEmailText.text = uiManager.apiManager.APIData.myProfile[0].email;
            contactContent.anchoredPosition = new Vector2(0,0);
            settingMainScreen.SetActive(false);
            contactUsScreen.SetActive(true);
            screenName = "profile";
            uiManager.screenSwitch = false;
        }
        public void OnBackButtonClicked()
        {
            if (screenSwitch == "")
            {
                settingMainScreen.SetActive(true);
                passwordChangeScreen.SetActive(false);
                contactUsScreen.SetActive(false);
                myProfileScreen.SetActive(false);
                screenName = "";
                uiManager.screenSwitch = true;
            }
            else
            {
                this.gameObject.SetActive(false);
                uiManager.homeScreen.SetActive(true);
                screenName = "";
                uiManager.screenSwitch = true;
            }
            
        }
        public void OnLogoutButtonClicked()
        {
            logoutScreen.SetActive(true);
            screenName = "logout";
            uiManager.screenSwitch = false;
        }
        public void OnNoButtonClicked()
        {
            logoutScreen.SetActive(false);
            screenName = "";
            uiManager.screenSwitch = true;
        }
        public void OnYesButtonClicked()
        {

            if (uiManager.CheckInternet())
            {
                uiManager.loadingScreen.SetActive(true);
                uiManager.apiManager.APILogout();
            }

            
        }
        public void LogoutCallBack()
        {
            if (PlayerPrefs.GetInt("fblogin") == 1)
            {
                uiManager.loginScreen.transform.GetComponent<LoginScreen>().FBLogout();
                PlayerPrefs.GetInt("fblogin",0);
            }
            imageFlag = false;
            uiManager.imageFlag = false;
            PlayerPrefs.SetInt("login", 0);
            this.gameObject.SetActive(false);
            uiManager.loginScreen.SetActive(true);
            uiManager.loginFlag = false;
            profile.sprite = avatar;
            homeProfile.sprite = avatar;
           
            var hi = Directory.GetFiles(streamingAssetsPath);

            if (hi.Length > 0)
            {
                for (int i = 0; i < hi.Length; i++)
                {
                    File.Delete(hi[i]);
                }
            }
            PlayerPrefs.SetInt("login", 0);
            uiManager.loadingScreen.SetActive(false);
        }

        public void OnChangePasswordSubmitButtonClicked()
        {
            // api call

            if (currentPasswordField.text.Length <= 0)
            {
                updateText.text = "Please enter current password";
                updateImage.SetActive(true);
                StartCoroutine(FieldRequriedImageOff());
            }
            else
            {
                if (newPasswordField.text.Length <= 0 || newPasswordShowField.text.Length <= 0)
                {
                    updateText.text = "Please enter new password";
                    updateImage.SetActive(true);
                    StartCoroutine(FieldRequriedImageOff());
                }
                else
                {
                    if (newPasswordField.text.Contains(" "))
                    {
                        updateText.text = "Space cannot be used in password";
                        updateImage.SetActive(true);
                        StartCoroutine(FieldRequriedImageOff());
                    }
                    else
                    {
                        if (confirmPasswordField.text.Length <= 0 || confirmPasswordShowField.text.Length <= 0)
                        {
                            updateText.text = "Please enter confirm password";
                            updateImage.SetActive(true);
                            StartCoroutine(FieldRequriedImageOff());
                        }
                        else
                        {
                            if (confirmPasswordField.text.Contains(" "))
                            {
                                updateText.text = "Space cannot be used in password";
                                updateImage.SetActive(true);
                                StartCoroutine(FieldRequriedImageOff());
                            }
                            else
                            {
                                if (newPasswordField.text.Length > 3 && newPasswordShowField.text.Length > 3 && confirmPasswordField.text.Length > 3 && confirmPasswordShowField.text.Length > 3 && newPasswordField.text.Length < 30 && newPasswordShowField.text.Length < 30 && confirmPasswordField.text.Length < 30 && confirmPasswordShowField.text.Length < 30)
                                {
                                    if (newPasswordField.text == confirmPasswordField.text)
                                    {
                                        if (uiManager.CheckInternet())
                                        {
                                            uiManager.loadingScreen.SetActive(true);
                                            uiManager.apiManager.APIChangePassword(currentPassword, confirmPassword);
                                        }
                                    }
                                    else
                                    {
                                        passwotdText.text = "Your new and confirm passwords does not match";
                                        paawordErrorImage.SetActive(true);
                                        StartCoroutine(FieldRequriedImageOff());
                                    }
                                }
                                else
                                {
                                    passwotdText.text = "Password should be between 4 and 30 letters";
                                    paawordErrorImage.SetActive(true);
                                    StartCoroutine(FieldRequriedImageOff());
                                }
                            }
                           
                        }
                    }
                   
                }
            }
            
        }
        public void OnNewPasswordShowButtonClciked()
        {
            newPasswordShow.SetActive(true);
            newPasswordHide.SetActive(false);
        }
        public void OnNewPasswordHideButtonClciked()
        {
            newPasswordShow.SetActive(false);
            newPasswordHide.SetActive(true);
        }
        public void OnConfirmPasswordShowButtonClciked()
        {
            confirmPasswordShow.SetActive(true);
            confirmPasswordHide.SetActive(false);
        }
        public void OnConfirmPasswordHideButtonClciked()
        {
            confirmPasswordShow.SetActive(false);
            confirmPasswordHide.SetActive(true);
        }
        public void OnChangePasswordSubmitButtonCallBack(string message=null)
        {
            uiManager.loadingScreen.SetActive(false);
            if (message == null)
            {
                PlayerPrefs.SetString("password", confirmPassword);
                updateText.text = "Change password successfully";
                updateImage.SetActive(true);
                StartCoroutine(FieldRequriedImageOff());
                passwordChangeScreen.SetActive(false);
                settingMainScreen.SetActive(true);
            }
            else
            {
                updateText.text = "Current password is incorrect";
                updateImage.SetActive(true);
                StartCoroutine(FieldRequriedImageOff());
            }
            
        }
        public void OnChangePasswordNewField(string password)
        {
            newPassword = password;
            newPasswordShowField.text = newPasswordField.text;
        }
        public void OnChangePasswordNewShowField(string password)
        {
            newPassword = password;
            newPasswordField.text = newPasswordShowField.text;
        }
        public void OnChangePasswordCurrentField(string password)
        {
            currentPassword = password;
        }
        public void OnChangePasswordConfirmField(string password)
        {
            confirmPassword = password;
            confirmPasswordShowField.text = confirmPasswordField.text;
        }
        public void OnChangePasswordConfirmShowField(string password)
        {
            confirmPassword = password;
            confirmPasswordField.text = confirmPasswordShowField.text;
        }
        public void OnFirstNameField(string name)
        {
            firstName = name;
            profileUpdateFlag = true;
        }
        public void OnSurNameField(string name)
        {
            surName = name;
            profileUpdateFlag = true;
        }
        public void OnVillageNoField(string no)
        {
            villageNo = no;
            profileUpdateFlag = true;
        }
        public void OnContactVillageNoField(string no)
        {
            villageNo = no;
        }
        public void OnContactNoteField(string notes)
        {
            addNote = notes;
        }
        public void OnCountryNameButtonClicked()
        {
            uiManager.loadingScreen.SetActive(true);
            for (int i = 0; i < countryContent.childCount; i++)
            {
                Destroy(countryContent.GetChild(i).gameObject);
            }
            for (int i = 0; i < countryNameList.Count; i++)
            {
                GameObject name = Instantiate(countryNamePrefab, countryContent);
                name.transform.GetChild(0).GetComponent<Text>().text = countryNameList[i];
                int count = i;
                name.transform.GetComponent<Button>().onClick.AddListener(() => CountryNameButtonClicked(count));
            }
            uiManager.loadingScreen.SetActive(false);
            countryContent.anchoredPosition = new Vector2(0, 0);
            countryScreen.SetActive(true);
            profileUpdateFlag = true;
        }
        public void CountryNameButtonClicked(int index)
        {
            countryNameText.text = countryNameList[index];
            //countryName = countryNameText.text;
            countryNameText.color = countryNameColorEnable;
            countryScreen.SetActive(false);
        }
        public void OnCountryNameBackButtonClicked()
        {
            countryScreen.SetActive(false);
        }
        public void OnContactEmailField(string email)
        {

            contactEmail = email;
            contactEmail = contactEmailField.text;
        }
        public void OnContackSubmitButtonClicked()
        {
            if (contactEmail.Length > 0)
            {
                if (Regex.IsMatch(contactEmail, matchEmailPattern))
                {
                    if (contactVillaheNoField.text.Length > 0 && contactAddNoteField.text.Length > 0)
                    {
                        if (uiManager.CheckInternet())
                        {
                            uiManager.loadingScreen.SetActive(true);
                            uiManager.apiManager.APIContactUs(addNote, gameStatus, villageNo,contactEmail);
                        }

                    }
                    else
                    {
                        fieldRequriedImage.SetActive(true);
                        StartCoroutine(FieldRequriedImageOff());
                    }
                }
                else
                {
                    updateText.text = "Please enter valid email";
                    updateImage.SetActive(true);
                    StartCoroutine(FieldRequriedImageOff());
                }
            }
            else
            {
                updateText.text = "Please enter your email";
                updateImage.SetActive(true);
                StartCoroutine(FieldRequriedImageOff());
            }
                
        }
        public void OnContactAPICallBack(string message=null)
        {
            if (message == null)
            {
                updateText.text = "Your message successfully send";
                updateImage.SetActive(true);
                StartCoroutine(FieldRequriedImageOff());
                contactUsScreen.SetActive(false);
                settingMainScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }
            else
            {
                updateText.text = message;
                updateImage.SetActive(true);
                StartCoroutine(FieldRequriedImageOff());
                uiManager.loadingScreen.SetActive(false);
            }
            
        }
        public void OnContactVipButtonClciked()
        {
            contactVipImage.SetActive(true);
            contactNonVipImage.SetActive(false);
            gameStatus="VIP";
        }
        public void OnContactNonVipButtonClciked()
        {
            contactVipImage.SetActive(false);
            contactNonVipImage.SetActive(true);
            gameStatus = "Non-VIP";
        }
        public void OnProfileVipButtonClciked()
        {
            vipImage.SetActive(true);
            nonVipImage.SetActive(false);
            profileUpdateFlag = true;
            gameStatus = "VIP";
        }
        public void OnProfileNonVipButtonClciked()
        {
            vipImage.SetActive(false);
            nonVipImage.SetActive(true);
            profileUpdateFlag = true;
            gameStatus = "Non-VIP";
        }
        public void OnUpdateProfileButtonClicked()
        {
            if (profileUpdateFlag == true)
            {
                if (uiManager.CheckInternet())
                {
                    uiManager.loadingScreen.SetActive(true);
                    uiManager.apiManager.APIUpdateProfile(firstName, surName, villageNo, gameStatus);
                }

            }
            else
            {
                if (screenSwitch == "")
                {
                    updateText.text = "No New Changes Done";
                    updateImage.SetActive(true);
                    StartCoroutine(FieldRequriedImageOff());
                    settingMainScreen.SetActive(true);
                    myProfileScreen.SetActive(false);
                }
                else
                {
                    uiManager.loadingScreen.SetActive(true);
                    StartCoroutine(HomeCallBack());
                }
               

            }

        }
        public void UpdateProfileCallBack(string message = null)
        {
            if (message == null)
            {
                homeNameText.text = firstName;

                updateText.text = "Profile Updated";
                updateImage.SetActive(true);
                StartCoroutine(FieldRequriedImageOff());
                uiManager.loadingScreen.SetActive(false);
                if (screenSwitch == "")
                {
                    settingMainScreen.SetActive(true);
                    myProfileScreen.SetActive(false);
                }
                else
                {
                    uiManager.loadingScreen.SetActive(true);
                    StartCoroutine( HomeCallBack());
                }
            }
            else
            {
                passwotdText.text = message;
                paawordErrorImage.SetActive(true);
                StartCoroutine(FieldRequriedImageOff());
            }
            uiManager.loadingScreen.SetActive(false);

        }
        public IEnumerator HomeCallBack()
        {
            yield return new WaitForSeconds(1);
            uiManager.loadingScreen.SetActive(false);
            this.gameObject.SetActive(false);
            uiManager.homeScreen.SetActive(true);
        }
        public IEnumerator FieldRequriedImageOff()
        {
            yield return new WaitForSeconds(1);
            fieldRequriedImage.SetActive(false);
            updateImage.SetActive(false);
            paawordErrorImage.SetActive(false);
        }
        #endregion
    }
}
