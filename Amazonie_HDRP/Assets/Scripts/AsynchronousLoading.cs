using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * @class AsynchronousLoading
 * La classe AsynchronousLoading correspond au chargement et déchargement progressif et asynchrone des morceaux de map en fonction du déplacement du joueur
 * @author Basile
 */
public class AsynchronousLoading : MonoBehaviour
{
    /**
     * Détecte le passage du joueur dans le trigger de chargement asynchrone
     * @param other : Collider qui est detecté à la collision
     */
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("okokok");
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }
}
