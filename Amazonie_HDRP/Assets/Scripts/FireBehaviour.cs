using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    private bool isCooked;

    private void Start()
    {
        this.isCooked = false;
    }

    private void OnTriggerStay(Collider other)
    {       
        // Si le joueur est dans la zone du feu et qu'il appuie sur entrée
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // S'il a du  poulet cru
            if (Craft_Book_Manager.Instance.number_Ingredient("Raw_Chicken") > 0)
            {
                //On réduit de 1 son nombre dans l'inventaire
                objet_Inventaire food = Inventaire.Instance.liste_Ingredient_Inventaire.Find(x => x.nom_Objet == "Raw_Chicken");
                food.quantite_Actuelle--;
                //Et si ce nombre est à 0 on supprime l'ingrédient de l'inventaire
                if (food.quantite_Actuelle == 0)
                {
                    Inventaire.Instance.liste_Ingredient_Inventaire.Remove(food);
                }

                //Au bout de 5 secondes, on appelle la methode Cook qui détermine si le poulet est cuit ou non
                Invoke("Cook", 5f);
            }
            // Si c'est cuit, on ajoute le poulet cuit dans l'inventaire et l'objet n'est plus à récupérer
            else if(this.isCooked)
            {
                Inventaire.Instance.ajout_Objet_Inventaire("Roasted_Chicken");
                this.isCooked = false;
            }
        }
        
    }

    public void Cook()
    {
        this.isCooked = true;
        Debug.Log("C'est prêt !");
    }
    
}
