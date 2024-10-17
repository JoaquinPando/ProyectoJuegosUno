using Godot;
using System;

public partial class Interfaz : Control
{

	
	public override void _Ready()
	{

		// Conectar las señales de los botones
		GetNode<Button>("play").Pressed += _on_play_pressed;
		GetNode<Button>("option").Pressed += _on_option_pressed;
		GetNode<Button>("quit").Pressed += _on_quit_pressed;
		
		
	}

	private void _on_play_pressed()
	{
		// Cargar y agregar la escena del tutorial
		var scenePath = "res://nivel_Tutorial.tscn"; // Asegúrate de que esta ruta sea correcta.
		var packedScene = ResourceLoader.Load<PackedScene>(scenePath);
			
		if (packedScene != null)
		{
			var sceneInstance = packedScene.Instantiate(); // Instanciar la escena
			GetTree().Root.AddChild(sceneInstance); // Agregar la escena al árbol

			// Opcionalmente, puedes quitar la escena actual (la interfaz) si lo deseas
			QueueFree(); // Esto eliminaría la interfaz actual
		}
		else
		{
			GD.Print("Error al cargar la escena: " + scenePath);
		}
	}

	private void _on_option_pressed()
	{
		// Lógica para opciones
		GD.Print("Opciones presionadas");
	}

	private void _on_quit_pressed()
	{
		// Salir del juego
		GetTree().Quit();
	}
}
