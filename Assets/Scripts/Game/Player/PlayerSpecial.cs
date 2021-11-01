using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecial : MonoBehaviour
{
    private Player playerScript;
    [SerializeField, Range(5.0f, 30.0f)] private float drinkCooldown = 0.5f;
    [SerializeField, Range(5.0f, 30.0f)] private float drunkDuration = 0.5f;
    [SerializeField, Range(3.0f, 10.0f)] private float hangoverDuration = 1f;
    [SerializeField, Range(0, 5f)]private float drunkDamageMultiplier;
    [SerializeField, Range(0, 1f)]private float hangoverDamageMultiplier;
    [SerializeField, Range(0, 2f)]private float drunkSpeedMultiplier;
    [SerializeField, Range(0, 1f)]private float hangoverSpeedMultiplier;
    [SerializeField, Range(0, 2f)]private float drunkJumpMultiplier;
    [SerializeField, Range(0, 1f)]private float hangoverJumpMultiplier;
    [HideInInspector]public bool drunk;
    [HideInInspector]public bool hangover;
    private float nextAbility;
    private float hangoverEnd;
    private float drunkTimer;
    [SerializeField]private Transform shootingPoint;
    [SerializeField]private GameObject bulletPrefab;
    [SerializeField, Range(1.0f, 30.0f)] private float shootCooldown = 0.5f;
    [HideInInspector]public int abilityNum = 0;
    private Transform bulletParent;

    #region Getters & Setters
    public bool GetAbilityCooldown()
    {
        return Time.time >= this.nextAbility;
    }
    public void SetSelectedItem(int i)
    {
        this.abilityNum = i;
        this.playerScript.healthBar.UpdateImage(this.abilityNum);
    }
    #endregion

    void Awake() => this.playerScript = GetComponent<Player>();
    void Start() => Load();
    void Update()
    {
        if (Time.time >= this.drunkTimer && this.drunk)
        {
            this.drunk = false;
            this.hangover = true;
            this.hangoverEnd = Time.time + this.hangoverDuration;
        }
        if (Time.time <= this.hangoverEnd && this.hangover)
        {
            this.playerScript.attackScript.multiplier = this.hangoverDamageMultiplier;
            this.playerScript.movementScript.speedMultiplier = this.hangoverSpeedMultiplier;
            this.playerScript.movementScript.jumpMultiplier = this.hangoverJumpMultiplier;
        } else if (this.hangover)
        {
            this.playerScript.attackScript.multiplier = 1;
            this.playerScript.movementScript.speedMultiplier = 1;
            this.playerScript.movementScript.jumpMultiplier = 1;
            this.hangover = false;
        }
    }
    public void Vodka()
    {
        this.drunk = true;
        this.playerScript.attackScript.multiplier = this.drunkDamageMultiplier;
        this.playerScript.movementScript.speedMultiplier = this.drunkSpeedMultiplier;
        this.playerScript.movementScript.jumpMultiplier = this.drunkJumpMultiplier;
        this.playerScript.stateScript.SetState("Drinking", false);
        this.drunkTimer = Time.time + this.drunkDuration;
        this.nextAbility = Time.time + this.drunkDuration + this.hangoverDuration + this.drinkCooldown;
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
        this.nextAbility = Time.time + this.shootCooldown;
                this.playerScript.stateScript.SetState("Shooting", false);
    }
    public void ChangeAbility()
    {
        if (GameManager.gM.abilityCount == 2)
        {
            if (this.abilityNum == 1)
                SetSelectedItem(2);
            else if (this.abilityNum == 2)
                SetSelectedItem(1);
        } else if(GameManager.gM.abilityCount == 0)
        {
            SetSelectedItem(0);
        }
        GameManager.gM.SaveGame();
    }
    public void SetAbility()
    {
        if (GameManager.gM.abilityCount == 1)
            if (GameManager.gM.GetAbilityAt(1))
                SetSelectedItem(1);
            else if(GameManager.gM.GetAbilityAt(2))
                SetSelectedItem(2);
        GameManager.gM.SaveGame();
    }
    private void Load()
    {
        GameData data = SaveAndLoadGame.Load();
        if (data != null)
            SetSelectedItem(data.selectedItem);
    }
}
