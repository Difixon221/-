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
        public string strKol;
        public string ship;
        public double procent;
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
                listBox1.Visible = false;
                BattlesInfo();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                Battles.Visible = false;
                Classes.Visible = true;
                Outcomes.Visible = false;
                Ships.Visible = false;
                listBox1.Visible = false;
                ClassesInfo();
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                Battles.Visible = false;
                Classes.Visible = false;
                Outcomes.Visible = true;
                Ships.Visible = false;
                listBox1.Visible = false;
                OutcomesInfo();
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                Battles.Visible = false;
                Classes.Visible = false;
                Outcomes.Visible = false;
                Ships.Visible = true;
                listBox1.Visible = false;
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

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Visible = true;
            Battles.Visible = false;
            Classes.Visible = false;
            Outcomes.Visible = false;
            Ships.Visible = false;
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = $"Select ship, Count(battle) as numbattle From Outcomes group by ship";
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                //получение значений, для примера
                ship = reader["ship"].ToString();
                strKol = reader["numbattle"].ToString();
                double a = Convert.ToDouble(strKol);
                procent = (a / 4)*100;
                listBox1.Items.Add(ship + " " + procent + "%");
            }
            reader.Close();
        }
    }
}
