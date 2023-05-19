using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RevolutionGames
{

    public class SpinningPatternAndRewardListScreen : MonoBehaviour
    {
        #region Variables

        public UIManager uiManager;
        public Text vipOrNonVipText;
        public GameObject spinnigPatternAndRewardMainScreen;
        public GameObject spinnigPatternAndRewardSelectScreen;
        public GameObject eventScreen;
        public GameObject spinningScreen;
        public GameObject rewardListScreen;
        public RectTransform eventContent;
        public GameObject eventTypePrefab;
        public Text headerText;
        public TMP_Text detailText;
        public TMP_Text headeTMPText;
        public Text rewardListHeadertext;
        public GameObject rewardListPrefab;
        public RectTransform rewardListContent;
        public RectTransform spinningContent;
        public GameObject vipImage, nonVipImage;
        public InputField rewardField;
        public GameObject noDataScreen;
        private string screenName = "";
        //public UniWebView webViewSpinning;
        //public UniWebView uniWebViewSpinning; 

        private string statusVip="";
        private List<GameObject> eventobject = new List<GameObject>();
        private List<GameObject> rewardListObject = new List<GameObject>();
        List<APIData.SpinningEvent> spinningListMain = new List<APIData.SpinningEvent>();
        List<APIData.EventList> rewardEventMain = new List<APIData.EventList>();
        List<APIData.RewardList> rewardListtMain = new List<APIData.RewardList>();


        #endregion

        #region Built in Methods
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            /*if (Input.GetKey(KeyCode.Escape))
            {
                SpinningBackButtonClicked();
            }*/
            OnPhoneBackButtonHandler();
        }
        #endregion

        #region Custom Methods

        public void OnPhoneBackButtonHandler()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (screenName == "spinning")
                {
                    OnSpinningSelectBackButtonClicked();
                }
                else if (screenName == "event")
                {
                    OnEventTypeBackButtonClicked();
                }else if (screenName == "spindesc")
                {
                    SpinningBackButtonClicked();
                }
            }
        }
        public void OnSpinningMainBackButtonClicked()
        {
            this.gameObject.SetActive(false);
            uiManager.homeScreen.SetActive(true);
            uiManager.screenName = "";
        }
        public void OnVipButtonClicked()
        {
            vipImage.SetActive(true);
            nonVipImage.SetActive(false);
            vipOrNonVipText.text = "VIP";
            rewardListHeadertext.text = "Reward List - VIP";
            statusVip = "VIP";
            print( "sd");
            spinnigPatternAndRewardSelectScreen.SetActive(true);
            spinnigPatternAndRewardMainScreen.SetActive(false);
            screenName = "spinning";
            uiManager.screenSwitch = false;
        }
        public void OnNonVipButtonClicked()
        {
            vipImage.SetActive(false);
            nonVipImage.SetActive(true);
            vipOrNonVipText.text = "Non-VIP";
            rewardListHeadertext.text = "Reward List - Non-VIP";
            statusVip = "Non-VIP";
            spinnigPatternAndRewardSelectScreen.SetActive(true);
            spinnigPatternAndRewardMainScreen.SetActive(false);
            screenName = "spinning";
            uiManager.screenSwitch = false;
        }
        public void OnSpinningSelectBackButtonClicked()
        {
            spinnigPatternAndRewardSelectScreen.SetActive(false);
            spinnigPatternAndRewardMainScreen.SetActive(true);
            screenName = "";
            uiManager.screenSwitch = true;
        }
        public void OnSpinningButtonClicked()
        {
            if (uiManager.CheckInternet())
            {
                uiManager.loadingScreen.SetActive(true);
                uiManager.apiManager.APISpinning();
            }
            spinningListMain.Clear();
            eventobject.Clear();
            eventContent.anchoredPosition = new Vector2(0, 0);
            for (int i = 0; i < eventContent.childCount; i++)
            {
                Destroy(eventContent.GetChild(i).gameObject);
            }
        }
        public void SpinningAPICallBack(List<APIData.SpinningEvent> spinningList)
        {
            spinningListMain = spinningList;
            if (spinningList.Count > 0)
            {
                for (int i = 0; i < spinningList.Count; i++)
                {
                    if (statusVip == spinningList[i].event_for)
                    {
                        if (spinningList[i].status == "Active")
                        {
                            int count = i;
                            GameObject eventType = Instantiate(eventTypePrefab, eventContent);
                            eventType.transform.GetChild(1).GetComponent<Text>().text = spinningList[i].title;
                            eventType.transform.GetComponent<Button>().onClick.AddListener(() => EventSpinningButtonClicked(count));
                            eventobject.Add(eventType);
                        }

                    }
                }
                uiManager.loadingScreen.SetActive(false);
                eventScreen.SetActive(true);
                spinnigPatternAndRewardSelectScreen.SetActive(false);
                screenName = "event";
            }
            else
            {
                noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }
            
        }
        public void EventSpinningButtonClicked(int index)
        {
            spinningContent.anchoredPosition = new Vector2(0, 0);
            headerText.text = spinningListMain[index].title;
            headeTMPText.text= spinningListMain[index].title;
            detailText.text=uiManager.TextFilter( spinningListMain[index].details);
            
            //uniWebViewSpinning.Load("");
            eventScreen.SetActive(false);
            spinningScreen.SetActive(true);
            screenName = "spindesc";
            //webViewSpinning.LoadWebView("http://docs.uniwebview.com/guide/");
        }
        public void SpinningBackButtonClicked()
        {
            eventScreen.SetActive(true);
            spinningScreen.SetActive(false);
            screenName = "event";
        }
        public void OnRewardListButtonClicked()
        {
            if (uiManager.CheckInternet())
            {
                uiManager.loadingScreen.SetActive(true);
                uiManager.apiManager.APIRewardEvent();
            }
            eventContent.anchoredPosition = new Vector2(0, 0);
            eventobject.Clear();
            for (int i = 0; i < eventContent.childCount; i++)
            {
                Destroy(eventContent.GetChild(i).gameObject);
            }
        }
        public void RewardEventAPICallBack(List<APIData.EventList> rewardEventsList)
        {
            rewardEventMain= rewardEventsList;
            if (rewardEventsList.Count > 0)
            {
                for (int i = 0; i < rewardEventsList.Count; i++)
                {
                    int count = i;
                    if (statusVip == rewardEventsList[i].reward_event_for)
                    {
                        GameObject eventType = Instantiate(eventTypePrefab, eventContent);
                        eventType.transform.GetChild(1).GetComponent<Text>().text = rewardEventsList[i].event_name;
                        eventType.transform.GetComponent<Button>().onClick.AddListener(() => RewardEventButtonClicked(count));
                        eventobject.Add(eventType);
                    }
                    
                }
                if (eventobject.Count > 0)
                {
                    uiManager.loadingScreen.SetActive(false);
                    eventScreen.SetActive(true);
                    spinnigPatternAndRewardSelectScreen.SetActive(false);
                }
                else
                {
                    noDataScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                }
                   
            }
            else 
            {
                noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }
            
        }
        public void OnNodataOkButtonClicked() 
        {
            noDataScreen.SetActive(false);
        }
        public void RewardEventButtonClicked(int index)
        {
            if (uiManager.CheckInternet())
            {
                uiManager.loadingScreen.SetActive(true);
                for (int i = 1; i < rewardListContent.childCount; i++)
                {
                    Destroy(rewardListContent.GetChild(i).gameObject);
                }
                rewardListContent.anchoredPosition = new Vector2(0, 0);
                rewardListObject.Clear();
                uiManager.apiManager.APIRewardEventList(rewardEventMain[index].event_name);
            }
           

            /*if (uiManager.CheckInternet())
            {
                uiManager.loadingScreen.SetActive(true);
                uiManager.apiManager.APIRewardList(rewardEventMain[index].reward_event_id);
            }*/

        }
        public void EventListAPiCallBack(List<APIData.RewardEvent> rewardEventsList)
        {
            for (int i = 0; i < rewardEventsList.Count; i++)
            {
                GameObject rewardList = Instantiate(rewardListPrefab, rewardListContent);
                rewardList.transform.GetChild(1).GetComponent<Text>().text = rewardEventsList[i].mission.ToString();
                rewardList.transform.GetChild(2).GetComponent<Text>().text = rewardEventsList[i].total_hammers.ToString();
                rewardList.transform.GetChild(3).GetComponent<Text>().text = rewardEventsList[i].rewards;
                rewardListObject.Add(rewardList);
            }
            rewardField.text = "";
            uiManager.loadingScreen.SetActive(false);
            eventScreen.SetActive(false);
            rewardListScreen.SetActive(true);

        }
        public void RewardListApiCallBack(List<APIData.RewardList> rewardLists)
        {
            for (int i = 0; i < rewardLists.Count; i++)
            {
                if (rewardLists[i].status == "Active")
                {
                    GameObject rewardList = Instantiate(rewardListPrefab,rewardListContent);
                    rewardList.transform.GetChild(1).GetComponent<Text>().text = rewardLists[i].mission.ToString();
                    rewardList.transform.GetChild(2).GetComponent<Text>().text = rewardLists[i].total_hammers.ToString();
                    rewardList.transform.GetChild(3).GetComponent<Text>().text = rewardLists[i].rewards;
                    rewardListObject.Add(rewardList);
                }
            }
            uiManager.loadingScreen.SetActive(false);
            eventScreen.SetActive(false);
            rewardListScreen.SetActive(true);
        }
        public void RewardListBackButtonClicked()
        {
            eventScreen.SetActive(true);
            rewardListScreen.SetActive(false);
        }
        public void OnEventTypeBackButtonClicked()
        {
            eventScreen.SetActive(false);
            spinnigPatternAndRewardSelectScreen.SetActive(true);
            screenName = "spinning";
        }
        public void OnRewardField(string mission)
        {
            if (mission.Length > 0)
            {
                for (int i = 0; i < rewardListObject.Count; i++)
                {
                    if (rewardListObject[i].transform.GetChild(1).GetComponent<Text>().text.Contains(mission))
                    {
                        rewardListObject[i].SetActive(true);
                    }
                    else
                    {
                        rewardListObject[i].SetActive(false);
                    }
                }
            }
            else
            {
                for (int i = 0; i < rewardListObject.Count; i++)
                {
                    rewardListObject[i].SetActive(true);
                }
            }
        }
        #endregion
    }
}
