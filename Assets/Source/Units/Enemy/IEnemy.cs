namespace TowerDefence.Units.Enemy
{
    public interface IEnemy
    {
        int Damage { get; }
        void Init(EnemyConfig config);
    }
}