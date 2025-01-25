using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private int health;
    [SerializeField] private GameObject UiDamage;
    [SerializeField] private GameObject UiGameOver;
    [SerializeField] private bool alive;

    private IEnumerator restoreLife;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 2;
        UpdateHealth();
        alive = true;

        restoreLife = RestoreLife();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        if (alive)
        {
            health--;
            UpdateHealth();
        }
    }

    IEnumerator RestoreLife()
    {
        yield return new WaitForSeconds(20);
        health = 2;
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        switch (health)
        {
            case 0:
                //game over
                UiGameOver.SetActive(true);
                UiDamage.SetActive(true);
                alive = false;
                Time.timeScale = 0;
                StopCoroutine(restoreLife);
                break;
            case 1:
                //Dañao
                UiGameOver.SetActive(false);
                UiDamage.SetActive(true);

                StartCoroutine(restoreLife);
                break;
            case 2:
                // tooo bien
                UiGameOver.SetActive(false);
                UiDamage.SetActive(false);
                break;
            default:
                //game over
                UiGameOver.SetActive(true);
                UiDamage.SetActive(true);
                break;
        }
    }
}
