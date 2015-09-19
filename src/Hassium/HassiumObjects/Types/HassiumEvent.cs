using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.HassiumObjects.Types
{
    public delegate HassiumObject HassiumEventHandler(HassiumObject[] args);

    public class HassiumEvent : HassiumObject
    {
        public event HassiumEventHandler EventHandler;

        public HassiumEvent()
        {
        }

        public void AddHandler(HassiumEventHandler hand)
        {
            EventHandler += hand;
        }

        public void RemoveHandler(HassiumEventHandler hand)
        {
            EventHandler -= hand;
        }

        public override HassiumObject Invoke(params HassiumObject[] args)
        {
            return EventHandler(args);
        }
    }
}