using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float MoveSpeed = 200.0f;  // Velocidad de movimiento
	[Export] public float JumpForce = -400.0f; // Fuerza del salto
	[Export] public float GravityForce = 800.0f; // Gravedad

	private AnimatedSprite2D _animatedSprite;

	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		// Aplicar gravedad si no estamos en el suelo
		if (!IsOnFloor())
			Velocity += new Vector2(0, GravityForce * (float)delta);

		// Movimiento lateral
		int direction = 0;
		if (Input.IsActionPressed("move_right"))
			direction += 1;
		if (Input.IsActionPressed("move_left"))
			direction -= 1;

		// Actualizamos la velocidad en el eje X
		Velocity = new Vector2(direction * MoveSpeed, Velocity.Y);

		// Cambiar la animación según el estado del jugador
		if (!IsOnFloor())
		{
			_animatedSprite.Play("jump");  // Animación al saltar
		}
		else if (direction != 0)
		{
			_animatedSprite.Play("walk");  // Animación al caminar
			_animatedSprite.FlipH = direction < 0;  // Voltear si se mueve a la izquierda
		}
		else
		{
			_animatedSprite.Play("default");  // Animación al estar quieto
		}

		// Saltar si estamos en el suelo y se presiona la tecla de salto
		if (IsOnFloor() && Input.IsActionJustPressed("move_up"))
			Velocity = new Vector2(Velocity.X, JumpForce);

		// Mover al jugador con colisiones
		MoveAndSlide(); // Solo llama a este método sin asignar el resultado
	}
}
