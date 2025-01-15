using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventaire
{
    public List<Plante> plantes = new List<Plante>(); // Liste des plantes
    public int argent = 50; // Argent du joueur
}
