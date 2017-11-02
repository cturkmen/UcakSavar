using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UcakSavar
{
    public interface IHareketEdebilir
    {
        void HAreketEt(Yonler yon);
    }
    public enum Yonler
        {
        Yukari,
        Asagi,
        Saga,
        Sola
    }
}
