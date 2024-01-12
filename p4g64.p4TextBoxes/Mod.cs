using p4g64.p4TextBoxes.Template;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Hooks.Definitions.X64;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using System.IO;
using System.Runtime.InteropServices;
using static p4g64.p4TextBoxes.Colour;
using IReloadedHooks = Reloaded.Hooks.ReloadedII.Interfaces.IReloadedHooks;

namespace p4g64.p4TextBoxes;
/// <summary>
/// Your mod logic goes here.
/// </summary>
public class Mod : ModBase // <= Do not Remove.
{
    /// <summary>
    /// Provides access to the mod loader API.
    /// </summary>
    private readonly IModLoader _modLoader;

    /// <summary>
    /// Provides access to the Reloaded.Hooks API.
    /// </summary>
    /// <remarks>This is null if you remove dependency on Reloaded.SharedLib.Hooks in your mod.</remarks>
    private readonly IReloadedHooks? _hooks;

    /// <summary>
    /// Provides access to the Reloaded logger.
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    /// Entry point into the mod, instance that created this class.
    /// </summary>
    private readonly IMod _owner;

    /// <summary>
    /// Provides access to this mod's configuration.
    /// </summary>
    private Config _configuration;

    /// <summary>
    /// The configuration of the currently executing mod.
    /// </summary>
    private readonly IModConfig _modConfig;

    private IAsmHook _openingYellowOutlineHook;
    private IAsmHook _closingYellowOutlineHook;
    private IAsmHook _openingOrangeOutlineHook;

    private IHook<Action> _renderMessageBoxHook;
    private IHook<Action> _renderBackgroundHook;
    private GetAngleDelegate GetAngle;

    private RenderRectangleDelegate RenderRectangle;
    private Action BeforeRenderRectangle;

    public Mod(ModContext context)
    {
        _modLoader = context.ModLoader;
        _hooks = context.Hooks;
        _logger = context.Logger;
        _owner = context.Owner;
        _configuration = context.Configuration;
        _modConfig = context.ModConfig;

        Utils.Initialise(_logger, _configuration, _modLoader);


        Utils.SigScan("89 5C 24 ?? E8 ?? ?? ?? ?? F3 0F 10 05 ?? ?? ?? ?? 44 8B CD", "Opening Yellow Outline", address =>
        {
            // We don't need to actually do anything, just remove the old code :)
            string[] function =
            {

            };
            _openingYellowOutlineHook = _hooks.CreateAsmHook(function, address, AsmHookBehaviour.DoNotExecuteOriginal).Activate();
            if (_configuration.ShowYellowStripe)
                _openingYellowOutlineHook.Disable();
        });

        Utils.SigScan("89 5C 24 ?? E8 ?? ?? ?? ?? F3 41 0F 10 46 ??", "Closing Yellow Outline", address =>
        {
            // We don't need to actually do anything, just remove the old code :)
            string[] function =
            {

            };
            _closingYellowOutlineHook = _hooks.CreateAsmHook(function, address, AsmHookBehaviour.DoNotExecuteOriginal).Activate();
            if (_configuration.ShowYellowStripe)
                _closingYellowOutlineHook.Disable();
        });

        Utils.SigScan("E8 ?? ?? ?? ?? F3 0F 10 15 ?? ?? ?? ?? 44 8B CD", "Opening Orange Stripe", address =>
        {
            string[] function =
            {

            };
            _openingOrangeOutlineHook = _hooks.CreateAsmHook(function, address, AsmHookBehaviour.DoNotExecuteOriginal).Activate();
        });

        Utils.SigScan("48 89 5C 24 ?? 57 48 83 EC 30 48 8D 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 33 FF 8B DF 66 0F 1F 44 ?? 00 8B CB E8 ?? ?? ?? ?? FF C3 83 FB 19 7C ?? BA 02 00 00 00", "BeforeRenderRectangle", address =>
        {
            BeforeRenderRectangle = _hooks.CreateWrapper<Action>(address, out _);
        });

        Utils.SigScan("4C 8B DC 55 41 56 41 57", "RenderRectangle", address =>
        {
            RenderRectangle = _hooks.CreateWrapper<RenderRectangleDelegate>(address, out _);
        });

        Utils.SigScan("48 83 EC 28 48 8D 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 33 C9 48 8D 05 ?? ?? ?? ??", "GetAngle", address =>
        {
            GetAngle = _hooks.CreateWrapper<GetAngleDelegate>(address, out _);
        });

        Utils.SigScan("48 89 5C 24 ?? 57 48 81 EC 80 00 00 00 48 8D 0D ?? ?? ?? ?? E8 ?? ?? ?? ??", "Render Message Box", address =>
        {
            _renderMessageBoxHook = _hooks.CreateHook(RenderMessageBox, address).Activate();
        });

        Utils.SigScan("48 8B C4 48 81 EC C8 00 00 00 0F 29 70 ?? 48 8D 0D ?? ?? ?? ?? 0F 29 78 ?? 44 0F 29 40 ?? 44 0F 29 48 ?? E8 ?? ?? ?? ??", "Render Message Box Background", address =>
        {
            _renderBackgroundHook = _hooks.CreateHook(RenderBackground, address).Activate();
        });

    }

    private void RenderMessageBox()
    {
        BeforeRenderRectangle();
        RenderRectangle(_configuration.MainBox.XPos, _configuration.MainBox.YPos, 0, _configuration.MainBox.Length, _configuration.MainBox.Height,
            _configuration.MainBox.GradientMain.Struct, _configuration.MainBox.GradientSub.Struct,
            1, 1, 0x390, 0x40, 0, 4, 4, (short)(_configuration.MainBox.ShowGradient ? 9 : 0));
    }

    private void RenderBackground()
    {
        BeforeRenderRectangle();
        float angle = GetAngle();
        float invAngle = 1 - angle;

        if (_configuration.ShowYellowStripe)
        {
            RenderRectangle(70, 413, 0, 0x36e, 0x84, YellowStripe.Struct, YellowStripe.Struct,
                1, 1, 0x36e, 0x42, invAngle * 3.0f + angle * 4.4f, 4, 4, 1);
        }

        // Orange stripe
        RenderRectangle(_configuration.OrangeStripe.XPos, _configuration.OrangeStripe.YPos, 0, _configuration.OrangeStripe.Length, _configuration.OrangeStripe.Height,
            _configuration.OrangeStripe.GradientMain.Struct, _configuration.OrangeStripe.GradientSub.Struct,
            1, 1, 0x38a, 0x40, invAngle + invAngle + angle * 3.4f, 4, 4, (short)(_configuration.OrangeStripe.ShowGradient ? 5 : 0));
    }

    private delegate void RenderRectangleDelegate(float xPos, float yPos, float param_3, int length, int height, ColourStruct gradientStart, ColourStruct gradientEnd,
        float stretchX, float stretchY, short endX2, short param_11, float angle, int param_13, float cornerRadius, short gradientType);

    private delegate float GetAngleDelegate();

    private struct Rectangle
    {
        internal float XPos;
        internal float YPos;
        internal float Length;
        internal float Height;
        internal float GradientMain;
        internal float GradientSub;
        internal float StretchX;
        internal float StretchY;
        internal short endX2;
        internal short param_11;
        internal float Angle;
        internal int unk;
        internal int param_13;
        internal float CornerRadius;
        internal int GradientType;
    }

    #region Standard Overrides
    public override void ConfigurationUpdated(Config configuration)
    {

        if (_configuration.ShowYellowStripe != configuration.ShowYellowStripe)
        {
            if (configuration.ShowYellowStripe)
            {
                _openingYellowOutlineHook.Enable();
                _closingYellowOutlineHook.Enable();
            }
            else
            {
                _openingYellowOutlineHook.Disable();
                _closingYellowOutlineHook.Disable();
            }
        }

        _configuration = configuration;
        _logger.WriteLine($"[{_modConfig.ModId}] Config Updated: Applying");
    }
    #endregion

    #region For Exports, Serialization etc.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Mod() { }
#pragma warning restore CS8618
    #endregion
}