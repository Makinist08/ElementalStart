using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float coolDown;

    [SerializeField]
    private GameObject dodgeball;

    private float timeOfLastShot;
    private float time;
    void Start()
    {
        timeOfLastShot = 0;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = transform.position - player.transform.position;
        
        
        
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        if (timeOfLastShot + coolDown < time)
        {
            Instantiate(dodgeball, transform.position, targetRotation);
            timeOfLastShot = time;
        }
        time += Time.deltaTime;
    }
}
