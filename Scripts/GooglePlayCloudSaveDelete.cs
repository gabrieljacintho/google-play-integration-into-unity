// Credits: Gabriel Bertasso - contact@gabrielbertasso.com

using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using UnityEngine;
using UnityEngine.Events;

namespace GooglePlayIntegration
{
    public class GooglePlayCloudSaveDelete : MonoBehaviour
    {
        public UnityEvent OnDeleteSuccess;
        public UnityEvent OnDeleteFail;


        public void DeleteGameData(string filename)
        {
            // Open the file to get the metadata.
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameDeleted);
        }

        private void OnSavedGameDeleted(SavedGameRequestStatus status, ISavedGameMetadata game)
        {
            if (status == SavedGameRequestStatus.Success)
            {
                ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
                savedGameClient.Delete(game);
                Debug.Log("Saved game deleted successfully!", gameObject);
                OnDeleteSuccess?.Invoke();
            }
            else
            {
                // handle error
                Debug.LogWarning("Failed to delete saved game!", gameObject);
                OnDeleteFail?.Invoke();
            }
        }
    }
}