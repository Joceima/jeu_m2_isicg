using UnityEngine;

public class GlobalVolumePersist : MonoBehaviour
{
    private static GlobalVolumePersist instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
}
