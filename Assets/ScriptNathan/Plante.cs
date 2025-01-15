[System.Serializable]
public class Plante
{
    public string nom; // Nom de la plante (Pens�e, Rose, Hibiscus, etc.)
    public string[] couleurs = new string[3]; // Trois couleurs diff�rentes
    public int quantiteGraines; // Quantit� de graines disponibles

    // Constructeur pour initialiser la plante avec son nom et ses couleurs
    public Plante(string nom, string couleur1, string couleur2, string couleur3, int quantiteGraines)
    {
        this.nom = nom;
        this.couleurs[0] = couleur1;
        this.couleurs[1] = couleur2;
        this.couleurs[2] = couleur3;
        this.quantiteGraines = quantiteGraines;
    }

    // M�thode statique pour obtenir une plante par d�faut parmi les choix
    public static Plante GetPlanteParDefaut(string nomPlante)
    {
        switch (nomPlante)
        {
            case "Pens�e":
                return new Plante("Pens�e", "Rouge", "Bleu", "Jaune", 10);
            case "Rose":
                return new Plante("Rose", "Rose", "Blanc", "Jaune", 10);
            case "Hibiscus":
                return new Plante("Hibiscus", "Rouge", "Blanc", "Orange", 10);
            case "Calibrachoa":
                return new Plante("Calibrachoa", "Violet", "Jaune", "Rose", 10);
            case "Lys":
                return new Plante("Lys", "Blanc", "Jaune", "Orange", 10);
            default:
                return null; // Si le nom ne correspond � rien, retourner null
        }
    }
}
