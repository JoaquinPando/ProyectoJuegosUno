extends Area2D
func _on_body_entered(body):
	if body is CharacterBody2D and body.has_method("IncrementHealth"):  
		body.IncrementHealth(1)  # Llama a la función IncrementHealth del jugador
		var sound = $"PICKUP SOUND"  # Nodo de sonido
		sound.play()  # Reproduce el sonido
		await get_tree().create_timer(0.1).timeout
		queue_free()  # Elimina la poción

