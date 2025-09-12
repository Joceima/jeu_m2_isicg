using UnityEngine;

public class Fire : MonoBehaviour
{
    Transform playerCamera;
    [SerializeField] int projectileSpeed = 10;
    [SerializeField] GameObject projectile;

    private void Start()
    {
        playerCamera = transform.Find("PlayerCamera");
    }

    void Update()
    {
    }

    void OnAttack()
    {
        GameObject projectileInstance = Instantiate(projectile, transform.position + playerCamera.forward * 0.6f, transform.rotation);
        projectileInstance.GetComponent<Rigidbody>().linearVelocity = playerCamera.forward * projectileSpeed;
        Destroy(projectileInstance, 10.0f);
    }
}
