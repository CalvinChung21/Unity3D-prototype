using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace CommandPattern
{
    public class Ghost : MonoBehaviour
    {
        // Event system
        public delegate void EnemyKilled();
        public static event EnemyKilled OnEnemyKilled;
        
        // the animation of hit will trigger the event of activateFist and deactivateFist
        public GameObject m_RightFist;
        
        public void activateFist()
        {
            m_RightFist.GetComponent<Collider>().enabled = true;
        }
        public void deactivateFist()
        {
            m_RightFist.GetComponent<Collider>().enabled = false;
        }
        
        [SerializeField]
        Transform _destination;
        NavMeshAgent _navMeshAgent;
        
        [SerializeField] Animator _animator;
        
        // code reference from https://www.youtube.com/watch?v=ZYeXmze5gxg
        public float health;
        public float maxHealth;
        public GameObject healthBarUI;
        public Slider slider;
        
        private void Start()
        {
            health = maxHealth;
            slider.value = CalculateHealth();
            _animator = this.GetComponent<Animator>();
            _navMeshAgent = this.GetComponent<NavMeshAgent>();
            _destination = gameObject.transform;
        }

        private void Update()
        {
            navMeshSetup();

            healthBar();

            die();
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
            // find decoy first, if not then player, if not then stay at the game object's own position
            if (GameObject.Find("Decoy(Clone)") != null)
                _destination = GameObject.Find("Decoy(Clone)").GetComponent<Transform>();
            else if (GameObject.Find("FirstPersonCharacter")!=null)
                _destination = GameObject.Find("FirstPersonCharacter").GetComponent<Transform>();
            else
                _destination = gameObject.transform;
            
            // set the target destination
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
            
            // play attack animation when near the player
            if (Vector3.Distance(gameObject.transform.position, targetVector) < 4f)
                _animator.SetBool("Attack", true);
            else
                _animator.SetBool("Attack", false);
        }

        float CalculateHealth()
        {
            return (float) health / maxHealth;
        }
        void healthBar()
        {
            // update health value and show the health bar when it is under 100
            slider.value = CalculateHealth();
            if (health < maxHealth)
                healthBarUI.SetActive(true);
            else
                healthBarUI.SetActive(false);
        }

        private bool isDie = false;
        void die()
        {
            if (health <= 0 && !isDie)
            {
                SoundManager.PlaySound(SoundManager.Sound.ghostDead, gameObject.transform.position);
                if (OnEnemyKilled != null)
                    OnEnemyKilled();
                isDie = true;
                
                Destroy(this.gameObject, 2f);
                _animator.SetBool("Die", true);
            }
        }

    }
}

