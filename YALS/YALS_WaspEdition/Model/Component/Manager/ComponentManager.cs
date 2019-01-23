using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using YALS_WaspEdition.Model.Component.Connection;

namespace YALS_WaspEdition.Model.Component.Manager
{
    [Serializable()]
    public class ComponentManager : IComponentManager
    {
        private readonly IConnectionManager connectionManager;
        private bool isRunning;

        public ComponentManager(IConnectionManager manager)
        {
            this.connectionManager = manager ?? throw new ArgumentNullException(nameof(manager));
            this.Components = new List<INode>();
            this.isRunning = false;
        }



        public ICollection<INode> Components
        {
            get;
            private set;
        }

        public ICollection<IConnection> Connections
        {
            get
            {
                return this.connectionManager.Connections;
            }
        }

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
        }

        public event EventHandler StepFinished;

        public void AddNode(INode node)
        {
            this.Components.Add(node);
        }

        public void Connect(IPin outputPin, IPin inputPin)
        {
            this.connectionManager.Connect(outputPin, inputPin);
        }

        public void Disconnect(IPin outputPin, IPin inputPin)
        {
            this.connectionManager.Disconnect(outputPin, inputPin);
        }

        public void Play()
        {
            this.isRunning = !this.isRunning;

            while (this.isRunning)
            {
                this.Step();
            }
        }

        public Task PlayAsync()
        {
            var task = Task.Factory.StartNew(() => {
                this.Play();
            });

            return task;
        }

        public void RemoveNode(INode node)
        {
            this.Components.Remove(node);
        }

        public void Step()
        {
            foreach (var component in this.Components)
            {
                component.Execute();
            }

            this.FireStepFinished();
        }

        public Task StepAsync()
        {
            var task = Task.Factory.StartNew(() => {
                this.Step();
            });

            return task;
        }

        public void Stop()
        {
            this.isRunning = false;
        }

        public Task StopAsync()
        {
            var task = Task.Factory.StartNew(() => {
                this.Stop();
            });

            return task;
        }

        protected internal void FireStepFinished()
        {
            this.StepFinished?.Invoke(this, EventArgs.Empty);
        }

        private bool CheckIfPinIsInput(INode component, IPin pin)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));
            if (pin == null) throw new ArgumentNullException(nameof(pin));

            var isInputPin = component.Inputs.Contains(pin);
            return isInputPin;
        }

        private bool CheckIfPinIsOutput(INode component, IPin pin)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));
            if (pin == null) throw new ArgumentNullException(nameof(pin));

            var isOutputPin = component.Outputs.Contains(pin);
            return isOutputPin;
        }
    }
}
