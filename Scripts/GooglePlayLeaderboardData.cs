// Credits: Gabriel Bertasso - contact@gabrielbertasso.com

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace GooglePlayIntegration
{
    public class GooglePlayLeaderboardData : MonoBehaviour
    {
        private LeaderboardScoreData _leaderboardData;


        private void CreateLeaderboard(string id)
        {
            ILeaderboard lb = PlayGamesPlatform.Instance.CreateLeaderboard();
            lb.id = id;
            lb.LoadScores(success =>
            {
                if (success)
                {
                    LoadUsersAndDisplay(lb);
                }
                else
                {
                    Debug.LogError("Error retrieving leaderboard!");
                }
            });
        }

        protected internal void LoadUsersAndDisplay(ILeaderboard leaderboard)
        {
            // get the user ids
            List<string> userIds = new List<string>();

            foreach (IScore score in leaderboard.scores)
            {
                userIds.Add(score.userID);
            }
            // load the profiles and display (or in this case, log)
            Social.LoadUsers(userIds.ToArray(), (users) =>
            {
                string status = "Leaderboard loading: " + leaderboard.title + " count = " + leaderboard.scores.Length;
                foreach (IScore score in leaderboard.scores)
                {
                    IUserProfile user = FindUser(users, score.userID);
                    status += "\n" + score.formattedValue + " by " +
                        ((user != null) ? user.userName : "**unk_" + score.userID + "**");
                }
                Debug.Log(status);
            });
        }

        private IUserProfile FindUser(IUserProfile[] users, string id)
        {
            return Array.Find(users, user => user.id == id);
        }

        private void LoadData(string leaderboardId)
        {
            PlayGamesPlatform.Instance.LoadScores(
                leaderboardId,
                LeaderboardStart.PlayerCentered,
                100,
                LeaderboardCollection.Public,
                LeaderboardTimeSpan.AllTime,
                (data) =>
                {
                    _leaderboardData = data;

                    string status = "Leaderboard data valid: " + data.Valid;
                    status += "\n approx:" + data.ApproximateCount + " have " + data.Scores.Length;
                    Debug.Log(status);
            });
        }

        private void LoadNextPageData(LeaderboardScoreData data)
        {
            PlayGamesPlatform.Instance.LoadMoreScores(data.NextPageToken, 10,
                (newData) =>
                {
                    _leaderboardData = newData;

                    string status = "Leaderboard data valid: " + data.Valid;
                    status += "\n approx:" + data.ApproximateCount + " have " + data.Scores.Length;
                    Debug.Log(status);
                });
        }
    }
}