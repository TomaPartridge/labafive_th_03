using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace labafive_th_03
{
    //что-то вроде разделения ответственности: теперь за чтение "пароля" отвечает этот шикарный класс
    class AuthorisPanel
    {
        public string ReadFirstPas()
        {

            StreamReader r = new StreamReader("pas.txt");//grehrius?dlk
            string pass = r.ReadLine();
            int g = pass.Length / 5;
            string psw = "";
            for (int i = 0; i < pass.Length; i += g)
            {
                psw += pass[i] >> pass.Length;
                for (int k = i + 1; k < i + g; k++)
                    psw += ~pass[i];
            }

            r.BaseStream.Position = 0;//?

            return psw;
        }
    }
}
