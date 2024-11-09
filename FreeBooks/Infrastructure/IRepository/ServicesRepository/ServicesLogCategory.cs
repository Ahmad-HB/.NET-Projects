using Domain.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.IRepository.ServicesRepository;

public class ServicesLogCategory: IServicesLogRepository<LogCategory>
{
    
    FreeBookDbContext _context;

    public ServicesLogCategory(FreeBookDbContext context)
    {
        _context = context;
    }
    
    public List<LogCategory> GetAll()
    {
        try
        {
            return _context.LogCategories.Include(x =>x.Category).OrderByDescending(x => x.Date).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public LogCategory FindById(Guid Id)
    {
        try
        {
            return _context.LogCategories.Include(x =>x.Category).FirstOrDefault(x => x.Id == Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool Save(Guid Id, Guid userId)
    {
        try
        {
            var logCategory = new LogCategory
            {
                Id = Guid.NewGuid(),
                Action = Helper.Save,
                Date = DateTime.Now,
                UserId = userId,
                CategoryId = Id
            };
            _context.LogCategories.Add(logCategory);
            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
            return false;
        }
    }

    public bool Update(Guid Id, Guid userId)
    {
        try
        {
            var logCategory = new LogCategory
            {
                Id = Guid.NewGuid(),
                Action = Helper.Update,
                Date = DateTime.Now,
                UserId = userId,
                CategoryId = Id
            };
            _context.LogCategories.Add(logCategory);
            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
            return false;
        }
    }

    public bool Delete(Guid Id, Guid userId)
    {
        try
        {

            var logCategory = new LogCategory
            {
                Id = Guid.NewGuid(),
                Action = Helper.Delete,
                Date = DateTime.Now,
                UserId = userId,
                CategoryId = Id
            };
            _context.LogCategories.Add(logCategory);
            _context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
            return false;
        }
    }

    public bool DeleteLog(Guid Id)
    {
        try
        {
            var result = FindById(Id);
            if (!result.Equals(null))
            {
                _context.LogCategories.Remove(result);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
            return false;
        }
    }
}