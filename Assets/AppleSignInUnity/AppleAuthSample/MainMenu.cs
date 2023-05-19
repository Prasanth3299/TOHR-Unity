using AppleAuth;
using AppleAuth.Enums;
using AppleAuth.Extensions;
using AppleAuth.Interfaces;
using AppleAuth.Native;
using UnityEngine;
using RevolutionGames;
using System.Text;

public class MainMenu : MonoBehaviour
{
    public UIManager uiManager;
    private const string AppleUserIdKey = "AppleUserId";
    
    private IAppleAuthManager _appleAuthManager;

    public LoginMenuHandler LoginMenu;
    public GameMenuHandler GameMenu;

    private void Start()
    {
        print("Started main menu");
        // If the current platform is supported
        if (AppleAuthManager.IsCurrentPlatformSupported)
        {
            print("Supported");
            // Creates a default JSON deserializer, to transform JSON Native responses to C# instances
            var deserializer = new PayloadDeserializer();
            // Creates an Apple Authentication manager with the deserializer
            this._appleAuthManager = new AppleAuthManager(deserializer);
        }

        //this.InitializeLoginMenu();
    }

    private void Update()
    {
        // Updates the AppleAuthManager instance to execute
        // pending callbacks inside Unity's execution loop
        if (this._appleAuthManager != null)
        {
            this._appleAuthManager.Update();
        }

        //this.LoginMenu.UpdateLoadingMessage(Time.deltaTime);
    }

    public void SignInWithAppleButtonPressed()
    {
        //this.InitializeLoginMenu();
        print("signin button pressed");
        this.SetupLoginMenuForAppleSignIn();
        this.SignInWithApple();

    }

    private void InitializeLoginMenu()
    {
        //this.LoginMenu.SetVisible(visible: true);
        //this.GameMenu.SetVisible(visible: false);

        // Check if the current platform supports Sign In With Apple
        if (this._appleAuthManager == null)
        {
            this.SetupLoginMenuForUnsupportedPlatform();
            return;
        }

        // If at any point we receive a credentials revoked notification, we delete the stored User ID, and go back to login
        this._appleAuthManager.SetCredentialsRevokedCallback(result =>
        {
            Debug.Log("Received revoked callback " + result);
            this.SetupLoginMenuForSignInWithApple();
            PlayerPrefs.DeleteKey(AppleUserIdKey);
        });

        // If we have an Apple User Id available, get the credential status for it
        if (PlayerPrefs.HasKey(AppleUserIdKey))
        {
            var storedAppleUserId = PlayerPrefs.GetString(AppleUserIdKey);
            this.SetupLoginMenuForCheckingCredentials();
            this.CheckCredentialStatusForUserId(storedAppleUserId);
        }
        // If we do not have an stored Apple User Id, attempt a quick login
        else
        {
            this.SetupLoginMenuForQuickLoginAttempt();
            this.AttemptQuickLogin();
        }
    }

    private void SetupLoginMenuForUnsupportedPlatform()
    {
        //this.LoginMenu.SetVisible(visible: true);
        //this.GameMenu.SetVisible(visible: false);
        //this.LoginMenu.SetSignInWithAppleButton(visible: false, enabled: false);
        //this.LoginMenu.SetLoadingMessage(visible: true, message: "Unsupported platform");
    }

    private void SetupLoginMenuForSignInWithApple()
    {
        //this.LoginMenu.SetVisible(visible: true);
        //this.GameMenu.SetVisible(visible: false);
        //this.LoginMenu.SetSignInWithAppleButton(visible: true, enabled: true);
        //this.LoginMenu.SetLoadingMessage(visible: false, message: string.Empty);
    }

    private void SetupLoginMenuForCheckingCredentials()
    {
        //this.LoginMenu.SetVisible(visible: true);
        //this.GameMenu.SetVisible(visible: false);
        //this.LoginMenu.SetSignInWithAppleButton(visible: true, enabled: false);
        //this.LoginMenu.SetLoadingMessage(visible: true, message: "Checking Apple Credentials");
    }

    private void SetupLoginMenuForQuickLoginAttempt()
    {
        //this.LoginMenu.SetVisible(visible: true);
        //this.GameMenu.SetVisible(visible: false);
        //this.LoginMenu.SetSignInWithAppleButton(visible: true, enabled: false);
        //this.LoginMenu.SetLoadingMessage(visible: true, message: "Attempting Quick Login");
    }

    private void SetupLoginMenuForAppleSignIn()
    {
        //this.LoginMenu.SetVisible(visible: true);
        //this.GameMenu.SetVisible(visible: false);
        //this.LoginMenu.SetSignInWithAppleButton(visible: true, enabled: false);
        //this.LoginMenu.SetLoadingMessage(visible: true, message: "Signing In with Apple");
    }

    private void SetupGameMenu(string appleUserId, ICredential credential)
    {
        //this.LoginMenu.SetVisible(visible: false);
        //this.GameMenu.SetVisible(visible: true);
        //this.GameMenu.SetupAppleData(appleUserId, credential);
    }

    private void CheckCredentialStatusForUserId(string appleUserId)
    {
        print("Checking credentials");
        // If there is an apple ID available, we should check the credential state
        this._appleAuthManager.GetCredentialState(
            appleUserId,
            state =>
            {
                switch (state)
                {
                    // If it's authorized, login with that user id
                    case CredentialState.Authorized:
                        this.SetupGameMenu(appleUserId, null);
                        return;

                    // If it was revoked, or not found, we need a new sign in with apple attempt
                    // Discard previous apple user id
                    case CredentialState.Revoked:
                    case CredentialState.NotFound:
                        this.SetupLoginMenuForSignInWithApple();
                        PlayerPrefs.DeleteKey(AppleUserIdKey);
                        return;
                }
            },
            error =>
            {
                var authorizationErrorCode = error.GetAuthorizationErrorCode();
                Debug.LogWarning("Error while trying to get credential state " + authorizationErrorCode.ToString() + " " + error.ToString());
                this.SetupLoginMenuForSignInWithApple();
            });
    }

    private void AttemptQuickLogin()
    {
        var quickLoginArgs = new AppleAuthQuickLoginArgs();

        // Quick login should succeed if the credential was authorized before and not revoked
        this._appleAuthManager.QuickLogin(
            quickLoginArgs,
            credential =>
            {
                // If it's an Apple credential, save the user ID, for later logins
                var appleIdCredential = credential as IAppleIDCredential;
                if (appleIdCredential != null)
                {
                    PlayerPrefs.SetString(AppleUserIdKey, credential.User);
                }

                this.SetupGameMenu(credential.User, credential);
            },
            error =>
            {
                // If Quick Login fails, we should show the normal sign in with apple menu, to allow for a normal Sign In with apple
                var authorizationErrorCode = error.GetAuthorizationErrorCode();
                Debug.LogWarning("Quick Login Failed " + authorizationErrorCode.ToString() + " " + error.ToString());
                this.SetupLoginMenuForSignInWithApple();
            });
    }

    private void SignInWithApple()
    {
        //print("Sign in with apple email call");
        var loginArgs = new AppleAuthLoginArgs(LoginOptions.IncludeEmail | LoginOptions.IncludeFullName);

        this._appleAuthManager.LoginWithAppleId(
            loginArgs,
            credential =>
            {
                //print("Inside credential");
                // If a sign in with apple succeeds, we should have obtained the credential with the user id, name, and email, save it
                //PlayerPrefs.SetString(AppleUserIdKey, credential.User);
                //this.SetupGameMenu(credential.User, credential);

                // Obtained credential, cast it to IAppleIDCredential
                var appleIdCredential = credential as IAppleIDCredential;
                if (appleIdCredential != null)
                {
                    //print("Inside email check ");
                    // Apple User ID
                    // You should save the user ID somewhere in the device
                    var userId = appleIdCredential.User;
                    if (userId.Length == 0 || userId == null|| userId=="")
                    {
                        return;
                    }
                    
                    // check it user id
                    PlayerPrefs.SetString(AppleUserIdKey, userId);

                   
                    // Identity token
                    var identityToken = Encoding.UTF8.GetString(
                        appleIdCredential.IdentityToken,
                        0,
                        appleIdCredential.IdentityToken.Length);
                    if (identityToken.Length == 0 || identityToken == null || identityToken == "")
                    {
                        return;
                    }

                    // Authorization code
                    var authorizationCode = Encoding.UTF8.GetString(
                        appleIdCredential.AuthorizationCode,
                        0,
                        appleIdCredential.AuthorizationCode.Length);
                    if (authorizationCode.Length == 0 || authorizationCode == null || authorizationCode == "")
                    {
                        return;
                    }
                    // Email (Received ONLY in the first login)
                    var email = appleIdCredential.Email;

                    // Full name (Received ONLY in the first login)
                    string lastName = "";
                    string firstName = "";
                    string name = "";
                    string emailid = "";
                    //print("id :"+ userId);
                   
                    try
                    {

                         firstName = appleIdCredential.FullName.GivenName;
                        //print("firstName :"+ firstName);
                        if (firstName.ToString() != null)
                        {
                            //print("name not null:" + firstName.ToString());
                            PlayerPrefs.SetString("fbname", firstName.ToString());
                        }
                        else
                        {
                            /* print("name null :");
                             name = "aaaa";*/
                            name = "";
                            PlayerPrefs.SetString("fbname", "");
                        }
                        
                    }
                    catch (System.Exception e)
                    {
                        //name = "aaaa";
                        name = "";
                        PlayerPrefs.SetString("fbname", "");
                    }
                    try
                    {
                         lastName = appleIdCredential.FullName.FamilyName;
                        //print("lastName :" + lastName);
                        if (lastName.ToString() != null)
                        {
                            //print("name not null:" + lastName.ToString());
                            PlayerPrefs.SetString("fbnamelast", lastName.ToString());
                        }
                        else
                        {
                            /* print("name null :");
                             name = "aaaa";*/
                            name = "";
                            PlayerPrefs.SetString("fbnamelast", "");
                        }
                    }
                    catch
                    {
                        PlayerPrefs.SetString("fbnamelast", "");
                    }
                    if (email != null)
                    {
                        //print("email :"+ email);
                        emailid = email;
                        PlayerPrefs.SetString("fbmail", email);
                    }
                   
                    else
                    {
                        //print("email :" + email);
                        emailid ="";
                        PlayerPrefs.SetString("fbmail", "");
                    }

                    PlayerPrefs.SetInt("fblogin", 1);
                    PlayerPrefs.SetString("fbid", userId.ToString());


                    //print("uimanager :" + uiManager);

                    if (uiManager.CheckInternet())
                    {
                        uiManager.loadingScreen.SetActive(true);
                        //print("api call");
                        PlayerPrefs.SetString("mode", "Apple");
                        uiManager.apiManager.APIFacebookLogin(emailid, PlayerPrefs.GetString("fbname") , PlayerPrefs.GetString("fbnamelast"), userId.ToString(),"Apple");
                    }
                    else
                    {
                        uiManager.noNetworkScreen.SetActive(true);
                    }


                    // And now you have all the information to create/login a user in your system
                }
                else
                {
                    //print("null :" + appleIdCredential);
                    // add error message
                }


            },
            error =>
            {
                var authorizationErrorCode = error.GetAuthorizationErrorCode();
                Debug.LogWarning("Sign in with Apple failed " + authorizationErrorCode.ToString() + " " + error.ToString());
                //this.SetupLoginMenuForSignInWithApple();
            });
    }
}
