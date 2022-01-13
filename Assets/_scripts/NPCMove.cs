using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class NPCMove : MonoBehaviour
{
    // code reference from https://www.youtube.com/watch?v=NGGoOa4BpmY
    [SerializeField]
    Transform _destination;
    NavMeshAgent _navMeshAgent;

    // code reference from https://www.youtube.com/watch?v=ZYeXmze5gxg
    public float health;
    public float maxHealth;
    public GameObject healthBarUI;
    public Slider slider;
    
    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;
    
    private void Start()
    {
        _destination = GameObject.Find("FirstPersonCharacter").GetComponent<Transform>();
        health = maxHealth;
        slider.value = CalculateHealth();
        navMeshSetup();
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
        navMeshSetup();

        attacked();

        healthBar();

        die();
    }

    void navMeshSetup()
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

    }

    private void SetDestination()
    {

        if (GameObject.Find("Decoy(Clone)") != null)
        {
            _destination = GameObject.Find("Decoy(Clone)").GetComponent<Transform>();
        }
        else if (_destination == null)
        {
            _destination = GameObject.Find("FirstPersonCharacter").GetComponent<Transform>();
        }
        Vector3 targetVector = _destination.transform.position;
        _navMeshAgent.SetDestination(targetVector);
    }

    IEnumerator recoverFromStop(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (_navMeshAgent != null)
        {
            _navMeshAgent.isStopped = false;
        }
    }

    void attacked()
    {
        if (_navMeshAgent.isStopped == true && health > 0)
        {
            if ((Spherecast.getCurrentHitObject() != null && Spherecast.getCurrentHitObject().name != gameObject.name) || FlashLightToggle.flashLightStatus() == false)
            {
                StartCoroutine(recoverFromStop(3));
            }
        }
    }
    float CalculateHealth()
    {
        return (float) health / maxHealth;
    }
    void healthBar()
    {
        slider.value = CalculateHealth();
        if (health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }
        else
        {
            healthBarUI.SetActive(false);
        }
    }

    void die()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            SoundManagerScript.playSound("NPC");
            if (OnEnemyKilled != null)
            {
                OnEnemyKilled();
            }
        }
        
    }

}
