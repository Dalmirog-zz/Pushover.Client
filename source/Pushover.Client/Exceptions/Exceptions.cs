using System;

namespace Pushover.Client.Exceptions
{
    public class PropertyLengthTooLongException : Exception
    {
        public PropertyLengthTooLongException (string propertyName, int propertyLength, int propertyMaxLength)
            :base(string.Format($"Property [{propertyName}] lenght ({propertyLength}) cannot be longer than {propertyMaxLength} chars"))
        {
        }
    }
}
