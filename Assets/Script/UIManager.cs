using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

namespace RevolutionGames
{

    public class UIManager : MonoBehaviour
    {
        #region Variables
        //public IAPManager iapManager;
        public APIManager apiManager;
        public GameObject splshScreen;
        public GameObject loginScreen;
        public GameObject homeScreen;
        public GameObject basicInfoScreen;
        public GameObject spinningPatternAndRewardScreen;
        public GameObject vikingScreen;
        public GameObject settingScreen;
        public GameObject faqScreen;
        public GameObject loadingScreen;
        public GameObject noNetworkScreen;
        public GameObject noDataScreen;
        public GameObject uploadImageLoadingScreen;
        public GameObject connectionFailedScreen;
        public GameObject termsScreen;
        public GameObject privacyScreen;
        public bool loginFlag = false;
        private bool networkFlag = false;
        public string screenName = "";
        public bool screenSwitch = false;
        public bool purchaseExpired = false;
        public bool noDataLogin = false;
        public bool imageFlag = false;
        private void Start()
        {
            //PlayerPrefs.DeleteAll();

            if (Application.platform == RuntimePlatform.Android)
                apiManager.APIData.game_Details.device_name = "Android";
            else
                apiManager.APIData.game_Details.device_name = "IOS";

            if (PlayerPrefs.GetInt("login") == 0)
            {
                loginFlag = false;
                loginScreen.SetActive(true);
                homeScreen.SetActive(false);
                loadingScreen.SetActive(false);
            }
            else
            {
                networkFlag = true;
                StartCoroutine( ExcistUserLogin());
            }
        }

        private void Update()
        {
            OnPhoneBackButtonHandler();
        }

        public void OnPhoneBackButtonHandler()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(screenSwitch==true)
                {
                    if (screenName == "basichome")
                    {
                        basicInfoScreen.transform.GetComponent<BasicInfoScreen>().OnMainBackButtonClicked();
                    }
                    else if (screenName == "settinghome")
                    {
                        settingScreen.transform.GetComponent<SettingScreen>().OnSeetingBackButtonClicked();
                    }
                    else if (screenName == "spinhome")
                    {
                        spinningPatternAndRewardScreen.transform.GetComponent<SpinningPatternAndRewardListScreen>().OnSpinningMainBackButtonClicked();
                    }
                    else if (screenName == "vikinghome")
                    {
                        vikingScreen.transform.GetComponent<VikingScreen>().OnMainBackButtonCLicked();
                    }else if (screenName == "faq")
                    {
                        faqScreen.transform.GetComponent<FAQScreenParent>().OnBackButtonClicked();
                    }
                    else
                    {
                        //Application.Quit();
                    }
                }
               
            }
        }

        public IEnumerator ExcistUserLogin()
        {
            yield return new WaitForSeconds(3.1f);
            if (CheckInternet())
            {
                networkFlag = false;
                loginFlag = true;
                loadingScreen.SetActive(true);
                if (PlayerPrefs.GetInt("fblogin") == 0)
                {
                    apiManager.APIUserLogin(PlayerPrefs.GetString("email"), PlayerPrefs.GetString("password")); 
                }
                else
                {
                    apiManager.APIFacebookLogin(PlayerPrefs.GetString("fbmail"), PlayerPrefs.GetString("fbname"), PlayerPrefs.GetString("fbnamelast"), PlayerPrefs.GetString("fbid"), PlayerPrefs.GetString("mode"));
                }


            }
            //homeScreen.SetActive(true);
            loginScreen.SetActive(false);
        }

        public bool CheckInternet()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                noNetworkScreen.SetActive(false);
                return true;
                
            }
            else
            {
                noNetworkScreen.SetActive(true);
                loadingScreen.SetActive(false);
                return false;
                
            }
        }

        public void OnNoNetworkOkButtonClicked()
        {
            connectionFailedScreen.SetActive(false);
            noNetworkScreen.SetActive(false);
            if (networkFlag == true)
            {
                loadingScreen.SetActive(true);
                StartCoroutine(ExcistUserLogin());
            }
        }
        public void OnNoDataOkButtonClicked()
        {
            connectionFailedScreen.SetActive(false);
            noDataScreen.SetActive(false);
            if (noDataLogin == false)
            {
                loadingScreen.SetActive(true);
                StartCoroutine(ExcistUserLogin());
            }
        }
        public string TextFilter(string incomingText)
        {
            string outText = "";

            var substring = Regex.Escape("<p>");
            var substitute = " ";
            var counter = 1;
            //outText = Regex.Replace(incomingText, substring, (m) => $"{counter++}");
            outText = Regex.Replace(incomingText, substring, substitute);

            substring = Regex.Escape("</p>");
            substitute = "";
            counter = 1;
            outText = Regex.Replace(outText, substring, substitute);

            substring = Regex.Escape("&nbsp;");
            substitute = " ";
            counter = 1;
            outText = Regex.Replace(outText, substring, substitute);

            substring = Regex.Escape("&amp;");
            substitute = " ";
            counter = 1;
            outText = Regex.Replace(outText, substring, substitute);

            substring = Regex.Escape("<ol>");
            substitute = "";
            counter = 1;
            outText = Regex.Replace(outText, substring, substitute);

            substring = Regex.Escape("<li>");
            substitute = "  - ";
            counter = 1;
            outText = Regex.Replace(outText, substring, substitute);

            substring = Regex.Escape("</li>");
            substitute = "";
            counter = 1;
            outText = Regex.Replace(outText, substring, substitute);

            substring = Regex.Escape("</ol>");
            substitute = "";
            counter = 1;
            outText = Regex.Replace(outText, substring, substitute);

            substring = Regex.Escape("&ndash;");
            substitute = " - ";
            counter = 1;
            outText = Regex.Replace(outText, substring, substitute);
            
            substring = Regex.Escape("<strong>");
            substitute = " B ";
            counter = 1;
            outText = Regex.Replace(outText, substring, substitute);
            
            substring = Regex.Escape("</strong>");
            substitute = "";
            counter = 1;
            outText = Regex.Replace(outText, substring, substitute);
            
            substring = Regex.Escape("&copy;");
            substitute = " ";
            counter = 1;
            outText = Regex.Replace(outText, substring, substitute);
            
            substring = Regex.Escape("&rsquo;");
            substitute = "’";
            counter = 1;
            outText = Regex.Replace(outText, substring, substitute);

            //print(outText);

            return outText;


        }
        public void OnTermsCloseButtonClicked()
        {
            termsScreen.SetActive(false);
            privacyScreen.SetActive(false);
        }
        #endregion

    }
}
