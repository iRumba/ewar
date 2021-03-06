using Microsoft.Xna.Framework;

using Rpg.Client.Engine;

namespace Rpg.Client.Models.Combat.GameObjects
{
    internal class MoveBack : IUnitStateEngine
    {
        private const double DURATION = 1;
        private readonly AnimationBlocker _blocker;
        private readonly UnitGraphics _graphics;
        private readonly SpriteContainer _graphicsRoot;
        private readonly Vector2 _startPosition;
        private readonly Vector2 _targetPosition;
        private double _counter;

        public MoveBack(UnitGraphics graphics, SpriteContainer graphicsRoot, Vector2 targetPosition,
            AnimationBlocker blocker)
        {
            _startPosition = graphicsRoot.Position;
            _targetPosition = targetPosition;
            _graphics = graphics;
            _graphicsRoot = graphicsRoot;
            _blocker = blocker;
        }

        public bool CanBeReplaced => false;
        public bool IsComplete { get; private set; }

        public void Cancel()
        {
            if (IsComplete)
            {
                return;
            }

            _blocker.Release();
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

                _graphicsRoot.Position = Vector2.Lerp(_startPosition, _targetPosition, (float)(1 - _counter));
            }
            else
            {
                IsComplete = true;

                _blocker.Release();

                _graphicsRoot.Position = _startPosition;
            }
        }
    }
}