// Credits: Gabriel Bertasso - contact@gabrielbertasso.com

using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace GooglePlayIntegration
{
    public class GooglePlayCloudSave : MonoBehaviour
    {
        public UnityEvent OnSaveSuccess;
        public UnityEvent OnSaveFail;


        public void SaveGame(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime, Texture2D savedImage = null)
        {
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

            SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
            builder = builder
                .WithUpdatedPlayedTime(totalPlaytime)
                .WithUpdatedDescription("Saved game at " + DateTime.Now);

            if (savedImage != null)
            {
                // This assumes that savedImage is an instance of Texture2D
                // and that you have already called a function equivalent to
                // getScreenshot() to set savedImage
                // NOTE: see sample definition of getScreenshot() method below
                byte[] pngData = savedImage.EncodeToPNG();
                builder = builder.WithUpdatedPngCoverImage(pngData);
            }

            SavedGameMetadataUpdate updatedMetadata = builder.Build();
            savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
        }

        public Texture2D GetScreenshot()
        {
            // Create a 2D texture that is 1024x700 pixels from which the PNG will be
            // extracted
            Texture2D screenshot = new Texture2D(1024, 700);

            // Takes the screenshot from top left hand corner of screen and maps to top
            // left hand corner of screenShot texture
            screenshot.ReadPixels(new Rect(0, 0, Screen.width, (Screen.width / 1024) * 700), 0, 0);
            return screenshot;
        }

        private void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
        {
            if (status == SavedGameRequestStatus.Success)
            {
                // handle reading or writing of saved game.
                Debug.Log("Game saved at " + DateTime.Now, gameObject);
                OnSaveSuccess?.Invoke();
            }
            else
            {
                // handle error
                Debug.LogWarning("Failed to save game!", gameObject);
                OnSaveFail?.Invoke();
            }
        }
    }
}