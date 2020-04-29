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
    public partial class RegisterForm : Form
    {
        setDefault setDef = new setDefault();
        public RegisterForm()
        {
            InitializeComponent();

            this.passField.Size = new Size(this.loginField.Width, this.loginField.Height);
            this.userSurnameField.Size = new Size(this.loginField.Width, this.loginField.Height);

            userNameField.ForeColor = Color.Gray;
            userSurnameField.ForeColor = Color.Gray;
            userNameField.Text = "Введите имя";//"Введите имя";
            userSurnameField.Text = "Введите фамилию";

            loginField.ForeColor = Color.Gray;
            loginField.Text = "Введите логин";

            passField.UseSystemPasswordChar = false;
            passField.ForeColor = Color.Gray;
            passField.Text = "Введите пароль";
        }

        Point lastPoint;
        private void mainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void mainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
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

        private void userNameField_Enter(object sender, EventArgs e)
        {
            if (userNameField.Text == "Введите имя")
            {
                userNameField.Text = "";
                userNameField.ForeColor = Color.Black;
            }
        }

        private void userNameField_Leave(object sender, EventArgs e)
        {
            if (userNameField.Text == "")
            {
                userNameField.ForeColor = Color.Gray;
                userNameField.Text = "Введите имя";
            }
        }

        private void userSurnameField_Enter(object sender, EventArgs e)
        {
            if (userSurnameField.Text == "Введите фамилию")
            {
                userSurnameField.Text = "";
                userSurnameField.ForeColor = Color.Black;
            }
        }

        private void userSurnameField_Leave(object sender, EventArgs e)
        {
            if (userSurnameField.Text == "")
            {
                userSurnameField.ForeColor = Color.Gray;
                userSurnameField.Text = "Введите фамилию";
            }
        }

        private void loginField_Enter(object sender, EventArgs e)
        {
            if (loginField.Text == "Введите логин")
            {
                loginField.Text = "";
                loginField.ForeColor = Color.Black;
            }
        }

        private void loginField_Leave(object sender, EventArgs e)
        {
            if (loginField.Text == "")
            {
                loginField.ForeColor = Color.Gray;
                loginField.Text = "Введите логин";
            }
        }

        private void passField_Enter(object sender, EventArgs e)
        {
            if (passField.Text == "Введите пароль")
            {
                passField.Text = "";
                passField.UseSystemPasswordChar = true;
                passField.ForeColor = Color.Black;
            }
        }

        private void passField_Leave(object sender, EventArgs e)
        {
            if (passField.Text == "")
            {
                passField.ForeColor = Color.Gray;
                passField.UseSystemPasswordChar = false;
                passField.Text = "Введите пароль";
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {

            if (userNameField.Text == "Введите имя")
            {
                MessageBox.Show("Необходиме ввести имя");
                return;
            }

            if (userSurnameField.Text == "Введите фамилию")
            {
                MessageBox.Show("Необходиме ввести фамилию");
                return;
            }

            if (loginField.Text == "Введите логин")
            {
                MessageBox.Show("Необходиме ввести логин");
                return;
            }

            if (passField.Text == "Введите пароль")
            {
                MessageBox.Show("Необходиме ввести пароль");
                return;
            }

            if (isUserExists())
            {
                MessageBox.Show("Такой логин уже занят, введите другой");
                return;
            }

            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `pass`, `name`, `surname`) " +
                                                    "VALUES (@login, @pass, @name, @surname);", db.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginField.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = passField.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = userNameField.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = userSurnameField.Text;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Регистрация прошла успешно");
                this.Hide();
                LoginForm loginForm = new LoginForm(loginField.Text, passField.Text);
                loginForm.Show();
            }
            else
            {
                MessageBox.Show("Аккаунт не был создан");
            }

            db.closeConnection();
        }

        public Boolean isUserExists()
        {
            string loginUser = loginField.Text;
            string passUser = passField.Text;

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @userLogin", db.getConnection());
            command.Parameters.Add("@userLogin", MySqlDbType.VarChar).Value = loginUser;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
                return true;
            else
                return false;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm(loginField.Text, passField.Text);
            loginForm.Show();
            this.Close();
        }

   
    }
}
