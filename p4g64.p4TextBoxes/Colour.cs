using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace p4g64.p4TextBoxes;
public class Colour
{
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct ColourStruct
    {
        public byte A;
        public byte B;
        public byte G;
        public byte R;
    }

    private byte _r;
    public byte R
    {
        get => _r;
        set
        {
            _r = value;
            var colourStruct = Struct;
            colourStruct.R = value;
            Struct = colourStruct;
        }
    }
    private byte _g;
    public byte G
    {
        get => _g;
        set
        {
            _g = value;
            var colourStruct = Struct;
            colourStruct.G = value;
            Struct = colourStruct;
        }
    }
    private byte _b;
    public byte B
    {
        get => _b;
        set
        {
            _b = value;
            var colourStruct = Struct;
            colourStruct.B = value;
            Struct = colourStruct;
        }
    }
    private byte _a;
    public byte A
    {
        get => _a;
        set
        {
            _a = value;
            var colourStruct = Struct;
            colourStruct.A = value;
            Struct = colourStruct;
        }
    }

    internal ColourStruct Struct { get; set; }

    public Colour(byte r, byte g, byte b, byte a)
    {
        R = r;
        G = g;
        B = b;
        A = a;

        Struct = new ColourStruct { R = R, G = G, B = B, A = A };
    }

    public static readonly Colour BoxGradientMain = new Colour(72, 67, 50, 0xFF);
    public static readonly Colour BoxGradientSub = new Colour(0x93, 0x7F, 0x56, 0xFF);

    public static readonly Colour YellowStripe = new Colour(0xFF, 0xEA, 0x2D, 0xFF);

    public static readonly Colour OrangeStripeMain = new Colour(255, 158, 17, 0xFF);
    public static readonly Colour OrangeStripeSub = new Colour(0xFF, 0x5A, 0x00, 0xFF);
}
