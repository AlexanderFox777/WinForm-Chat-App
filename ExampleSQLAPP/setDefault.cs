using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleSQLAPP
{
    class setDefault
    {
        public struct DefaultTextWithColor
        {
            public string text;
            public Color color;
            public DefaultTextWithColor(string s, Color c)
            {
                text = s;
                color = c;
            }
        }

        public DefaultTextWithColor userNameField = new DefaultTextWithColor("Введите имя", Color.Gray);
        public DefaultTextWithColor userSurnameField = new DefaultTextWithColor("Введите фамилию", Color.Gray);
        public DefaultTextWithColor passField = new DefaultTextWithColor("Введите пароль", Color.Gray);
        public DefaultTextWithColor loginField = new DefaultTextWithColor("Введите логин", Color.Gray);

       



    }
}
