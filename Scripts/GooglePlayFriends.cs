// Credits: Gabriel Bertasso - contact@gabrielbertasso.com

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;

namespace GooglePlayIntegration
{
    public class GooglePlayFriends : MonoBehaviour
    {
        private IUserProfile[] _friends;

        public UnityEvent OnLoadFriendsSuccess;
        public UnityEvent OnLoadFriendsFail;


        private void Start()
        {
            if (CheckPermission())
            {
                AskPermission();
            }
            else
            {
                LoadFriends();
            }
        }

        private bool CheckPermission()
        {
            LoadFriendsStatus status = PlayGamesPlatform.Instance.GetLastLoadFriendsStatus();
            return status == LoadFriendsStatus.ResolutionRequired;
        }

        private void AskPermission()
        {
            PlayGamesPlatform.Instance.AskForLoadFriendsResolution((result) =>
            {
                if (result == UIStatus.Valid)
                {
                    LoadFriends();
                }
                else
                {
                    Debug.LogWarning("Failed to load friends!", gameObject);
                    OnLoadFriendsFail?.Invoke();
                }
            });
        }

        private void LoadFriends()
        {
            Social.localUser.LoadFriends((success) =>
            {
                if (success)
                {
                    _friends = Social.localUser.friends;

                    string usernames = string.Empty;
                    foreach (IUserProfile friend in _friends)
                    {
                        if (usernames.Length > 0)
                        {
                            usernames += ", ";
                        }

                        usernames += friend.userName;
                    }

                    Debug.Log("Friends loaded successfully!\nUsernames: " + usernames, gameObject);
                    OnLoadFriendsSuccess?.Invoke();
                }
                else
                {
                    Debug.LogWarning("Failed to load friends!", gameObject);
                    OnLoadFriendsFail?.Invoke();
                }
            });
        }
    }
}