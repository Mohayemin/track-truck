using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

namespace Ssi.TrackTruck.Bussiness.DAL
{
    public interface IRepository
    {
        T FindOne<T>(Expression<Func<T, bool>> condition);
        T Create<T>(T item) where T: IEntity;
        IQueryable<T> GetAll<T>();
        T GetById<T>(string id) where T : IEntity;
        IQueryable<T> WhereIn<T, TProp>(Expression<Func<T, TProp>> property, IEnumerable<TProp> values);
        IQueryable<T> GetAllProjected<T>(params Expression<Func<T, object>>[] property);
        bool Exists<T>(Expression<Func<T, bool>> condition) where T : IEntity;
        T SoftDelete<T>(string id) where T : IEntity;
        IQueryable<T> GetAllUndeleted<T>() where T:IEntity;
        T Save<T>(T item);
        IQueryable<T> GetWhere<T>(Expression<Func<T, bool>> condition);
    }
}
