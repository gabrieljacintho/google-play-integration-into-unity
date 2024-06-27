// Credits: Gabriel Bertasso - contact@gabrielbertasso.com

using GooglePlayGames;
using UnityEngine;
using UnityEngine.Events;

namespace GooglePlayIntegration
{
    public class GooglePlayAchievement : MonoBehaviour
    {
        [SerializeField] private string _achievementId;

        public string AchievementId
        {
            get => _achievementId;
            set => _achievementId = value;
        }

        [Space]
        public UnityEvent OnReportProgress;
        public UnityEvent OnUnlock;

        /*
        * Notice that according to the expected behavior of Social.ReportProgress, a progress of 0.0f means revealing
        * the achievement and a progress of 100.0f means unlocking the achievement. Therefore, to reveal an achievement
        * (that was previously hidden) without unlocking it, simply call Social.ReportProgress with a progress of 0.0f.
        */
        public void ReportProgress(string achievementId, float progress)
        {
            Social.ReportProgress(achievementId, progress, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Achievement \"" + achievementId + "\" updated successfully (" + progress.ToString("F1") + "%)!", gameObject);

                    OnReportProgress?.Invoke();

                    if (progress >= 100f)
                    {
                        OnUnlock?.Invoke();
                    }
                }
                else
                {
                    Debug.LogWarning("Failed to update achievement \"" + achievementId + "\" (" + progress.ToString("F1") + "%)!", gameObject);
                }
            });
        }

        public void ReportProgress(float progress)
        {
            ReportProgress(_achievementId, progress);
        }

        public void Unlock(string achievementId)
        {
            ReportProgress(achievementId, 100f);
        }

        public void Unlock()
        {
            Unlock(_achievementId);
        }

        public void Reveal(string achievementId)
        {
            ReportProgress(achievementId, 0f);
        }

        public void Reveal()
        {
            Reveal(_achievementId);
        }

        public void Increment(string achievementId, int steps)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(achievementId, steps, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Achievement \"" + achievementId + "\" incremented successfully (" + steps + ")!", gameObject);
                }
                else
                {
                    Debug.LogWarning("Failed to increment achievement \"" + achievementId + "\" (" + steps + ")!", gameObject);
                }
            });
        }

        public void Increment(int steps)
        {
            Increment(_achievementId, steps);
        }
    }
}