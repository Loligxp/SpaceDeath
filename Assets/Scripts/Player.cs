using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    private Vector2 _moveInput;
    private Vector2 _mousePositionInput;
    private bool shooting;
    private AudioSource Audio;

    [Header("stats")] 
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private SpriteRenderer SPR;
    
    [Header("References")]
    private PlayerInput _playerInput;
    private Rigidbody2D RB2D;
    
    [SerializeField] private Transform graphicPivot;
    [SerializeField] private Transform velocityGraphic;
    
    private void Awake()
    {
        _playerInput = new PlayerInput();
        GameManager.Instance.playerObject = this.gameObject;
        RB2D = GetComponent<Rigidbody2D>();
        Audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Fire.started += shootStart => { shooting = true;};
        _playerInput.Player.Fire.canceled += shootStart => { shooting = false;};
        _playerInput.Player.Pause.started += PauseGame;

    }
    

    private void FixedUpdate()
    {
        RB2D.AddForce(_moveInput * Time.deltaTime * moveSpeed, ForceMode2D.Impulse);
    }

    private void PauseGame(InputAction.CallbackContext ctx)
    {
        UI_Manager.Instance.PauseGame();
    }

    private void Update()
    {
        if (currentHealth < 0)
        {
            UI_Manager.Instance.changeUIState(UI_Manager.UI_State.Death);
            moveSpeed = 0;
            return;
        }
        
        _moveInput = _playerInput.Player.Move.ReadValue<Vector2>();
        _moveInput.Normalize();
        _mousePositionInput = _playerInput.Player.MousePosition.ReadValue<Vector2>();

        velocityGraphic.up = RB2D.velocity;

        if (currentHealth < maxHealth)
        {
            currentHealth += (maxHealth / 100) * Time.deltaTime;
        }

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        
        var mousePosition = Camera.main.ScreenToWorldPoint(_mousePositionInput);
        var mouseDirVector = new Vector2
        (
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );
        graphicPivot.up = mouseDirVector;
        
        if(shooting)
            WeaponsManager.Instance.ShootPrimaryWeapons(graphicPivot.transform, velocityGraphic.transform);

    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
            currentHealth -= col.GetComponent<Enemy>().damage;
            CamShake.Instance.Shake(3);
            Audio.pitch = Random.Range(0.9f, 1.1f);
            Audio.Play();
            StartCoroutine(damageFlash());

        }

    }
    
    IEnumerator damageFlash()
    {
        transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        SPR.color = Color.red;
        for (float i = 0; i < 25; i += 1)
        {
            transform.localScale = new Vector3(0.9f + (i) / 250, 0.9f + (i) / 250, 0.9f + (i) / 250);
            SPR.color = new Color(1f, i / 25, i / 25);

            yield return new WaitForFixedUpdate();
        }
    }

    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }
}
