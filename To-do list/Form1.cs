using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace To_do_list
{
    public partial class Form1 : Form
    {
        private TextBox taskTextBox;
        private ListBox tasksListBox;

        public Form1()
        {
            InitializeComponent();
            FormSetup();
        }

        private void FormSetup()
        {
            this.Size = new Size(400, 400);

            taskTextBox = new TextBox();
            taskTextBox.Location = new Point(20, 50);
            taskTextBox.Size = new Size(340, 20);
            this.Controls.Add(taskTextBox);

            
            tasksListBox = new ListBox();
            tasksListBox.Location = new Point(20, 110);
            tasksListBox.Size = new Size(340, 250);
            this.Controls.Add(tasksListBox);
        }
    }
}
