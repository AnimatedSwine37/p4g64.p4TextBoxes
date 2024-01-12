using System.ComponentModel;

namespace p4g64.p4TextBoxes.Configuration;
public class OrangeStripeConfig
{
    [DisplayName("Show Gradient")]
    [Description("Show the gradient in the orange stripe behind the main box.")]
    [DefaultValue(false)]
    public bool ShowGradient { get; set; } = false;

    [DisplayName("Orange Stripe Colour Main")]
    [Description("The main colour of the orange stripe behind the main box.")]
    public Colour GradientMain { get; set; } = Colour.OrangeStripeMain;

    [DisplayName("Orange Stripe Colour Secondary")]
    [Description("The secondary colour of the orange stripe behind the main box.\nThis is only used if gradient is shown.")]
    public Colour GradientSub { get; set; } = Colour.OrangeStripeSub;

    [DisplayName("X Position")]
    [Description("Changes the horizontal position of the orange stripe behind the main box.")]
    [DefaultValue(36)]
    public int XPos { get; set; } = 36;

    [DisplayName("Y Position")]
    [Description("Changes the vertical position of the orange stripe behind the main box.")]
    [DefaultValue(411)]
    public int YPos { get; set; } = 411;

    [DisplayName("Height")]
    [Description("Changes how tall the orange stripe behind the main box is.")]
    [DefaultValue(126)]
    public int Height { get; set; } = 126;

    [DisplayName("Length")]
    [Description("Changes how long the orange stripe behind the main box is.")]
    [DefaultValue(928)]
    public int Length { get; set; } = 928;
}
