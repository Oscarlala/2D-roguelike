using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Roaming,

    Following, 

    Dying
};

public class EnemyController : MonoBehaviour
{
    GameObject player;
    public EnemyState currentState = EnemyState.Roaming;
    public float range;
    public float moveSpeed;
    private bool chosenDirection = false;
    private bool dead = false;
    private Vector3 randDirection;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case (EnemyState.Roaming):
                Roam();
                break;
            case (EnemyState.Following):
                Follow();
                break;
            case (EnemyState.Dying):
                break;
        }   

        if(currentState != EnemyState.Dying && InRangeOfPlayer(range))
        {
            currentState = EnemyState.Following;
        } 
        else if(currentState != EnemyState.Dying && !InRangeOfPlayer(range))
        {
            currentState = EnemyState.Roaming;
        }
    }

    private bool InRangeOfPlayer(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        chosenDirection = true;
        yield return new WaitForSeconds(Random.Range(2f, 7f));
        randDirection = new Vector3(0, 0, Random.Range(0, 360));
        chosenDirection = false;
    }

    void Roam()
    {
        if(!chosenDirection)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * moveSpeed * Time.deltaTime;
        if(InRangeOfPlayer(range))
        {
            currentState = EnemyState.Following;
        }
    }

    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
