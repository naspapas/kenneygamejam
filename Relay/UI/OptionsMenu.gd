extends Control

func _ready():
	$VBoxContainer/Button.grab_focus()

func _on_exit_pressed():
	get_tree().change_scene_to_file("res://UI/MainMenu.tscn")
