using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat damage;
    public Stat armor;

    public int maxHealth = 100;
    public int currentHealth { get; set; }

    public event System.Action<int, int> OnHealthChanged;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");
        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
        }

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //die in som way
        //this metod is meant to be overwritten
        Debug.Log(transform.name + " died.");
    }
}
