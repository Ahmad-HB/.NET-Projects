namespace Infrastructure.IRepository;

public interface IServicesLogRepository<T> where T : class
{
    List<T> GetAll(); 
    
    T FindById(Guid Id);
    
    bool Save(Guid Id,Guid userId);
    
    bool Update(Guid Id,Guid userId);
    
    bool Delete(Guid Id, Guid userId);
    
    bool DeleteLog(Guid Id);
}