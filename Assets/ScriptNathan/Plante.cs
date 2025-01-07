[System.Serializable]
public class Plante
{
    public string nom; // Nom de la plante (Pensée, Rose, Hibiscus, etc.)
    public string[] couleurs = new string[3]; // Trois couleurs différentes
    public int quantiteGraines; // Quantité de graines disponibles

    // Constructeur pour initialiser la plante avec son nom et ses couleurs
    public Plante(string nom, string couleur1, string couleur2, string couleur3, int quantiteGraines)
    {
        this.nom = nom;
        this.couleurs[0] = couleur1;
        this.couleurs[1] = couleur2;
        this.couleurs[2] = couleur3;
        this.quantiteGraines = quantiteGraines;
    }

    // Méthode statique pour obtenir une plante par défaut parmi les choix
    public static Plante GetPlanteParDefaut(string nomPlante)
    {
        switch (nomPlante)
        {
            case "Pensée":
                return new Plante("Pensée", "Rouge", "Bleu", "Jaune", 10);
            case "Rose":
                return new Plante("Rose", "Rose", "Blanc", "Jaune", 10);
            case "Hibiscus":
                return new Plante("Hibiscus", "Rouge", "Blanc", "Orange", 10);
            case "Calibrachoa":
                return new Plante("Calibrachoa", "Violet", "Jaune", "Rose", 10);
            case "Lys":
                return new Plante("Lys", "Blanc", "Jaune", "Orange", 10);
            default:
                return null; // Si le nom ne correspond à rien, retourner null
        }
    }
}
