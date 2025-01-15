using UnityEngine;

public class ExempleJeu : MonoBehaviour
{
    public GestionSauvegarde gestionSauvegarde;
    public Economie economie;

    void Start()
    {
        gestionSauvegarde.Charger(); // Charger les données de sauvegarde au démarrage
    }

    void Update()
    {
        // Sauvegarder avec la touche "S"
        if (Input.GetKeyDown(KeyCode.U))
        {
            gestionSauvegarde.Sauvegarder();
        }

        // Acheter une plante avec la touche "A" et spécifier le nom de la plante
        if (Input.GetKeyDown(KeyCode.Y))
        {
            // Acheter une plante spécifique (par exemple, "Pensée")
            economie.AcheterPlante(gestionSauvegarde.inventaire, "Pensée");
        }

        // Vendre des graines avec la touche "V"
        if (Input.GetKeyDown(KeyCode.V))
        {
            economie.VendreGraines(gestionSauvegarde.inventaire, 1);
        }
    }
}
