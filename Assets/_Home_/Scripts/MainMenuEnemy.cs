using ExtensionMethods;
using UnityEngine;

public class MainMenuEnemy : MonoBehaviour
{
    public bool goesLeft;
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        GetComponent<Animator>().SetBool("isWalking", true);
        if(goesLeft)
            transform.localScale = transform.localScale.WithX(-transform.localScale.x);
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (goesLeft)
        {
            transform.position += (Vector3) Vector2.left * speed * Time.deltaTime;
        }else
        {
            transform.position += (Vector3) Vector2.right * speed * Time.deltaTime;
        }
    }
}
