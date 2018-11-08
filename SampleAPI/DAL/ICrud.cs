using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.DAL
{
    /// <summary>
    /// Konsepmya men-standarisasi CRUD, agar semua memakai penamaan yang sama. Layaknya 
    /// </summary>
    public interface ICrud<T>
    {
        //Inumerable krn isinya bisa byak
        IEnumerable<T> GetAll();
        T GetByID(string id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(string id);

    }
}
