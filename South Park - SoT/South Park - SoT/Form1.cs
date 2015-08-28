﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Memory;

namespace South_Park___SoT
{
    public partial class Form1 : Form
    {
        public static IntPtr pHandle;
        private ProcessModule mainModule;
        public static string gameID, exp, LVL;
        public static float combatHP;

        public string codeFile = Application.StartupPath + @"\codes.ini";
        public Mem MemLib = new Mem();

        public bool loaded = false;

        private void openGame()
        {
            if (loaded)
                return;

            int gameProcId = MemLib.getProcIDFromName("South Park - The Stick of Truth");

            if (gameProcId != 0)
            {
                loaded = true;
                procID_label.Text = gameProcId.ToString();
                MemLib.OpenGameProcess(gameProcId);
                toggleHP.Text = "ON";
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                openGame();

                MemLib.writeMemory("combatHP", codeFile, "int", "99999");
                MemLib.writeMemory("combatMP", codeFile, "int", "20");
                MemLib.writeMemory("buddyCombatHP", codeFile, "int", "99999");
                MemLib.writeMemory("buddyCombatMP", codeFile, "int", "20");

                if (!String.ReferenceEquals(expBox.Text, exp))
                    expBox.Text = exp;

                if (!String.ReferenceEquals(levelBox.Text, LVL))
                    levelBox.Text = LVL;
            }
        }

        private void levelBox_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(levelBox.Text))
                MemLib.writeMemory("level", codeFile, "int", levelBox.Text);
        }

        private void expBox_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(expBox.Text))
                MemLib.writeMemory("experience", codeFile, "int", expBox.Text);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Process.Start("http://newagesoldier.com");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //dont want to update too fast.
            exp = MemLib.readString("experience", codeFile);
            LVL = MemLib.readString("level", codeFile);
        }
    }
}