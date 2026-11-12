public class Player
{
    public string Name { get; }
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    public int Gold { get; set; }

    public int Level { get; private set; }
    public int Experience { get; private set; }

    public int ExperienceToNextLevel => Level * 100;

    public Player(string name, int maxHealth, int gold)
    {
        Name = name;
        MaxHealth = maxHealth;
        Health = maxHealth;
        Gold = gold;
        Level = 1;
        Experience = 0;
    }

    public void GainExperience(int amount)
    {
        if (amount <= 0) return;

        Experience += amount;

        while (Experience >= ExperienceToNextLevel)
        {
            Experience -= ExperienceToNextLevel;
            Level++;
            MaxHealth += 20;
            Health = MaxHealth;
        }
    }

    public bool TakeDamage(int damage)
    {
        if (damage <= 0 || !IsAlive()) return false;

        Health -= damage;
        if (Health < 0) Health = 0;
        return true;
    }

    public bool Heal(int amount)
    {
        if (amount <= 0 || !IsAlive() || Health == MaxHealth) return false;

        Health += amount;
        if (Health > MaxHealth) Health = MaxHealth;
        return true;
    }

    public bool IsAlive()
    {
        return Health > 0;
    }

    public string GetSummary()
    {
        return $"{Name} [Lvl {Level} | EXP {Experience}/{ExperienceToNextLevel}] HP: {Health}/{MaxHealth} | Złoto: {Gold}";
    }
}