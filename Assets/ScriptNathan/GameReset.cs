using System.IO;
using UnityEngine;

public class ResetSave : MonoBehaviour
{
    private string saveFilePath;
    private void Start()
    {
        saveFilePath = Application.persistentDataPath + "/sauvegarde.json";
    }
    public void ResetGameSave()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Les fichiers ont été écrasés.");
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        Debug.Log("La partie a bien été réinitialisée !");
    }
}