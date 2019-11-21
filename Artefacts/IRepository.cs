namespace HRSaga.Artefacts
{
    public interface IRepository<T> where T : Entity
    {
        void Create(T Entity);
        void Delete(T Entity);
        void Update(T Entity);
        T getById();
        
    } 
}