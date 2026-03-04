
public interface IDamageable
{
    public Team EntityTeam { get; }
    public float CurrentHP { get; }
    public void TakeDamage(float amount, AEntity attacker);
    public bool IsDead { get; }
}
