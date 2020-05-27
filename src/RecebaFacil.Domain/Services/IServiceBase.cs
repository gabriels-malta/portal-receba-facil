namespace RecebaFacil.Domain.Services
{
    public interface IServiceBase<TEntity, TId>
    {
        /// <summary>
        /// Busca uma entidade de acordo om o id fornecido
        /// </summary>
        /// <param name="id">Id referente a entidade</param>
        /// <returns>Entidade encontrada ou null</returns>
        TEntity BuscarPorId(TId id);

    }
}
