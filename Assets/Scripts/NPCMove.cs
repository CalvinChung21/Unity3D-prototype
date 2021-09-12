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

    public float health;
    public float maxHealth;
    public GameObject healthBarUI;
    public Slider slider;

    private void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthBarFade.Damage();
            SoundManagerScript.playSound("ah");
        }
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

        if(_navMeshAgent.isStopped == true && health > 0)
        {
            if(Spherecast.getCurrentHitObject() != null && Spherecast.getCurrentHitObject().name != gameObject.name)
            {
                StartCoroutine(recoverFromStop(3));
            }
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
            Destroy(gameObject);
            SoundManagerScript.playSound("NPC");
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

    IEnumerator recoverFromStop(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (_navMeshAgent != null)
        {
            _navMeshAgent.isStopped = false;
            GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
