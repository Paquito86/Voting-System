﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VotingSystem
{
    public partial class HomePage : Form
    {
        Image[] images = new Image[3];
        public HomePage()
        {
            InitializeComponent();
            images[0] = Properties.Resources.vote1;
            images[1] = Properties.Resources.vote2;
            images[2] = Properties.Resources.vote3;
        }
        public HomePage(string str)
        {
            InitializeComponent();
            TimeLabel.Text = str;
        }
        private int i = 0;
        ImageList ilist = new ImageList();
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartVoting startVoting = new StartVoting();
            this.Hide();
            startVoting.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CandidateInformation candidateInformation = new CandidateInformation();
            this.Close();
            candidateInformation.ShowDialog(this);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Image = images[i];
            i++;
            if (i == 3) { i = 0; }
        }

        private void registeredLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register register = new Register();
            this.Hide();
            register.ShowDialog(this);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}
