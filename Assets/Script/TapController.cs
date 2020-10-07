using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TapController : MonoBehaviour
{
    public delegate void PlayerDeleget();
    public static event PlayerDeleget OnPlayerDied;
    public static event PlayerDeleget OnPlayerScored; 

    private float tapForce = 200;
    private float tiltSmooth = 2;
    public Vector3 startPos;

    public AudioSource tapAudio;
    public AudioSource scoreAudio;
    public AudioSource dieAudio;

    Rigidbody2D rigidbody2D;
    Quaternion downRotation;
    Quaternion forwardRotation;
    GameManager game;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0, 0, -90);
        forwardRotation = Quaternion.Euler(0, 0, 35);
        game = GameManager.Instance;
        rigidbody2D.simulated = false;
    }
    private void OnEnable()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }
    private void OnDisable()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }
    void OnGameStarted()
    {
        rigidbody2D.velocity = Vector3.zero;
        rigidbody2D.simulated = true;
    }
    void OnGameOverConfirmed()
    {
        transform.localPosition = startPos;
        transform.rotation = Quaternion.identity;
    }
    void Update()
    {
        if (game.GameOver) return;
        if (Input.GetMouseButtonDown(0))
        {
            tapAudio.Play();
            transform.rotation = forwardRotation;
            rigidbody2D.velocity = Vector3.zero;
            rigidbody2D.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Scorezone")
        {
            //Register the score and play sound
            OnPlayerScored();//Event for Game Manager
            scoreAudio.Play();
        }
        if(col.gameObject.tag == "Deadzone")
        {
            rigidbody2D.simulated = false;
            //Register the dead event and play sound
            OnPlayerDied();//Event for Game Manager
            dieAudio.Play();
        }
    }
}
//Design and Made by Rathod Studio