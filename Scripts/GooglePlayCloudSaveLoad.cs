// Credits: Gabriel Bertasso - contact@gabrielbertasso.com

using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames;
using UnityEngine;
using UnityEngine.Events;
using GooglePlayGames.BasicApi;

namespace GooglePlayIntegration
{
    public class GooglePlayCloudSaveLoad : MonoBehaviour
    {
        public UnityEvent OnLoadSuccess;
        public UnityEvent OnLoadFail;


        #region Displaying saved games UI

        public void ShowSelectUI()
        {
            uint maxNumToDisplay = 5;
            bool allowCreateNew = false;
            bool allowDelete = true;

            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.ShowSelectSavedGameUI("Select saved game",
                maxNumToDisplay,
                allowCreateNew,
                allowDelete,
                OnSavedGameSelected);
        }

        private void OnSavedGameSelected(SelectUIStatus status, ISavedGameMetadata game)
        {
            if (status == SelectUIStatus.SavedGameSelected)
            {
                // handle selected game save
                OpenSavedGame(game.Filename);
            }
            else
            {
                // handle cancel or error
                Debug.LogError("Failed to select saved game!", gameObject);
            }
        }

        #endregion

        #region Opening a saved game

        private void OpenSavedGame(string filename)
        {
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
        }

        private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
        {
            if (status == SavedGameRequestStatus.Success)
            {
                // handle reading or writing of saved game.
                LoadGameData(game);
            }
            else
            {
                // handle error
                Debug.LogError("Failed to open saved game!", gameObject);
            }
        }

        #endregion

        #region Reading a saved game

        public void LoadGameData(ISavedGameMetadata game)
        {
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
        }

        private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
        {
            if (status == SavedGameRequestStatus.Success)
            {
                // handle processing the byte array data
                Debug.Log("Saved game loaded successfully!", gameObject);
                OnLoadSuccess?.Invoke();
            }
            else
            {
                // handle error
                Debug.LogWarning("Failed to load saved game!", gameObject);
                OnLoadFail?.Invoke();
            }
        }

        #endregion
    }
}