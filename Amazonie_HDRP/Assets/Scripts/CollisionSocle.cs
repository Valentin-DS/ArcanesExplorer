using UnityEngine;

/**
 * @class CollisionSocle
 * La classe CollisionSocle permet de déterminer et d'afficher si le socle touche le sol ou non
 * @author Valentin
 */
public class CollisionSocle : MonoBehaviour
{
    /**
     * Material correspondant à une position valide du socle (il touche le sol)
     */
    [SerializeField]
    private Material MaterialPositionValide;
    /**
     * Material correspondant à une position invalide du sol (il ne touche pas le sol)
     */
    [SerializeField]
    private Material MaterialPositionInterdite;
    /**
     * Composant MeshRenderer du socle
     */
    private MeshRenderer MeshRenderer;

    /**
     * Initialisation (avant Start) des paramètres
     */
    private void Awake()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
    }

    /**
     * Détecte la collision du socle au sol
     * @param other : Collider qui est detecté à la collision
     */
    private void OnTriggerEnter(Collider other)
    {
        MeshRenderer.material = MaterialPositionValide;
        gameObject.tag = "Collision";
    }

    /**
     * Détecte la sortie du socle du sol
     * @param other : Collider qui est detecté à la collision
     */
    private void OnTriggerExit(Collider other)
    {
        MeshRenderer.material = MaterialPositionInterdite;
        gameObject.tag = "Non-Collision";
    }
}
