using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPCMove : MonoBehaviour
{
    [SerializeField]
    Transform _destination;

    NavMeshAgent _navMeshAgent;
    public GameObject NPC;

    public float health;
    public float maxHealth;
    public GameObject healthBarUI;
    public Slider slider;

    private void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();
    }

    private void Update()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent component is not attached to " + gameObject.name);
        }
        else
        {
            SetDestination();
        }

        if(_navMeshAgent.isStopped == true)
        {
            NPC.GetComponent<Renderer>().material.color = Color.blue;
            health++;
        }

        if (health > maxHealth)
        {
            health = maxHealth;
            _navMeshAgent.isStopped = false;
            NPC.GetComponent<Renderer>().material.color = Color.red;
        }

        slider.value = CalculateHealth();

        if (health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }
        else
        {
            healthBarUI.SetActive(false);
        }

        if (health <= 0)
        {
            _navMeshAgent.isStopped = true;
            SoundManagerScript.playSound("NPC");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Player")
        {
            Debug.Log("hit the player!");
        }
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }

    private void SetDestination()
    {
        if(_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }
}
