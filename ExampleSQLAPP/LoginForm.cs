using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleSQLAPP
{
    
    public partial class LoginForm : Form
    {
        
        public LoginForm()
        {
            InitializeComponent();
            //this.pwdField.autosize
            this.passField.Size = new Size(this.loginField.Width, this.loginField.Height);
            //MaterialSkinManager manager = MaterialSkinManager.Instance;
            //manager.AddFormToManage(this);
            //manager.Theme = MaterialSkinManager.Themes.LIGHT;
            //manager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue500, Primary.Blue500, Accent.LightBlue200, TextShade.WHITE); 
        }

        public LoginForm(string login, string pass)
        {
            InitializeComponent();
            this.passField.Size = new Size(this.loginField.Width, this.loginField.Height);
            loginField.Text = login;
            passField.Text = pass;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void closeButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Red;
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.White;
        }

        Point lastPoint;
        private void mainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void mainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string loginUser = loginField.Text;
            string passUser = passField.Text;

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @userLogin AND `pass` = @userPass", db.getConnection());
            command.Parameters.Add("@userLogin", MySqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@userPass", MySqlDbType.VarChar).Value = passUser;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                this.Hide();
                MainForm mainForm = new MainForm(loginField.Text);
                mainForm.Show();
            }

            else
                MessageBox.Show("Проверьте правильность заполения логина и пароля");
        }

        private void registerLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
        }

        private void registerLabel_MouseEnter(object sender, EventArgs e)
        {
            registerLabel.ForeColor = Color.White;
        }

        private void registerLabel_MouseLeave(object sender, EventArgs e)
        {
            registerLabel.ForeColor = Color.MidnightBlue;
        }
    }
}
