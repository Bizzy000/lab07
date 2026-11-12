public class Enemy
{
    public string Name { get; }
    public int Health { get; private set; }
    public int MaxHealth { get; }
    public int Damage { get; }

    public Enemy(string name, int health, int damage)
    {
        Name = name;
        MaxHealth = health;
        Health = health;
        Damage = damage;
    }
    public bool TakeDamage(int damage)
    {
        if (damage <= 0 || !IsAlive())
        {
            return false;
        }

        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }

        return true;
    }

    public bool IsAlive()
    {
        return Health > 0;
    }

    public string GetSummary()
    {
        string status = IsAlive() ? $"{Health}/{MaxHealth} HP" : "POKONANY";
        return $"{Name} ({status}, DMG: {Damage})";
    }
}