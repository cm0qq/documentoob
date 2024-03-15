using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace documentoob
{
    public partial class sbp : Form
    {
        public sbp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string newPassword = textBox2.Text;
            string confirmPassword = textBox3.Text;
            string connectionString = "Host=localhost;Username=postgres;Password=1111;Database=documentoob";

            try
            {
                // Проверка, что новый пароль введен одинаково в двух текстовых полях
                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("Новый пароль и подтверждение пароля не совпадают.");
                    return;
                }

                if (!newPassword.Contains('@') && !newPassword.Contains('#') && !newPassword.Contains('%') && !newPassword.Contains(')') && !newPassword.Contains('(') && !newPassword.Contains('.') && !newPassword.Contains('<'))
                {
                    MessageBox.Show("Пароль должен содержать хотя бы один из символов @#%)(.<");
                    return;
                }

                if (newPassword.Count(char.IsLetter) != 5)
                {
                    MessageBox.Show("Пароль должен содержать 5 букв");
                    return;
                }

                if (newPassword.Count(char.IsDigit) != 3)
                {
                    MessageBox.Show("Пароль должен содержать 3 цифры");
                    return;
                }

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Обновление пароля
                    string updatePasswordQuery = "UPDATE Пользователи SET Пароль = @Пароль WHERE Логин = @Логин";

                    using (NpgsqlCommand updatePasswordCommand = new NpgsqlCommand(updatePasswordQuery, connection))
                    {
                        updatePasswordCommand.Parameters.AddWithValue("Пароль", newPassword);
                        updatePasswordCommand.Parameters.AddWithValue("@Логин", login);

                        int rowsAffected = updatePasswordCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Пароль успешно обновлен!");
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при обновлении пароля. Возможно, такого пользователя не существует.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }
    }
}
