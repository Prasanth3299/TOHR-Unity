using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;
using TMPro;

namespace RevolutionGames
{
    public class BasicInfoScreen : MonoBehaviour
    {
        #region Variables

        public UIManager uiManager;
        public GameObject basicMainScreen;
        public GameObject villageCostScreen;
        public GameObject petInfoMainScreen;
        public GameObject petInfoScreen;
        public GameObject chestScreen;
        public GameObject setScreen;
        public GameObject sideEventScreen;
        public GameObject sideEventDescriptionScreen;
        public GameObject faqMainScreen;
        public GameObject subSetScreen;
        public Text petHeaderText,petPercentageText;
        public RectTransform villageCostContent,chestCostContent,setsContent,petInfoContent,sideEventContent;
        public GameObject villageCostPrefab,chestCostPrefab,setPrefab,petInfoPrefab,sideEventPrefab;
        public InputField villageCostField, chestCostField, setsField, petInfoField;
        public TMP_Text sideEventDescText;
        public GameObject petMainPrefab;
        public RectTransform petMainContent;
        public GameObject subSetPrefab;
        public RectTransform subSetContent;
        private string screenName = "";

        List<APIData.SideEvents> sideEventListMain = new List<APIData.SideEvents>();

        private List<GameObject> villageCostObject = new List<GameObject>();
        private List<GameObject> chestCostObject = new List<GameObject>();
        private List<GameObject> PetObject = new List<GameObject>();
        private List<GameObject> PetDetailObject = new List<GameObject>();
        private List<GameObject> setsObject = new List<GameObject>();
        private List<GameObject> subSetsObject = new List<GameObject>();
        private List<GameObject> sideEventObject = new List<GameObject>();
        List<APIData.PetInfo> petInfosListMain = new List<APIData.PetInfo>();
        List<APIData.Sets> setListMain = new List<APIData.Sets>(); 
        private string petName="";
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

        #region Custom Methods


        public void OnPhoneBackButtonHandler()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (screenName == "viking")
                {
                    OnVillageCostBackButtonClicked();
                }
                else if (screenName == "chest")
                {
                    OnChestCostBackButtonClicked();
                }else if (screenName == "petinfomain")
                {
                    OnPetMainBackButtonClicked();
                }else if (screenName == "petinfo")
                {
                    OnPetBackButtonClicked();
                }else if (screenName == "setmain")
                {
                    OnSetBackButtonClicked();
                }else if (screenName == "subset")
                {
                    OnSubSetBackButtonClicked();
                }else if (screenName == "sideevent")
                {
                    OnSideEventMainBackButtonClicked();
                }else if (screenName == "sidedesc")
                {
                    OnSideEventDescCloseButtonClicked();
                }
            }
        }

        public void OnMainBackButtonClicked()
        {
            this.gameObject.SetActive(false);
            uiManager.homeScreen.SetActive(true);
            uiManager.screenName = "";
        }
        public void OnVillageCostButtonClicked()
        {
            // api call
            if (uiManager.CheckInternet())
            {
                uiManager.loadingScreen.SetActive(true);
                uiManager.apiManager.APIVillageCost();
                villageCostField.text = "";
                villageCostObject.Clear();
                for (int i = 0; i < villageCostContent.childCount; i++)
                {
                    Destroy(villageCostContent.GetChild(i).gameObject);
                }
                villageCostContent.anchoredPosition = new Vector2(0, 0);
            }
            
            
        }
        public void OnVillageCostButtonCallBack(List<APIData.VillageCost> villageList)
        {
            // api call back
            for (int i = 0; i < villageList.Count; i++)
            {
                if (villageList[i].status == "Active")
                {
                    GameObject villageCost = Instantiate(villageCostPrefab, villageCostContent);
                    villageCostObject.Add(villageCost);
                    villageCostObject[i].transform.GetChild(1).GetComponent<Text>().text = villageList[i].village_id.ToString();
                    villageCostObject[i].transform.GetChild(2).GetComponent<Text>().text = villageList[i].village_name;
                    villageCostObject[i].transform.GetChild(3).GetComponent<Text>().text = villageList[i].village_cost;
                    villageCostObject[i].transform.GetChild(4).GetComponent<Text>().text = villageList[i].twenty_percentage_vm_cost;
                    villageCostObject[i].transform.GetChild(5).GetComponent<Text>().text = villageList[i].sixtyfive_percentage_vm_cost;
                }

            }
            uiManager.loadingScreen.SetActive(false);
            basicMainScreen.SetActive(false);
            villageCostScreen.SetActive(true);
            screenName = "viking";
            uiManager.screenSwitch = false;
        }
        public void OnVillageCostBackButtonClicked()
        {
            basicMainScreen.SetActive(true);
            villageCostScreen.SetActive(false);
            uiManager.screenSwitch = true;
            screenName = "";
        }
        public void OnChestCostButtonClicked()
        {
            // api call
            if (uiManager.CheckInternet())
            {
                uiManager.loadingScreen.SetActive(true);
                chestCostObject.Clear();
                uiManager.apiManager.APIChestCost();
                for (int i = 0; i < chestCostContent.childCount; i++)
                {
                    Destroy(chestCostContent.GetChild(i).gameObject);
                }
                chestCostField.text = "";
                chestCostContent.anchoredPosition = new Vector2(0, 0);
            }
            
            
        }
        public void OnChestCostButtonCallBack(List<APIData.ChestCost> chestCostList)
        {
            // api call back
            for (int i = 0; i < chestCostList.Count; i++)
            {
                GameObject chestCost = Instantiate(chestCostPrefab, chestCostContent);
                chestCostObject.Add(chestCost);
                chestCostObject[i].transform.GetChild(1).GetComponent<Text>().text = chestCostList[i].village_id.ToString();
                chestCostObject[i].transform.GetChild(2).GetComponent<Text>().text = chestCostList[i].village_name;
                chestCostObject[i].transform.GetChild(3).GetComponent<Text>().text = chestCostList[i].wooden_value;
                chestCostObject[i].transform.GetChild(4).GetComponent<Text>().text = chestCostList[i].golden_value;
                chestCostObject[i].transform.GetChild(5).GetComponent<Text>().text = chestCostList[i].magical_value;

            }
            uiManager.loadingScreen.SetActive(false);
            basicMainScreen.SetActive(false);
            chestScreen.SetActive(true);
            screenName = "chest";
            uiManager.screenSwitch = false;
        }
        public void OnChestCostBackButtonClicked()
        {
            basicMainScreen.SetActive(true);
            chestScreen.SetActive(false);
            uiManager.screenSwitch = true;
            screenName = "";

        }
        public void OnSetButtonClicked()
        {
            // api call
            if (uiManager.CheckInternet())
            {
                uiManager.loadingScreen.SetActive(true);
                setsField.text = "";
                setsObject.Clear();
                setListMain.Clear();
                uiManager.apiManager.APISets();
                for (int i = 0; i < setsContent.childCount; i++)
                {
                    Destroy(setsContent.GetChild(i).gameObject);
                }
                setsContent.anchoredPosition = new Vector2(0, 0);
            }
            
            
        }
        public void OnSetButtonCallBack(List<APIData.Sets> setList)
        {
            // api call back
            setListMain = setList;
            for (int i = 0; i < setList.Count; i++)
            {
                GameObject sets = Instantiate(setPrefab, setsContent);
                setsObject.Add(sets);
                setsObject[i].transform.GetChild(1).GetComponent<Text>().text = setList[i].set_id.ToString();
                setsObject[i].transform.GetChild(2).GetComponent<Text>().text = setList[i].set_name;
                setsObject[i].transform.GetChild(3).GetComponent<Text>().text = setList[i].un_lock.ToString();
                setsObject[i].transform.GetChild(4).GetComponent<Text>().text = setList[i].complete.ToString();
                setsObject[i].transform.GetChild(5).GetComponent<Text>().text = setList[i].spin.ToString();
                setsObject[i].transform.GetChild(6).GetComponent<Text>().text = setList[i].xp;
                setsObject[i].transform.GetChild(7).GetComponent<Text>().text = setList[i].pet_food;
                int count = i;
                setsObject[i].transform.GetComponent<Button>().onClick.AddListener(() => OnSubSetButtonClicked(count));

            }
            uiManager.loadingScreen.SetActive(false);
            basicMainScreen.SetActive(false);
            setScreen.SetActive(true);
            screenName = "setmain";
            uiManager.screenSwitch = false;
        }
        public void OnSubSetButtonClicked(int index)
        {
            subSetContent.anchoredPosition = new Vector2(0, 0);
            for (int i = 0; i < subSetContent.childCount; i++)
            {
                Destroy(subSetContent.GetChild(i).gameObject);
            }
            subSetsObject.Clear();
            if (uiManager.CheckInternet())
            {
                uiManager.loadingScreen.SetActive(true);
                uiManager.apiManager.APISubSets(setListMain[index].set_id);
            }
        }
        public void SubSetCallBack(List<APIData.SubSet> subSets)
        {
            for (int i = 0; i < subSets.Count; i++)
            {
                GameObject sets = Instantiate(subSetPrefab, subSetContent);
                subSetsObject.Add(sets);
                subSetsObject[i].transform.GetChild(1).GetComponent<Text>().text = subSets[i].sub_set_title;
                subSetsObject[i].transform.GetChild(2).GetComponent<Text>().text = subSets[i].un_lock.ToString();
            }
            uiManager.loadingScreen.SetActive(false);
            subSetScreen.SetActive(true);
            screenName = "subset";
        }
        public void OnSetBackButtonClicked()
        {
            basicMainScreen.SetActive(true);
            setScreen.SetActive(false);
            uiManager.screenSwitch = true;
            screenName = "";
        }
        public void OnSubSetBackButtonClicked()
        {
            subSetScreen.SetActive(false);
            screenName = "setmain";
        }
        public void OnPetButtonClicked()
        {
            if (uiManager.CheckInternet())
            {
                uiManager.loadingScreen.SetActive(true);
                PetObject.Clear();
                uiManager.apiManager.APIPetInfo();
                for (int i = 0; i < petMainContent.childCount; i++)
                {
                    Destroy(petMainContent.GetChild(i).gameObject);
                }
                petMainContent.anchoredPosition = new Vector2(0, 0);
            }
            
        }
        public void PetInfoCallBack(List<APIData.PetInfo> petInfosList)
        {
            petInfosListMain = petInfosList;
            for (int i = 0; i < petInfosList.Count; i++)
            {
                GameObject petInfo = Instantiate(petMainPrefab, petMainContent);
                PetObject.Add(petInfo);
                PetObject[i].transform.GetChild(1).GetComponent<Text>().text = petInfosList[i].pet_name;
                int count = i;
                PetObject[i].transform.GetComponent<Button>().onClick.AddListener(() => PetButtonClicked(count));
                PetObject[i].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => PetButtonClicked(count));
            }
            uiManager.loadingScreen.SetActive(false);
            basicMainScreen.SetActive(false);
            petInfoMainScreen.SetActive(true);
            screenName = "petinfomain";
            uiManager.screenSwitch = false;
        }

        public void PetButtonClicked(int index)
        {
            PetDetailObject.Clear();
            if (uiManager.CheckInternet())
            {
                uiManager.loadingScreen.SetActive(true);
                uiManager.apiManager.APIPetDetails(petInfosListMain[index].pet_id);
            }
            
            petInfoField.text = "";
            petInfoContent.anchoredPosition = new Vector2(0, 0);
            for (int i = 0; i < petInfoContent.childCount; i++)
            {
                Destroy(petInfoContent.GetChild(i).gameObject);
            }
            if (petInfosListMain[index].pet_id == 1)
            {
                petHeaderText.text = "Foxy";
                petPercentageText.text = "Raid Rewards%";
            }
            else if (petInfosListMain[index].pet_id == 2)
            {
                petHeaderText.text = "Tiger";
                petPercentageText.text = "Attack Rewards%";
            }
            else
            {
                petHeaderText.text = "Rhino";
                petPercentageText.text = "Protection%";
            }
        }
        public void OnPetMainBackButtonClicked()
        {
            basicMainScreen.SetActive(true);
            petInfoMainScreen.SetActive(false);
            screenName = "";
            uiManager.screenSwitch = true;
        }
        public void OnSideEventButtonClicked()
        {
            // api call
            if (uiManager.CheckInternet())
            {
                uiManager.loadingScreen.SetActive(true);
                uiManager.apiManager.APISideEvents();
            }
            
            sideEventObject.Clear();
            for (int i = 0; i < sideEventContent.childCount; i++)
            {
                Destroy(sideEventContent.GetChild(i).gameObject);
            }
            sideEventContent.anchoredPosition = new Vector2(0, 0);
            
        }
        public void OnSideEventButtonCallBack(List<APIData.SideEvents> sideEventList)
        {
            // api call back
            
            sideEventListMain = sideEventList;
            if (sideEventList.Count > 0)
            {
                for (int i = 0; i < sideEventList.Count; i++)
                {
                    GameObject sideEvents = Instantiate(sideEventPrefab, sideEventContent);
                    int count = i;
                    sideEventObject.Add(sideEvents);
                    sideEventObject[i].transform.GetChild(1).GetComponent<Text>().text = sideEventList[i].event_name;
                    sideEventObject[i].transform.GetComponent<Button>().onClick.AddListener(() => OnSideEventDescButtonClicked(count));
                    sideEventObject[i].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnSideEventDescButtonClicked(count));

                }
                uiManager.loadingScreen.SetActive(false);
                basicMainScreen.SetActive(false);
                sideEventScreen.SetActive(true);
                screenName = "sideevent";
                uiManager.screenSwitch = false;
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }
            
        }
        public void OnSideEventDescButtonClicked(int index)
        {
            sideEventDescText.text = sideEventListMain[index].description;
            sideEventDescriptionScreen.SetActive(true);
            screenName = "sidedesc";
        }
        public void OnSideEventDescCloseButtonClicked()
        {
            sideEventDescriptionScreen.SetActive(false);
            screenName = "sideevent";
        }
        public void OnSideEventMainBackButtonClicked()
        {
            basicMainScreen.SetActive(true);
            sideEventScreen.SetActive(false);
            screenName = "";
            uiManager.screenSwitch = true;
        }
        public void OnFaqButtonClicked()
        {
            if (uiManager.CheckInternet())
            {
                this.gameObject.SetActive(false);
                uiManager.faqScreen.SetActive(true);
                uiManager.screenName = "faq";
                uiManager.screenSwitch = true;
            }
            
        }

        public void OnFoxyButtonCLicked()
        {
           
            petInfoField.text = "";
            petInfoContent.anchoredPosition = new Vector2(0, 0);
            petHeaderText.text = "Foxy";
            petName = "Foxy";
            petPercentageText.text = "Raid Rewards%";

            PetObject.Clear();
            uiManager.apiManager.APIPetInfo();
            for (int i = 1; i < petInfoContent.childCount; i++)
            {
                Destroy(petInfoContent.GetChild(i).gameObject);
            }
            /*petInfoMainScreen.SetActive(false);
            petInfoScreen.SetActive(true);*/
        }
        public void OnTigerButtonCLicked()
        {
            petInfoField.text = "";
            petInfoContent.anchoredPosition = new Vector2(0, 0);
            petHeaderText.text = "Tiger";
            petName = "Tiger";
            petPercentageText.text = "Attack Rewards%";
            PetObject.Clear();
            uiManager.apiManager.APIPetInfo();
            for (int i = 1; i < petInfoContent.childCount; i++)
            {
                Destroy(petInfoContent.GetChild(i).gameObject);
            }
           /* petInfoMainScreen.SetActive(false);
            petInfoScreen.SetActive(true);*/
        }
        public void OnRhinoButtonCLicked()
        {
            petInfoField.text = "";
            petInfoContent.anchoredPosition = new Vector2(0, 0);
            petHeaderText.text = "Rhino";
            petName = "Rhino";
            petPercentageText.text = "Protection%";
            PetObject.Clear();
            uiManager.apiManager.APIPetInfo();
            for (int i = 1; i < petInfoContent.childCount; i++)
            {
                Destroy(petInfoContent.GetChild(i).gameObject);
            }
            /*petInfoMainScreen.SetActive(false);
            petInfoScreen.SetActive(true);*/
        }
        public void PetInfoAPICallBack(List<APIData.PetDetails> petInfoList)
        {
            for (int i = 0; i < petInfoList.Count; i++)
            {
                GameObject prtInfo = Instantiate(petInfoPrefab, petInfoContent);
                PetDetailObject.Add(prtInfo);
                PetDetailObject[i].transform.GetChild(1).GetComponent<Text>().text = petInfoList[i].level.ToString();
                PetDetailObject[i].transform.GetChild(2).GetComponent<Text>().text = petInfoList[i].xp;
                PetDetailObject[i].transform.GetChild(3).GetComponent<Text>().text = petInfoList[i].attack;
                PetDetailObject[i].transform.GetChild(4).GetComponent<Text>().text = petInfoList[i].star;

            }
            uiManager.loadingScreen.SetActive(false);
            petInfoMainScreen.SetActive(false);
            petInfoScreen.SetActive(true);
            screenName = "petinfo";
        }

        public void OnPetBackButtonClicked()
        {
            petInfoMainScreen.SetActive(true);
            petInfoScreen.SetActive(false);
            screenName = "petinfomain";
        }
        public void OnFaqBackButtonClicked()
        {
            faqMainScreen.SetActive(false);
            basicMainScreen.SetActive(true);
        }

        public void OnVillageField(string villageNo)
        {
            if (villageNo.Length > 0)
            {
                for (int i = 0; i < villageCostObject.Count; i++)
                {
                    if (villageCostObject[i].transform.GetChild(1).GetComponent<Text>().text.Contains( villageNo))
                    {
                        villageCostObject[i].SetActive(true);
                    }
                    else
                    {
                        villageCostObject[i].SetActive(false);
                    }
                }
            }
            else
            {
                for (int i = 0; i < villageCostObject.Count; i++)
                {
                    villageCostObject[i].SetActive(true);
                }
            }
        }
        public void OnChestField(string villageNo)
        {
            if (villageNo.Length > 0)
            {
                for (int i = 0; i < chestCostObject.Count; i++)
                {
                    if (chestCostObject[i].transform.GetChild(1).GetComponent<Text>().text.Contains( villageNo))
                    {
                        chestCostObject[i].SetActive(true);
                    }
                    else
                    {
                        chestCostObject[i].SetActive(false);
                    }
                }
            }
            else
            {
                for (int i = 0; i < chestCostObject.Count; i++)
                {
                    chestCostObject[i].SetActive(true);
                }
            }
        }
        public void OnSetsField(string setName)
        {
            if (setName.Length > 0)
            {
                for (int i = 0; i < setsObject.Count; i++)
                {
                    if (setsObject[i].transform.GetChild(2).GetComponent<Text>().text.ToLower().Contains(setName.ToLower())|| setsObject[i].transform.GetChild(1).GetComponent<Text>().text.Contains(setName))
                    {
                        setsObject[i].SetActive(true);
                    }
                    else
                    {
                        setsObject[i].SetActive(false);
                    }
                }
            }
            else
            {
                for (int i = 0; i < setsObject.Count; i++)
                {
                    setsObject[i].SetActive(true);
                }
            }
        }
        public void OnPetField(string level)
        {
            if (level.Length > 0)
            {
                for (int i = 0; i < PetDetailObject.Count; i++)
                {
                    if (PetDetailObject[i].transform.GetChild(1).GetComponent<Text>().text.Contains(level))
                    {
                        PetDetailObject[i].SetActive(true);
                    }
                    else
                    {
                        PetDetailObject[i].SetActive(false);
                    }
                }
            }
            else
            {
                for (int i = 0; i < PetDetailObject.Count; i++)
                {
                    PetDetailObject[i].SetActive(true);
                }
            }
        }
        #endregion
    }
}
