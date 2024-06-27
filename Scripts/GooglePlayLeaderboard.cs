// Credits: Gabriel Bertasso - contact@gabrielbertasso.com

using UnityEngine;

namespace GooglePlayIntegration
{
    public class GooglePlayLeaderboard : MonoBehaviour
    {
        [SerializeField] private string _leaderboardId;

        public string LeaderboardId
        {
            get => _leaderboardId;
            set => _leaderboardId = value;
        }

        /*
         * Note that the platform and the server will automatically discard scores that are lower than
         * the player's existing high score, so you can submit scores freely without any checks to test
         * whether or not the score is greater than the player's existing score.
        */
        public void ReportScore(string leaderboardId, int score)
        {
            Social.ReportScore(score, leaderboardId, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Leaderboard \"" + leaderboardId + "\" score updated successfully (" + score + ")!", gameObject);
                }
                else
                {
                    Debug.LogWarning("Failed to update leaderboard \"" + leaderboardId + "\" (" + score + ")!", gameObject);
                }
            });
        }

        public void ReportScore(int score)
        {
            ReportScore(_leaderboardId, score);
        }
    }
}