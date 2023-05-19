using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RevolutionGames
{

    public class VikingScreen : MonoBehaviour
    {
        #region Variables

        public UIManager uiManager;
        public GameObject vikingMainScreen;
        public GameObject vikingScreen;
        public Text headerText;
        public Text symbolText;
        public TMP_Text detailText;
        public TMP_Text headerTMpText;
        public RectTransform content;
        public GameObject noDataScreen;
        private string screenName = "";
        public GameObject vipImage, nonVipImage;
        //public UniWebView webViewViking;
        //public UniWebView uniWebViewViking;

        private string status = "";

        #endregion

        #region Built in Methods
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            OnPhoneBackButtonHandler();
        }
        #endregion

        #region Cusrom Methods
        public void OnPhoneBackButtonHandler()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (screenName == "viking")
                {
                    OnBackButtonClicked();
                }

            }
        }
        public void OnMainBackButtonCLicked()
        {
            this.gameObject.SetActive(false);
            uiManager.homeScreen.SetActive(true);
            uiManager.screenName = "";
        }
        public void OnVipButtonClicked()
        {
            vipImage.SetActive(true);
            nonVipImage.SetActive(false);
            headerText.text = "VIP Vikings";
            status = "VIP";
            if (uiManager.CheckInternet())
            {
                uiManager.loadingScreen.SetActive(true);
                uiManager.apiManager.APIViking();
            }
            
            content.anchoredPosition = new Vector2(0, 0);
            
        }
        public void OnNonVipButtonClicked()
        {
            vipImage.SetActive(false);
            nonVipImage.SetActive(true);
            headerText.text = "Non-VIP Vikings";
            status = "Non-VIP";
            if (uiManager.CheckInternet())
            {
                uiManager.loadingScreen.SetActive(true);
                uiManager.apiManager.APIViking();
            }
            
            content.anchoredPosition = new Vector2(0, 0);
        }
        public void VikingAPICallBAck(List<APIData.Vikings> vikingsList)
        {
            bool flag = false;
            if (vikingsList.Count > 0)
            {
                for (int i = 0; i < vikingsList.Count; i++)
                {
                    if (vikingsList[i].status == "Active")
                    {
                        if (status == vikingsList[i].event_for)
                        {
                            flag = true;
                            symbolText.text = vikingsList[i].title;
                            headerTMpText.text= vikingsList[i].title;
                            detailText.text = uiManager.TextFilter( vikingsList[i].details);
                            uiManager.loadingScreen.SetActive(false);
                            vikingMainScreen.SetActive(false);
                            vikingScreen.SetActive(true);
                            screenName = "viking";
                            uiManager.screenSwitch = false;
                            //webViewViking.LoadWebView("http://docs.uniwebview.com/guide/");
                            //uniWebViewViking.Load("");
                        }
                    }
                }
                if (flag == false)
                {
                    noDataScreen.SetActive(true);
                }
       
            }
            else
            {
                noDataScreen.SetActive(true);
            }
            uiManager.loadingScreen.SetActive(false);

        }
        public void OnNodataOkButtonClicked()
        {
            noDataScreen.SetActive(false);
        }
        public void OnBackButtonClicked()
        {
            vikingMainScreen.SetActive(true);
            vikingScreen.SetActive(false);
            screenName = "";
            uiManager.screenSwitch = true;
        }
        #endregion
    }
}
