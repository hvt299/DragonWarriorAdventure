using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth = 3;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool isDead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth -  _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            //iframes
        } else
        {
            if (!isDead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                isDead = true;
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}
