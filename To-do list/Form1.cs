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

        private NumericUpDown priorityNum;
        private ComboBox categoryComboBox;
        private DateTimePicker datePicker;
        private Label priorityLabel;
        private Label categoryLabel;
        private Label dateLabel;

        private List<Task> tasks = new List<Task>();

        public Form1()
        {
            InitializeComponent();
            FormSetup();
        }

        private void FormSetup()
        {
            this.Size = new Size(900, 400);

            taskTextBox = new TextBox();
            taskTextBox.Location = new Point(20, 50);
            taskTextBox.Size = new Size(340, 20);
            this.Controls.Add(taskTextBox);

            tasksListBox = new ListBox();
            tasksListBox.Location = new Point(20, 110);
            tasksListBox.Size = new Size(600, 250);
            this.Controls.Add(tasksListBox);

            addButton = new Button();
            addButton.Location = new Point(370, 50);
            addButton.Size = new Size(120, 20);
            addButton.Text = "Добавить задачу";
            addButton.Click += AddTask;
            this.Controls.Add(addButton);

            removeButton = new Button();
            removeButton.Location = new Point(500, 50);
            removeButton.Size = new Size(120, 20);
            removeButton.Text = "Удалить задачу";
            removeButton.Click += RemoveTask;
            this.Controls.Add(removeButton);

            clearAllButton = new Button();
            clearAllButton.Location = new Point(500, 80);
            clearAllButton.Size = new Size(120, 20);
            clearAllButton.Text = "Очистить всё";
            clearAllButton.Click += Clear_All;
            this.Controls.Add(clearAllButton);

            editButton = new Button();
            editButton.Location = new Point(370, 80);
            editButton.Size = new Size(120, 20);
            editButton.Text = "Редактировать";
            editButton.Click += EditTask;
            this.Controls.Add(editButton);

            doneButton = new Button();
            doneButton.Location = new Point(630, 80);
            doneButton.Size = new Size(120, 20);
            doneButton.Text = "Отметить выполненой";
            doneButton.Click += MarkAsDone;
            this.Controls.Add(doneButton);

            priorityNum = new NumericUpDown();
            priorityNum.Location = new Point(630, 135);
            priorityNum.Size = new Size(100, 20);
            priorityNum.Minimum = 1;
            priorityNum.Maximum = 10;
            priorityNum.Value = 1;
            this.Controls.Add(priorityNum);

            categoryComboBox = new ComboBox();
            categoryComboBox.Location = new Point(630, 185);
            categoryComboBox.Size = new Size(120, 20);
            categoryComboBox.Items.AddRange(new string[] { "Учеба", "Работа", "Дом", "Личное", "Здоровье", "Другое" });
            categoryComboBox.SelectedIndex = 5;
            this.Controls.Add(categoryComboBox);

            datePicker = new DateTimePicker();
            datePicker.Location = new Point(630, 235);
            datePicker.Size = new Size(120, 20);
            datePicker.Value = DateTime.Now;
            this.Controls.Add(datePicker);

            priorityLabel = new Label();
            priorityLabel.Location = new Point(630, 110);
            priorityLabel.Size = new Size(80, 20);
            priorityLabel.Text = "Приоритет:";
            this.Controls.Add(priorityLabel);

            categoryLabel = new Label();
            categoryLabel.Location = new Point(630, 160);
            categoryLabel.Size = new Size(80, 20);
            categoryLabel.Text = "Категория:";
            this.Controls.Add(categoryLabel);

            dateLabel = new Label();
            dateLabel.Location = new Point(630, 210);
            dateLabel.Size = new Size(160, 20);
            dateLabel.Text = "Срок выполнения:";
            this.Controls.Add(dateLabel);

        }

        private void AddTask(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(taskTextBox.Text))
            {   
                Task newTask = new Task(taskTextBox.Text, (int)priorityNum.Value, categoryComboBox.SelectedItem.ToString(), datePicker.Value);
                tasks.Add(newTask);
                tasksListBox.Items.Add(newTask.DisplayText);
                taskTextBox.Clear();
            }   
        }

        private void Clear_All(object sender, EventArgs e)
        {   
            tasks.Clear();
            tasksListBox.Items.Clear();
        }

        private void RemoveTask(object sender, EventArgs e)
        {
            if (tasksListBox.SelectedIndex != -1)
            {
                tasksListBox.Items.RemoveAt(tasksListBox.SelectedIndex);
            }
        }

        private void EditTask(object sender, EventArgs e)
        {
            if (tasksListBox.SelectedIndex != -1)
            {
                int selectedIndex = tasksListBox.SelectedIndex;
                Task selectedTask = tasks[selectedIndex];

                EditForm editForm = new EditForm(selectedTask);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    tasks[selectedIndex] = editForm.EditedTask;
                    tasksListBox.Items[selectedIndex] = editForm.EditedTask.DisplayText;

                }
            }
        }

        private void MarkAsDone(object sender, EventArgs e)
        {
            if (tasksListBox.SelectedIndex != -1)
            {
                int selectedIndex = tasksListBox.SelectedIndex;
                tasks[selectedIndex].IsCompleted = !tasks[selectedIndex].IsCompleted;
                tasksListBox.Items[selectedIndex] = tasks[selectedIndex].DisplayText;
            }
        }
    }

    public class EditForm : Form
    {
        private TextBox titleTextBox;
        private NumericUpDown priorityNum;
        private ComboBox categoryComboBox;
        private DateTimePicker datePicker;
        private CheckBox isCompletedCheckBox;
        private Button okButton;
        private Button cancelButton;

        private Label priorityLabel;
        private Label categoryLabel;
        private Label dateLabel;

        public Task EditedTask { get; private set; }

        public EditForm(Task taskToEdit)
        {
            InitializeComponents();
            LoadTaskData(taskToEdit);
        }

        private void InitializeComponents()
        {
            this.Size = new Size(400, 400);

            titleTextBox = new TextBox();
            titleTextBox.Location = new Point(20, 30);
            titleTextBox.Size = new Size(350, 20);
            this.Controls.Add(titleTextBox);

            priorityNum = new NumericUpDown();
            priorityNum.Location = new Point(20, 80);
            priorityNum.Size = new Size(100, 20);
            priorityNum.Minimum = 1;
            priorityNum.Maximum = 10;
            priorityNum.Value = 1;
            this.Controls.Add(priorityNum);

            categoryComboBox = new ComboBox();
            categoryComboBox.Location = new Point(20, 130);
            categoryComboBox.Size = new Size(150, 20);
            categoryComboBox.Items.AddRange(new string[] { "Учеба", "Работа", "Дом", "Личное", "Здоровье", "Другое" });
            this.Controls.Add(categoryComboBox);

            datePicker = new DateTimePicker();
            datePicker.Location = new Point(20, 180);
            datePicker.Size = new Size(150, 20);
            this.Controls.Add(datePicker);

            isCompletedCheckBox = new CheckBox();
            isCompletedCheckBox.Location = new Point(200, 80);
            isCompletedCheckBox.Text = "Выполнена";
            this.Controls.Add((isCompletedCheckBox));

            priorityLabel = new Label();
            priorityLabel.Location = new Point(20, 60);
            priorityLabel.Text = "Приоритет:";
            this.Controls.Add(priorityLabel);

            categoryLabel = new Label();
            categoryLabel.Location = new Point(20, 110);
            categoryLabel.Text = "Категория:";
            this.Controls.Add(categoryLabel);

            dateLabel = new Label();
            dateLabel.Location = new Point(20, 160);
            dateLabel.Text = "Срок выполнения:";
            this.Controls.Add(dateLabel);

            okButton = new Button();
            okButton.Location = new Point(200, 220);
            okButton.Size = new Size(80, 30);
            okButton.Text = "OK";
            okButton.Click += okButtonClick;
            this.Controls.Add(okButton);

            cancelButton = new Button();
            cancelButton.Location = new Point(290, 220);
            cancelButton.Size = new Size(80, 30);
            cancelButton.Text = "Отмена";
            cancelButton.Click += cancelButtonClick;
            this.Controls.Add(cancelButton);
            
        }

        private void LoadTaskData(Task task)
        {
            titleTextBox.Text = task.Title;
            priorityNum.Value = task.Priority;

            if (categoryComboBox.Items.Contains(task.Category))
            {
                categoryComboBox.SelectedItem = task.Category;
            }
            else
            {
                categoryComboBox.SelectedIndex = 5;
            }

            datePicker.Value = task.Date;
            isCompletedCheckBox.Checked = task.IsCompleted;
        }

        private void okButtonClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(titleTextBox.Text))
            {
                return;
            }

            EditedTask = new Task(titleTextBox.Text, (int)priorityNum.Value, categoryComboBox.SelectedItem?.ToString() ?? "Другое", datePicker.Value)
            {
                IsCompleted = isCompletedCheckBox.Checked
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

    }

    public class Task
    {
        public string Title { get; set; }
        public int Priority { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }

        public string DisplayText => $"{Title} [Приоритет: {Priority}] [Категория: {Category}] [Дата: {Date:dd.MM.yyyy}] [Выполнена: {(IsCompleted ? "Да" : "Нет")}]";

        public Task(string title)
        {
            Title = title;
            Priority = 1;
            Category = "Другое";
            Date = DateTime.Now.AddDays(7);
            IsCompleted = false;
        }

        public Task(string title, int priority, string category, DateTime date)
        {
            Title = title;
            Priority = priority;
            Category = category;
            Date = date;
        }

    }
}