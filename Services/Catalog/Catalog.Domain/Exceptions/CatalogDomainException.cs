namespace Catalog.Domain.Exceptions
{
    [Serializable]
    public class CatalogDomainException : Exception
    {
        public CatalogDomainException() { }
        public CatalogDomainException(string message) : base(message) { }
        public CatalogDomainException(string message, Exception inner) : base(message, inner) { }
        protected CatalogDomainException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
