﻿using NinjaTrader.NinjaScript;
using System;

namespace Nt.Scripts.Services.Design
{
    public class DesignNinjascript : NinjascriptBase
    {
        public DesignNinjascript() : base()
        {
        }

        protected override void Configure()
        {
            Instance = null;
            State = State.Configure;
            Print = Console.WriteLine;
            ClearOutputWindow = Console.Clear;
        }
    }
}