extends Area2D

func _on_body_entered(body):
	var sound = $"PICKUP SOUND"  # Referencia al nodo de sonido
	sound.play()  # Reproduce el sonido
	await get_tree().create_timer(0.1).timeout
	queue_free()  # Elimina el nodo principal
	pass

