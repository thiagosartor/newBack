﻿using Domain.Entities;
using Infrasctructure.DAO.ORM.Contexts;
using Infrastructure.DAO.ORM.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.DAO.EF
{
    public abstract class RepositoryBase<T> where T : Entity
    {
        protected DiarioAcademiaContext dataContext;
        protected readonly IDbSet<T> dbset;

        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected DiarioAcademiaContext DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
        }

        public virtual T Add(T entity)
        {
            dbset.Add(entity);
            return entity;
        }

        public virtual void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = dataContext.Entry(entity);

            if (dbEntityEntry.State == EntityState.Detached)
            {
                dbset.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;

            dbset.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = dataContext.Entry(entity);

            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                dbset.Attach(entity);
                dbset.Remove(entity);
            }

            dbset.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Deleted;

            dbset.Remove(entity);
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);

            dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IList<T> objects = dbset.Where<T>(where).ToList();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }

        public virtual T GetById(int id)
        {
            return dbset.Find(id);
        }

        public virtual T GetByIdIncluding(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbset;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.FirstOrDefault();
        }

        public virtual IList<T> GetAll()
        {
            return dbset.ToList();
        }

        public virtual IList<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }

        public virtual IList<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbset;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.ToList();
        }

        protected IQueryable<T> GetQueryable()
        {
            return dbset.AsQueryable();
        }
    }
}