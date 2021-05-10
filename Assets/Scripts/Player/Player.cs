using Assets.Scripts.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public int maxHealth = 3;
    public int currentDamageLevel = 0;
    public float knockBackForce = 50.0f;
    public GameController gameController;
    public bool isMovementLocked;
    public PlayerIO playerIO;
    public Sprite[] skins = new Sprite[5];
    private Color originalColor;
    public AudioClip damageClip;
    public AudioClip agonizingDamageClip;
    private AudioSource soundEffectsPlayer;
    private AudioController audioController;
    public bool isImmune = false;
    public GameTimer timer;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerIO = FileIO.loadPlayer();
        GetComponent<SpriteRenderer>().sprite = skins[playerIO.SkinChoice-1];
        originalColor = GetComponent<Renderer>().material.color;
        soundEffectsPlayer = FindObjectOfType<ReferenceManager>().soundEffectPlayer;
        timer = FindObjectOfType<ReferenceManager>().gameTimer;
        audioController = soundEffectsPlayer.GetComponent<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDamageLevel > maxHealth)
        {
            isMovementLocked = true;
            gameController.executeDeath();
        }
        else if (rb.position.y < -5)
        {
            isMovementLocked = true;
            gameController.executeDeath();
        }

        if (isMovementLocked && timer.remainingTime <= 0 && Math.Abs(rb.velocity.x) <= 0.1f) 
        {
            isMovementLocked = true;
            gameController.executeDeath(); 
        }
    }

    //Plays the effects and code associated with the player marble taking damage
    public void takeDamage()
    {
        //Increment Damage Level and Set damage flash
        isImmune = true;
        currentDamageLevel += 1;
        GetComponent<Renderer>().material.color = Color.red;

        //Play Sound Effect and reset flash color
        if (currentDamageLevel >= 4) audioController.playOnce(agonizingDamageClip);
        else audioController.playOnce(damageClip);

        //Reset Values
        Invoke("ResetColor", 0.2f);
        Invoke("ResetImmunity", 1.0f);
    }

    private void ResetColor()
    {
        GetComponent<Renderer>().material.color = originalColor;
    }

    private void ResetImmunity()
    {
        isImmune = false;
    }
}
