using System.Collections.Generic;
using UnityEngine;

public class Economie : MonoBehaviour
{
    public int prixPlante = 10; 
    public void AcheterPlante(Inventaire inventaire, string nomPlante)
    {
        if (inventaire.argent >= prixPlante)
        {
            inventaire.argent -= prixPlante; 
            Plante planteAchetee = Plante.GetPlanteParDefaut(nomPlante);

            if (planteAchetee != null)
            {
                inventaire.plantes.Add(planteAchetee); 
                Debug.Log(nomPlante + " achetée !");
            }
            else
            {
                Debug.LogWarning("Plante inconnue : " + nomPlante);
            }
        }
        else
        {
            Debug.LogWarning("Pas assez d'argent pour acheter une plante !");
        }
    }
    public void VendreGraines(Inventaire inventaire, int quantite)
    {
        if (inventaire.plantes.Count > 0)
        {
            inventaire.argent += quantite;
            inventaire.plantes[0].quantiteGraines -= quantite;
            Debug.Log("Graines vendues !");
        }
        else
        {
            Debug.Log("Pas de plantes à vendre !");
        }
    }
}