
public interface IDamageable
{
    public Team Team { get; }
    public float CurHp { get; }
    public bool IsDead { get; }
    public void TakeDamage(float argDamage, AEntity argAttacker);
}
