namespace Hassium.HassiumObjects
{
    public class HassiumModule : HassiumObject
    {
        public string Name { get; private set; }

        public HassiumModule(string name)
        {
            Name = name;
        }
    }
}