using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine.Networking;
using Models;
using Proyecto26;
using BestHTTP;

namespace RevolutionGames
{
    #region ResponseCode
    class APIResponseCode
    {
        public static int SUCCESS = 200;
        public static int INPUT_ERROR = 300;
        public static int DB_ERROR = 400;
        public static int SERVER_ERROR = 500;
        public static int FAILURE = 404;
        public static int WRONG_DATA = 201;
    }
    #endregion

    #region API Data sent class

    public class UploadImage
    {
        public int player_id;
        public byte[] profile_image;
    }

    class UserLogin
    {
        public string email;
        public string password;
        public string device_name;
        public string device_token;
        public bool is_admin;
    }
    class FacebookLogin
    {
        public string first_name;
        public string sur_name;
        public string email;
        public string social_id;
        public string player_login_mode;
        public string game_status;
        public string device_name;
        public string device_token;
    }
    class ForgotPassword
    {
        public string email;
    }
    class EmailValidation
    {
        public string player_id;
    }
    class ChangePassword
    {
        public int player_id;
        public string old_password;
        public string new_password;
    }
    class UserRegister
    {
        public string first_name;
        public string sur_name;
        public string email;
        public string password;
        public int village_number;
        public string game_status;
        public string device_name;
        public string device_token;
        public string reference_no;
        public string version_no;
    }
    class VillageCost
    {
        public string customer_Id;
    }
    class GetProfile
    {
        public int player_id;
    }
    class ProfileUpdate
    {
        public int player_id;
        public string first_name;
        public string sur_name;
        public string email;
        public string village_number;
        public string game_status;
    }
    class ContactUs
    {
        public int village_no;
        public string email;
        public string note;
        public string game_status;

    }
    class RewardList
    {
        public int reward_list_id;
    }
    class PetDetails
    {
        public int pet_id;
    }
    class CheckSubcribe
    {
        public int player_id;
    }
    class SubSet
    {
        public int set_id;
    }
    class promoCode
    {
        public int player_id;
        public string promo_code;
    }
    class UpdateNotification
    {
        public int player_id;
        public bool notification;
    }
    class SuncribeDetail
    {
        public int player_id;
    }
    class Logout
    {
        public int player_id;
    }
    class Subscription
    {
        public int player_id;
        public string plan_name;
        public float plan_price;
        public string payment_id;
    }
    class AskExperts
    {
        public string email;
        public string title;
        public string note;
        public string game_status;
    }
    class EventList
    {
        public string event_name;
    }
        

    #endregion

    public class APIManager : MonoBehaviour
    {
        #region Properties
        private string screenName="";
        public UIManager uiManager;
        private APIData apiData = null;
        private bool network = true;
        private RequestHelper currentRequest;
        private System.Action<RequestException, ResponseHelper, Post> user_Login_Action;
        private System.Action<RequestException, ResponseHelper, Post> user_ForgotPassword_Action;
        private System.Action<RequestException, ResponseHelper, Post> user_Register_Action;
        private System.Action<RequestException, ResponseHelper, Post> villageCost_Action;
        private System.Action<RequestException, ResponseHelper, Post> chestCost_Action;
        private System.Action<RequestException, ResponseHelper, Post> petInfo_Action;
        private System.Action<RequestException, ResponseHelper, Post> sets_Action;
        private System.Action<RequestException, ResponseHelper, Post> sideEvents_Action;
        private System.Action<RequestException, ResponseHelper, Post> faq_Action;
        private System.Action<RequestException, ResponseHelper, Post> changePassword_Action;
        private System.Action<RequestException, ResponseHelper, Post> getProfile_Action;
        private System.Action<RequestException, ResponseHelper, Post> updateProfile_Action;
        private System.Action<RequestException, ResponseHelper, Post> contactUs_Action;
        private System.Action<RequestException, ResponseHelper, Post> spinning_Action;
        private System.Action<RequestException, ResponseHelper, Post> rewardList_Action;
        private System.Action<RequestException, ResponseHelper, Post> rewardEvent_Action;
        private System.Action<RequestException, ResponseHelper, Post> viking_Action;
        private System.Action<RequestException, ResponseHelper, Post> introduction_Action;
        private System.Action<RequestException, ResponseHelper, Post> petDetails_Action;
        private System.Action<RequestException, ResponseHelper, Post> terms_Action;
        private System.Action<RequestException, ResponseHelper, Post> getAllSubscription_Action;
        private System.Action<RequestException, ResponseHelper, Post> checkSubscribe_Action;
        private System.Action<RequestException, ResponseHelper, Post> promoCode_Action;
        private System.Action<RequestException, ResponseHelper, Post> inclusion_Action;
        private System.Action<RequestException, ResponseHelper, Post> facebookLogin_Action;
        private System.Action<RequestException, ResponseHelper, Post> subSet_Action;
        private System.Action<RequestException, ResponseHelper, Post> subscription_Action;
        private System.Action<RequestException, ResponseHelper, Post> updateNotification_Action;
        private System.Action<RequestException, ResponseHelper, Post> subcriptionDetails_Action;
        private System.Action<RequestException, ResponseHelper, Post> subcriptionDetailsLogin_Action;
        private System.Action<RequestException, ResponseHelper, Post> subcriptionDetailsHome_Action;
        private System.Action<RequestException, ResponseHelper, Post> subcriptionUpdateDetails_Action;
        private System.Action<RequestException, ResponseHelper, Post> uploadImage_Action;
        private System.Action<RequestException, ResponseHelper, Post> logout_Action;
        private System.Action<RequestException, ResponseHelper, Post> askExpert_Action;
        private System.Action<RequestException, ResponseHelper, Post> eventList_Action;
        private System.Action<RequestException, ResponseHelper, Post> emailValidation_Action;

        #endregion

        #region Built in methods
        private void Awake()
        {
            apiData = new APIData();
        }
        private void Start()
        {
            
            user_Login_Action += ResponseForAPIUserLogin;
            user_ForgotPassword_Action += ResponseForAPIUserForgotPassword;
            user_Register_Action += ResponseForAPIUserRegister;
            villageCost_Action += ResponseForAPIVillageCost;
            chestCost_Action += ResponseForAPIChestCost;
            petInfo_Action += ResponseForAPIPetInfo;
            sets_Action += ResponseForAPISets;
            sideEvents_Action += ResponseForAPISideEvents;
            faq_Action += ResponseForAPIFAQ;
            changePassword_Action += ResponseForAPIChangePassword;
            getProfile_Action += ResponseForAPIGetProfile;
            updateProfile_Action += ResponseForAPIUpdateProfile;
            contactUs_Action += ResponseForAPIContactUS;
            spinning_Action += ResponseForAPISpinning;
            rewardList_Action += ResponseForAPIRewardList;
            rewardEvent_Action += ResponseForAPIRewardEvent;
            viking_Action += ResponseForAPIViking;
            introduction_Action += ResponseForAPIIntroduction;
            petDetails_Action += ResponseForAPIPetDetails;
            terms_Action += ResponseForAPITermsAndCondition;
            getAllSubscription_Action += ResponseForAPIGetAllSubscription;
            inclusion_Action += ResponseForAPIinclusion;
            facebookLogin_Action += ResponseForAPIfacebookLogin;
            checkSubscribe_Action += ResponseForAPINotificationCheck;
            subSet_Action += ResponseForAPISubSets;
            promoCode_Action += ResponseForAPIPromoCode;
            subscription_Action += ResponseForAPISubscriptionCode;
            updateNotification_Action += ResponseForAPIUpdateNotification;
            subcriptionDetails_Action += ResponseForAPISubcriptionDetails;
            subcriptionDetailsLogin_Action += ResponseForAPISubcriptionDetailsLogin;
            subcriptionDetailsHome_Action += ResponseForAPISubcriptionDetailsHomeFirst;
            subcriptionUpdateDetails_Action += ResponseForAPIUpdateSubcriptionDetails;
            logout_Action += ResponseForLogout;
            askExpert_Action += ResponseAPIForAskExperts;
            eventList_Action += ResponseForAPIRewardEventList;
            emailValidation_Action += ResponseForAPIAPIEmailValidation;
        }

        #endregion

        #region Property methods

        public APIData APIData
        {
            get => apiData;
        }

        #endregion

        #region API URLs

        //string api_Base_Url = "https://api.tohrlimited.com:3002/tohr/api/v1/";
        string api_Base_Url = "http://54.88.23.250:3002/tohr/api/v1/";
        string user_Login_Url = "users/login_via_email";
        //string user_Register_Url = "users/register_via_email";
        string user_Register_Url = "users/register_email";
        string forgot_password_Url = "users/forgot_password";
        string villageCost_Url = "admin/village/get_all_village";
        string chestCost_Url = "admin/chest/get_all_chest";
        string petInfo_Url = "admin/pet/get_all_pet";
        string petDetail_Url = "admin/pet_level/get_pet_level";
        string sets_Url = "admin/set/get_all_sets";
        string sideEvents_Url = "admin/side_event/get_all_side_event";
        string faq_Url = "admin/faq/get_all_faq";
        string changePassword_Url = "users/change_password";
        string getProfile_Url = "users/get_player_profile";
        string updateProfile_Url = "users/update_my_profile";
        string contactUs_Url = "contact_us/create_contact_us";
        string spinning_Url = "admin/main_event/get_all_main_event";
        string rewardList_Url = "admin/reward_list/get_reward_list_by_id";
        string rewardEvent_Url = "admin/reward_event/get_reward_event_name";
        string viking_Url = "admin/viking_event/get_all_viking_event";
        string introduction_Url = "admin/introduction/get_all_introduction";
        string terms_Url = "admin/terms_and_conditions/get_all_terms_and_conditions";
        string getAllSubscription_Url = "admin/subscription/get_all_subscription";
        string inclusion_Url = "admin/push_notification/get_all_inclusion";
        string facebookLogin_Url = "users/login_via_social";
        string checkSubscribe_Url = "transaction/get_user_transaction";
        string promoCode_Url = "subscription/create_promo_code_subscription";
        string subSet_Url = "admin/sub_set/get_sub_set_by_set_id";
        string subscription_Url = "subscription/create_subscription";
        string updateNotificaatio_Url = "users/update_notification";
        string subcriptionDetails_Url = "subscription/get_subscription_detail";
        string subcriptionUpdateDetails_Url = "subscription/update_subscription_detail";
        string uploadImage_Url = "users/upload_profile_image";
        string logout_Url = "users/log_out_user";
        string askExpert_Url = "contact_us/ask_to_expert";
        string eventList_Url = "admin/reward_event/get_reward_event_details";
        string emailValidation_Url = "users/get_user_subsciption_details";

        #endregion


        #region Upload Document API

        public void APIUploadDocument(byte[] data, string fileName,string screen)
        {
            screenName = screen;
            try
            {
                string url = api_Base_Url + uploadImage_Url;

                HTTPRequest request = new HTTPRequest(new Uri(url), BestHTTP.HTTPMethods.Post, ResponseForAPIUploadDocument);
                request.IsKeepAlive = false;
                request.Timeout = TimeSpan.FromSeconds(600);//10 minutes

                request.AddBinaryData("profile_image", data, fileName);
                print("iddd :"+apiData.myProfile[0].player_id.ToString());
                request.AddField("player_id", apiData.myProfile[0].player_id.ToString());
                request.OnUploadProgress = OnUploadProgress;
                request.Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIUploadDocument(HTTPRequest request, HTTPResponse response)
        {
            print("callback");
            switch (request.State)
            {

                // The request finished without any problem.
                case HTTPRequestStates.Finished:
                    print("finish");
                    if (response.IsSuccess)
                    {
                        print("success");
                        var jsonObject = (JObject)JsonConvert.DeserializeObject(response.DataAsText);
                        print("codee :" + jsonObject["response_code"].Value<int>());
                        if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                        {
                           
                            var response_body = jsonObject["response_body"].Value<JArray>();
                            Debug.Log(response_body);
                            apiData.myProfile = response_body.ToObject<List<APIData.MyProfile>>();
                            if (screenName == "home")
                            {
                                uiManager.homeScreen.transform.GetComponent<HomeScreen>().UpdateImageCallBack();
                            }
                            else
                            {
                                uiManager.settingScreen.transform.GetComponent<SettingScreen>().OnUploadImageCallBack();
                            }
                            
                        }
                        else
                        {
                            //errorMessageTextField.text = "Server is not responding now. Please try again";
                            if (screenName == "home")
                            {
                                uiManager.homeScreen.transform.GetComponent<HomeScreen>().UpdateImageCallBack();
                            }
                            else
                            {
                                uiManager.settingScreen.transform.GetComponent<SettingScreen>().OnUploadImageFailure();
                            }
                            print("Server is not responding now. Please try again");
                        }
                    }
                    else
                    {
                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                        if (screenName == "home")
                        {
                            uiManager.homeScreen.transform.GetComponent<HomeScreen>().UpdateImageCallBack();
                        }
                        else
                        {
                            uiManager.settingScreen.transform.GetComponent<SettingScreen>().OnUploadImageFailure();
                        }
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));
                    //APIManager.Instance().ServerDownPopup();
                    uiManager.settingScreen.transform.GetComponent<SettingScreen>().OnUploadImageFailure();
                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:
                    //APIManager.Instance().ServerDownPopup();
                    Debug.LogWarning("Request Aborted!");
                    uiManager.settingScreen.transform.GetComponent<SettingScreen>().OnUploadImageFailure();
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:
                    //APIManager.Instance().ServerDownPopup();
                    Debug.LogError("Connection Timed Out!");
                    uiManager.settingScreen.transform.GetComponent<SettingScreen>().OnUploadImageFailure();
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:
                    //APIManager.Instance().ServerDownPopup();
                    Debug.LogError("Processing the request Timed Out!");
                    uiManager.settingScreen.transform.GetComponent<SettingScreen>().OnUploadImageFailure();
                    break;
            }

        }
        void OnUploadProgress(HTTPRequest request, long uploaded, long length)
        {
            float progressPercent = (uploaded / (float)length) * 100.0f;
            Debug.Log(progressPercent);

            Debug.Log("Uploading IPA file : " + progressPercent.ToString("F2") + "%");
            print("Uploading Table file : " + progressPercent.ToString("F2") + "%");
        }

        #endregion

        #region Download Image

        public void APIDownloadImage(string url)
        {
            try
            {
                HTTPRequest request = new HTTPRequest(new Uri(url), (request, response) =>
                {
                    var tex = new Texture2D(0, 0);
                    tex.LoadImage(response.Data);
                    uiManager.settingScreen.transform.GetComponent<SettingScreen>().OnImageDownloadApiCallBack(tex);
                }).Send();
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        void ResponseForAPIDownloadImage(HTTPRequest request, HTTPResponse response)
        {
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:

                    if (response.IsSuccess)
                    {
                        var tex = new Texture2D(0, 0);
                        tex.LoadImage(response.Data);
                        uiManager.settingScreen.transform.GetComponent<SettingScreen>().OnImageDownloadApiCallBack(tex);
                    }
                    else
                    {
                        uiManager.settingScreen.transform.GetComponent<SettingScreen>().OnUploadImageFailure();
                        Debug.LogWarning(string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        response.StatusCode,
                                                        response.Message,
                                                        response.DataAsText));
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:

                    Debug.LogError("Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception"));
                    uiManager.settingScreen.transform.GetComponent<SettingScreen>().OnUploadImageFailure();
                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:
                    uiManager.settingScreen.transform.GetComponent<SettingScreen>().OnUploadImageFailure();
                    Debug.LogWarning("Request Aborted!");
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:
                    uiManager.settingScreen.transform.GetComponent<SettingScreen>().OnUploadImageFailure();
                    Debug.LogError("Connection Timed Out!");
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:
                    uiManager.settingScreen.transform.GetComponent<SettingScreen>().OnUploadImageFailure();
                    Debug.LogError("Processing the request Timed Out!");
                    break;
            }

        }

        #endregion

        #region API methods and callbacks


        // user login API
        public void APIUserLogin(string email, string password)
        {
            print("api token :"+PlayerPrefs.GetString("token"));
            // We can add default query string params for all requests
            string finalToken = apiData.game_Details.device_token;
            string token = PlayerPrefs.GetString("token");
            if (token.Length > 0)
            {
                finalToken = token;
            }

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + user_Login_Url,
                Params = new Dictionary<string, string> {
                },
                Body = new UserLogin
                {
                    email = email,
                    password = password,
                    device_name = apiData.game_Details.device_name,
                    //device_token = PlayerPrefs.GetString("token"),
                    device_token = finalToken,
                    is_admin = apiData.game_Details.is_admin
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, user_Login_Action);
        }

        void ResponseForAPIUserLogin(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null&& response.Text!="")
            {
                print("this12 : " + response.Text);
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                print("this12 : " + response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JObject>();
                        var user_details = response_body["user_details"].Value<JArray>();
                        var external_link = response_body["external_link"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        apiData.myProfile = user_details.ToObject<List<APIData.MyProfile>>();
                        apiData.external_link = external_link.ToObject<List<APIData.ExternaLink>>();

                        //apiData.userDetails = response_body.ToObject<APIData.UserDetails>();
                        //apiData.myProfile = response_body.ToObject<APIData.MyProfile>();
                        uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        errorMessage = "You have entered an incorrect email address or password.  Please try again.";
                        uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack(errorMessage);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                   
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.connectionFailedScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }



        }

        // user login API
        public void APIFacebookLogin(string email, string firstName,string lastName ,string id,string mode)
        {
            // We can add default query string params for all requests
            string loginMode = mode;
            if (loginMode.Length == 0 || loginMode == null)
            {
                loginMode = "Facebook";
            }
            string finalToken = apiData.game_Details.device_token;
            string token = PlayerPrefs.GetString("token");
            if (token.Length > 0)
            {
                finalToken = token;
            }
            if (email.Length == 0 || email == null || email == "")
            {
                email = "";
            }
            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + facebookLogin_Url,
                Params = new Dictionary<string, string> {
                },
                Body = new FacebookLogin
                {
                    first_name = firstName,
                    sur_name = lastName,
                    email = email,
                    social_id = id,
                    player_login_mode = loginMode,
                    game_status = "Non-VIP",
                    device_name = apiData.game_Details.device_name,
                    //device_token = PlayerPrefs.GetString("token"),
                    device_token = finalToken,
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, facebookLogin_Action);
        }

        void ResponseForAPIfacebookLogin(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JObject>();
                        var user_details = response_body["user_details"].Value<JArray>();
                        var external_link = response_body["external_link"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        apiData.myProfile = user_details.ToObject<List<APIData.MyProfile>>();
                        apiData.external_link = external_link.ToObject<List<APIData.ExternaLink>>();
                        //apiData.userDetails = response_body.ToObject<APIData.UserDetails>();
                        //apiData.myProfile = response_body.ToObject<APIData.MyProfile>();
                        uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        errorMessage = jsonObject["response_message"].Value<string>();
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack(errorMessage);
                        uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack(errorMessage);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                }
            }
            else
            {
                uiManager.connectionFailedScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }



        }

        // Forgot password
        public void APIUserForgotPassword(string email)
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + forgot_password_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new ForgotPassword
                {
                    email = email,

                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, user_ForgotPassword_Action);
        }

        void ResponseForAPIUserForgotPassword(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JObject>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        uiManager.loginScreen.transform.GetComponent<LoginScreen>().ForgotPasswordSubmitButtonCallBack();
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        errorMessage = "The mail id is not registerd with us. Please check";
                        uiManager.loginScreen.transform.GetComponent<LoginScreen>().ForgotPasswordSubmitButtonCallBack(errorMessage);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                }

            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }


        }

        // Email validation
        public void APIEmailValidation()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + emailValidation_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new EmailValidation
                {
                   player_id= apiData.myProfile[0].player_id.ToString(),

                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, emailValidation_Action);
        }

        void ResponseForAPIAPIEmailValidation(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        uiManager.homeScreen.transform.GetComponent<HomeScreen>().OnEmailRegisterWithLinkApiCallBack(response_body[0]["is_actived"].Value<int>(),response_body[0]["paid_user"].Value<int>(), response_body[0]["notification"].Value<int>());
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().ForgotPasswordSubmitButtonCallBack(errorMessage);
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                }

            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }


        }

        // user register API
        public void APIUserRegister(string firstName, string surName, string email, string password, string villageNo, string gameStatus,string referCode)
        {
            print("gameStatus" + gameStatus);
            print("referCode" + referCode);
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + user_Register_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new UserRegister
                {
                    first_name = firstName,
                    sur_name = surName,
                    email = email,
                    password = password,
                    village_number = int.Parse(villageNo),
                    game_status = gameStatus,
                    device_name = apiData.game_Details.device_name,
                    //device_token = PlayerPrefs.GetString("token"),
                    device_token = apiData.game_Details.device_token,
                    reference_no = referCode,
                    version_no= apiData.game_Details.version_no
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, user_Register_Action);
        }

        void ResponseForAPIUserRegister(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        //var user_details = response_body["user_details"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        //apiData.myProfile = user_details.ToObject<List<APIData.MyProfile>>();
                        //apiData.myProfile = response_body.arr<APIData.UserDetails>();
                        uiManager.loginScreen.transform.GetComponent<LoginScreen>().OnRegisterButtonCallBack();
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        //errorMessage = "You have entered an incorrect email address or password.  Please try again.";
                        errorMessage = jsonObject["response_message"].Value<string>();
                        uiManager.loginScreen.transform.GetComponent<LoginScreen>().OnRegisterButtonCallBack(errorMessage);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }



        }

        // user Get Profile API
        public void APIGetProfile()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + getProfile_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new GetProfile
                {
                    player_id = apiData.myProfile[0].player_id,
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, getProfile_Action);
        }

        void ResponseForAPIGetProfile(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JObject>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        apiData.playerData = response_body["player_data"].ToObject<APIData.PlayerData>();
                        //apiData.myProfile = response_body.ToObject<APIData.MyProfile>();
                        uiManager.settingScreen.transform.GetComponent<SettingScreen>().OnMyProfileAPICallBack();
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        errorMessage = "You have entered an incorrect email address or password.  Please try again.";
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack(errorMessage);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.loadingScreen.SetActive(false);
                uiManager.noDataScreen.SetActive(true);
               
            }


        }

        // user Get Profile API
        public void APIUpdateProfile(string firstName, string surName, string villageNumber, string gameStatus)
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + updateProfile_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new ProfileUpdate
                {
                    player_id = apiData.myProfile[0].player_id,
                    first_name = firstName,
                    sur_name = surName,
                    email = apiData.myProfile[0].email,
                    village_number = villageNumber,
                    game_status = gameStatus
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, updateProfile_Action);
        }

        void ResponseForAPIUpdateProfile(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        //apiData.playerData = response_body["player_data"].ToObject<APIData.PlayerData>();
                        //apiData.myProfile = response_body.ToObject<APIData.MyProfile>();
                        uiManager.settingScreen.transform.GetComponent<SettingScreen>().UpdateProfileCallBack();
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        errorMessage = jsonObject["response_message"].Value<string>();
                        uiManager.settingScreen.transform.GetComponent<SettingScreen>().UpdateProfileCallBack(errorMessage);
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack(errorMessage);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }


        }

        // user Change password API
        public void APIChangePassword(string oldPassword, string newPassword)
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + changePassword_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new ChangePassword
                {
                    player_id = apiData.myProfile[0].player_id,
                    old_password = oldPassword,
                    new_password = newPassword
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, changePassword_Action);
        }

        void ResponseForAPIChangePassword(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        //apiData.userDetails = response_body.ToObject<APIData.UserDetails>();
                        //apiData.myProfile = response_body.ToObject<APIData.MyProfile>();
                        uiManager.settingScreen.transform.GetComponent<SettingScreen>().OnChangePasswordSubmitButtonCallBack();
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        errorMessage = jsonObject["response_message"].Value<string>();
                        uiManager.settingScreen.transform.GetComponent<SettingScreen>().OnChangePasswordSubmitButtonCallBack(errorMessage);
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack(errorMessage);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }

            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }


        }

        // user Contact us API
        public void APIContactUs(string notes, string gameStatus, string villageNo,string email)
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + contactUs_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new ContactUs
                {
                    village_no = int.Parse(villageNo),
                    email = email,
                    note = notes,
                    game_status = gameStatus
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, contactUs_Action);
        }

        void ResponseForAPIContactUS(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        //var response_body = jsonObject["response_body"].Value<JObject>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        //Debug.Log(response_body);
                        //apiData.userDetails = response_body.ToObject<APIData.UserDetails>();
                        //apiData.myProfile = response_body.ToObject<APIData.MyProfile>();
                        uiManager.settingScreen.transform.GetComponent<SettingScreen>().OnContactAPICallBack();
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        errorMessage = jsonObject["response_message"].Value<string>();
                        uiManager.settingScreen.transform.GetComponent<SettingScreen>().OnContactAPICallBack(errorMessage);
                        //uiManager.noDataScreen.SetActive(true);
                        //uiManager.loadingScreen.SetActive(false);
                        //uiManager.settingScreen.transform.GetComponent<SettingScreen>().OnChangePasswordSubmitButtonCallBack(errorMessage);
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack(errorMessage);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }



        }

        // village cost API
        public void APIVillageCost()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + villageCost_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new VillageCost
                {

                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, villageCost_Action);
        }

        void ResponseForAPIVillageCost(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        List<APIData.VillageCost> villageList = response_body.ToObject<List<APIData.VillageCost>>();
                        uiManager.basicInfoScreen.transform.GetComponent<BasicInfoScreen>().OnVillageCostButtonCallBack(villageList);
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }



        }

        // Chest cost API
        public void APIChestCost()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + chestCost_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new VillageCost
                {

                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, chestCost_Action);
        }

        void ResponseForAPIChestCost(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        List<APIData.ChestCost> villageList = response_body.ToObject<List<APIData.ChestCost>>();
                        //apiData.userDetails = response_body.ToObject<APIData.UserDetails>();
                        uiManager.basicInfoScreen.transform.GetComponent<BasicInfoScreen>().OnChestCostButtonCallBack(villageList);
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }

                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }



        }

        // Pet info cost API
        public void APIPetInfo()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + petInfo_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new VillageCost
                {

                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, petInfo_Action);
        }

        void ResponseForAPIPetInfo(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text!=null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        List<APIData.PetInfo> petInfo = response_body.ToObject<List<APIData.PetInfo>>();
                        //apiData.userDetails = response_body.ToObject<APIData.UserDetails>();
                        uiManager.basicInfoScreen.transform.GetComponent<BasicInfoScreen>().PetInfoCallBack(petInfo);
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }


        }

        // Pet info cost API
        public void APIPetDetails(int petId)
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + petDetail_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new PetDetails
                {
                    pet_id = petId
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, petDetails_Action);
        }

        void ResponseForAPIPetDetails(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        List<APIData.PetDetails> petInfo = response_body.ToObject<List<APIData.PetDetails>>();
                        //apiData.userDetails = response_body.ToObject<APIData.UserDetails>();
                        uiManager.basicInfoScreen.transform.GetComponent<BasicInfoScreen>().PetInfoAPICallBack(petInfo);
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }


        }

        // Sets API
        public void APISets()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + sets_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new VillageCost
                {

                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, sets_Action);
        }

        void ResponseForAPISets(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        List<APIData.Sets> sets = response_body.ToObject<List<APIData.Sets>>();
                        //apiData.userDetails = response_body.ToObject<APIData.UserDetails>();
                        uiManager.basicInfoScreen.transform.GetComponent<BasicInfoScreen>().OnSetButtonCallBack(sets);
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }


        }

        // side events API
        public void APISideEvents()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + sideEvents_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new VillageCost
                {
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, sideEvents_Action);
        }

        void ResponseForAPISideEvents(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        List<APIData.SideEvents> spinningEvents = response_body.ToObject<List<APIData.SideEvents>>();
                        //apiData.userDetails = response_body.ToObject<APIData.UserDetails>();
                        uiManager.basicInfoScreen.transform.GetComponent<BasicInfoScreen>().OnSideEventButtonCallBack(spinningEvents);
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                        print(errorMessage);
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }


        }

        // FAQ API
        public void APIFAQ()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + faq_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new VillageCost
                {
                    
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, faq_Action);
        }

        void ResponseForAPIFAQ(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        List<APIData.FAQ> faq = response_body.ToObject<List<APIData.FAQ>>();
                        //apiData.userDetails = response_body.ToObject<APIData.UserDetails>();
                        uiManager.faqScreen.transform.GetComponent<FAQScreenParent>().OnFaqApiCallBack(faq);
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }


        }

        // Spinning API
        public void APISpinning()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + spinning_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new VillageCost
                {

                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, spinning_Action);
        }

        void ResponseForAPISpinning(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        List<APIData.SpinningEvent> spinning = response_body.ToObject<List<APIData.SpinningEvent>>();
                        uiManager.spinningPatternAndRewardScreen.transform.GetComponent<SpinningPatternAndRewardListScreen>().SpinningAPICallBack(spinning);
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }
           


        }

        // Reward list API
        public void APIRewardList(int rewardId)
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + rewardList_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new RewardList
                {
                    reward_list_id = rewardId
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, rewardList_Action);
        }

        void ResponseForAPIRewardList(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        List<APIData.RewardList> spinning = response_body.ToObject<List<APIData.RewardList>>();
                        uiManager.spinningPatternAndRewardScreen.transform.GetComponent<SpinningPatternAndRewardListScreen>().RewardListApiCallBack(spinning);
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }


        }

        // Reward event API
        public void APIRewardEvent()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + rewardEvent_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new VillageCost
                {

                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, rewardEvent_Action);
        }

        void ResponseForAPIRewardEvent(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        List<APIData.EventList> spinning = response_body.ToObject<List<APIData.EventList>>();
                        uiManager.spinningPatternAndRewardScreen.transform.GetComponent<SpinningPatternAndRewardListScreen>().RewardEventAPICallBack(spinning);
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                        print(errorMessage);
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }


        }

        // Reward event API
        public void APIRewardEventList(string name)
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + eventList_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new EventList
                {
                    event_name=name
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, eventList_Action);
        }

        void ResponseForAPIRewardEventList(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        List<APIData.RewardEvent> spinning = response_body.ToObject<List<APIData.RewardEvent>>();
                        uiManager.spinningPatternAndRewardScreen.transform.GetComponent<SpinningPatternAndRewardListScreen>().EventListAPiCallBack(spinning);
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                        print(errorMessage);
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }


        }


        // Viking API
        public void APIViking()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + viking_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new VillageCost
                {

                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, viking_Action);
        }

        void ResponseForAPIViking(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        List<APIData.Vikings> spinning = response_body.ToObject<List<APIData.Vikings>>();
                        uiManager.vikingScreen.transform.GetComponent<VikingScreen>().VikingAPICallBAck(spinning);
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }

            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }


        }
        
        // Intoduction API
        public void APIIntroduction()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + introduction_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new VillageCost
                {

                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, introduction_Action);
        }

        void ResponseForAPIIntroduction(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        List<APIData.Intoduction> spinning = response_body.ToObject<List<APIData.Intoduction>>();
                        uiManager.homeScreen.transform.GetComponent<HomeScreen>().IntoCallBack(spinning);
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }


        }

        // Terms API
        public void APITermsAndCondition()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + terms_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new VillageCost
                {

                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, terms_Action);
        }

        void ResponseForAPITermsAndCondition(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        List<APIData.Terms> spinning = response_body.ToObject<List<APIData.Terms>>();
                        uiManager.loginScreen.transform.GetComponent<LoginScreen>().TermsCallBack(spinning);
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }


        }

        // Get all subscription API
        public void APIGetAllSubscription()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + getAllSubscription_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new VillageCost
                {

                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, getAllSubscription_Action);
        }

        void ResponseForAPIGetAllSubscription(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
            Debug.Log(jsonObject);
            if (request == null)
            {
                if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                {
                    var response_body = jsonObject["response_body"].Value<JArray>();
                    errorMessage = jsonObject["response_message"].Value<string>();
                    print(errorMessage);
                    Debug.Log(response_body);
                    List<APIData.GetAllSubscription> spinning = response_body.ToObject<List<APIData.GetAllSubscription>>();
                    uiManager.homeScreen.transform.GetComponent<HomeScreen>().GetAllSubscribeAPiCAllBack(spinning);
                }
                else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                {
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                    print(errorMessage);
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                }
            }
            else
            {
                uiManager.connectionFailedScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
                //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
            }


        }
        
        // Inclusion API
        public void APIinclusion()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + inclusion_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new VillageCost
                {

                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, inclusion_Action);
        }

        void ResponseForAPIinclusion(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        List<APIData.Inclusion> spinning = response_body.ToObject<List<APIData.Inclusion>>();
                        uiManager.homeScreen.transform.GetComponent<HomeScreen>().InclusionAPICallBack(spinning);
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }


        }

         // Check notification API
        public void APINoticicationCheck()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + checkSubscribe_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new CheckSubcribe
                {
                    player_id=apiData.myProfile[0].player_id
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, checkSubscribe_Action);
        }

        void ResponseForAPINotificationCheck(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        List<APIData.CheckSubscribe> spinning = response_body.ToObject<List<APIData.CheckSubscribe>>();
                        uiManager.homeScreen.transform.GetComponent<HomeScreen>().CheckNotificationCallBack();
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        errorMessage = jsonObject["response_message"].Value<string>();
                        uiManager.homeScreen.transform.GetComponent<HomeScreen>().CheckNotificationCallBack(errorMessage);
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            


        }

        // Check notification API
        public void APISubSets(int setId)
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + subSet_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new SubSet
                {
                    set_id = setId
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, subSet_Action);
        }

        void ResponseForAPISubSets(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != null && response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        List<APIData.SubSet> spinning = response_body.ToObject<List<APIData.SubSet>>();
                        uiManager.basicInfoScreen.transform.GetComponent<BasicInfoScreen>().SubSetCallBack(spinning);
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        errorMessage = jsonObject["response_message"].Value<string>();
                        //uiManager.homeScreen.transform.GetComponent<HomeScreen>().CheckNotificationCallBack(errorMessage);
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }


        }
        // Check Promo code API
        public void APIPromoCode(string promoCode)
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + promoCode_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new promoCode
                {
                    player_id = apiData.myProfile[0].player_id,
                    promo_code=promoCode
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, promoCode_Action);
        }

        void ResponseForAPIPromoCode(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
            Debug.Log(jsonObject);
            if (request == null)
            {
                if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                {
                    var response_body = jsonObject["response_body"].Value<JArray>();
                    errorMessage = jsonObject["response_message"].Value<string>();
                    print(errorMessage);
                    Debug.Log(response_body);
                    //List<APIData.SubSet> spinning = response_body.ToObject<List<APIData.SubSet>>();
                    uiManager.homeScreen.transform.GetComponent<HomeScreen>().PromoCodeAPICallBack(errorMessage);
                }
                else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                {
                    errorMessage = jsonObject["response_message"].Value<string>();
                    uiManager.homeScreen.transform.GetComponent<HomeScreen>().PromoCodeAPICallBack(errorMessage);
                    //uiManager.homeScreen.transform.GetComponent<HomeScreen>().CheckNotificationCallBack(errorMessage);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                    
                    print(errorMessage);
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                }
            }
            else
            {
                uiManager.connectionFailedScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
                //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
            }


        }   
        // Check Subscription API
        public void APISubscriptionCode(string planName,float price,string paymentId)
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + subscription_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new Subscription
                {
                    player_id = apiData.myProfile[0].player_id,
                    plan_name = planName,
                    plan_price =price,
                    payment_id= paymentId
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, subscription_Action);
        }

        void ResponseForAPISubscriptionCode(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
            Debug.Log(jsonObject);
            if (request == null)
            {
                if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                {
                    var response_body = jsonObject["response_body"].Value<JObject>();
                    errorMessage = jsonObject["response_message"].Value<string>();
                    print(errorMessage);
                    Debug.Log(response_body);
                    //List<APIData.SubSet> spinning = response_body.ToObject<List<APIData.SubSet>>();
                    uiManager.homeScreen.transform.GetComponent<HomeScreen>().CreateSubscriptionCallback();
                }
                else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                {
                    errorMessage = jsonObject["response_message"].Value<string>();
                    //uiManager.homeScreen.transform.GetComponent<HomeScreen>().PromoCodeAPICallBack(errorMessage);
                    //uiManager.homeScreen.transform.GetComponent<HomeScreen>().CheckNotificationCallBack(errorMessage);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();
                    
                    print(errorMessage);
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                }
            }
            else
            {
                uiManager.connectionFailedScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
                //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
            }


        }

        // Check Update Notification API
        public void APIUpdateNotification(bool condition)
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + updateNotificaatio_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new UpdateNotification
                {
                    player_id = apiData.myProfile[0].player_id,
                    notification = condition
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, updateNotification_Action);
        }

        void ResponseForAPIUpdateNotification(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text!=null&&response.Text != "")
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        //apiData.playerData = response_body["player_data"].ToObject<APIData.PlayerData>();
                        if (apiData.myProfile[0].notification == 1)
                        {
                            apiData.myProfile[0].notification = 0;
                        }
                        else
                        {
                            apiData.myProfile[0].notification = 1;
                        }
                        //apiData.myProfile[0].notification = apiData.playerData.notification;
                        //List<APIData.SubSet> spinning = response_body.ToObject<List<APIData.SubSet>>();
                        uiManager.homeScreen.transform.GetComponent<HomeScreen>().UpdateNotificatiionApiCallBack();
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        errorMessage = jsonObject["response_message"].Value<string>();
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                        //uiManager.homeScreen.transform.GetComponent<HomeScreen>().UpdateNotificatiionApiCallBack(errorMessage);
                        //uiManager.homeScreen.transform.GetComponent<HomeScreen>().CheckNotificationCallBack(errorMessage);
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();

                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }
   


        }

        // Check Update Notification API
        public void APISubcriptionDetails()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + subcriptionDetails_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new SuncribeDetail
                {
                    player_id = apiData.myProfile[0].player_id
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, subcriptionDetails_Action);
        }

        void ResponseForAPISubcriptionDetails(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != ""&& response.Text!=null)
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        //apiData.playerData = response_body["player_data"].ToObject<APIData.PlayerData>();

                        //apiData.myProfile[0].notification = apiData.playerData.notification;
                        //List<APIData.SubSet> spinning = response_body.ToObject<List<APIData.SubSet>>();
                        apiData.subcriptionDetails = response_body.ToObject<List<APIData.CheckSubscribe>>();
                        uiManager.homeScreen.transform.GetComponent<HomeScreen>().OnGetSubscriptionDetailsCallback(response_body[0]["plan_name"].Value<String>(),
                                response_body[0]["paid_user"].Value<int>(), response_body[0]["payment_id"].Value<string>());
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        errorMessage = jsonObject["response_message"].Value<string>();
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                        //uiManager.homeScreen.transform.GetComponent<HomeScreen>().UpdateNotificatiionApiCallBack(errorMessage);
                        //uiManager.homeScreen.transform.GetComponent<HomeScreen>().CheckNotificationCallBack(errorMessage);
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();

                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }



        }

        // Check Update Notification API
        public void APISubcriptionDetailsLogin()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + subcriptionDetails_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new SuncribeDetail
                {
                    player_id = apiData.myProfile[0].player_id
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, subcriptionDetailsLogin_Action);
        }

        void ResponseForAPISubcriptionDetailsLogin(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != "" && response.Text != null)
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        //apiData.playerData = response_body["player_data"].ToObject<APIData.PlayerData>();

                        //apiData.myProfile[0].notification = apiData.playerData.notification;
                        //List<APIData.SubSet> spinning = response_body.ToObject<List<APIData.SubSet>>();
                        apiData.subcriptionDetails = response_body.ToObject<List<APIData.CheckSubscribe>>();
                        uiManager.loginScreen.transform.GetComponent<LoginScreen>().OnGetSubscriptionDetailsCallback(response_body[0]["plan_name"].Value<String>(),
                                response_body[0]["paid_user"].Value<int>(), response_body[0]["payment_id"].Value<string>());
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        errorMessage = jsonObject["response_message"].Value<string>();
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                        //uiManager.homeScreen.transform.GetComponent<HomeScreen>().UpdateNotificatiionApiCallBack(errorMessage);
                        //uiManager.homeScreen.transform.GetComponent<HomeScreen>().CheckNotificationCallBack(errorMessage);
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();

                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }



        }

        // Check Update Notification API
        public void APISubcriptionDetailsHomeFirst()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + subcriptionDetails_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new SuncribeDetail
                {
                    player_id = apiData.myProfile[0].player_id
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, subcriptionDetailsHome_Action);
        }

        void ResponseForAPISubcriptionDetailsHomeFirst(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != "" && response.Text != null)
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        //apiData.playerData = response_body["player_data"].ToObject<APIData.PlayerData>();

                        //apiData.myProfile[0].notification = apiData.playerData.notification;
                        //List<APIData.SubSet> spinning = response_body.ToObject<List<APIData.SubSet>>();
                        apiData.subcriptionDetails = response_body.ToObject<List<APIData.CheckSubscribe>>();
                        uiManager.homeScreen.transform.GetComponent<HomeScreen>().OnGetSubscriptionDetailsFirstCallback(response_body[0]["plan_name"].Value<String>(),
                                response_body[0]["paid_user"].Value<int>(), response_body[0]["payment_id"].Value<string>());
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        errorMessage = jsonObject["response_message"].Value<string>();
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                        //uiManager.homeScreen.transform.GetComponent<HomeScreen>().UpdateNotificatiionApiCallBack(errorMessage);
                        //uiManager.homeScreen.transform.GetComponent<HomeScreen>().CheckNotificationCallBack(errorMessage);
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();

                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }



        }

        // Check Update Notification API
        public void APIUpdateSubcriptionDetails()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + subcriptionUpdateDetails_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new SuncribeDetail
                {
                    player_id = apiData.myProfile[0].player_id
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, subcriptionUpdateDetails_Action);
        }

        void ResponseForAPIUpdateSubcriptionDetails(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != "" && response.Text != null)
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        Debug.Log(response_body);
                        apiData.myProfile = response_body.ToObject<List<APIData.MyProfile>>();
                        //apiData.playerData = response_body["player_data"].ToObject<APIData.PlayerData>();

                        //apiData.myProfile[0].notification = apiData.playerData.notification;
                        //List<APIData.SubSet> spinning = response_body.ToObject<List<APIData.SubSet>>();
                        //uiManager.homeScreen.transform.GetComponent<HomeScreen>().OnGetSubscriptionDetailsCallback(response_body[0]["plan_name"].Value<String>(),
                        //response_body[0]["paid_user"].Value<int>(), response_body[0]["payment_id"].Value<string>());
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        errorMessage = jsonObject["response_message"].Value<string>();
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                        //uiManager.homeScreen.transform.GetComponent<HomeScreen>().UpdateNotificatiionApiCallBack(errorMessage);
                        //uiManager.homeScreen.transform.GetComponent<HomeScreen>().CheckNotificationCallBack(errorMessage);
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();

                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }



        }


        // Logout API
        public void APILogout()
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + logout_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new Logout
                {
                    player_id = apiData.myProfile[0].player_id
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, logout_Action);
        }

        void ResponseForLogout(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != "" && response.Text != null)
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        //var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        //Debug.Log(response_body);
                        //apiData.myProfile = response_body.ToObject<List<APIData.MyProfile>>();
                        //apiData.playerData = response_body["player_data"].ToObject<APIData.PlayerData>();

                        //apiData.myProfile[0].notification = apiData.playerData.notification;
                        //List<APIData.SubSet> spinning = response_body.ToObject<List<APIData.SubSet>>();
                        uiManager.settingScreen.transform.GetComponent<SettingScreen>().LogoutCallBack();
                        //response_body[0]["paid_user"].Value<int>(), response_body[0]["payment_id"].Value<string>());
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        errorMessage = jsonObject["response_message"].Value<string>();
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                        //uiManager.homeScreen.transform.GetComponent<HomeScreen>().UpdateNotificatiionApiCallBack(errorMessage);
                        //uiManager.homeScreen.transform.GetComponent<HomeScreen>().CheckNotificationCallBack(errorMessage);
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();

                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }



        }

        // Ask experts API
        public void APIAskExperts(string titleText,string contentText,string status,string email)
        {
            // We can add default query string params for all requests

            RestClient.DefaultRequestParams["param1"] = "My first param";
            RestClient.DefaultRequestParams["param3"] = "My other param";

            currentRequest = new RequestHelper
            {
                Uri = api_Base_Url + askExpert_Url,
                Params = new Dictionary<string, string> {
                    { "param1", "value 1" },
                    { "param2", "value 2" }
                },
                Body = new AskExperts
                {
                    email = apiData.myProfile[0].email,
                    title=titleText,
                    note = contentText,
                    game_status=status
                },
                EnableDebug = true
            };
            RestClient.Post<Post>(currentRequest, askExpert_Action);
        }

        void ResponseAPIForAskExperts(RequestException request, ResponseHelper response, Post post)
        {
            string errorMessage = "";
            print("this : " + response.Text);
            if (response.Text != "" && response.Text != null)
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(response.Text);
                Debug.Log(jsonObject);
                if (request == null)
                {
                    if (jsonObject["response_code"].Value<int>() == APIResponseCode.SUCCESS)
                    {
                        //var response_body = jsonObject["response_body"].Value<JArray>();
                        errorMessage = jsonObject["response_message"].Value<string>();
                        print(errorMessage);
                        //Debug.Log(response_body);
                        //apiData.myProfile = response_body.ToObject<List<APIData.MyProfile>>();
                        //apiData.playerData = response_body["player_data"].ToObject<APIData.PlayerData>();

                        //apiData.myProfile[0].notification = apiData.playerData.notification;
                        //List<APIData.SubSet> spinning = response_body.ToObject<List<APIData.SubSet>>();
                        uiManager.homeScreen.transform.GetComponent<HomeScreen>().AskSubmitCallBack();
                        //response_body[0]["paid_user"].Value<int>(), response_body[0]["payment_id"].Value<string>());
                    }
                    else if (jsonObject["response_code"].Value<int>() == APIResponseCode.WRONG_DATA)
                    {
                        errorMessage = jsonObject["response_message"].Value<string>();
                        uiManager.noDataScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                        //uiManager.homeScreen.transform.GetComponent<HomeScreen>().UpdateNotificatiionApiCallBack(errorMessage);
                        //uiManager.homeScreen.transform.GetComponent<HomeScreen>().CheckNotificationCallBack(errorMessage);
                        //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SigninButtonCallBack();

                        print(errorMessage);
                    }
                    else
                    {
                        uiManager.connectionFailedScreen.SetActive(true);
                        uiManager.loadingScreen.SetActive(false);
                    }
                }
                else
                {
                    uiManager.connectionFailedScreen.SetActive(true);
                    uiManager.loadingScreen.SetActive(false);
                    //uiManager.loginScreen.transform.GetComponent<LoginScreen>().SignButtonCallBack(errorMessage);
                }
            }
            else
            {
                uiManager.noDataScreen.SetActive(true);
                uiManager.loadingScreen.SetActive(false);
            }



        }


        #endregion

    }
}
