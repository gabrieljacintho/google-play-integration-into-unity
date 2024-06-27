// Credits: Gabriel Bertasso - contact@gabrielbertasso.com

using GooglePlayGames;
using UnityEngine;
using UnityEngine.Events;

namespace GooglePlayIntegration
{
    public class GooglePlayIncrementEvent : MonoBehaviour
    {
        [SerializeField] private string _eventId;

        public string EventId
        {
            get => _eventId;
            set => _eventId = value;
        }

        [Space]
        public UnityEvent OnIncrement;


        public void Increment(string eventId, int steps = 1)
        {
            PlayGamesPlatform.Instance.Events.IncrementEvent(eventId, (uint)steps);
            OnIncrement?.Invoke();
        }

        public void Increment(int steps = 1)
        {
            Increment(_eventId, steps);
        }
    }
}