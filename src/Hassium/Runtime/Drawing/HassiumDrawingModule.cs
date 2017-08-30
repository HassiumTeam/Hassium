namespace Hassium.Runtime.Drawing
{
    public class HassiumDrawingModule : InternalModule
    {
        public HassiumDrawingModule() : base("Drawing")
        {
            AddAttribute("Bitmap", HassiumBitmap.TypeDefinition);
            AddAttribute("Color", HassiumColor.TypeDefinition);
        }
    }
}
