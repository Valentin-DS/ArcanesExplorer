using System.ComponentModel;

[System.Serializable]
public class SaveData
{
    public float[] playerPosition;
    public float playerFood;
    public float playerWater;
    public float playerRest;
    public int currentMissionNumber;
    public bool[] goals;
    /*
    public List<float[]> trapsLocation;
    public float[] trapsRegenTime;
    public List<float[]> berryTreesLocation;
    public float[] berryTreesRegenTime;
    public List<float[]> loggingTreesLocation;
    public float[] loggingTreesRegenTime;
    */

    //Sauvegarder objectifs/Missions
    public string[] ingredients;
    public int[] ingredientsAmounts;
    public string[] objects;
    public int[] objectsAmounts;
    public string[] tools;
    public int[] toolsAmounts;

    public SaveData()
    {
        this.playerPosition = new float[3];
        this.playerPosition[0] = PlayerMovement.Instance.transform.position.x;
        this.playerPosition[1] = PlayerMovement.Instance.transform.position.y;
        this.playerPosition[2] = PlayerMovement.Instance.transform.position.z;
        this.playerFood = SanteJoueur.Instance.Nourriture;
        this.playerWater = SanteJoueur.Instance.Eau;
        this.playerRest = SanteJoueur.Instance.Repos;
        for (int i = 0; i < MissionGUI.Instance.Missions.Count; i++)
        {
            if (MissionGUI.Instance.Missions[i].IsCompleted)
            {
                this.currentMissionNumber++;
            }
        }

        this.goals = MissionGUI.Instance.Missions[this.currentMissionNumber].GoalsAchievement.ToArray();
        this.ingredients = new string[Inventaire.Instance.liste_Ingredient_Inventaire.Count];
        this.ingredientsAmounts = new int[Inventaire.Instance.liste_Ingredient_Inventaire.Count];
        for (int i=0; i < Inventaire.Instance.liste_Ingredient_Inventaire.Count; i++)
        {
            this.ingredients[i] = Inventaire.Instance.liste_Ingredient_Inventaire[i].nom_Objet;
            this.ingredientsAmounts[i] = Inventaire.Instance.liste_Ingredient_Inventaire[i].quantite_Actuelle;
        }

        this.objects = new string[Inventaire.Instance.liste_Objet_Inventaire.Count];
        this.objectsAmounts = new int[Inventaire.Instance.liste_Objet_Inventaire.Count];
        for (int i=0; i < Inventaire.Instance.liste_Objet_Inventaire.Count; i++)
        {
            this.objects[i] = Inventaire.Instance.liste_Objet_Inventaire[i].nom_Objet;
            this.objectsAmounts[i] = Inventaire.Instance.liste_Objet_Inventaire[i].quantite_Actuelle;
        }

        this.tools = new string[Inventaire.Instance.liste_Outil_Inventaire.Count];
        this.toolsAmounts = new int[Inventaire.Instance.liste_Outil_Inventaire.Count];
        for (int i=0; i < Inventaire.Instance.liste_Outil_Inventaire.Count; i++)
        {
            this.tools[i] = Inventaire.Instance.liste_Outil_Inventaire[i].nom_Objet;
            this.toolsAmounts[i] = Inventaire.Instance.liste_Outil_Inventaire[i].quantite_Actuelle;
        }
    }
}
