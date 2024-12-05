extends Area2D

func _on_body_entered(body):
	# Verificar si el cuerpo es el jugador y tiene los métodos requeridos
	if body is Player and body.has_method("DecrementHealth"):
		# Reducir vida del jugador
		body.DecrementHealth(1)
		var sound = $"daño"  # Nodo de sonido
		sound.play()  # Reproduce el sonido
		# Calcular la dirección de empuje
		var direction = (body.global_position - global_position).normalized()
		var knockback_force = 600  # Ajusta según la fuerza deseada
		
		# Aplicar el empuje llamando a un método del jugador
		body.ApplyKnockback(direction * knockback_force)
		
		# Reproducir la animación de daño
		if body.has_node("AnimationPlayer"):
			var flash = body.get_node("AnimationPlayer")
			flash.play("FlashDaño")



