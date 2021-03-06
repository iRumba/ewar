using Microsoft.Xna.Framework;

namespace Rpg.Client.Models.Combat.GameObjects
{
    public interface IUnitStateEngine
    {
        public bool CanBeReplaced { get; }
        public bool IsComplete { get; }
        void Cancel();
        public void Update(GameTime gameTime);
    }
}