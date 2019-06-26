using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlayController : MonoBehaviour
{
    public GameObject btnLogin, btnLeaderboard;
    int randomScore;

    // Start is called before the first frame update
    void Start()
    {
        Login();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Login()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Debug.Log("Login");

        Social.localUser.Authenticate((bool success) =>
        {
            if (success == true)
            {
                btnLogin.SetActive(false);
                PostScore();
            }
            else
            {
                btnLogin.SetActive(true);
            }
        });
    }

    public void ShowLeaderboardUI()
    {
        Debug.Log("Show Leaderboard");
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_score);
    }

    public void PostScore()
    {
        randomScore = Random.Range(0, 1000);
        PlayGamesPlatform.Instance.ReportScore(randomScore, GPGSIds.leaderboard_score, (bool success) => {
        });
    }
}
