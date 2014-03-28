namespace MDM.Sync
{
    using System;

    public class MdmLoadConcurrencyException : Exception
    {
        public MdmLoadConcurrencyException()
        {
        }

        public MdmLoadConcurrencyException(string message)
            : base(message)
        {
        }
    }
}