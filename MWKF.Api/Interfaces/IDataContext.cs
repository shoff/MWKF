namespace AUSKF.Api.Interfaces
{
    public interface IDataContext : IQueryableDataContext
    {
        /// <summary>
        /// Tracks the changes.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        void TrackChanges(bool value);
    }
}