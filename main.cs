using Godot;
using System;

public partial class Main : Node
{
	public override void _Ready()
	{
		// Instanciar la escena de Control
		var controlScene = GD.Load<PackedScene>("res://Control.tscn"); // Asegúrate de que la ruta sea correcta
		var controlInstance = controlScene.Instantiate<Control>();

		// Agregar el nodo de Control a la escena principal
		AddChild(controlInstance);
	}
}
