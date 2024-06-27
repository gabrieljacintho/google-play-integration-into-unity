// Credits: Gabriel Bertasso - contact@gabrielbertasso.com

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.Events;

namespace GooglePlayIntegration
{
    public class GooglePlayStats : MonoBehaviour
    {
        public UnityEvent OnGetStatsSuccess;
        public UnityEvent OnGetStatsFail;

        private PlayerStats _playerStats;


        private void Start()
        {
            ((PlayGamesLocalUser)Social.localUser).GetStats((rc, stats) =>
            {
                // -1 means cached stats, 0 is succeess
                // see  CommonStatusCodes for all values.
                if (rc <= 0)
                {
                    _playerStats = stats;
                    Debug.Log("Player stats loaded successfully! Number of sessions: " + _playerStats.NumberOfSessions, gameObject);
                    OnGetStatsSuccess?.Invoke();
                }
                else
                {
                    Debug.LogWarning("Failed to load player stats!", gameObject);
                    OnGetStatsFail?.Invoke();
                }
            });
        }
    }
}