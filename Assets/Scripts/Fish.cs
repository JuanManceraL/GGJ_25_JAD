using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float speed;
    [SerializeField] private float speedRotation;
    [SerializeField] private float timeToStartSwim;
    [SerializeField] private int actualObjP;
    [SerializeField] private int actualObjR;
    [SerializeField] private int state;
    private bool changeDirection;

    [Header("State Machine")]
    [SerializeField] private bool canAttack;
    [SerializeField] private float timeToEscape;
    [SerializeField] private float timeToCalm;
    [SerializeField] private float speedEscape;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = 0;
        actualObjP = 0;
        actualObjR = 0;
        changeDirection = false;
    }


    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 0: //Nadando
                Swim();
                break;
            case 1: //Espantando (/atacando?)

                break;
            case 2: //Huyendo
                Escape();
                break;
            default:
                break;
        }

    }

    public void Attack(Transform playerPosition)
    {
        if (state == 0)
        {
            transform.LookAt(playerPosition);
            state = 1;
            //play animation
            actualObjP++;
            actualObjR++;
            if (actualObjP == points.Length)
            {
                actualObjP = 0;
                actualObjR = 0;
            }
            Invoke("Escape", timeToEscape);
            if (canAttack)
            {
                //Dañar a jugador
                StartCoroutine(AttackPlayer(playerPosition.gameObject.GetComponent<Health>()));
            }
        }
    }

    IEnumerator AttackPlayer(Health healthPlayer)
    {
        yield return new WaitForSeconds(timeToEscape*0.8f);
        healthPlayer.Damage();
    }

    private void Escape()
    {
        state = 2;
        transform.LookAt(points[actualObjP].position);
        transform.position = Vector3.MoveTowards(transform.position, points[actualObjP].position, speed * speedEscape * Time.deltaTime);
        Invoke("Calm", timeToCalm);
    }

    private void Calm()
    {
        state = 0;
    }



    private void Swim()
    {
        //play animation

        transform.position = Vector3.MoveTowards(transform.position, points[actualObjP].position, speed * Time.deltaTime);

        //transform.LookAt(points[actualObj].position);
        Vector3 direction = (points[actualObjR].position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);

        if ((transform.position - points[actualObjP].position).magnitude <= 1f)
        {
            //Debug.Log("Llegué");
            if (!changeDirection)
            {
                actualObjR++;
                if (actualObjR >= points.Length)
                {
                    actualObjR = 0;
                }
                changeDirection = true;
                Invoke("ChangingDirection", timeToStartSwim);
            }
        }
    }

    private void ChangingDirection()
    {
        actualObjP++;
        if (actualObjP >= points.Length)
        {
            actualObjP = 0;
        }
        changeDirection = false;
    }


}
