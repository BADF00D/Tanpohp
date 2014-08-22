using System;

namespace Tanpohp.Data
{
    /// <summary>
    /// This class provides a basic implemtation for ISealable.
    /// </summary>
    /// <remarks>Use Assign method for each property that should be sealable.</remarks>
    public class SealableObject : ISealable
    {
        public bool IsSealed { get; private set; }

        public void Seal()
        {
            IsSealed = true;
        }

        protected void Assign<T>(ref T backingField, T value)
        {
            if(IsSealed) throw new Exception("This class is sealed.");

            backingField = value;
        }
    }
}