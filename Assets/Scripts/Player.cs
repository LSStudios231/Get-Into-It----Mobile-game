using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public Button jumpButton;
    public float movementSpeed;
    public float leftRightSpeed = 0.025f;
    public AudioSource audioSource;

    public TrailRenderer trailRenderer;

    private Touch touch;

    private bool isGrounded;

    private Renderer playerRenderer;

    public Material[] Materials;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        if (!GetComponent<Collider>())
        {
            gameObject.AddComponent<BoxCollider>();
        }

        if (trailRenderer == null)
        {
            trailRenderer = gameObject.AddComponent<TrailRenderer>();
        }

        ConfigureTrailRenderer();

        playerRenderer = GetComponent<Renderer>();

        ChangeMaterial(PlayerPrefs.GetInt("SelectedSkin", 0)); 
    }

    void Update()
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 movement = new Vector3(touch.deltaPosition.x * leftRightSpeed, 0, 0);
                rb.AddForce(movement, ForceMode.VelocityChange);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GroundDown"))
        {
            isGrounded = true;
            DisableTrail();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("GroundDown"))
        {
            isGrounded = false;
            EnableTrail();
        }
    }

    public void JumpButtonIsPressed()
    {
        rb.AddForce(0f, 400f, 0f);
        audioSource.Play();
        EnableTrail();
    }

    private void EnableTrail()
    {
        if (!isGrounded)
        {
            trailRenderer.enabled = true;
        }
    }

    private void DisableTrail()
    {
        trailRenderer.enabled = false;
    }

    private void ConfigureTrailRenderer()
    {
        //Properties for the player that can be assigned in editor
    }

    private void ChangeMaterial(int skinIndex)
    {
        if (skinIndex < Materials.Length)
        {
            Material newMaterial = Materials[skinIndex];
            playerRenderer.material = newMaterial;
        }
    }
}

