extends Control

func _ready():
	$ButtonsContainer/VBoxContainer/Start.grab_focus()

func _on_start_pressed():
	get_tree().change_scene_to_file("res://Levels/level1.tscn")


func _on_options_pressed():
	get_tree().change_scene_to_file("res://UI/OptionsMenu.tscn")


func _on_exit_pressed():
	get_tree().quit()
