using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Luxary.Services
{
    public class Dao<T> where T : IEntity
    {
        LiteDatabase database;
        string entityName;
        LiteCollection<T> collection;

        public Dao(LiteDatabase database, string entityName)
        {
            this.database = database;
            this.entityName = entityName;
            collection = database.GetCollection<T>(entityName);
        }

        public LiteCollection<T> GetCollection()
        {
            return collection;
        }

        public T Find(int id)
        {
            return Find(a => a.Id == id);
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return collection.FindOne(predicate);
        }

        public List<T> FindAll()
        {
            return new List<T>(collection.FindAll());
        }

        public List<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return new List<T>(collection.Find(predicate));
        }

        public void Save(T instance)
        {
            if (instance.Id == 0)
            {
                Insert(instance);
            }
            else
            {
                Update(instance);
            }
        }

        public int Delete(T instance)
        {
            return Delete(instance.Id);
        }

        public int Delete(int id)
        {
            return collection.Delete(a => a.Id == id);
        }

        private void Insert(T instance)
        {
            collection.Insert(instance);
        }

        private void Update(T instance)
        {
            collection.Update(instance);
        }

    }
}