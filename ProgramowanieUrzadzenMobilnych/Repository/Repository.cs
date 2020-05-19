using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class Repository<T> where T : IRecord
    {
        protected Database database;

        public Repository(Database database)
        {
            this.database = database;
        }

        public T GetById(int id, IList<T> source)
        {
            T record = source
                .Where(t => t.Id == id)
                .Where(t => !t.IsDeleted)
                .FirstOrDefault();

            return record;
        }

        public void Add(T record, IList<T> source)
        {
            int newId = source.Count;

            record.Id = newId + 1;

            source.Add(record);
        }

        public void Delete(int id, IList<T> source)
        {
            T record = source
                .Where(t => t.Id == id)
                .FirstOrDefault();

            int index = source.IndexOf(record);
            source[index].IsDeleted = true;
        }
    }
}
