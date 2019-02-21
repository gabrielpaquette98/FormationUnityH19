using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Transform Attributes")]
    [SerializeField] float minScale = 4f;
    [SerializeField] float maxScale = 4.5f;
    [SerializeField] int rotationAngle = 45;
    [SerializeField] float movementRange = 0.4f;

    [Header("Trigger Transform Modifications")]
    [SerializeField] bool enableRotation = true;
    [SerializeField] bool enableTranslation = true;

    Vector3 rotation;
    Vector3 translation;

    Rigidbody asteroidRigidbody;

    public bool IsHit { get; set; }
    GameRules rules;
    void Start()
    {
        rules = GameObject.FindGameObjectWithTag("Rules").GetComponent<GameRules>();
        asteroidRigidbody = gameObject.GetComponent<Rigidbody>();
        transform.localScale = new Vector3(Random.Range(minScale, maxScale), 
                                           Random.Range(minScale, maxScale), 
                                           Random.Range(minScale, maxScale));
        if (enableRotation)
            rotation = new Vector3(Random.Range(-rotationAngle, rotationAngle),
                                   Random.Range(-rotationAngle, rotationAngle),
                                   Random.Range(-rotationAngle, rotationAngle));
        if (enableTranslation)
        {
            translation = new Vector3(Random.Range(-movementRange, movementRange),
                          Random.Range(-movementRange, movementRange),
                          Random.Range(-movementRange, movementRange));
            asteroidRigidbody.velocity = translation;
        }
    }
    
    void Update()
    {
        if (enableRotation)
            transform.Rotate(rotation * Time.deltaTime);
    }

    public void Explode()
    {
        StartCoroutine(GameRules.timerDeleteCoroutine(0.7f, gameObject));
    }
}
