using System.Collections.Generic;
using UnityEngine;

public class Economie : MonoBehaviour
{
    public int prixPlante = 10; // Prix fixe de chaque plante

    // Acheter une plante et l'ajouter à l'inventaire
    public void AcheterPlante(Inventaire inventaire, string nomPlante)
    {
        if (inventaire.argent >= prixPlante)
        {
            inventaire.argent -= prixPlante; // Retirer l'argent

            // Créer la plante avec les valeurs par défaut basées sur le nom
            Plante planteAchetee = Plante.GetPlanteParDefaut(nomPlante);

            if (planteAchetee != null)
            {
                inventaire.plantes.Add(planteAchetee); // Ajouter la plante à l'inventaire
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

    // Vendre des graines de plante et ajouter de l'argent
    public void VendreGraines(Inventaire inventaire, int quantite)
    {
        if (inventaire.plantes.Count > 0)
        {
            // Supposons qu'on vend une graine pour 1 unité de monnaie
            inventaire.argent += quantite;
            inventaire.plantes[0].quantiteGraines -= quantite;
            Debug.Log("Graines vendues !");
        }
        else
        {
            Debug.LogWarning("Pas de plantes à vendre !");
        }
    }
}
