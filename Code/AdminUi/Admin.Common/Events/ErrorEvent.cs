namespace Common.Events
{
    using System;

    using EnergyTrading.Mdm.Contracts;

    public class ErrorEvent
    {
        public ErrorEvent(Exception exception)
        {
            this.Exception = exception;
            this.Error = exception.Message;
        }

        public ErrorEvent(Fault fault)
        {
            this.Fault = fault;

            if (fault != null)
            {
                this.Error = fault.Message;
            }
        }

        public ErrorEvent(string message)
        {
            this.Error = message;
        }

        public string Error { get; private set; }

        public Exception Exception { get; private set; }

        public Fault Fault { get; private set; }
    }
}