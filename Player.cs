using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float MoveSpeed = 200.0f;  // Velocidad de movimiento
	[Export] public float JumpForce = -350.0f; // Fuerza del salto
	[Export] public float GravityForce = 800.0f; // Gravedad

	private AnimatedSprite2D _animatedSprite;
	private AudioStreamPlayer2D footsteps;
	private AudioStreamPlayer2D jumpsound;
	private bool isWalking = false; // Variable para rastrear si el jugador está caminando
	private bool hasJumped = false; // Variable para rastrear si el sonido de salto ya se reprodujo

	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		footsteps = GetNode<AudioStreamPlayer2D>("Pasos");
		jumpsound = GetNode<AudioStreamPlayer2D>("Jump");
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
			StopFootsteps(); // Detener el sonido de pasos
			PlayJumpSound(); // Reproducir el sonido de salto
		}
		else if (direction != 0)
		{
			_animatedSprite.Play("walk");  // Animación al caminar
			_animatedSprite.FlipH = direction < 0;  // Voltear si se mueve a la izquierda
			PlayFootsteps(); // Reproducir el sonido al caminar
			StopJumpSound(); // Detener el sonido de salto al aterrizar
		}
		else
		{
			_animatedSprite.Play("default");  // Animación al estar quieto
			StopFootsteps(); // Detener el sonido de pasos
			StopJumpSound(); // Asegurarse de detener el sonido de salto
		}

		// Saltar si estamos en el suelo y se presiona la tecla de salto
		if (IsOnFloor() && Input.IsActionJustPressed("move_up"))
		{
			Velocity = new Vector2(Velocity.X, JumpForce);
			hasJumped = false; // Reiniciar el estado del salto para reproducir el sonido
		}

		// Mover al jugador con colisiones
		MoveAndSlide();
	}

	private void PlayFootsteps()
	{
		// Reproducir el sonido solo si no está sonando
		if (!isWalking)
		{
			footsteps.Play();
			isWalking = true;
		}
	}

	private void StopFootsteps()
	{
		// Detener el sonido solo si está sonando
		if (isWalking)
		{
			footsteps.Stop();
			isWalking = false;
		}
	}

	private void PlayJumpSound()
	{
		// Reproducir el sonido solo una vez mientras estamos en el aire
		if (!hasJumped)
		{
			jumpsound.Play();
			hasJumped = true;
		}
	}

	private void StopJumpSound()
	{
		// No es necesario detener el sonido, ya que se reproduce una sola vez,
		// pero puedes asegurarte de reiniciar el estado si lo prefieres
		if (hasJumped && IsOnFloor())
		{
			hasJumped = false; // Reiniciar el estado al aterrizar
		}
	}
}
