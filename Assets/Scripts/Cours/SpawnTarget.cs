using UnityEngine;
using UnityEngine.UIElements;

public class SpawnTarget : MonoBehaviour
{

    [SerializeField] BoxCollider boxCollider;
    [SerializeField] GameObject target;
    [SerializeField] int numberOfTargets = 10;
    int currentNumberOfTarget = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*if (boxCollider == null)
        {
            Debug.Log("No box collider assigned in the inspector");
        }

        if(target == null)
        {
            Debug.Log("No target prefab assigned in the inspector");
        }
        SpawnSomeTargets(numberOfTargets);*/

    }

    // Update is called once per frame
    void Update()
    {
        /*currentNumberOfTarget = GameObject.FindGameObjectsWithTag("Cible").Length;
        if (currentNumberOfTarget == 0)
        {
            SpawnSomeTargets(numberOfTargets);
        }*/

        /*GameObject[] targets = GameObject.FindGameObjectsWithTag("Cible");
        if(targets.Length == 0)
        {
            SpawnSomeTargets(numberOfTargets);
            //transform.Translate(-1,0,1);
        }*/

        // Time.deltaTime ne va pas dépendre de l'horloge de la machine
    }

    public void SpawnSomeTargets(int numberOfTargets)
    {
        for(int i = 0; i < numberOfTargets; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(
                    boxCollider.bounds.min.x + target.GetComponent<Collider>().bounds.size.x / 2,
                    boxCollider.bounds.max.x - target.GetComponent<Collider>().bounds.size.x / 2),

                Random.Range(
                    boxCollider.bounds.min.y + target.GetComponent<Collider>().bounds.size.y / 2,
                    boxCollider.bounds.max.y - target.GetComponent<Collider>().bounds.size.y / 2),
                
                Random.Range(
                    boxCollider.bounds.min.z + target.GetComponent<Collider>().bounds.size.z / 2,
                    boxCollider.bounds.max.z - target.GetComponent<Collider>().bounds.size.z / 2)

                );
            //currentNumberOfTarget = numberOfTargets;
            Instantiate(target, randomPosition, Quaternion.identity);
        }
    }

}
