using _24.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _24
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private SQLiteConnection DB;
        private DataTable dataTable = new DataTable();
        #region дефолт загрузка
        private async void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
            DB = new SQLiteConnection(Database.connection);
            await DB.OpenAsync();
            SQLiteCommand command = new SQLiteCommand("SELECT ID, ProcessID, Priority, ResourceID, Kolvo, Requested, Highlighted, Owner, Price FROM PlannedResources", DB);
            SQLiteCommand command2 = new SQLiteCommand("SELECT ID, NameProcess, Priority, Class, OwnerID FROM Process", DB);
            using (var reader = command2.ExecuteReader())
            {
                // Очищаем предыдущие элементы ComboBox
                comboBox1.Items.Clear();

                // Перебираем результаты запроса
                while (reader.Read())
                {
                    // Получаем значение колонки, которое вам нужно использовать
                    var value = reader["NameProcess"].ToString(); // Замените "ColumnName" на имя колонки с данными, которые вы хотите использовать
                    var value2 = reader["Priority"].ToString();
                    var value3 = reader["OwnerID"].ToString();
                    // Добавляем значение в ComboBox
                    comboBox1.Items.Add(value);
                    comboBox2.Items.Add(value2);
                    comboBox4.Items.Add(value3);
                }
            }
            SQLiteCommand command3 = new SQLiteCommand("SELECT ID, ResourceName, KolvoR, Price FROM Resources", DB);
            using (var reader = command3.ExecuteReader())
            {
                // Очищаем предыдущие элементы ComboBox
                comboBox3.Items.Clear();

                // Перебираем результаты запроса
                while (reader.Read())
                {
                    // Получаем значение колонки, которое вам нужно использовать
                    var value = reader["ResourceName"].ToString(); // Замените "ColumnName" на имя колонки с данными, которые вы хотите использовать
                    var value2 = reader["kolvoR"].ToString();
                    var value3 = reader["Price"].ToString();
                    // Добавляем значение в ComboBox
                    comboBox3.Items.Add(value);
                    comboBox6.Items.Add(value2);
                    comboBox5.Items.Add(value3);
                }
            }

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("ProcessID", "ProcessID");
            dataGridView1.Columns.Add("Priority", "Priority");
            dataGridView1.Columns.Add("ResourceID", "ResourceID");
            dataGridView1.Columns.Add("Kolvo", "Kolvo");
            dataGridView1.Columns.Add("Requested", "Requested");
            dataGridView1.Columns.Add("Highlighted", "Highlighted");
            dataGridView1.Columns.Add("Owner", "Owner");
            dataGridView1.Columns.Add("Price", "Price");

            dataGridView1.Columns["ProcessID"].HeaderText = "ID процесса";
            dataGridView1.Columns["Priority"].HeaderText = "Приоритет";
            dataGridView1.Columns["ResourceID"].HeaderText = "ID ресурса";
            dataGridView1.Columns["Kolvo"].HeaderText = "Количество";
            dataGridView1.Columns["Requested"].HeaderText = "Запрошено";
            dataGridView1.Columns["Highlighted"].HeaderText = "Выделено";
            dataGridView1.Columns["Owner"].HeaderText = "Владелец";
            dataGridView1.Columns["Price"].HeaderText = "Цена";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataRow row in dataTable.Rows)
            {
                dataGridView1.Rows.Add(row["ID"], row["ProcessID"], row["Priority"], row["ResourceID"],
                    row["Kolvo"], row["Requested"], row["Highlighted"], row["Owner"], row["Price"]);
            }
            DB.Close();
        }
        #endregion
        #region 1выборка
        private async void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            DB = new SQLiteConnection(Database.connection);
            await DB.OpenAsync();
            SQLiteCommand command = new SQLiteCommand("SELECT ID, ProcessID, Priority, ResourceID, Kolvo, Requested, Highlighted, Owner, Price FROM PlannedResources", DB);

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("ProcessID", "ProcessID");
            dataGridView1.Columns.Add("Priority", "Priority");
            dataGridView1.Columns.Add("ResourceID", "ResourceID");
            dataGridView1.Columns.Add("Kolvo", "Kolvo");
            dataGridView1.Columns.Add("Requested", "Requested");
            dataGridView1.Columns.Add("Highlighted", "Highlighted");
            dataGridView1.Columns.Add("Owner", "Owner");
            dataGridView1.Columns.Add("Price", "Price");

            dataGridView1.Columns["ProcessID"].HeaderText = "ID процесса";
            dataGridView1.Columns["Priority"].HeaderText = "Приоритет";
            dataGridView1.Columns["ResourceID"].HeaderText = "ID ресурса";
            dataGridView1.Columns["Kolvo"].HeaderText = "Количество";
            dataGridView1.Columns["Requested"].HeaderText = "Запрошено";
            dataGridView1.Columns["Highlighted"].HeaderText = "Выделено";
            dataGridView1.Columns["Owner"].HeaderText = "Владелец";
            dataGridView1.Columns["Price"].HeaderText = "Цена";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataRow row in dataTable.Rows)
            {
                int requested = Convert.ToInt32(row["Requested"]);
                int highlighted = Convert.ToInt32(row["Highlighted"]);

                if (requested > highlighted)
                {
                    dataGridView1.Rows.Add(row["ID"], row["ProcessID"], row["Priority"], row["ResourceID"],
                    row["Kolvo"], row["Requested"], row["Highlighted"], row["Owner"], row["Price"]);
                }
            }
        }
        #endregion
        #region 2выборка
        private async void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            DB = new SQLiteConnection(Database.connection);
            await DB.OpenAsync();
            SQLiteCommand command = new SQLiteCommand("SELECT ID, ProcessID, Priority, ResourceID, Kolvo, Requested, Highlighted, Owner, Price FROM PlannedResources WHERE ResourceID = 'Data1'", DB);

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("ProcessID", "ProcessID");
            dataGridView1.Columns.Add("Priority", "Priority");
            dataGridView1.Columns.Add("ResourceID", "ResourceID");
            dataGridView1.Columns.Add("Kolvo", "Kolvo");
            dataGridView1.Columns.Add("Requested", "Requested");
            dataGridView1.Columns.Add("Highlighted", "Highlighted");
            dataGridView1.Columns.Add("Owner", "Owner");
            dataGridView1.Columns.Add("Price", "Price");

            dataGridView1.Columns["ProcessID"].HeaderText = "ID процесса";
            dataGridView1.Columns["Priority"].HeaderText = "Приоритет";
            dataGridView1.Columns["ResourceID"].HeaderText = "ID ресурса";
            dataGridView1.Columns["Kolvo"].HeaderText = "Количество";
            dataGridView1.Columns["Requested"].HeaderText = "Запрошено";
            dataGridView1.Columns["Highlighted"].HeaderText = "Выделено";
            dataGridView1.Columns["Owner"].HeaderText = "Владелец";
            dataGridView1.Columns["Price"].HeaderText = "Цена";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Определение порядка приоритетов
            string[] priorities = { "1", "2", "3", "4" };

            var sortedData = dataTable.AsEnumerable()
                .OrderByDescending(row => Array.IndexOf(priorities, row["Priority"].ToString()))
                .CopyToDataTable();

            foreach (DataRow row in sortedData.Rows)
            {
                dataGridView1.Rows.Add(row["ID"], row["ProcessID"], row["Priority"], row["ResourceID"],
                    row["Kolvo"], row["Requested"], row["Highlighted"], row["Owner"], row["Price"]);
            }

            DB.Close();
        }
        #endregion
        #region 4выборка
        private async void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            DB = new SQLiteConnection(Database.connection);
            await DB.OpenAsync();
            SQLiteCommand command = new SQLiteCommand("SELECT ID, ProcessID, Priority, ResourceID, Kolvo, Requested, Highlighted, Owner, Price FROM PlannedResources", DB);

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("ProcessID", "ProcessID");
            dataGridView1.Columns.Add("Priority", "Priority");
            dataGridView1.Columns.Add("ResourceID", "ResourceID");
            dataGridView1.Columns.Add("Kolvo", "Kolvo");
            dataGridView1.Columns.Add("Requested", "Requested");
            dataGridView1.Columns.Add("Highlighted", "Highlighted");
            dataGridView1.Columns.Add("Owner", "Owner");
            dataGridView1.Columns.Add("Price", "Price");

            dataGridView1.Columns["ProcessID"].HeaderText = "ID процесса";
            dataGridView1.Columns["Priority"].HeaderText = "Приоритет";
            dataGridView1.Columns["ResourceID"].HeaderText = "ID ресурса";
            dataGridView1.Columns["Kolvo"].HeaderText = "Количество";
            dataGridView1.Columns["Requested"].HeaderText = "Запрошено";
            dataGridView1.Columns["Highlighted"].HeaderText = "Выделено";
            dataGridView1.Columns["Owner"].HeaderText = "Владелец";
            dataGridView1.Columns["Price"].HeaderText = "Цена";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Dictionary<string, int> ownerSpending = new Dictionary<string, int>();

            foreach (DataRow row in dataTable.Rows)
            {
                dataGridView1.Rows.Add(row["ID"], row["ProcessID"], row["Priority"], row["ResourceID"],
                    row["Kolvo"], row["Requested"], row["Highlighted"], row["Owner"], row["Price"]);
                string owner = row["Owner"].ToString();
                int quantity = Convert.ToInt32(row["Requested"]);
                int price = Convert.ToInt32(row["Price"]);

                // Вычисляем расход для текущей строки
                int spending = quantity * price;

                // Проверяем, есть ли уже запись для данного владельца в словаре
                if (ownerSpending.ContainsKey(owner))
                {
                    // Если есть, добавляем текущий расход к существующему значению
                    ownerSpending[owner] += spending;
                }
                else
                {
                    // Если нет, создаем новую запись для владельца
                    ownerSpending[owner] = spending;
                }
            }

            // Переменные для отслеживания максимального общего расхода и владельца
            int maxSpending = 0;
            string maxSpender = "";

            // Проходимся по всем записям в словаре и находим владельца с наибольшим общим расходом
            foreach (var kvp in ownerSpending)
            {
                string owner = kvp.Key;
                int spending = kvp.Value;

                if (spending > maxSpending)
                {
                    maxSpending = spending;
                    maxSpender = owner;
                }
            }

            string message = $"Владелец, потративший больше всего: {maxSpender}";
            MessageBox.Show(message);
        }
        #endregion
        #region Обновление таблицы
        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            Form1_Load(sender, e);
        }
        #endregion
        #region таблица ресурсов
        private async void button7_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            DB = new SQLiteConnection(Database.connection);
            await DB.OpenAsync();
            SQLiteCommand command = new SQLiteCommand("SELECT ID, ResourceName, KolvoR, Price FROM Resources", DB);

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("ResourceName", "ResourceName");
            dataGridView1.Columns.Add("KolvoR", "KolvoR");
            dataGridView1.Columns.Add("Price", "Price");

            dataGridView1.Columns["ResourceName"].HeaderText = "Имя ресурса";
            dataGridView1.Columns["KolvoR"].HeaderText = "Количество";
            dataGridView1.Columns["Price"].HeaderText = "Цена";


            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataRow row in dataTable.Rows)
            {
                dataGridView1.Rows.Add(row["ID"], row["ResourceName"], row["KolvoR"], row["Price"]);
            }
            DB.Close();
        }
        #endregion
        #region таблица процессов
        private async void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            DB = new SQLiteConnection(Database.connection);
            await DB.OpenAsync();
            SQLiteCommand command = new SQLiteCommand("SELECT ID, NameProcess, Priority, Class, OwnerID FROM Process", DB);

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("NameProcess", "NameProcess");
            dataGridView1.Columns.Add("Priority", "Priority");
            dataGridView1.Columns.Add("Class", "Class");
            dataGridView1.Columns.Add("OwnerID", "OwnerID");

            dataGridView1.Columns["NameProcess"].HeaderText = "Имя процесса";
            dataGridView1.Columns["Priority"].HeaderText = "Приоритет";
            dataGridView1.Columns["Class"].HeaderText = "Класс";
            dataGridView1.Columns["OwnerID"].HeaderText = "Владелец";


            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataRow row in dataTable.Rows)
            {
                dataGridView1.Rows.Add(row["ID"], row["NameProcess"], row["Priority"], row["Class"],
                    row["OwnerID"]);
            }
            DB.Close();
        }
        #endregion
        #region процесс
        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox1.Items.Contains(comboBox1.SelectedItem))
            {
                DB = new SQLiteConnection(Database.connection);
                await DB.OpenAsync();
                // Получаем выбранное значение из comboBox1
                var selectedValue = comboBox1.SelectedItem.ToString();

                // Создаем новый SQL-запрос, чтобы получить значение из другого столбца по выбранному ID
                SQLiteCommand command2 = new SQLiteCommand("SELECT Priority, OwnerID FROM Process WHERE NameProcess = @selectedValue", DB);
                command2.Parameters.AddWithValue("@selectedValue", selectedValue);

                using (var reader = command2.ExecuteReader())
                {
                    // Проверяем, что результаты запроса есть
                    if (reader.Read())
                    {
                        // Получаем значение из столбца NameProcess
                        var value1 = reader["Priority"].ToString();

                        // Получаем значение из столбца OwnerID
                        var value2 = reader["OwnerID"].ToString();

                        // Устанавливаем значение в comboBox2 и comboBox4
                        comboBox2.SelectedItem = value1;
                        comboBox4.SelectedItem = value2;
                    }
                }
            }
        }
        #endregion
        #region ресурсы
        private async void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null && comboBox3.Items.Contains(comboBox3.SelectedItem))
            {
                DB = new SQLiteConnection(Database.connection);
                await DB.OpenAsync();
                // Получаем выбранное значение из comboBox1
                var selectedValue = comboBox3.SelectedItem.ToString();

                // Создаем новый SQL-запрос, чтобы получить значение из другого столбца по выбранному ID
                SQLiteCommand command3 = new SQLiteCommand("SELECT KolvoR, Price FROM Resources", DB);
                command3.Parameters.AddWithValue("@selectedValue", selectedValue);

                using (var reader = command3.ExecuteReader())
                {
                    // Проверяем, что результаты запроса есть
                    if (reader.Read())
                    {
                        // Получаем значение из столбца NameProcess
                        var value1 = reader["KolvoR"].ToString();

                        // Получаем значение из столбца OwnerID
                        var value2 = reader["Price"].ToString();

                        // Устанавливаем значение в comboBox2 и comboBox4
                        comboBox6.SelectedItem = value1;
                        comboBox5.SelectedItem = value2;
                    }
                }
            }
        }
        #endregion
        #region добавление записи
        private void button8_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=24.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(comboBox6.Text)
                    || string.IsNullOrEmpty(comboBox1.Text)
                    || string.IsNullOrEmpty(comboBox2.Text) || string.IsNullOrEmpty(comboBox3.Text)
                    || string.IsNullOrEmpty(comboBox4.Text) || string.IsNullOrEmpty(textBox2.Text)
                    || string.IsNullOrEmpty(comboBox5.Text))
                {
                    MessageBox.Show("Заполните все поля");
                    return;
                }
                string r = comboBox1.Text;
                string t = comboBox2.Text;
                string y = comboBox3.Text;
                string a = comboBox6.Text;
                string q = textBox1.Text;
                string w = textBox2.Text;
                string u = comboBox4.Text;
                string i = comboBox5.Text;

                string query = $"INSERT INTO PlannedResources (ProcessID, Priority, ResourceID, Kolvo, Requested, Highlighted, Owner, Price) " +
                               $"VALUES ('{r}', '{t}', '{y}', '{a}', '{q}', '{w}', '{u}', '{i}')";

                using (SQLiteCommand commandInsert = new SQLiteCommand(query, connection))
                {
                    commandInsert.ExecuteNonQuery();
                    MessageBox.Show("Запись добавлена");
                }
            }
        }
        #endregion
    }
}
