using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameController gameController;
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Cible")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            gameController.score++;
            Debug.Log("Score: " + gameController.score);
        }
        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
