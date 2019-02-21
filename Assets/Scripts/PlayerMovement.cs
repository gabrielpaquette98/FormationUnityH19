using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float flightSpeed = 15;
    [SerializeField] float turnSpeed = 75;
    [SerializeField] Image healthBar;
    [SerializeField] GameObject Missile;
    [SerializeField] Transform missileSpawnPoint;

    GameRules Rules { get; set; }
    public float Health { get; private set; }
    public bool IsAlive { get; private set; }
    Rigidbody playerRigidbody;

    void Start()
    {
        Rules = GameObject.FindGameObjectWithTag("Rules").GetComponent<GameRules>();
        Health = GameRules.START_HEALTH;
        IsAlive = true;
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (IsAlive)
        {
            TurningMovement();
            ForwardMovement();
            if (Input.GetKeyDown("space"))
            {
                ShootMissile();
            }
        }
    }

    void TurningMovement()
    {
        float yaw = turnSpeed * Input.GetAxis("Yaw") * Time.deltaTime;
        float pitch = turnSpeed * Input.GetAxis("Pitch") * Time.deltaTime;
        float roll = turnSpeed * Input.GetAxis("Roll") * Time.deltaTime * 2;
        transform.Rotate(-pitch, yaw, -roll);
    }

    void ForwardMovement()
    {
        //La position: On y ajoute 1 droit devant, multiplié par le ratio d'input (-1 à 1), la vitesse et le temps
        if (Input.GetAxis("Vertical") > 0)
            transform.position += transform.forward * Input.GetAxis("Vertical") * flightSpeed * Time.deltaTime;
    }

    void ShootMissile()
    {
        GameObject missile = Instantiate(Missile, missileSpawnPoint) as GameObject;
        missile.transform.parent = null;
    }

    void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
            IsAlive = false;
        }
        healthBar.fillAmount = Health / GameRules.START_HEALTH;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid" && collision.collider.enabled)
        {
            collision.collider.enabled = false;
            Destroy(collision.gameObject);
            TakeDamage(3);
        }
    }
}
