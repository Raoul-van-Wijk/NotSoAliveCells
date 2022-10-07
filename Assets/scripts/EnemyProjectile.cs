using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    private Vector3 targetPosition;
    public float projectileSpeed;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = FindObjectOfType<PlayerController>().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, projectileSpeed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
    }
}
