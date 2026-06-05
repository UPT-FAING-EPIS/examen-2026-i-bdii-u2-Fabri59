namespace Ticketing.EntityMapper.Mapping;

public interface IDocumentMapper<TDomain, TDocument>
{
    TDocument ToDocument(TDomain entity);
    TDomain ToDomain(TDocument document);
}
