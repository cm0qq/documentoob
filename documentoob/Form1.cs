using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace documentoob
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int failedLoginAttempts = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            string connectionString = "Host=localhost;Username=postgres;Password=1111;Database=documentoob";
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT COUNT(*) FROM Пользователи WHERE Логин = @Логин AND Пароль = @Пароль";

                    using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Логин", login);
                        command.Parameters.AddWithValue("@Пароль", password);

                        int userCount = Convert.ToInt32(command.ExecuteScalar());

                        if (userCount > 0)
                        {
                            glav glav = new glav();
                            glav.Show();
                            this.Hide();
                            failedLoginAttempts = 0;
                        }
                        else
                        {
                            failedLoginAttempts++;

                            if (failedLoginAttempts >= 3)
                            {
                                MessageBox.Show("Вы ввели неверный пароль 3 раза. Перейдите на форму сброса пароля.");
                            }
                            else
                            {
                                MessageBox.Show("Неверный логин или пароль.");
                            }
                        }   
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
            Login.UserLogin = textBox1.Text;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sbp sbp = new sbp();
            sbp.Show();
            this.Hide();
        }

        public class Login
        {
            public static string UserLogin { get; set; }
        }
    }
}
