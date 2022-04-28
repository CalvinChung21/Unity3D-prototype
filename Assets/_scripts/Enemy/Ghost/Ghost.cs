using System.Collections;
using UnityEngine;

    public class Ghost : MonoBehaviour
    {
        public delegate void EnemyKilled();
        public static event EnemyKilled OnEnemyKilled;
        
        public bool stopPathFinding;

        [SerializeField] Animator _animator;
        
        // code reference from https://www.youtube.com/watch?v=ZYeXmze5gxg
        // public float health;
        // public float maxHealth;
        // public GameObject healthBarUI;
        // public Slider slider;

        public GameObject m_RightFist;
        // the animation of hit will trigger the event of activateFist and deactivateFist
        public void activateFist()
        {
            m_RightFist.GetComponent<SphereCollider>().enabled = true;
        }
        public void deactivateFist()
        {
            m_RightFist.GetComponent<SphereCollider>().enabled = false;
        }
        
        private void Awake()
        {
            // health = maxHealth;
            // slider.value = CalculateHealth();
            _animator = this.GetComponent<Animator>();
            stopPathFinding = false;
        }
        
        private void Update()
        {
            Attack();

            if (stopPathFinding)
                StartCoroutine(recoverFromStopState());
            else
                StopCoroutine(recoverFromStopState());
            
        }

        private void Attack()
        {
            // play attack animation when near the player
            if (Vector3.Distance(gameObject.transform.position, GameObject.Find("FPSController").transform.position) < 4f)
                _animator.SetBool("Attack", true);
            else
                _animator.SetBool("Attack", false);
        }
        
        IEnumerator recoverFromStopState()
        {
            yield return new WaitForSeconds(5);
            _animator.SetBool("Hit", false);
            stopPathFinding = false;
        }

        // float CalculateHealth()
        // {
        //     return (float) health / maxHealth;
        // }
        // void healthBar()
        // {
        //     // update health value and show the health bar when it is under 100
        //     slider.value = CalculateHealth();
        //     if (health < maxHealth)
        //         healthBarUI.SetActive(true);
        //     else
        //         healthBarUI.SetActive(false);
        // }
        //
        // private bool isDie = false;
        // void die()
        // {
        //     if (health <= 0 && !isDie)
        //     {
        //         SoundManager.PlaySound(SoundManager.Sound.ghostDead, gameObject.transform.position);
        //         if (OnEnemyKilled != null)
        //             OnEnemyKilled();
        //         isDie = true;
        //         
        //         Destroy(this.gameObject, 2f);
        //         _animator.SetBool("Die", true);
        //     }
        // }

    }

