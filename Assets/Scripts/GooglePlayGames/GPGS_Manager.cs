using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Threading.Tasks;
using System;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class GPGS_Manager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _textName;

    public string Token;
    public string Error;

    void Awake()
    {
        PlayGamesPlatform.Activate();
    }

    async void Start()
    {
        await UnityServices.InitializeAsync();
        await LoginGooglePlayGames();
        await SignInWithGooglePlayGamesAsync(Token);
    }
    //Fetch the Token / Auth code
    public Task LoginGooglePlayGames()
    {
        var tcs = new TaskCompletionSource<object>();
        PlayGamesPlatform.Instance.Authenticate((success) =>
        {
            if (success == SignInStatus.Success)
            {
                Debug.Log("Login with Google Play games successful.");
                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    Debug.Log("Authorization code: " + code);
                    Token = code;
                    _textName.text = PlayGamesPlatform.Instance.GetUserDisplayName();
                    // This token serves as an example to be used for SignInWithGooglePlayGames
                    tcs.SetResult(null);
                });
            }
            else
            {
                Error = "Failed to retrieve Google play games authorization code";
                Debug.Log("Login Unsuccessful");
                tcs.SetException(new Exception("Failed"));
                _textName.text = Error;
            }
        });
        return tcs.Task;
    }


    async Task SignInWithGooglePlayGamesAsync(string authCode)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithGooglePlayGamesAsync(authCode);
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}"); //Display the Unity Authentication PlayerID
            Debug.Log("SignIn is successful.");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

    public void ShowAchUI()
    {
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    } 

    public void ShowLBUI()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }

    public async void RetrySignIn()
    {
        await SignInWithGooglePlayGamesAsync(Token);
    }
}
