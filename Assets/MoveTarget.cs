using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    new Transform transform;
    float time = 0;
    Vector3 translation;
    void Start()
    {
        transform = GetComponent<Transform>();
        time = Time.time;
        translation = new Vector3(1f, 0, 0f);
    }


    // Update is called once per frame
    void Update()
    {
        if(Time.time - time > 5.0f)
        {
            translation = -translation;
            time = Time.time;
        }
        transform.Translate(translation * Time.deltaTime);
    }
}
