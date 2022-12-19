using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    private Vector3 targetPosition;
    public float projectileSpeed;
    public Rigidbody2D r;
    public PlayerController playerCon;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
        r = GetComponent<Rigidbody2D>();
        targetPosition = FindObjectOfType<PlayerController>().transform.position;
        playerCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, projectileSpeed * Time.deltaTime);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        // layer 3 is ground layer
        if (collision.gameObject.layer == 3)
        {
            Destroy(this.gameObject);
            //Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            playerCon.TakeDamage(3f, gameObject);
            Destroy(this.gameObject);
            //Destroy(collision.gameObject);
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
    }
}
