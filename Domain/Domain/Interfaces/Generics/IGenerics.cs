namespace Domain.Interfaces.Generics
{
    public interface IGenerics<T> where T : class //T é qualquer tipo, mas com restrição de ser uma classe  -> principais beneficios: reutilização de código, segurança de tipo(Type Safety) e Desempenho (Evita operações boxing e unboxing)
    {
        Task Add(T objeto);
        Task Uppdate(T objeto);
        Task Delete(T objeto);
        Task<T> GetEntityById(int id);
        Task<List<T>> List();
    }
}
