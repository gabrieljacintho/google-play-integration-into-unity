// Credits: Gabriel Bertasso - contact@gabrielbertasso.com

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.Events;

namespace GooglePlayIntegration
{
    public class GooglePlaySignIn : MonoBehaviour
    {
        public UnityEvent OnSignInSuccess;
        public UnityEvent OnSignInFail;


        private void Start()
        {
            if (PlayGamesPlatform.Instance.IsAuthenticated())
            {
                OnSignInSuccess?.Invoke();
            }
            else
            {
                PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
            }
        }

        protected internal void ProcessAuthentication(SignInStatus status)
        {
            if (status == SignInStatus.Success)
            {
                string playerUserName = PlayGamesPlatform.Instance.localUser.userName;
                Debug.Log("Login successfully! (\"" + playerUserName + "\")", gameObject);
                OnSignInSuccess?.Invoke();
            }
            else
            {
                Debug.LogWarning("Failed to login!", gameObject);
                OnSignInFail?.Invoke();
            }
        }

        public void ManuallyAuthenticate()
        {
            PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
        }
    }
}
