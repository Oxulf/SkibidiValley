[System.Serializable]
public class Plante
{
    public string nom; 
    public string[] couleurs = new string[3]; 
    public int quantiteGraines; 
    public Plante(string nom, string couleur1, string couleur2, string couleur3, int quantiteGraines)
    {
        this.nom = nom;
        this.couleurs[0] = couleur1;
        this.couleurs[1] = couleur2;
        this.couleurs[2] = couleur3;
        this.quantiteGraines = quantiteGraines;
    }
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
                return null; 
        }
    }
}
