using Microsoft.Xna.Framework;

using Rpg.Client.Engine;

namespace Rpg.Client.Models.Combat.GameObjects
{
    internal class MoveToTarget : IUnitStateEngine
    {
        private const double DURATION = 1;
        private readonly UnitGraphics _graphics;
        private readonly SpriteContainer _graphicsRoot;

        private readonly Vector2 _startPosition;
        private readonly Vector2 _targetPosition;
        private double _counter;

        public MoveToTarget(UnitGraphics graphics, SpriteContainer graphicsRoot, Vector2 targetPosition)
        {
            _startPosition = graphicsRoot.Position;
            _targetPosition = targetPosition;
            _graphics = graphics;
            _graphicsRoot = graphicsRoot;
        }

        public bool CanBeReplaced => false;
        public bool IsComplete { get; private set; }

        public void Cancel()
        {
            if (IsComplete)
            {
            }
        }

        public void Update(GameTime gameTime)
        {
            if (IsComplete)
            {
                return;
            }

            if (_counter == 0)
            {
                _graphics.PlayAnimation("MoveForward");
            }

            if (_counter <= DURATION)
            {
                _counter += gameTime.ElapsedGameTime.TotalSeconds;

                _graphicsRoot.Position = Vector2.Lerp(_startPosition, _targetPosition, (float)_counter);
            }
            else
            {
                IsComplete = true;
                _graphicsRoot.Position = _targetPosition;
            }
        }
    }
}