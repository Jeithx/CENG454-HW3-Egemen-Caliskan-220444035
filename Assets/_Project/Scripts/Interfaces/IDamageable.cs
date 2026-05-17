namespace CoreBreach.Interfaces
{
    public interface IDamageable
    {
        int CurrentHealth { get; }
        void TakeDamage(int amount);
    }
}
