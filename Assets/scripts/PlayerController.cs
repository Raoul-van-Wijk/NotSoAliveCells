using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// This script will be used for combat

public class PlayerController : MonoBehaviour
{
    // Variable to set immortal state
    public bool immortal = false;
    private PlayerMovement playerMovement;
 
    private float health;
    [SerializeField] float maxHealth;

    // vars for damage slider beneath health slider
    private float currentHealth;
    [SerializeField] float healthReduction = 100f;
    [SerializeField] float initialDelay = 0.3f;
    [SerializeField] float delayHealthReduction = 0f;

    public Slider sliderHealth;
    public Slider sliderDamage;

    [SerializeField] private AudioClip deathSound, backgroundMusic;

    [SerializeField] Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        health = currentHealth = maxHealth;

        AudioManager.Instance.PlayBackground(backgroundMusic);
    }

    // Update is called once per frame
    void Update()
    {
        // yellow bar below red bar for cool decrease effect
        // if currentHealth is bigger than health, keep lowering it by healthReduction (health/second) until it is with an interval of intervalHealthReduction
        if (currentHealth > health && Time.time > delayHealthReduction)
		{
            currentHealth -= healthReduction * Time.deltaTime;
            
            // in case currentHealth is reduced below health, set it equal to health
            if (currentHealth <= health)
                currentHealth = health;
            sliderDamage.value = currentHealth / maxHealth;
        }


        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            TakeDamage(10f);
        }

    }

	void FixedUpdate()
	{
        if (rb.position.y < 0)
        {
            GameOver();
        }
    }

	public void GameOver()
	{
        AudioManager.Instance.PlaySound(deathSound);
        SceneManager.LoadScene("GameOver");
    }

    /// <summary>
    /// Call this function whenever player needs to take damage and needs to get knocked back
    /// </summary>
    /// <param name="damage">How much damage the player should take</param>
    /// <param name="origin">Origin (source) of the damage</param>
    public void TakeDamage(float damage, GameObject origin = null)
	{
        health -= damage;

        // sets slider fill to health%
        sliderHealth.value = health / maxHealth;

        // sets initial delay
        delayHealthReduction = Time.time + initialDelay;

        if (health <= 0) GameOver();
        else
        {
            if (origin != null)
            {
                playerMovement.Knockback(origin);
            }
        }
	}

    public void SetImmortal(bool b)
    {
        immortal = b;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collision.gameObject.CompareTag("Enemy"))
		{
            // access enemy script and retrieve strength to determine how much damage it deals
            TakeDamage(collision.gameObject.GetComponent<TempEnemy>().Strength, collision.gameObject);
		}
    }
}
