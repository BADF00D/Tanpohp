namespace Tanpohp.Data
{
    /// <summary>
    /// Defines a object, that can be sealed in order to forbid changes.
    /// </summary>
    /// <remarks>Each implementation has to garantie that after Seal() was called, no changes are possible.</remarks>
    public interface ISealable
    {
        /// <summary>
        /// Determines whether object is sealed.
        /// </summary>
        bool IsSealed { get; }

        /// <summary>
        /// Seals this object.
        /// </summary>
        void Seal();
    }
}