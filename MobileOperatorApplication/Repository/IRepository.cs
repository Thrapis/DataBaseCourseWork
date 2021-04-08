using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileOperatorApplication.Repository
{
    interface IRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetAll(); // получение всех объектов
        T Get(int id); // получение одного объекта по id
        int Insert(T item); // создание объекта
        int Update(T item); // обновление объекта
        int Delete(int id); // удаление объекта по id
    }
}
