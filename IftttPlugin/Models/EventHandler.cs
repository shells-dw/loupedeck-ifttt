namespace Loupedeck.IftttPlugin.Models
{
    using System;

  
        public class IftttEventTrigger : EventArgs
        {
            public string TriggeredEvent { get; }

            public IftttEventTrigger(string triggeredEvent)
            {
                this.TriggeredEvent = triggeredEvent;
            }
        }
    }
