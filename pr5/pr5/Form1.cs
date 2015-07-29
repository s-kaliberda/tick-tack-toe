using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace pr5
{
    public partial class Form1 : Form
    {
        int[,] winsPos = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 },
                 { 1, 4, 7 }, { 2, 5, 8 }, { 3, 6, 9 }, { 1, 5, 9 }, { 7, 5, 3 } };
        string gameUser, gameComputer; Button[] buttons = new Button[9]; List<int> Gamer = new List<int>();
        List<int> Computer = new List<int>(); List<int> variants = new List<int>();
        bool[] clicked = new bool[9]; public Form1() { InitializeComponent(); }
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                buttons[i] = new Button();
                buttons[i].Width = 50; buttons[i].Height = 50;
                buttons[i].Location = new Point(10 + 60 * (i % 3), 30 + 60 * (i / 3));
                buttons[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F,
                    ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))),
                    System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                Controls.Add(buttons[i]); variants.Add(i + 1);
            }
            buttons[0].Click += b1_Click; buttons[1].Click += b2_Click; buttons[2].Click += b3_Click;
            buttons[3].Click += b4_Click; buttons[4].Click += b5_Click; buttons[5].Click += b6_Click;
            buttons[6].Click += b7_Click; buttons[7].Click += b8_Click; buttons[8].Click += b9_Click;
            gameUser = "x"; gameComputer = "o";
        }
        bool endgame()
        {
            for (int i = 0; i < 8; i++)
            {
                bool win = true; for (int j = 0; j < 3; j++)
                {
                    win = Gamer.Contains(winsPos[i, j]); if (!win) { break; }
                }
                if (win) { MessageBox.Show("Победил человек."); Application.Restart(); return true; }
            }
            for (int i = 0; i < 8; i++)
            {
                bool win = true; for (int j = 0; j < 3; j++)
                {
                    win = Computer.Contains(winsPos[i, j]); if (!win) { break; }
                }
                if (win) { MessageBox.Show("Победил компьютер."); Application.Restart(); return true; }
            }
            if ((Computer.Count + Gamer.Count) == 9) { MessageBox.Show("Ничья."); Application.Restart(); return true; } return false;
        }
        private void playComputer()
        {
            первыйХодToolStripMenuItem.Enabled = false; int selvar = 0;
            for (int i = 0; i < 8; i++)
            {
                int counter = 0; for (int j = 0; j < 3; j++)
                {
                    if (Computer.Contains(winsPos[i, j]))
                    {
                        counter++;
                    }
                    else { selvar = winsPos[i, j]; }
                }
                if ((counter == 2) && !clicked[selvar - 1])
                {
                    buttons[selvar - 1].Text = gameComputer; Computer.Add(selvar);
                    endgame(); return;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int counter = 0; for (int j = 0; j < 3; j++)
                {
                    if (Gamer.Contains(winsPos[i, j])) { counter++; } else { selvar = winsPos[i, j]; }
                }
                if ((counter == 2) && !clicked[selvar - 1])
                {
                    buttons[selvar - 1].Text = gameComputer;
                    Computer.Add(selvar); variants.Remove(selvar); clicked[selvar - 1] = true; endgame(); return;
                }
            }
            if (!clicked[4]) { selvar = 5; }
            else if (buttons[4].Text == "x")
            {
                if (!clicked[0]) { selvar = 1; }
                else if (!clicked[2]) { selvar = 3; }
                else if (!clicked[6]) { selvar = 7; }
                else if (!clicked[8]) { selvar = 9; }
                else
                {
                    Random rnd = new Random();
                    int sel = rnd.Next(1, variants.Count); selvar = variants[sel];
                }
            }
            else if (buttons[4].Text == gameComputer)
            {
                if ((Gamer.Contains(1) || Gamer.Contains(3) || Gamer.Contains(7) || Gamer.Contains(9)) && (Gamer.Contains(2) || Gamer.Contains(4) || Gamer.Contains(6) || Gamer.Contains(8)))
                {
                    if (Gamer.Contains(1) && !clicked[8]) { selvar = 9; }
                    else if (Gamer.Contains(3) && !clicked[6]) { selvar = 7; }
                    else if (Gamer.Contains(7) && !clicked[2]) { selvar = 3; }
                    else if (Gamer.Contains(9) && !clicked[0]) { selvar = 1; }
                    else
                    {
                        Random rnd = new Random();
                        int sel = rnd.Next(1, variants.Count); selvar = variants[sel];
                    }
                }
                else if ((Gamer.Contains(6) && Gamer.Contains(8)) && !clicked[8]) { selvar = 9; }
                else if ((Gamer.Contains(8) && Gamer.Contains(4)) && !clicked[6]) { selvar = 7; }
                else if ((Gamer.Contains(4) && Gamer.Contains(2)) && !clicked[0]) { selvar = 1; }
                else if ((Gamer.Contains(2) && Gamer.Contains(6)) && !clicked[2]) { selvar = 3; }
                else if ((Gamer[Gamer.Count - 1] == 0) && !clicked[8]) { selvar = 9; }
                else if ((Gamer[Gamer.Count - 1] == 3) && !clicked[6]) { selvar = 7; }
                else if ((Gamer[Gamer.Count - 1] == 9) && !clicked[0]) { selvar = 1; }
                else if ((Gamer[Gamer.Count - 1] == 7) && !clicked[2]) { selvar = 3; }
                else if (!clicked[1]) { selvar = 2; }
                else if (!clicked[3]) { selvar = 4; }
                else if (!clicked[5]) { selvar = 6; }
                else if (!clicked[7]) { selvar = 8; }
                else { Random rnd = new Random(); int sel = rnd.Next(1, variants.Count); selvar = variants[sel]; }
            }
            else { Random rnd = new Random(); int sel = rnd.Next(1, variants.Count); selvar = variants[sel]; }
            clicked[selvar - 1] = true; buttons[selvar - 1].Text = gameComputer; Computer.Add(selvar); variants.Remove(selvar); endgame();
        }
        private void click(int n)
        {
            первыйХодToolStripMenuItem.Enabled = false; if (clicked[n - 1])
            {
                MessageBox.Show("Данная ячейка уже содержит данные!" + n); return;
            } clicked[n - 1] = true;
            buttons[n - 1].Text = gameUser; Gamer.Add(n); variants.Remove(n); if (!endgame()) { playComputer(); }
        }
        private void b9_Click(object sender, EventArgs e) { click(9); }
        private void b8_Click(object sender, EventArgs e) { click(8); }
        private void b7_Click(object sender, EventArgs e) { click(7); }
        private void b6_Click(object sender, EventArgs e) { click(6); }
        private void b5_Click(object sender, EventArgs e) { click(5); }
        private void b4_Click(object sender, EventArgs e) { click(4); }
        private void b3_Click(object sender, EventArgs e) { click(3); }
        private void b2_Click(object sender, EventArgs e) { click(2); }
        private void b1_Click(object sender, EventArgs e) { click(1); }
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e) { Application.Restart(); }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e) { Application.Exit(); }
        private void первыйХодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            первыйХодToolStripMenuItem.Enabled = false;
            clicked[4] = true; Computer.Add(5); gameUser = "o"; gameComputer = "x"; buttons[4].Text = gameComputer;
        }
    }
}
