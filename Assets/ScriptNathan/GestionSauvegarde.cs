using System.IO;
using UnityEngine;

public class GestionSauvegarde : MonoBehaviour
{
    public Inventaire inventaire; // Inventaire à sauvegarder/charger
    private string cheminSauvegarde;

    void Start()
    {
        cheminSauvegarde = "Assets/SaveData/sauvegarde.json";
    }

    // Sauvegarder les données de l'inventaire dans un fichier JSON
    public void Sauvegarder()
    {
        string json = JsonUtility.ToJson(inventaire);
        File.WriteAllText(cheminSauvegarde, json);
        Debug.Log("Sauvegarde effectuée !");
    }

    // Charger les données de l'inventaire depuis un fichier JSON
    public void Charger()
    {
        if (File.Exists(cheminSauvegarde))
        {
            string json = File.ReadAllText(cheminSauvegarde);
            inventaire = JsonUtility.FromJson<Inventaire>(json);
            Debug.Log("Sauvegarde chargée !");
        }
        else
        {
            Debug.LogWarning("Aucune sauvegarde trouvée !");
        }
    }
}
