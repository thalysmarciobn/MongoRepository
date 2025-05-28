using System.Linq.Expressions;
using MongoDB.Driver;

namespace SharpMongoRepository.Interface;

/// <summary>
/// Representa uma interface gen�rica de reposit�rio para opera��es com MongoDB.
/// </summary>
/// <typeparam name="TDocument">Tipo do documento que implementa <see cref="IDocument"/>.</typeparam>
/// <remarks>
/// Esta interface fornece opera��es s�ncronas e ass�ncronas de CRUD para MongoDB,
/// com suporte a filtragem, proje��o, contagem e execu��o de transa��es.
/// </remarks>
public interface IMongoRepository<TDocument, TKey> where TDocument : IDocument<TKey>
{
    /// <summary>
    /// Encontra documentos que correspondem � defini��o de filtro do MongoDB especificada.
    /// </summary>
    /// <param name="filter">A defini��o de filtro do MongoDB.</param>
    /// <returns>Um <see cref="IFindFluent{TDocument, TDocument}"/> para opera��es adicionais de consulta.</returns>
    IFindFluent<TDocument, TDocument> Find(FilterDefinition<TDocument> filter);

    /// <summary>
    /// Fornece capacidades de consulta LINQ para a cole��o de documentos.
    /// </summary>
    /// <returns>Um <see cref="IQueryable{TDocument}"/> para realizar consultas LINQ.</returns>
    IQueryable<TDocument?> AsQueryable();

    /// <summary>
    /// Filtra documentos usando a express�o de predicado especificada.
    /// </summary>
    /// <param name="filterExpression">Express�o LINQ utilizada para filtrar os documentos.</param>
    /// <returns>Uma cole��o de documentos que correspondem ao filtro.</returns>
    IEnumerable<TDocument?> FilterBy(Expression<Func<TDocument, bool>> filterExpression);

    /// <summary>
    /// Filtra e projeta documentos utilizando as express�es fornecidas.
    /// </summary>
    /// <typeparam name="TProjected">Tipo do resultado projetado.</typeparam>
    /// <param name="filterExpression">Express�o LINQ usada para filtrar os documentos.</param>
    /// <param name="projectionExpression">Express�o LINQ usada para projetar os documentos.</param>
    /// <returns>Uma cole��o de resultados projetados.</returns>
    IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression);

    /// <summary>
    /// Recupera assincronamente todos os documentos da cole��o.
    /// </summary>
    /// <returns>Uma tarefa que retorna um cursor ass�ncrono para iterar sobre os documentos.</returns>
    Task<IAsyncCursor<TDocument>> AllAsync();

    /// <summary>
    /// Encontra um �nico documento que corresponda � express�o fornecida.
    /// </summary>
    /// <param name="filterExpression">Express�o LINQ para filtrar o documento.</param>
    /// <returns>O documento correspondente, ou null se n�o encontrado.</returns>
    TDocument? FindOne(Expression<Func<TDocument, bool>> filterExpression);

    /// <summary>
    /// Encontra assincronamente um �nico documento que corresponda � express�o fornecida.
    /// </summary>
    /// <param name="filterExpression">Express�o LINQ para filtrar o documento.</param>
    /// <returns>Uma tarefa que retorna o documento correspondente ou null.</returns>
    Task<TDocument?> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);

    /// <summary>
    /// Encontra um documento pelo seu identificador �nico.
    /// </summary>
    /// <param name="id">Representa��o em string do ObjectId do documento.</param>
    /// <returns>O documento se encontrado; caso contr�rio, null.</returns>
    TDocument? FindById(TKey id);

    /// <summary>
    /// Encontra assincronamente um documento pelo seu identificador �nico.
    /// </summary>
    /// <param name="id">Representa��o em string do ObjectId do documento.</param>
    /// <returns>Uma tarefa que retorna o documento se encontrado; caso contr�rio, null.</returns>
    Task<TDocument?> FindByIdAsync(TKey id);

    /// <summary>
    /// Insere um �nico documento na cole��o.
    /// </summary>
    /// <param name="document">Documento a ser inserido.</param>
    void InsertOne(TDocument document);

    /// <summary>
    /// Insere assincronamente um �nico documento na cole��o.
    /// </summary>
    /// <param name="document">Documento a ser inserido.</param>
    /// <returns>Uma tarefa que representa a opera��o ass�ncrona.</returns>
    Task InsertOneAsync(TDocument document);

    /// <summary>
    /// Insere m�ltiplos documentos na cole��o em uma �nica opera��o.
    /// </summary>
    /// <param name="documents">Cole��o de documentos a serem inseridos.</param>
    void InsertMany(ICollection<TDocument> documents);

    /// <summary>
    /// Insere assincronamente m�ltiplos documentos na cole��o.
    /// </summary>
    /// <param name="documents">Cole��o de documentos a serem inseridos.</param>
    /// <param name="options">Op��es opcionais para a opera��o de inser��o.</param>
    /// <returns>Uma tarefa que representa a opera��o ass�ncrona de inser��o.</returns>
    Task InsertManyAsync(ICollection<TDocument> documents, InsertManyOptions? options = null);

    /// <summary>
    /// Conta assincronamente o n�mero de documentos que correspondem � express�o de filtro.
    /// </summary>
    /// <param name="filterExpression">Express�o para filtrar os documentos.</param>
    /// <returns>Uma tarefa que retorna o n�mero de documentos correspondentes.</returns>
    Task<long> AsyncCount(Expression<Func<TDocument, bool>> filterExpression);

    /// <summary>
    /// Conta o n�mero de documentos que correspondem � express�o de filtro.
    /// </summary>
    /// <param name="filterExpression">Express�o para filtrar os documentos.</param>
    /// <returns>O n�mero de documentos correspondentes.</returns>
    long Count(Expression<Func<TDocument, bool>> filterExpression);

    /// <summary>
    /// Substitui um �nico documento identificado pelo seu Id.
    /// </summary>
    /// <param name="document">Novo documento para substitui��o.</param>
    /// <param name="options">Op��es para a opera��o de substitui��o.</param>
    /// <returns>O documento substitu�do.</returns>
    TDocument FindOneAndReplace(TDocument document, FindOneAndReplaceOptions<TDocument>? options);

    /// <summary>
    /// Substitui assincronamente um documento identificado pelo seu Id.
    /// </summary>
    /// <param name="document">Novo documento para substitui��o.</param>
    /// <param name="options">Op��es para a opera��o de substitui��o.</param>
    /// <returns>Uma tarefa que retorna o documento substitu�do.</returns>
    Task<TDocument> FindOneAndReplaceAsync(TDocument document, FindOneAndReplaceOptions<TDocument>? options);

    /// <summary>
    /// Exclui um �nico documento que corresponde � express�o de filtro.
    /// </summary>
    /// <param name="filterExpression">Express�o para localizar o documento a ser exclu�do.</param>
    /// <param name="options">Op��es para a opera��o de exclus�o.</param>
    void DeleteOne(Expression<Func<TDocument, bool>> filterExpression, FindOneAndDeleteOptions<TDocument>? options);

    /// <summary>
    /// Exclui assincronamente um �nico documento que corresponde � express�o de filtro.
    /// </summary>
    /// <param name="filterExpression">Express�o para localizar o documento a ser exclu�do.</param>
    /// <param name="options">Op��es para a opera��o de exclus�o.</param>
    /// <returns>Uma tarefa que representa a opera��o de exclus�o.</returns>
    Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression, FindOneAndDeleteOptions<TDocument>? options);

    /// <summary>
    /// Exclui um documento pelo seu identificador �nico.
    /// </summary>
    /// <param name="id">Representa��o em string do ObjectId do documento.</param>
    /// <param name="options">Op��es para a opera��o de exclus�o.</param>
    void DeleteById(TKey id, FindOneAndDeleteOptions<TDocument>? options);

    /// <summary>
    /// Exclui assincronamente um documento pelo seu identificador �nico.
    /// </summary>
    /// <param name="id">Representa��o em string do ObjectId do documento.</param>
    /// <param name="options">Op��es para a opera��o de exclus�o.</param>
    /// <returns>Uma tarefa que representa a opera��o de exclus�o.</returns>
    Task DeleteByIdAsync(TKey id, FindOneAndDeleteOptions<TDocument>? options);

    /// <summary>
    /// Exclui m�ltiplos documentos que correspondem � express�o de filtro.
    /// </summary>
    /// <param name="filterExpression">Express�o para localizar os documentos a serem exclu�dos.</param>
    void DeleteMany(Expression<Func<TDocument, bool>> filterExpression);

    /// <summary>
    /// Exclui assincronamente m�ltiplos documentos que correspondem � express�o de filtro.
    /// </summary>
    /// <param name="filterExpression">Express�o para localizar os documentos a serem exclu�dos.</param>
    /// <returns>Uma tarefa que representa a opera��o de exclus�o.</returns>
    Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);

    /// <summary>
    /// Executa uma s�rie de opera��es dentro de um contexto transacional do MongoDB, de forma ass�ncrona.
    /// </summary>
    /// <typeparam name="TResult">Tipo do resultado retornado pelo corpo da transa��o.</typeparam>
    /// <param name="transactionBody">Fun��o que define as opera��es transacionais usando a sess�o fornecida.</param>
    /// <returns>Uma tarefa que representa a opera��o transacional, contendo o resultado das opera��es executadas.</returns>
    Task<TResult> WithTransactionAsync<TResult>(
        Func<IClientSessionHandle, Task<TResult>> transactionBody);
}
