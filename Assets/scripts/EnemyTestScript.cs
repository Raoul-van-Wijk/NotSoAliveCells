using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestScript : MonoBehaviour
{
    public SpriteRenderer sprite;

    /// <summary>
    /// Variable used to keep track of enemy hp
    /// </summary>
    public float originalHP,
                  hp;


    private float frozenMultiplier = 1;


    /// <summary>
    /// Variable used to keep track of effect timers
    /// </summary>
    private float poisenTimer,
                  burningTimer,
                  shockedTimer,
                  frozenTimer;


    /// <summary>
    /// Variable used to store the statusEffect
    /// </summary>
    private StatusEffects statusEffect;

    /// <summary>
    /// Variable used to store a coroutine so that you can stop it
    /// </summary>
    private Coroutine frozenCoroutine,
                      burningCoroutine,
                      shockedCoroutine,
                      poisenCoroutine;


    /// <summary>
    /// bool used to keep track if an enemy is frozen or not
    /// </summary>
    private bool isFrozen;

    // Start is called before the first frame update
    void Start()
    {
        hp = originalHP;
        Debug.Log(hp);
    }

    // Update is called once per frame
    void Update()
    {
        if(isFrozen)
        {
            // Disable enemy movement
        }
    }

    /// <summary>
    /// Fuction used to manage the damage that is being applied to an enemy
    /// </summary>
    /// <param name="dmg">used to be sent to TakeDamage() function</param>
    /// <param name="statusEffect">used to store statusEffect of weapon</param>
    public void ManageDamage(float dmg, StatusEffects statusEffect)
    {
        this.statusEffect = statusEffect;
        TakeDamage(dmg);
        StatusEffect();
    }

    /// <summary>
    /// Function used to dmg an enemy
    /// </summary>
    /// <param name="dmg">Param used to inflict dmg to enemy</param>
    public void TakeDamage(float dmg)
    {
        hp -= dmg * frozenMultiplier;
        StartCoroutine(ChangeColor());

        if (hp <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Function used to destroy enemy once it dies
    /// </summary>
    private void Die()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Function used to change enemy color on hit
    /// </summary>
    private IEnumerator ChangeColor()
    {
        Color orColor = new Color(0, 0 , 255);
        sprite.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(0.2f);
        sprite.color = orColor;
    }

    /// <summary>
    /// Function used to handle the statuseffects inflicted on enemy
    /// </summary>
    private void StatusEffect()
    {
        switch (statusEffect)
        {
            case StatusEffects.fire:
                burningTimer += Random.Range(4f, 8f);

                // When an enemy is frozen and gets hit with fire weapon
                // Stop burning effect
                if (frozenTimer != 0)
                {
                    frozenTimer = 0;
                    TakeDamage(Random.Range(1f, 10f));
                    isFrozen = false;
                    StopCoroutine(frozenCoroutine);
                }

                // start burning coroutine
                burningCoroutine = StartCoroutine(EffectDMG(burningTimer, 5f));
                burningTimer -= Time.deltaTime;
                break;
            case StatusEffects.poisen:
                poisenTimer += Random.Range(8f, 12f);

                // start poisen coroutine
                poisenCoroutine = StartCoroutine(EffectDMG(poisenTimer, 2.5f));
                poisenTimer -= Time.deltaTime;
                break;
            case StatusEffects.electric:
                shockedTimer += Random.Range(2f, 6f);

                // start shoked coroutine
                shockedCoroutine = StartCoroutine(EffectDMG(shockedTimer, 6f));
                shockedTimer -= Time.deltaTime;
                break;
            case StatusEffects.freeze:
                if(frozenTimer == 0)
                {
                    frozenTimer = Random.Range(3f, 6f);
                    frozenMultiplier = 1.25f;

                    // When an enemy is burning and gets hit with freeze weapon
                    // Stop burning effect
                    if(burningTimer != 0)
                    {
                        StopCoroutine(burningCoroutine);
                    }

                    // Start a Coroutine to Freeze the enemy
                    frozenCoroutine = StartCoroutine(FreezeEnemy(frozenTimer));
                }
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// While an effect is active on enemy call this function in a Coroutine,   
    /// so that enemy keeps on taking damage based on that effect
    /// </summary>
    /// <param name="timer">var used to keep track of how long the effect needs to be inflicted</param>
    /// <param name="dmg">amount of damage the needs to be inflicted every seconds</param>
    /// <returns>WaitForSeconds(1f)</returns>
    private IEnumerator EffectDMG(float timer, float dmg)
    {
        for(float i = timer; i > 0; i--)    
        {
            TakeDamage(dmg);
            Debug.Log(hp);
            StartCoroutine(ChangeColor());
            yield return new WaitForSeconds(1f);
        }
     //   StopCoroutine(EffectDMG);
    }

    /// <summary>
    /// Function used to freeze an enemy
    /// </summary>
    /// <param name="frozenTimer">time enemy needs to be frozen for</param>
    /// <returns>WaitForSeceonds(frozenTimer)</returns>
    private IEnumerator FreezeEnemy(float frozenTimer)
    {
        TakeDamage(Random.Range(4f, 8f));
        // Disable Enemy movement
        isFrozen = true;

        yield return new WaitForSeconds(frozenTimer);

        // Enable Enemy movement
        isFrozen = false;
        frozenMultiplier = 1f;
        this.frozenTimer = 0;
    }


    /// <summary>
    /// Function used to adjust enemy strength bases on the current difficulty
    /// </summary>
    /// <param name="difficulty">Param used to keep track of difficulty</param>
    public void IncreaseStrengthByDifficulty(float difficulty)
    {
        originalHP *= difficulty;
        // dmg += (difficulty / 2);
    }
}
