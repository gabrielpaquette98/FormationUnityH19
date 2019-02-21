using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] Asteroid asteroid;
    [SerializeField] int asteroidPerAxis = 10;
    [SerializeField] int asteroidSpacing = 20;

    GameRules Rules { get; set; }
    

    [SerializeField] SphereCollider zoneCollider;

    void Start()
    {
        Rules = GameObject.FindGameObjectWithTag("Rules").GetComponent<GameRules>();
        PlaceAsteroids();
    }
    void Update()
    {
        zoneCollider.center = Rules.GetPlayerPosition().position;
    }

    void PlaceAsteroids()
    {
        for (int i = -asteroidPerAxis/2; i < asteroidPerAxis/2; i++)
        {
            for (int j = -asteroidPerAxis / 2; j < asteroidPerAxis/2; j++)
            {
                for (int k = -asteroidPerAxis / 2; k < asteroidPerAxis/2; k++)
                {
                    InstantiateAsteroid(i, j, k);
                }
            }
        }
    }
    void InstantiateAsteroid(int x, int y, int z)
    {
        Vector3 position = new Vector3(x + Random.Range(-asteroidSpacing / 2f, asteroidSpacing / 2f),
                                       y + Random.Range(-asteroidSpacing / 2f, asteroidSpacing / 2f),
                                       z + Random.Range(-asteroidSpacing / 2f, asteroidSpacing / 2f));

        Asteroid instanciatedAsteroid = Instantiate(asteroid, position * asteroidSpacing + transform.position, Quaternion.identity, transform) as Asteroid;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            collision.gameObject.transform.position = 
                (2*zoneCollider.center - collision.gameObject.transform.position);
        }
        if (collision.gameObject.tag == "Missile")
        {
            Destroy(collision.gameObject);
        }
    }
}
