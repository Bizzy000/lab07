using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n Expiarka i Przeciwnicy");
            Console.WriteLine("1. Expiarka i LVL");
            Console.WriteLine("2. Atak i leczenie");
            Console.WriteLine("3. Przeciwnicy i ich staty");
            Console.WriteLine("4. Wybor przeciwnika");
            Console.WriteLine("0. Finito");
            Console.Write("Wybierz zadanie od 1 do 4 (lub 0 aby zakończyć): ");

            string menu = Console.ReadLine() ?? "";
            Console.WriteLine();

            switch (menu)
            {
                case "1": Task1(); break;
                case "2": Task2(); break;
                case "3": Task3(); break;
                case "4": Task4(); break;
                case "0": return;
                default:
                    Console.WriteLine("Nie ma takiej opcji, wybierz 1, 2, 3 lub 0");
                    break;
            }
        }
    }

    static void Task1()
    {
        Player player = new Player("Maciuś", 100, 25);
        Console.WriteLine("Stan gracza: " + player.GetSummary());

        Console.WriteLine("\n +40 EXP:");
        player.GainExperience(40);
        Console.WriteLine(player.GetSummary());

        Console.WriteLine("\n +60 EXP (wbijasz 2LVL!!!):");
        player.GainExperience(60);
        Console.WriteLine(player.GetSummary());

        Console.WriteLine("\n +250 EXP (multi level-up):");
        player.GainExperience(250);
        Console.WriteLine(player.GetSummary());
    }
    static void Task2()
    {
        Player player = new Player("Maciuś", 100, 25);
        Enemy goblin = new Enemy("Goblin", 30, 8);

        Console.WriteLine(" PLAYER");
        TestPlayerDamage(player, 30);
        TestPlayerDamage(player, 0);
        TestPlayerDamage(player, -10);

        TestPlayerHeal(player, 20);
        TestPlayerHeal(player, 0);
        TestPlayerHeal(player, -5);

        Console.WriteLine("\n ENEMY");
        TestEnemyDamage(goblin, 30);
        TestEnemyDamage(goblin, 10);
    }

    static void TestPlayerDamage(Player p, int dmg)
    {
        bool result = p.TakeDamage(dmg);
        Console.WriteLine(result
            ? $"Zadano {dmg} obrażeń. Aktualne HP: {p.Health}"
            : $"Niepoprawna wartosc obrazen ({dmg}) lub kipnales.");
    }

    static void TestPlayerHeal(Player p, int amount)
    {
        bool result = p.Heal(amount);
        Console.WriteLine(result
            ? $"Uleczono o {amount}. Aktualne HP: {p.Health}"
            : $"Niepoprawne leczenie ({amount}) lub pelne zdrowie.");
    }

    static void TestEnemyDamage(Enemy e, int dmg)
    {
        bool result = e.TakeDamage(dmg);
        Console.WriteLine(result
            ? $"Zadano {dmg} obrażeń. Pozostałe HP: {e.Health}"
            : $"Niepoprawna wartosc obrazen ({dmg}) lub kipnales.");
    }

    static void Task3()
    {
        List<Enemy> enemies = new List<Enemy>
        {
            new Enemy("Goblin", 30, 8),
            new Enemy("Nietoperz", 20, 5),
            new Enemy("Ork", 70, 15)
        };

        enemies[1].TakeDamage(20);

        int aliveCount = 0;
        Enemy? strongest = null;

        Console.WriteLine("Raport listy:");
        foreach (Enemy enemy in enemies)
        {
            Console.WriteLine($"{enemy.GetSummary()}");
            if (enemy.IsAlive())
            {
                aliveCount++;
                if (strongest == null || enemy.Damage > strongest.Damage)
                {
                    strongest = enemy;
                }
            }
        }

        Console.WriteLine($"\nPodsumowanie kolekcji:");
        Console.WriteLine($"Tyle jest zywych przeciwnikow: {aliveCount} / {enemies.Count}");
        if (strongest != null)
        {
            Console.WriteLine($"Kogo masz sie bac: {strongest.Name} (Zadaje {strongest.Damage} DMG)");
        }
    }
    static void Task4()
    {
        Player player = new Player("Maciuś", 100, 25);
        int playerDamage = 15;

        List<Enemy> enemies = new List<Enemy>
        {
            new Enemy("Goblin", 30, 8),
            new Enemy("Nietoperz", 20, 5),
            new Enemy("Ork", 70, 15)
        };

        enemies[1].TakeDamage(20);

        Enemy? target = SelectEnemy(enemies);

        if (target != null)
        {
            Console.WriteLine($"\n{player.Name} atakuje: {target.Name} za {playerDamage} DMG!");
            target.TakeDamage(playerDamage);

            if (target.IsAlive())
            {
                Console.WriteLine($"Pozostałe HP przeciwnika: {target.Health}/{target.MaxHealth}");
            }
            else
            {
                Console.WriteLine($"{target.Name} został pokonany!");
            }
        }
    }

    static Enemy? SelectEnemy(List<Enemy> enemies)
    {
        Console.WriteLine("Wybierz przeciwnika do ataku:");
        for (int i = 0; i < enemies.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {enemies[i].GetSummary()}");
        }

        Console.Write("Podaj numer przeciwnika: ");
        string input = Console.ReadLine() ?? "";

        if (!int.TryParse(input, out int choice))
        {
            Console.WriteLine("Błąd: podano tekst zamiast liczby.");
            return null;
        }

        if (choice < 1)
        {
            Console.WriteLine("Błąd: numer przeciwnika nie może być mniejszy niż 1.");
            return null;
        }

        if (choice > enemies.Count)
        {
            Console.WriteLine($"Błąd: nie ma przeciwnika o numerze {choice} (maksymalny to {enemies.Count}).");
            return null;
        }

        Enemy selected = enemies[choice - 1];

        if (!selected.IsAlive())
        {
            Console.WriteLine($"Błąd: {selected.Name} jest już pokonany i nie można go zaatakować.");
            return null;
        }

        return selected;
    }
}