using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RevolutionGames
{
    public class APIData : MonoBehaviour
    {
        #region List data Variables

        public PlayerData playerData;
        public List<MyProfile> myProfile = new List<MyProfile>();
        public List<ExternaLink> external_link = new List<ExternaLink>();
        public List<CheckSubscribe> subcriptionDetails = new List<CheckSubscribe>();
        public GameDetails game_Details = new GameDetails();
        public SubcriptionData subcription_Data = new SubcriptionData();

        #endregion

        #region Data Class

        public class SubcriptionData
        {
            public string subscriptionPlan = "Trial";
            public string latestsubscriptionDate = "";
            public List<string> subscriptionDate = new List<string>();
            public string subscriptionAmount = "0";
            public List<string> subscriptionTransactionId = new List<string>();
            public string latestSubscriptionTransactionId = "";
            public int subscriptionStatus = 0;
            public string subscriptionToken = "";
            public bool saveDone = false;
        }

        public class GameDetails
        {
            public string device_name = "Android";
            public string device_token = "78901-2150123";
            public bool is_admin = false;
            public string version_no = "1.3";
        }

        public class MyProfile
        {
            public int player_id;
            public string player_uuid;
            public string reference_uuid;
            public string first_name;
            public string sur_name;
            public string email;
            public string profile_image_url;
            public string password;
            public string social_id;
            public string player_login_mode;
            public string village_id;
            public string game_status;
            public string is_admin;
            public string paid_user;
            public int notification;
            public string country_name;
            public string is_delete;
            public string created_date;
            public string updated_date;
            public string version_no;
            public string history_id;
            public string device_name;
            public string device_token;
        }
        public class PlayerData
        {
            public int player_id;
            public string player_uuid;
            public string reference_uuid;
            public string first_name;
            public string sur_name;
            public string email;
            public string profile_image_url;
            public string password;
            public string social_id;
            public string player_login_mode;
            public string village_id;
            public string game_status;
            public string is_admin;
            public string paid_user;
            public int notification;
            public string country_name;
            public string is_delete;
            public string created_date;
            public string updated_date;
            public string version_no;
        }
        public class ExternaLink
        {
            public string external_logo_id;
            public string logo;
            public string url;
            public string enable_or_disable;
            public string created_date;
            public string updated_date;
        }
        public class VillageCost
        {
            public int village_id;
            public string village_name;
            public string village_cost;
            public string twenty_percentage_vm_cost;
            public string sixtyfive_percentage_vm_cost;
            public string village_level;
            public string position;
            public string status;
            public int is_delete;
            public string created_date;
            public string updated_date;
        }
        public class ChestCost
        {
            public int chest_id;
            public int village_id;
            public string village_name;
            public string wooden_value;
            public string golden_value;
            public string magical_value;
            public string position;
            public int is_delete;
            public string created_date;
            public string updated_date;
        }
        public class PetInfo
        {
            public int pet_id;
            public string pet_name;
            public string front_image;
            public string background_image;
            public string is_delete;
            public string created_date;
            public string updated_date;
        }
        public class PetDetails
        {
            public int pet_level_id;
            public int pet_id;
            public long level;
            public string xp;
            public string attack;
            public string star;
            public int is_delete;
            public string created_date;
            public string updated_date;
        }
        public class Sets
        {
            public int set_id;
            public string set_name;
            public int un_lock;
            public int complete;
            public long spin;
            public string xp;
            public string pet_food;
            public string position;
            public int is_delete;
            public string created_date;
            public string updated_date;

        }
        public class SideEvents
        {
            public int event_id;
            public string event_name;
            public string description;
            public string event_image;
            public int is_delete;
            public string created_date;
            public string updated_date;
        }
        public class FAQ
        {
            public int faq_id;
            public string faq_for;
            public string question;
            public string answer;
            public int is_delete;
            public string created_date;
            public string updated_date;
        }

        public class SpinningEvent
        {
            public int main_event_id;
            public string event_for;
            public string title;
            public string image;
            public string background_image;
            public string details;
            public string detail_url;
            public string status;
            public int is_delete;
            public string created_date;
            public string updated_date;
        }
        public class EventList
        {
            public string event_name;
            public string reward_event_for;
        }
        public class RewardEvent
        {
            public int reward_event_id;
            public string reward_event_for;
            public string event_name;
            public string mission;
            public string total_hammers;
            public string rewards;
            public string status;
            public int is_delete;
            public string created_date;
            public string updated_date;

        }
        public class RewardList
        {
            public int reward_list_id;
            public string event_for;
            public int reward_event_id;
            public int mission;
            public long total_hammers;
            public string rewards;
            public string status;
            public int is_delete;
            public string created_date;
            public string updated_date;

        }
        public class Vikings
        {
            public int viking_event_id;
            public string event_for;
            public string title;
            public string image;
            public string background_image;
            public string details;
            public string detail_url;
            public string status;
            public int is_delete;
            public string created_date;
            public string updated_date;
        }
        public class Intoduction
        {
            public int introduction_id;
            public string title;
            public string detail;
            public int is_delete;
            public string created_date;
            public string updated_date;
        }
        public class Terms
        {
            public int terms_and_conditions_id;
            public string title;
            public string details;
            public string detail_url;
            public int is_delete;
            public string created_date;
            public string updated_date;
        }
        public class GetAllSubscription
        {
            public int subscription_id;
            public string plan_name;
            public float plan_price;
            public string validity;
            public string status;
            public int is_delete;
            public string created_date;
            public string updated_date;
        }
        public class Inclusion
        {
            public int push_notification_id;
            public string inclusion;
            public string message;
            public string created_date;
            public string updated_date;

        }
        public class CheckSubscribe
        {
            public string user_subscription_id;
            public int player_id;
            public string plan_name;
            public string plan_price;
            public string payment_id;
            public string promo_code;
            public string started_date;
            public string expire_date;
            public string paid_user;
        }

        public class SubSet
        {
            public long sub_set_id;
            public int set_id;
            public string set_name;
            public string sub_set_title;
            public long un_lock;
            public int is_delete;
            public string created_date;
            public string updated_date;
        }

        #endregion
    }
}
