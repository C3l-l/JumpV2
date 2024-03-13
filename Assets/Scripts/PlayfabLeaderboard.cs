using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;


public class PlayfabLeaderboard : MonoBehaviour
{
    void OnError(PlayFabError error) {
        Debug.Log("Error when updating leaderboard");
        Debug.Log(error.GenerateErrorReport());
    }
    public void SendLeaderboard(int score) {
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = "PlatformScore",
                    Value = score
            }
        }
    };
    PlayFabClientAPI.UpdatePlayerStatistics( request, OnLeaderboardUpdate, OnError);
   } 
   void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Leaderboard updated successfully!");
    }  
}
