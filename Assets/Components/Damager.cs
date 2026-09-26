using UnityEngine;

public class Damager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    private int damage = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        Debug.Log(collision.gameObject.GetComponent<PlayerHealth>());

        if (playerHealth != null)
        {
            playerHealth.TakeDamge(damage);
        }

    }
}
