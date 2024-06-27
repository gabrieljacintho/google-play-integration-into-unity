// Credits: Gabriel Bertasso - contact@gabrielbertasso.com

using GooglePlayGames;
using UnityEngine;

namespace GooglePlayIntegration
{
    public class GooglePlayShowLeaderboardUI : MonoBehaviour
    {
        [SerializeField] private string _leaderboardId;

        public string LeaderboardId
        {
            get => _leaderboardId;
            set => _leaderboardId = value;
        }

        // If you wish to show a particular leaderboard instead of all leaderboards, you can pass a leaderboard ID to the method.
        public void Show(string leaderboardId = default)
        {
            if (string.IsNullOrEmpty(leaderboardId))
            {
                Social.ShowLeaderboardUI();
            }
            else
            {
                PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboardId);
            }
        }

        public void Show()
        {
            Show(_leaderboardId);
        }
    }
}