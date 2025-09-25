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
        private Button addButton;
        private Button clearAllButton;
        private Button removeButton;
        private Button editButton;
        private Button doneButton;


        public Form1()
        {
            InitializeComponent();
            FormSetup();
        }

        private void FormSetup()
        {
            this.Size = new Size(500, 400);

            taskTextBox = new TextBox();
            taskTextBox.Location = new Point(20, 50);
            taskTextBox.Size = new Size(340, 20);
            this.Controls.Add(taskTextBox);

           
            tasksListBox = new ListBox();
            tasksListBox.Location = new Point(20, 110);
            tasksListBox.Size = new Size(340, 250);
            this.Controls.Add(tasksListBox);

            addButton = new Button();
            addButton.Location = new Point(20, 20);
            addButton.Size = new Size(120, 20);
            addButton.Text = "Добавить";
            addButton.Click += AddButton_Click;
            this.Controls.Add(addButton);

            clearAllButton = new Button();
            clearAllButton.Location = new Point(160, 20);
            clearAllButton.Size = new Size(120, 20);
            clearAllButton.Text = "Очистить всё";
            clearAllButton.Click += Clear_All;
            this.Controls.Add(clearAllButton);

            removeButton = new Button();
            removeButton.Location = new Point(300, 20);
            removeButton.Size = new Size(120, 20);
            removeButton.Text = "Удалить Задачу";
            removeButton.Click += RemoveTask;
            this.Controls.Add(removeButton);

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(taskTextBox.Text))
            {
                tasksListBox.Items.Add(taskTextBox.Text);
                taskTextBox.Clear();
            }
        }

        private void Clear_All(object sender, EventArgs e)
        {
            tasksListBox.Items.Clear();
        }

        private void RemoveTask(object sender, EventArgs e)
        {
            if (tasksListBox.SelectedIndex != -1)
            {
                tasksListBox.Items.RemoveAt(tasksListBox.SelectedIndex);
            }
        }
    }
}
