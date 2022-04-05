using UnityEngine;
using UnityEngine.AI;

namespace CommandPattern
{
    public class LightSeeker: MonoBehaviour
    {
        [SerializeField]
        Transform _destination;
        NavMeshAgent _navMeshAgent;
        
        private void Start()
        {
            _destination = gameObject.transform;
        }
        
        #region whenHitingThePlayer
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                ScreenShake.Execute();
                HealthBarFade.Damage();
                SoundManager.PlaySound(SoundManager.Sound.playerHurt, gameObject.transform.position);
            }
        }
        #endregion
        private void Update()
        {
            navMeshSetup();
        }
    
        private void navMeshSetup()
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
            if (GameObject.Find("FlareThrew(Clone)") != null)
            {
                _destination = GameObject.Find("FlareThrew(Clone)").GetComponent<Transform>();
            }
            else if (FlashLightToggle.FlashlightActive)
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
}

