using Godot;

public partial class Light : PointLight2D {

  private float _lightEnergy = 1f;

  public override void _Ready() {
    base._Ready();
  }

  public float LightEnergy { get => _lightEnergy; set => SetLightEnergy(value); }
  private void SetLightEnergy(float value) {
    _lightEnergy = value;
    Scale = Vector2.One * _lightEnergy;
    // Energy = _lightEnergy;
  }

  // I used this to generate the light texture... we don't need it anymore but I'm saving it for later use/reference :3
  void UpdateLightTexture() {
    var textureImage = Texture.GetImage();
    var width = textureImage.GetWidth();
    var height = textureImage.GetHeight();
    var newTexture = Image.Create(width, height, false, Image.Format.Rgba8);

    for (int x = 0; x < width; x++) {
      for (int y = 0; y < height; y++) {
        var color = textureImage.GetPixel(x, y);
        color.A = (color.R + color.B + color.G) / 3 * color.A;
        color.R = 1;
        color.B = 1;
        color.G = 1;
        newTexture.SetPixel(x, y, color);
      }
    }

    Texture = ImageTexture.CreateFromImage(newTexture);
    newTexture.SavePng("res://newtexture.png");
  }
}
