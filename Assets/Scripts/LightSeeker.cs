using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class LightSeeker: MonoBehaviour
{
    [SerializeField]
    Transform _destination;

    Transform _temp;

    NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _destination = gameObject.transform;
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
        else if (FlashLightToggle.flashLightStatus())
        {
            _destination = GameObject.Find("FirstPersonCharacter").GetComponent<Transform>();
        }
        else
        {
            _destination = gameObject.transform;
        }
        Vector3 targetVector = _destination.transform.position;
        _navMeshAgent.SetDestination(targetVector);
        
    }

}
