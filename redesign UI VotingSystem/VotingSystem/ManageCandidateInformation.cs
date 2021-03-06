﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Imaging;
namespace VotingSystem
{
    public partial class ManageCandidateInformation : Form
    {
        public ManageCandidateInformation()
        {
            InitializeComponent();
            label3.Text = LoginInfo.CurrentUser.UserName;

        }
        string strcon, strsql;
        SqlConnection mycon;
        SqlCommand command;
        private bool DBConnect()
        {
            try
            {
                //strcon = "Data Source=DESKTOP-6UGITVT;Initial Catalog=Voting;Integrated Security=True";
                strcon = "Data Source=localhost;Initial Catalog=Voting;Integrated Security=True";
                mycon = new SqlConnection(strcon);
                mycon.Open();
                MessageBox.Show("DB Connect is good");
                return true;
                //Displayed when the database connection is successful
            }
            catch
            {
                MessageBox.Show("DB Connect is not connect");

                return false;
                //Displayed when the database connection is fail
            }
        }
        private bool check()
        {
            if (textBox1.Text.Length == 0)
            {
                textBox1.Select();
                return false;
            }
            if (textBox2.Text.Length == 0)
            {
                textBox2.Select();
                return false;
            }
            return true;
            //Ensure that the textbox is not blank and save the data
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ManageCandidateInformation_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        private void ManageCandidateInformation_DragEnter(object sender, DragEventArgs e)
        {
            //string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            //string file = files[0];
            //if (!file.ToLower().EndsWith(".png") && !file.ToLower().EndsWith(".jpg"))
            //{
            //    MessageBox.Show("Need Picture!");
            //    return;
            //}
           
            //pictureBox1.Load(file);

        }

        private void ManageCandidateInformation_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“votingDataSet.Voting”中。您可以根据需要移动或删除它。
            comboBox1.Text = " ";
            this.votingTableAdapter.Fill(this.votingDataSet.Voting);
            this.AllowDrop = true;
            label1.Text = DateTime.Now.ToString();
            timer1.Interval = 1000;
            timer1.Start();

        }
        public bool ExecuteNone(string[] sql)
        {
            bool result;
            SqlConnection conn = new SqlConnection(" Data Source=localhost;Initial Catalog=Voting;Integrated Security=True");
            //SqlConnection conn = new SqlConnection("Data Source=DESKTOP-6UGITVT;Initial Catalog=Voting;Integrated Security=True");
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.Transaction = tran;

            try
            {
                for (int i = 0; i < sql.Length; i++)
                {
                    if (sql[i] == null || sql[i] == "")
                    {
                        continue;
                    }
                    cmd.CommandText = sql[i];
                    cmd.ExecuteNonQuery();
                }
                tran.Commit();
                result = true;

            }

            catch (System.Exception)
            {
                tran.Rollback();
                result = false;
            }
            conn.Close();

            return result;
            //connect to the database and train the connection
        }
        private void button1_Click(object sender, EventArgs e)
        {
            

        }
      



        private void button2_Click(object sender, EventArgs e)
        {
           
            DBConnect();
            strsql = string.Format("insert into Candidate(VoteName,Name,Information,VoteNum) values('{0}','{1}','{2}',0)", comboBox1.Text, textBox1.Text,textBox2.Text);
            MessageBox.Show(strsql);
            command = new SqlCommand(strsql, mycon);
            try
            {
                command.ExecuteScalar();
                MessageBox.Show("upload successful");
                comboBox1.Text = " ";
                textBox1.Text = " ";
                textBox2.Text = " ";               
            }
            catch
            { 
                MessageBox.Show("upload failed");
            }
            finally
            {
                mycon.Close();
            }
        //connect to the database and save changes

        }
       

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.votingTableAdapter.FillBy(this.votingDataSet.Voting);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            //show data from the database in the massagebox
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            this.Hide();
            adminMenu.ShowDialog(this);
            //Interface conversion function
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString();
            //display the datetime
        }

        private int Write(string strSql, byte[] imageBytes)
        {
            //string connStr = "Data Source=DESKTOP-6UGITVT;Initial Catalog=Voting;Integrated Security=True";
            string connStr = "Data Source=localhost;Initial Catalog=Voting;Integrated Security=True";
            //Get the database path
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(strSql, conn))
                {
                    try
                    {
                        conn.Open();
                        SqlParameter sqlParameter = new SqlParameter("@image", SqlDbType.Image);
                        sqlParameter.Value = imageBytes;
                        // Numerical conversion
                        cmd.Parameters.Add(sqlParameter);
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
            }
        }


    }
}
