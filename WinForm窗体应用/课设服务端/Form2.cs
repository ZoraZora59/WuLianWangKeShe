﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace 课设服务端
{
    public partial class control : Form
    {
        transducerServer ts;

        public control()
        {
            InitializeComponent();
        }
        
        private void control_FormClosed(object sender, FormClosedEventArgs e)
        {
            ts.socketServerClose();
            System.Environment.Exit(0);
        }
        private void buttonOn_Click(object sender, EventArgs e)
        {
            ts.ctrl("CurtainOn");
        }
        private void buttonOff_Click(object sender, EventArgs e)
        {
            ts.ctrl("CurtainOff");
        }
        private void control_Load(object sender, EventArgs e)
        {
            ts = new transducerServer();
            ts.thStart();

            this.timer1.Interval = 1000;
            this.timer1.Start();
        }

        private void thStart()
        {
            Thread showlabelLight = new Thread(new ThreadStart(showLight));
            showlabelLight.Start();
        }
        public void showLight()
        {
            while (true)
            {
                labelLight.Text = transducerServer.light.ToString();
                Thread.Sleep(transducerServer.freq);
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            labelLight.Text = transducerServer.light.ToString();
            if(transducerServer.light>transducerServer.lightCheck)
            {
                ts.ctrl("CurtainOff");
            }
            else
            {
                ts.ctrl("CurtainOn");
            }

        }
    }
}
