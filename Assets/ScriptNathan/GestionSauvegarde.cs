using System.IO;
using UnityEngine;

public class GestionSauvegarde : MonoBehaviour
{
    public Inventaire inventaire; // Inventaire � sauvegarder/charger
    private string cheminSauvegarde;

    void Start()
    {
        cheminSauvegarde = "Assets/SaveData/sauvegarde.json";
    }

    // Sauvegarder les donn�es de l'inventaire dans un fichier JSON
    public void Sauvegarder()
    {
        string json = JsonUtility.ToJson(inventaire);
        File.WriteAllText(cheminSauvegarde, json);
        Debug.Log("Sauvegarde effectu�e !");
    }

    // Charger les donn�es de l'inventaire depuis un fichier JSON
    public void Charger()
    {
        if (File.Exists(cheminSauvegarde))
        {
            string json = File.ReadAllText(cheminSauvegarde);
            inventaire = JsonUtility.FromJson<Inventaire>(json);
            Debug.Log("Sauvegarde charg�e !");
        }
        else
        {
            Debug.LogWarning("Aucune sauvegarde trouv�e !");
        }
    }
}
