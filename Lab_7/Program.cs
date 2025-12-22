using Lab_7;

var system = new EffectSystem();
var ctx = new CharacterContext { Health = 100, Armor = 0 };

void Regenerate(CharacterContext c) => c.Health += 10;
void ApplyPoison(CharacterContext c)
{
    c.Health -= 5;
    c.Poisoned = true;
}

EffectHandler AddShield = c => c.Armor += 3;

Console.WriteLine($"Initial: Health={ctx.Health}, Armor={ctx.Armor}, Poisoned={ctx.Poisoned}");

system.OnEffect += Regenerate;
system.OnEffect += ApplyPoison;
system.OnEffect += AddShield;

system.Run(ctx);
Console.WriteLine($"After all effects: Health={ctx.Health}, Armor={ctx.Armor}, Poisoned={ctx.Poisoned}");

system.OnEffect -= ApplyPoison;
ctx.Health = 100;
ctx.Poisoned = false;

system.Run(ctx);
Console.WriteLine($"After removing poison: Health={ctx.Health}, Armor={ctx.Armor}, Poisoned={ctx.Poisoned}");
