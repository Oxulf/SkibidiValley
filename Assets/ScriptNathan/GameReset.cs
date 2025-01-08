using System.IO;
using UnityEngine;

public class ResetSave : MonoBehaviour
{
    private string saveFilePath;

    private void Start()
    {
        // Définir le chemin du fichier de sauvegarde
        saveFilePath = Application.persistentDataPath + "/sauvegarde.json";
    }

    public void ResetGameSave()
    {
        // Vérifier si le fichier existe
        if (File.Exists(saveFilePath))
        {
            // Supprimer le fichier
            File.Delete(saveFilePath);
            Debug.Log("Les fichiers ont été écrasés.");
        }

        // Recharge la scène pour recommencer le jeu
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        Debug.Log("La partie a bien été réinitialisée !");
    }
}