using UnityEngine;

public class Timer : MonoBehaviour
{
    public double tempsCourant;
    public double tempsLimite;

    void Update()
    {
        tempsCourant += Time.deltaTime;
        //Debug.Log(tempsCourant);
    }

    public void Reinitialise()
    {
        tempsCourant = 0;
    }

    public void Randomise(int min, int max)
    {
        tempsLimite = Random.Range(min, max);
    }
}
