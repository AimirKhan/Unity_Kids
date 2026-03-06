using R3;

namespace Game.Level
{
    public class LevelModel
    {
        public readonly ReactiveProperty<int> Level = new(1);
    }
}