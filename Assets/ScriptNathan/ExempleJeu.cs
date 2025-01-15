using UnityEngine;

public class ExempleJeu : MonoBehaviour
{
    public GestionSauvegarde gestionSauvegarde;
    public Economie economie;
    void Start()
    {
        gestionSauvegarde.Charger(); 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            gestionSauvegarde.Sauvegarder();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            economie.AcheterPlante(gestionSauvegarde.inventaire, "Pensée");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            economie.VendreGraines(gestionSauvegarde.inventaire, 1);
        }
    }
}
