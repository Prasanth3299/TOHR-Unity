using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RevolutionGames
{
    public class FAQScreenParent : MonoBehaviour
    {
        #region Variables

        public UIManager uiManager;
        public GameObject faqMainScreen;
        public GameObject faqScreen;
        public Text headetText;
        public GameObject questionPrefab, answerPrefab;
        public RectTransform content;
        public GameObject noDataScreen;
        private string status = "";
        private List<GameObject> faqQuestionObject = new List<GameObject>();
        private List<GameObject> faqAnswerObject = new List<GameObject>();
        private string screenName = "";

        #endregion

        private void Update()
        {
            OnPhoneBackButtonHandler();
        }

        #region Custom Methods
        public void OnPhoneBackButtonHandler()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (screenName == "faq")
                {
                    OnBackFaqButtonClicked();
                }
                
            }
        }
        public void OnBackButtonClicked()
        {
            this.gameObject.SetActive(false);
            uiManager.basicInfoScreen.SetActive(true);
            uiManager.screenName = "basichome";
        }
        public void OnBackFaqButtonClicked()
        {
            faqScreen.SetActive(false);
            faqMainScreen.SetActive(true);
            screenName = "";
            uiManager.screenSwitch = true;
        }
        public void OnVipButtonClicked()
        {
            // api call
            faqQuestionObject.Clear();
            faqAnswerObject.Clear();
            if (uiManager.CheckInternet())
            {
                uiManager.loadingScreen.SetActive(true);
                uiManager.apiManager.APIFAQ();
            }
            
            headetText.text = "FAQ's - VIP";
            status = "VIP";
            for (int i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject);
            }
           
        }
        public void OnNonVipButtonClicked()
        {
            // api call
            if (uiManager.CheckInternet())
            {
                uiManager.loadingScreen.SetActive(true);
                uiManager.apiManager.APIFAQ();
            }
            faqQuestionObject.Clear();
            faqAnswerObject.Clear();
            for (int i = 0; i < content.childCount; i++)
            {
                Destroy(content.GetChild(i).gameObject);
            }
            status = "Non-VIP";
            headetText.text = "FAQ's - NON-VIP";
            
        }

        public void OnFaqApiCallBack(List<APIData.FAQ> faqList)
        {
            if (faqList.Count > 0)
            {
                print("count"+faqList.Count);
                int count = 0;
                for (int i = 0; i < faqList.Count; i++)
                {
                    print(faqList[i].faq_for);
                    
                    if (status == faqList[i].faq_for)
                    {

                        GameObject question = Instantiate(questionPrefab, content);
                        faqQuestionObject.Add(question);
                        question.transform.GetChild(0).GetComponent<Text>().text = uiManager.TextFilter(faqList[i].question);
                        GameObject answer = Instantiate(answerPrefab, content);
                        faqAnswerObject.Add(answer);
                        answer.transform.GetChild(0).GetComponent<TMP_Text>().text = uiManager.TextFilter(faqList[i].answer);
                        answer.gameObject.SetActive(false);
                        int count1 = count;
                        question.transform.GetComponent<Button>().onClick.AddListener(() => OnQustionButtonClicked(count1));
                        question.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnQustionButtonClicked(count1));
                        count = count + 1;
                    }
                   
                }
                uiManager.loadingScreen.SetActive(false);
                faqMainScreen.SetActive(false);
                faqScreen.SetActive(true);
                screenName = "faq";
                uiManager.screenSwitch = false;
            }
            else
            {
                uiManager.loadingScreen.SetActive(false);
                noDataScreen.SetActive(true);
            }
            
        }
        public void OnNodataOkButtonClicked()
        {
            noDataScreen.SetActive(false);
        }
        public void OnQustionButtonClicked(int index)
        {
            if (faqAnswerObject[index].activeSelf)
            {
                faqAnswerObject[index].SetActive(false);
            }
            else
            {
                faqAnswerObject[index].SetActive(true);
            }
        }

        #endregion
    }
}
