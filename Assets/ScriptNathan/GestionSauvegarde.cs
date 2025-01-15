using System.IO;
using UnityEngine;

public class GestionSauvegarde : MonoBehaviour
{
    public Inventaire inventaire; 
    private string cheminSauvegarde;

    void Start()
    {
        cheminSauvegarde = "Assets/SaveData/sauvegarde.json";
    }
    public void Sauvegarder()
    {
        string json = JsonUtility.ToJson(inventaire);
        File.WriteAllText(cheminSauvegarde, json);
        Debug.Log("Sauvegarde effectu�e !");
    }
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
