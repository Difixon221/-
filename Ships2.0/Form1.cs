using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;

namespace Ships2._0
{
    public partial class Form1 : Form
    {
        public static string Connection = @"Data Source=PK306NEW-10;Initial Catalog=Ships;Integrated Security=True";
        private SqlConnection sqlConnection = new SqlConnection(Connection);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "shipsDataSet.Outcomes". При необходимости она может быть перемещена или удалена.
            this.outcomesTableAdapter.Fill(this.shipsDataSet.Outcomes);
            sqlConnection.Open();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                Battles.Visible = true;
                Classes.Visible = false;
                Outcomes.Visible = false;
                Ships.Visible = false;
                BattlesInfo();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                Battles.Visible = false;
                Classes.Visible = true;
                Outcomes.Visible = false;
                Ships.Visible = false;
                ClassesInfo();
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                Battles.Visible = false;
                Classes.Visible = false;
                Outcomes.Visible = true;
                Ships.Visible = false;
                OutcomesInfo();
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                Battles.Visible = false;
                Classes.Visible = false;
                Outcomes.Visible = false;
                Ships.Visible = true;
                ShipsInfo();
            }

        }
        public void BattlesInfo()
        {
            Battles.Rows.Clear();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = $"Select * from Battles";
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Battles.Rows.Add(reader[0].ToString(), reader[1].ToString());
            }
            reader.Close();
        }
        public void ClassesInfo()
        {
            Classes.Rows.Clear();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = $"Select * from Classes";
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Classes.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString());
            }
            reader.Close();
        }
        public void OutcomesInfo()
        {
            Outcomes.Rows.Clear();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = $"Select * from Outcomes";
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Outcomes.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
            }
            reader.Close();
        }
        public void ShipsInfo()
        {
            Ships.Rows.Clear();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = $"Select * From Ships";
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                var id = Ships.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());

                if(int.Parse(reader[2].ToString()) < 1920)
                    Ships.Rows[id].DefaultCellStyle.BackColor = Color.Yellow;
            }
            reader.Close();
        }
    }
}
