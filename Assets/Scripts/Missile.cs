using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    GameRules rules;

    void Start()
    {
        rules = GameObject.FindGameObjectWithTag("Rules").GetComponent<GameRules>();
        GetComponent<Rigidbody>().velocity += transform.forward * 150;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid" && !collision.gameObject.GetComponent<Asteroid>().IsHit)
        {
            collision.gameObject.GetComponent<Asteroid>().IsHit = true;
            collision.gameObject.GetComponent<Asteroid>().Explode();
            StartCoroutine(GameRules.timerDeleteCoroutine(0.5f, gameObject));
            rules.IncreaseScore();
        }
    }
}
