using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecial : MonoBehaviour
{
    private Player playerScript;
    [SerializeField, Range(5.0f, 30.0f)] private float drinkCooldown = 0.5f;
    [SerializeField, Range(5.0f, 30.0f)] private float drunkDuration = 0.5f;
    [SerializeField, Range(3.0f, 10.0f)] private float hangoverDuration = 1f;
    [SerializeField, Range(0, 5f)]private float damageMultiplier;
    [SerializeField, Range(0, 1f)]private float hangoverMultiplier;
    [HideInInspector]public bool drunk;
    [HideInInspector]public bool hangover;
    private float nextDrink;
    private float hangoverEnd;
    private float drunkTimer;
    [SerializeField]private Transform shootingPoint;
    [SerializeField]private GameObject bulletPrefab;
    [SerializeField, Range(1.0f, 30.0f)] private float shootCooldown = 0.5f;
    private float nextShoot;
    private Transform bulletParent;

    #region Getters & Setters
    public bool GetDrinkingCooldown()
    {
        return Time.time >= this.nextDrink;
    }
    public bool GetShootingCooldown()
    {
        return Time.time >= this.nextShoot;
    }
    #endregion

    void Awake()
    {
        this.playerScript = GetComponent<Player>();
    }
    void Update()
    {
        if (Time.time >= this.drunkTimer && this.drunk)
        {
            this.playerScript.attackScript.multiplier = 1;
            this.drunk = false;
            this.hangover = true;
            this.hangoverEnd = Time.time + this.hangoverDuration;
        }
        if (Time.time <= this.hangoverEnd && this.hangover)
        {
            this.playerScript.attackScript.multiplier = this.hangoverMultiplier;
        } else if (this.hangover)
        {
            this.nextDrink = Time.time + this.drinkCooldown;
            this.playerScript.attackScript.multiplier = 1;
            this.hangover = false;
        }
    }
    public void Vodka()
    {
        this.drunk = true;
        this.playerScript.attackScript.multiplier = this.damageMultiplier;
        this.playerScript.stateScript.SetState("Drinking", false);
        this.drunkTimer = Time.time + drunkDuration;
    }
    public void Arquebus()
    {
        this.playerScript.playerAudio.Play("Shoot");
        bulletParent = GameObject.Find("/Enemies/BulletParent").GetComponent<Transform>();
        GameObject bullet = Instantiate(this.bulletPrefab, this.shootingPoint.position, Quaternion.identity, this.bulletParent);
        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        if (this.playerScript.movementScript.facingRight)
            bulletScript.direction= 1;
        else
            bulletScript.direction = -1;
    }
    public void EndShoot()
    {
        this.playerScript.stateScript.SetState("Shooting", false);
        this.nextShoot = Time.time + this.shootCooldown;
    }
}
