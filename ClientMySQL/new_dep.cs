﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientMySQL
{
    public partial class new_dep : Form
    {
        public new_dep()
        {
            InitializeComponent();
            type.SelectedIndex = 0;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string result = null; int year = 1;
            bool check1 = radioButton1.Checked; bool check2 = radioButton2.Checked; bool check3 = radioButton3.Checked;
            if (check1)
                year = 1;
            if (check2)
                year = 3;
            if (check3)
                year = 5;
            if (type.SelectedIndex == 0)
            {
                result = SocketClient.SendMessage("/new_dep;" + Client.id + ";" + type.SelectedItem.ToString() + ";"+year);                
                if (result == "success!")
                    this.Hide();
                else { label1.Text = result; }
            }
            if (type.SelectedIndex == 1)
            {
                bool check21 = creditBox.SelectedIndex >= 0;
                int credit = 0;
                if (check21)
                {
                    if (creditBox.SelectedIndex == 0) credit = 100000;
                    else if (creditBox.SelectedIndex == 1) credit = 500000;
                    else if (creditBox.SelectedIndex == 2) credit = 1000000;
                    result = SocketClient.SendMessage("/new_dep;" + Client.id + ";" + type.SelectedItem.ToString() + ";"
                        +year+";" + credit);
                    if (result == "success!")
                        this.Hide();
                    else { label1.Text = result; }
                }
            }
            if (type.SelectedIndex == 2)
            {
                bool check31 = string.IsNullOrEmpty(textCredit.Text);
                bool check32 = int.TryParse(textCredit.Text, out int ignore);
                if (!check31 && check32)
                {
                    result = SocketClient.SendMessage("/new_dep;" + Client.id + ";" + type.SelectedItem.ToString() + ";"
                        +year+";" + textCredit.Text);
                    if (result == "success!")
                        this.Hide();
                    else { label1.Text = result; }
                }
            }
        }

        private void type_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (type.SelectedIndex == 1 || type.SelectedIndex == 2)
            {
                //radioButton1.Checked = true;
                if (type.SelectedIndex == 1)
                {
                    credit.Text = "Размер кредита";
                    textCredit.Visible = false;
                    creditBox.Visible = true;
                    //radioButton2.Enabled = false;
                }
                else
                {
                    credit.Text = "Сумма взноса";
                    creditBox.Visible = false;
                    textCredit.Visible = true;
                    //radioButton2.Enabled = true;
                }
                credit.Visible = true;
                button1.Location = new Point(65, 202);
                this.Size = new Size(259, 282);
            }
            else
            {
                radioButton1.Checked = false;
                //radioButton2.Enabled = true;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                creditBox.Visible = false;
                credit.Visible = false;
                textCredit.Visible = false;
                button1.Location = new Point(65, 157);
                this.Size = new Size(259, 240);
            }
        }
    }
}
