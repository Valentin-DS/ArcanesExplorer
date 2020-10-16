using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public static class SaveSystem
{
    static string Path = Application.persistentDataPath + "/aedata.aex";
    static BinaryFormatter Formatter;
    static FileStream Stream;

    public static void Save()
    {
        Stream = new FileStream(Path, FileMode.Create);
        SaveData data = new SaveData();
        Formatter = new BinaryFormatter();
        Formatter.Serialize(Stream, data);
        Stream.Close();
        Debug.Log("Le jeu a été sauvegardé");
    }

    public static void Load()
    {
        if (File.Exists(Path))
        {
            Stream = new FileStream(Path, FileMode.Open);
            Formatter = new BinaryFormatter();
            SaveData data = Formatter.Deserialize(Stream) as SaveData;
            Stream.Close();
            SanteJoueur.Instance.Nourriture = data.playerFood;
            SanteJoueur.Instance.Eau = data.playerWater;
            SanteJoueur.Instance.Repos = data.playerRest;
            MissionGUI.Instance.MissionNumber = data.currentMissionNumber;
            MissionGUI.Instance.Missions[MissionGUI.Instance.MissionNumber].GoalsAchievement = data.goals.ToList();
            MissionGUI.Instance.UpdatePanel();
            PlayerMovement.Instance.transform.position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
            for (int i = 0; i < data.ingredients.Length; i++)
            {
                for(int j=0; j < data.ingredientsAmounts[i]; j++)
                {
                    Inventaire.Instance.ajout_Ingredient_Inventaire(data.ingredients[i]);
                }
            }

            for (int i = 0; i < data.objects.Length; i++)
            {
                for (int j = 0; j < data.objectsAmounts[i]; j++)
                {
                    Inventaire.Instance.ajout_Objet_Inventaire(data.objects[i]);
                }
            }

            for (int i = 0; i < data.tools.Length; i++)
            {
                for (int j = 0; j < data.toolsAmounts[i]; j++)
                {
                    Inventaire.Instance.ajout_Outil_Inventaire(data.tools[i]);
                }
            }

            Debug.Log("La sauvegarde est chargée");
        }
        else
        {
            Debug.LogError("Fichier de sauvegarde introuvable dans " + Path);
        }
    }
}

