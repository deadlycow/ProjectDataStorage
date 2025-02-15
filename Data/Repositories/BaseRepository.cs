﻿using Data.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;
public abstract class BaseRepository<TEntity>(ContextDb context) : IBaseRepository<TEntity> where TEntity : class
{
  protected readonly ContextDb _context = context;
  protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

  public async Task<TEntity?> CreateAsync(TEntity entity)
  {
    try
    {
      await _dbSet.AddAsync(entity);
      var changes = await _context.SaveChangesAsync();
      return changes > 0 ? entity : null;
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"Error creating entity: {ex.Message}");
      return null;
    }
  }

  public virtual async Task<IEnumerable<TEntity>?> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null)
  {
    try
    {
      IQueryable<TEntity> query = _dbSet;

      if (includeExpression != null)
        query = includeExpression(query);
      
      return await query.ToListAsync();
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"Error fetching entities: {ex.Message}");
      return [];
    }
  }
  public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null)
  {
    try
    {
      IQueryable<TEntity> query = _dbSet;

      if (includeExpression != null)
        query = includeExpression(query);

      return await query.FirstOrDefaultAsync(predicate);
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"Error fetching entity: {ex.Message}");
      return null;
    }
  }
  public async Task<bool> UpdateAsync(TEntity entity)
  {
    try
    {
      _dbSet.Update(entity);
      return await _context.SaveChangesAsync() > 0;
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"Error updating entity: {ex.Message}");
      return false;
    }
  }
  public async Task<bool> DeleteAsync(TEntity entity)
  {
    try
    {
      _dbSet.Remove(entity);
      return await _context.SaveChangesAsync() > 0;
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"Error deleting entity: {ex.Message}");
      return false;
    }
  }
  public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
  {
    try
    {
      return await _dbSet.AnyAsync(predicate);
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"Error checking entity existence: {ex.Message}");
      return false;
    }
  }
}
