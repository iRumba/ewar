using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Rpg.Client.Core;
using Rpg.Client.Engine;

namespace Rpg.Client.Models.Combat.GameObjects
{
    internal class UnitGameObject
    {
        private readonly IList<IUnitStateEngine> _actorStateEngineList;
        private readonly GameObjectContentStorage _gameObjectContentStorage;

        private readonly UnitGraphics _graphics;

        public UnitGameObject(CombatUnit unit, Vector2 position, GameObjectContentStorage gameObjectContentStorage)
        {
            _actorStateEngineList = new List<IUnitStateEngine>();

            _graphics = new UnitGraphics(unit, position, gameObjectContentStorage);

            Unit = unit;
            Position = position;
            _gameObjectContentStorage = gameObjectContentStorage;
        }

        public bool IsActive { get; set; }

        public Vector2 Position { get; }

        public CombatUnit? Unit { get; internal set; }

        public void Attack(UnitGameObject target, AnimationBlocker animationBlocker, CombatSkillCard combatSkillCard)
        {
            var attackInteraction = new AttackInteraction(Unit, target.Unit, combatSkillCard, () =>
            {
                if (target.Unit.Unit.IsDead)
                {
                    target.AddStateEngine(new DeathState(target._graphics));
                }
                else
                {
                    target.AddStateEngine(new WoundState(target._graphics));
                }
            });
            var state = new UnitAttackState(_graphics, _graphics.Root, target._graphics.Root, animationBlocker,
                attackInteraction);
            AddStateEngine(state);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _graphics.ShowActiveMarker = IsActive;

            _graphics.Draw(spriteBatch);

            spriteBatch.DrawString(_gameObjectContentStorage.GetFont(), Unit.Unit.UnitScheme.Name,
                _graphics.Root.Position - new Vector2(0, 100), Color.White);
            spriteBatch.DrawString(_gameObjectContentStorage.GetFont(), $"{Unit.Unit.Hp}/{Unit.Unit.MaxHp}",
                _graphics.Root.Position - new Vector2(0, 80), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            HandleEngineStates(gameTime);

            _graphics.Update(gameTime);
        }

        private void AddStateEngine(IUnitStateEngine actorStateEngine)
        {
            foreach (var state in _actorStateEngineList.ToArray())
            {
                if (state.CanBeReplaced)
                {
                    _actorStateEngineList.Remove(state);
                }
            }

            _actorStateEngineList.Add(actorStateEngine);
        }

        private void HandleEngineStates(GameTime gameTime)
        {
            if (!_actorStateEngineList.Any())
            {
                return;
            }

            var activeStateEngine = _actorStateEngineList.First();
            activeStateEngine.Update(gameTime);

            if (activeStateEngine.IsComplete)
            {
                _actorStateEngineList.Remove(activeStateEngine);

                if (!_actorStateEngineList.Any())
                {
                    AddStateEngine(new UnitIdleState(_graphics));
                }

                ResetActorRootSpritePosition();
            }
        }

        private void ResetActorRootSpritePosition()
        {
            _graphics.Root.Position = Position;
        }
    }
}