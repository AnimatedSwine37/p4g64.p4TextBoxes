using p4g64.p4TextBoxes.Template.Configuration;
using System.ComponentModel;

namespace p4g64.p4TextBoxes.Configuration;
public class MainBoxConfig
{
    [DisplayName("Show Gradient")]
    [Description("Show the gradient in the main brown box.")]
    [DefaultValue(false)]
    public bool ShowGradient { get; set; } = false;

    [DisplayName("Main Box Colour Main")]
    [Description("The main colour of the brown box.")]
    public Colour GradientMain { get; set; } = Colour.BoxGradientMain;

    [DisplayName("Main Box Colour Secondary")]
    [Description("The secondary colour of the brown box.\nThis is only used if gradient is shown.")]
    public Colour GradientSub { get; set; } = Colour.BoxGradientSub;

    [DisplayName("X Position")]
    [Description("Changes the horizontal position of the main brown box.")]
    [DefaultValue(47)]
    public int XPos { get; set; } = 47;

    [DisplayName("Y Position")]
    [Description("Changes the vertical position of the main brown box.")]
    [DefaultValue(406)]
    public int YPos { get; set; } = 406;

    [DisplayName("Height")]
    [Description("Changes how tall the main brown box is")]
    [DefaultValue(120)]
    public int Height { get; set; } = 120;

    [DisplayName("Length")]
    [Description("Changes how long the main brown box is")]
    [DefaultValue(1000)]
    public int Length { get; set; } = 1000;
}
