using Domain.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Domain.Entity.Category;

namespace Infrastructure.IRepository.ServicesRepository;

public class ServicesCategory : IServicesRepository<Category>
{
    FreeBookDbContext _context;

    public ServicesCategory(FreeBookDbContext context)
    {
        _context = context;
    }

    public List<Category> GetAll()
    {
        try
        {
            return _context.Categories.OrderBy(x => x.Name).Where(x=>x.CurrentState>0).ToList();
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public Category FindById(Guid Id)
    {
        try
        {
            return _context.Categories.FirstOrDefault(x => x.Id.Equals(Id) && x.CurrentState>0);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Category FindByName(string Name)
    {
        try
        {
            return _context.Categories.FirstOrDefault(x => x.Name.Equals(Name.Trim())  && x.CurrentState>0);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    

    public bool Save(Category model)
    {
        try
        {
            var result = FindById(model.Id);
            if (result == null)
            {
                model.Id = Guid.NewGuid();
                model.CurrentState = (int)Helper.eCurrentState.Active;
                _context.Categories.Add(model);
            }
            else
            {
                result.Name = model.Name;
                result.Description = model.Description;
                result.CurrentState = (int)Helper.eCurrentState.Active;
                _context.Categories.Update(result);
            }

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

    public bool Delete(Guid Id)
    {
        try
        {
            var result = FindById(Id);
            result.CurrentState = (int)Helper.eCurrentState.Deleted;
            _context.Categories.Update(result);
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
}