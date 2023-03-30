using NinjaTrader.NinjaScript;
using Nt.Core.Data;

namespace Nt.Scripts.Ninjatrader
{
    public abstract class BaseNinjaScript : INinjaScript
    {
        //private readonly NinjaScriptBase _ninjascript;

        //public State State => _ninjascript.State;
        public State State => State.Configure;
        public NinjaScriptEvent NinjaScriptEvent { get; private set; }

        public BaseNinjaScript()
        {
        }

        public void OnNinjaScriptChange(NinjaScriptEvent ninjascriptEvent)
        {
            NinjaScriptEvent = ninjascriptEvent;

            if (NinjaScriptEvent == NinjaScriptEvent.BarUpdate)
                OnBarUpdate();
        }

        public void OnStateChange()
        {
            //if (_ninjascript.State == State.SetDefaults)
            //    SetDafaults();
            //else if (_ninjascript.State == State.Configure)
            //    Configure();
            //else if (_ninjascript.State == State.Active)
            //    Active();
            //else if (_ninjascript.State == State.DataLoaded)
            //    DataLoaded();
            //else if (_ninjascript.State == State.Historical)
            //    Historical();
            //else if (_ninjascript.State == State.Transition)
            //    Transition();
            //else if (_ninjascript.State == State.Realtime)
            //    Realtime();
            //else if (_ninjascript.State == State.Terminated)
            //    Terminated();
        }

        protected virtual void SetDafaults() { }
        protected virtual void Configure() { }
        protected virtual void Active() { }
        protected virtual void DataLoaded() { }
        protected virtual void Historical() { }
        protected virtual void Transition() { }
        protected virtual void Realtime() { }
        protected virtual void Terminated() { }
        
        protected virtual void OnBarUpdate() { }
    }
}
