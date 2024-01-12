using p4g64.p4TextBoxes.Configuration;
using p4g64.p4TextBoxes.Template.Configuration;
using System.ComponentModel;

namespace p4g64.p4TextBoxes;
public class Config : Configurable<Config>
{
    [DisplayName("Show Yellow Stripe")]
    [Description("Show the yellow stripe behind the main message box.")]
    [DefaultValue(false)]
    public bool ShowYellowStripe { get; set; } = false;

    [DisplayName("Main Box Configuration")]
    [Description("Configure the main brown message window.")]
    public MainBoxConfig MainBox { get; set; } = new();

    [DisplayName("Orange Stripe Configuration")]
    [Description("Configure the orange stripe behind the main box.")]
    public OrangeStripeConfig OrangeStripe { get; set; } = new();

    [DisplayName("Debug Mode")]
    [Description("Logs additional information to the console that is useful for debugging.")]
    [DefaultValue(false)]
    public bool DebugEnabled { get; set; } = false;
}

/// <summary>
/// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
/// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
/// </summary>
public class ConfiguratorMixin : ConfiguratorMixinBase
{
    // 
}