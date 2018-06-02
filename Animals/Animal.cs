using System;
using Godot;

namespace Snake.Animals {
    public class Animal : Node2D
    {
        public override void _Ready() {
            QueueNextMovement();
        }

        private static readonly Random _random = new Random();

        public void QueueNextMovement() {
            Move(GetNextMovement());
        }

        protected virtual Vector2 GetNextMovement() {
            switch (_random.Next(4)) {
                default: return new Vector2(1, 0); 
                case 1: return new Vector2(-1, 0);
                case 2: return new Vector2(0, 1);
                case 3: return new Vector2(0, -1);
            }
        }

        public void MoveRight() => Move(new Vector2(1, 0));
        public void MoveLeft() => Move(new Vector2(-1, 0));
        public void MoveUp() => Move(new Vector2(0, -1));
        public void MoveDown() => Move(new Vector2(0, 1));

        protected void Move(Vector2 direction) {
            var tween = new Tween();
            AddChild(tween);
            tween.InterpolateProperty(this, "position", 
                                      Position, Position + direction*150, 1, 
                                      Tween.TransitionType.Cubic, Tween.EaseType.InOut);
            tween.InterpolateCallback(this, 1, nameof(QueueNextMovement));
            tween.InterpolateCallback(tween, 1, "queue_free");
            tween.Start();
        }
    }
}
