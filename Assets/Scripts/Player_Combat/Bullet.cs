using UnityEngine;

public class ProjectileBullet : Projectile
{
    public float speed = 10f;
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bullet Hit: " + other.name);
        
        if (other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
        }
        
        Destroy(gameObject);
    }
}