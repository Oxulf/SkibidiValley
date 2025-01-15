using UnityEngine;

public class ExempleJeu : MonoBehaviour
{
    public GestionSauvegarde gestionSauvegarde;
    public Economie economie;

    void Start()
    {
        gestionSauvegarde.Charger(); // Charger les donn�es de sauvegarde au d�marrage
    }

    void Update()
    {
        // Sauvegarder avec la touche "S"
        if (Input.GetKeyDown(KeyCode.U))
        {
            gestionSauvegarde.Sauvegarder();
        }

        // Acheter une plante avec la touche "A" et sp�cifier le nom de la plante
        if (Input.GetKeyDown(KeyCode.Y))
        {
            // Acheter une plante sp�cifique (par exemple, "Pens�e")
            economie.AcheterPlante(gestionSauvegarde.inventaire, "Pens�e");
        }

        // Vendre des graines avec la touche "V"
        if (Input.GetKeyDown(KeyCode.V))
        {
            economie.VendreGraines(gestionSauvegarde.inventaire, 1);
        }
    }
}
