using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] public const float START_HEALTH = 5;
    [SerializeField] public const int SCORE_PER_ASTEROID = 100;
    [SerializeField] public const int DAMAGE_PER_ASTEROID = 2;
    SphereCollider coll;

    public int Score { get; set; }
    void Start()
    {
        Score = 0;
        coll = GetComponent<SphereCollider>();
    }
    
    public Transform GetPlayerPosition() {
        return player.transform;
    }
    public float GetPlayerHealth() {
        return player.GetComponent<PlayerMovement>().Health;
    }
    public static IEnumerator timerDeleteCoroutine(float time, GameObject objectToDelete)
    {
        yield return new WaitForSeconds(time);
        Destroy(objectToDelete);
    }
    public void IncreaseScore()
    {
        Score += SCORE_PER_ASTEROID;
    }
}
