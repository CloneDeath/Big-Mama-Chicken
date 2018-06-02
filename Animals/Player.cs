using Godot;

namespace Snake.Animals {
	public class Player : Animal {
		private Vector2 _nextMovement;
		public override void _Ready() {
			_nextMovement = GetRandomMovement();
			base._Ready();
		}

		protected override Vector2 GetNextMovement() {
			return _nextMovement;
		}

		public override void _Process(float delta) {
			base._Process(delta);

			if (Input.IsActionPressed("move_up")) {
				_nextMovement = new Vector2(0, -1);
			} else if (Input.IsActionPressed("move_down")) {
				_nextMovement = new Vector2(0, 1);
			} else if (Input.IsActionPressed("move_left")) {
				_nextMovement = new Vector2(-1, 0);
			} else if (Input.IsActionPressed("move_right")) {
				_nextMovement = new Vector2(1, 0);
			}
		}

		public void OnCollisionAreaAreaEntered(Object area) {
			var other = area as Area2D;
			other?.GetParent().QueueFree();
		}
	}
}