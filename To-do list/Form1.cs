using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;

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
        private Button markButton;
        private Button sortTitleButton;
        private Button sortPriorityButton;
        private Button sortCategoryButton;
        private Button sortDateButton;
        private Button sortIsCompletedButton;
        private Button addSubtaskButton;
        private Button editSubtaskButton;
        private Button removeSubtaskButton;
        private Button markSubtaskButton;
        private Button saveButton;

        private NumericUpDown priorityNum;
        private ComboBox categoryComboBox;
        private ComboBox isCompletedComboBox;
        private DateTimePicker datePicker;
        private Label priorityLabel;
        private Label categoryLabel;
        private Label dateLabel;
        private Label isCompletedLabel;
        private Label mainTitleLabel;

        private List<Task> tasks = new List<Task>();
        private const string SAVE_FILE = "tasks.json";

        public Form1()
        {
            InitializeComponent();
            FormSetup();
            LoadTasks();
        }

        private void FormSetup()
        {
            this.Size = new Size(900, 500);

            taskTextBox = new TextBox();
            taskTextBox.Location = new Point(20, 100);
            taskTextBox.Size = new Size(340, 20);
            this.Controls.Add(taskTextBox);

            tasksListBox = new ListBox();
            tasksListBox.Location = new Point(20, 160);
            tasksListBox.Size = new Size(600, 250);
            this.Controls.Add(tasksListBox);

            addButton = new Button();
            addButton.Location = new Point(370, 100);
            addButton.Size = new Size(120, 20);
            addButton.Text = "Добавить задачу";
            addButton.Click += AddTask;
            this.Controls.Add(addButton);

            removeButton = new Button();
            removeButton.Location = new Point(500, 100);
            removeButton.Size = new Size(120, 20);
            removeButton.Text = "Удалить задачу";
            removeButton.Click += RemoveTask;
            this.Controls.Add(removeButton);

            clearAllButton = new Button();
            clearAllButton.Location = new Point(500, 130);
            clearAllButton.Size = new Size(120, 20);
            clearAllButton.Text = "Очистить всё";
            clearAllButton.Click += Clear_All;
            this.Controls.Add(clearAllButton);

            editButton = new Button();
            editButton.Location = new Point(370, 130);
            editButton.Size = new Size(120, 20);
            editButton.Text = "Редактировать";
            editButton.Click += EditTask;
            this.Controls.Add(editButton);

            markButton = new Button();
            markButton.Location = new Point(630, 130);
            markButton.Size = new Size(120, 20);
            markButton.Text = "Отметить";
            markButton.Click += MarkAsCompleted;
            this.Controls.Add(markButton);

            sortTitleButton = new Button();
            sortTitleButton.Location = new Point(20, 420);
            sortTitleButton.Size = new Size(110, 30);
            sortTitleButton.Text = "Сорт. по назв.";
            sortTitleButton.Click += SortByTitle;
            this.Controls.Add(sortTitleButton);

            sortPriorityButton = new Button();
            sortPriorityButton.Location = new Point(140, 420);
            sortPriorityButton.Size = new Size(110, 30);
            sortPriorityButton.Text = "Сорт. по приор.";
            sortPriorityButton.Click += SortByPriority;
            this.Controls.Add(sortPriorityButton);

            sortCategoryButton = new Button();
            sortCategoryButton.Location = new Point(260, 420);
            sortCategoryButton.Size = new Size(110, 30);
            sortCategoryButton.Text = "Сорт. по кат.";
            sortCategoryButton.Click += SortByCategory;
            this.Controls.Add(sortCategoryButton);

            sortDateButton = new Button();
            sortDateButton.Location = new Point(380, 420);
            sortDateButton.Size = new Size(110, 30);
            sortDateButton.Text = "Сорт. по дате";
            sortDateButton.Click += SortByDate;
            this.Controls.Add(sortDateButton);

            sortIsCompletedButton = new Button();
            sortIsCompletedButton.Location = new Point(500, 420);
            sortIsCompletedButton.Size = new Size(110, 30);
            sortIsCompletedButton.Text = "Сорт. по вып.";
            sortIsCompletedButton.Click += SortByMark;
            this.Controls.Add(sortIsCompletedButton);

            priorityNum = new NumericUpDown();
            priorityNum.Location = new Point(630, 185);
            priorityNum.Size = new Size(100, 20);
            priorityNum.Minimum = 1;
            priorityNum.Maximum = 10;
            priorityNum.Value = 1;
            this.Controls.Add(priorityNum);

            categoryComboBox = new ComboBox();
            categoryComboBox.Location = new Point(630, 245);
            categoryComboBox.Size = new Size(120, 20);
            categoryComboBox.Items.AddRange(new string[] { "Учеба", "Работа", "Дом", "Личное", "Здоровье", "Другое" });
            categoryComboBox.SelectedIndex = 5;
            this.Controls.Add(categoryComboBox);

            isCompletedComboBox = new ComboBox();
            isCompletedComboBox.Location = new Point(630, 365);
            isCompletedComboBox.Size = new Size(120, 20);
            isCompletedComboBox.Items.AddRange(new string[] { "Нет", "Да" });
            isCompletedComboBox.SelectedIndex = 0;
            this.Controls.Add(isCompletedComboBox);

            datePicker = new DateTimePicker();
            datePicker.Location = new Point(630, 305);
            datePicker.Size = new Size(120, 20);
            datePicker.Value = DateTime.Now.AddDays(7);
            this.Controls.Add(datePicker);

            priorityLabel = new Label();
            priorityLabel.Location = new Point(630, 160);
            priorityLabel.Size = new Size(80, 20);
            priorityLabel.Text = "Приоритет:";
            this.Controls.Add(priorityLabel);

            categoryLabel = new Label();
            categoryLabel.Location = new Point(630, 220);
            categoryLabel.Size = new Size(80, 20);
            categoryLabel.Text = "Категория:";
            this.Controls.Add(categoryLabel);

            dateLabel = new Label();
            dateLabel.Location = new Point(630, 280);
            dateLabel.Size = new Size(80, 20);
            dateLabel.Text = "Срок:";
            this.Controls.Add(dateLabel);

            isCompletedLabel = new Label();
            isCompletedLabel.Location = new Point(630, 340);
            isCompletedLabel.Size = new Size(80, 20);
            isCompletedLabel.Text = "Выполнена:";
            this.Controls.Add(isCompletedLabel);

            mainTitleLabel = new Label();
            mainTitleLabel.Location = new Point(20, 35);
            mainTitleLabel.Size = new Size(200, 30);
            mainTitleLabel.Text = "To-Do List";
            mainTitleLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            this.Controls.Add(mainTitleLabel);

            addSubtaskButton = new Button();
            addSubtaskButton.Location = new Point(750, 160);
            addSubtaskButton.Size = new Size(120, 20);
            addSubtaskButton.Text = "Добавить подзадачу";
            addSubtaskButton.Click += AddSubtask;
            this.Controls.Add(addSubtaskButton);

            editSubtaskButton = new Button();
            editSubtaskButton.Location = new Point(750, 190);
            editSubtaskButton.Size = new Size(120, 20);
            editSubtaskButton.Text = "Редакт. подзадачу";
            editSubtaskButton.Click += EditSubtask;
            this.Controls.Add(editSubtaskButton);

            removeSubtaskButton = new Button();
            removeSubtaskButton.Location = new Point(750, 220);
            removeSubtaskButton.Size = new Size(120, 20);
            removeSubtaskButton.Text = "Удалить подзадачу";
            removeSubtaskButton.Click += RemoveSubtask;
            this.Controls.Add(removeSubtaskButton);

            markSubtaskButton = new Button();
            markSubtaskButton.Location = new Point(750, 250);
            markSubtaskButton.Size = new Size(120, 20);
            markSubtaskButton.Text = "Отметить подзадачу";
            markSubtaskButton.Click += MarkSubtask;
            this.Controls.Add(markSubtaskButton);

            saveButton = new Button();
            saveButton.Location = new Point(750, 280);
            saveButton.Size = new Size(120, 20);
            saveButton.Text = "Сохранить";
            saveButton.Click += (s, e) => SaveTasks();
            this.Controls.Add(saveButton);
        }

        private void LoadTasks()
        {
            try
            {
                if (File.Exists(SAVE_FILE))
                {
                    string json = File.ReadAllText(SAVE_FILE);
                    tasks = JsonSerializer.Deserialize<List<Task>>(json);
                    RefreshListBox();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке задач: {ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SaveTasks();
            base.OnFormClosing(e);
        }

        private void SaveTasks()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(tasks, options);
                File.WriteAllText(SAVE_FILE, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении задач: {ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddSubtask(object sender, EventArgs e)
        {
            if (tasksListBox.SelectedIndex == -1) return;

            int index = tasksListBox.SelectedIndex;
            Task task = null;
            int currentIndex = 0;

            // Ищем задачу по индексу в ListBox
            foreach (var t in tasks)
            {
                if (currentIndex == index)
                {
                    task = t;
                    break;
                }
                currentIndex++;

                // Пропускаем подзадачи
                currentIndex += t.Subtasks.Count;
            }

            if (task == null) return;

            AddSubtaskForm form = new AddSubtaskForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                task.Subtasks.Add(form.NewSubtask);
                RefreshListBox();
                SaveTasks(); // Автосохранение
            }
        }

        private void AddTask(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(taskTextBox.Text))
            {
                bool isCompleted = isCompletedComboBox.SelectedIndex == 1;
                Task newTask = new Task(taskTextBox.Text, (int)priorityNum.Value, categoryComboBox.SelectedItem.ToString(), datePicker.Value, isCompleted);
                tasks.Add(newTask);
                tasksListBox.Items.Add(newTask.DisplayText);
                taskTextBox.Clear();
                SaveTasks(); // Автосохранение
            }
        }

        private void Clear_All(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите удалить все задачи? Это действие нельзя отменить.",
                                        "Подтверждение",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                tasks.Clear();
                tasksListBox.Items.Clear();
                SaveTasks(); // Автосохранение
            }
        }

        private void RemoveTask(object sender, EventArgs e)
        {
            if (tasksListBox.SelectedIndex != -1)
            {
                int selectedIndex = tasksListBox.SelectedIndex;

                // Находим выбранную задачу (учитывая, что в ListBox также отображаются подзадачи)
                Task taskToRemove = null;
                int currentIndex = 0;

                foreach (var task in tasks)
                {
                    if (currentIndex == selectedIndex)
                    {
                        taskToRemove = task;
                        break;
                    }
                    currentIndex++;

                    // Пропускаем подзадачи
                    currentIndex += task.Subtasks.Count;
                }

                if (taskToRemove != null)
                {
                    var result = MessageBox.Show("Вы уверены, что хотите удалить эту задачу?",
                                                "Подтверждение удаления",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        tasks.Remove(taskToRemove);
                        RefreshListBox();
                        SaveTasks(); // Автосохранение
                    }
                }
            }
        }

        private void EditTask(object sender, EventArgs e)
        {
            if (tasksListBox.SelectedIndex != -1)
            {
                int selectedIndex = tasksListBox.SelectedIndex;
                Task selectedTask = null;
                int currentIndex = 0;

                // Ищем задачу по индексу в ListBox
                foreach (var task in tasks)
                {
                    if (currentIndex == selectedIndex)
                    {
                        selectedTask = task;
                        break;
                    }
                    currentIndex++;

                    // Пропускаем подзадачи
                    currentIndex += task.Subtasks.Count;
                }

                if (selectedTask == null) return;

                EditForm editForm = new EditForm(selectedTask);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    int index = tasks.IndexOf(selectedTask);
                    tasks[index] = editForm.EditedTask;
                    RefreshListBox();
                    SaveTasks(); // Автосохранение
                }
            }
        }

        private void MarkAsCompleted(object sender, EventArgs e)
        {
            if (tasksListBox.SelectedIndex != -1)
            {
                int selectedIndex = tasksListBox.SelectedIndex;
                Task selectedTask = null;
                int currentIndex = 0;

                // Ищем задачу по индексу в ListBox
                foreach (var task in tasks)
                {
                    if (currentIndex == selectedIndex)
                    {
                        selectedTask = task;
                        break;
                    }
                    currentIndex++;

                    // Пропускаем подзадачи
                    currentIndex += task.Subtasks.Count;
                }

                if (selectedTask != null)
                {
                    selectedTask.IsCompleted = !selectedTask.IsCompleted;
                    RefreshListBox();
                    SaveTasks(); // Автосохранение
                }
            }
        }

        private void SortByTitle(object sender, EventArgs e)
        {
            tasks = tasks.OrderBy(task => task.Title).ToList();
            RefreshListBox();
        }

        private void SortByPriority(object sender, EventArgs e)
        {
            tasks = tasks.OrderBy(task => task.Priority).ToList();
            RefreshListBox();
        }

        private void SortByCategory(object sender, EventArgs e)
        {
            tasks = tasks.OrderBy(task => task.Category).ToList();
            RefreshListBox();
        }

        private void SortByDate(object sender, EventArgs e)
        {
            tasks = tasks.OrderBy(task => task.Date).ToList();
            RefreshListBox();
        }

        private void SortByMark(object sender, EventArgs e)
        {
            tasks = tasks.OrderBy(task => task.IsCompleted).ToList();
            RefreshListBox();
        }

        private void EditSubtask(object sender, EventArgs e)
        {
            if (tasksListBox.SelectedIndex == -1) return;

            // Находим индекс выбранной задачи и подзадачи
            int selectedIndex = tasksListBox.SelectedIndex;
            Task parentTask = null;
            Subtask selectedSubtask = null;
            int subtaskIndex = -1;

            // Ищем задачу и подзадачу по выбранному индексу в ListBox
            int currentIndex = 0;
            foreach (var task in tasks)
            {
                if (currentIndex == selectedIndex)
                {
                    // Если выбрана сама задача, а не подзадача
                    MessageBox.Show("Выберите подзадачу для редактирования", "Информация",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                currentIndex++;

                foreach (var subtask in task.Subtasks)
                {
                    if (currentIndex == selectedIndex)
                    {
                        parentTask = task;
                        selectedSubtask = subtask;
                        subtaskIndex = task.Subtasks.IndexOf(subtask);
                        break;
                    }
                    currentIndex++;
                }

                if (parentTask != null) break;
            }

            if (parentTask == null || selectedSubtask == null) return;

            // Создаем форму редактирования подзадачи
            using (var editForm = new Form())
            {
                editForm.Text = "Редактирование подзадачи";
                editForm.Size = new Size(300, 200);

                var titleText = new TextBox()
                {
                    Location = new Point(20, 20),
                    Size = new Size(240, 20),
                    Text = selectedSubtask.Title
                };
                editForm.Controls.Add(titleText);

                var completedCheck = new CheckBox()
                {
                    Location = new Point(20, 60),
                    Text = "Выполнена",
                    Checked = selectedSubtask.IsCompleted
                };
                editForm.Controls.Add(completedCheck);

                var okButton = new Button()
                {
                    Location = new Point(20, 110),
                    Text = "OK",
                    DialogResult = DialogResult.OK
                };
                editForm.Controls.Add(okButton);

                var cancelButton = new Button()
                {
                    Location = new Point(120, 110),
                    Text = "Отмена",
                    DialogResult = DialogResult.Cancel
                };
                editForm.Controls.Add(cancelButton);

                editForm.AcceptButton = okButton;
                editForm.CancelButton = cancelButton;

                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(titleText.Text))
                    {
                        // Обновляем подзадачу
                        parentTask.Subtasks[subtaskIndex] = new Subtask(
                            titleText.Text,
                            completedCheck.Checked
                        );
                        RefreshListBox();
                        SaveTasks(); // Автосохранение
                    }
                }
            }
        }

        private void RemoveSubtask(object sender, EventArgs e)
        {
            if (tasksListBox.SelectedIndex == -1) return;

            // Находим индекс выбранной задачи и подзадачи
            int selectedIndex = tasksListBox.SelectedIndex;
            Task parentTask = null;
            int subtaskIndex = -1;

            // Ищем задачу и подзадачу по выбранному индексу в ListBox
            int currentIndex = 0;
            foreach (var task in tasks)
            {
                if (currentIndex == selectedIndex)
                {
                    // Если выбрана сама задача, а не подзадача
                    MessageBox.Show("Выберите подзадачу для удаления", "Информация",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                currentIndex++;

                for (int i = 0; i < task.Subtasks.Count; i++)
                {
                    if (currentIndex == selectedIndex)
                    {
                        parentTask = task;
                        subtaskIndex = i;
                        break;
                    }
                    currentIndex++;
                }

                if (parentTask != null) break;
            }

            if (parentTask == null || subtaskIndex == -1) return;

            // Подтверждение удаления
            var result = MessageBox.Show("Вы уверены, что хотите удалить эту подзадачу?",
                                        "Подтверждение удаления",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                parentTask.Subtasks.RemoveAt(subtaskIndex);
                RefreshListBox();
                SaveTasks(); // Автосохранение
            }
        }

        private void MarkSubtask(object sender, EventArgs e)
        {
            if (tasksListBox.SelectedIndex == -1) return;

            // Находим индекс выбранной задачи и подзадачи
            int selectedIndex = tasksListBox.SelectedIndex;
            Task parentTask = null;
            Subtask selectedSubtask = null;
            int subtaskIndex = -1;

            // Ищем задачу и подзадачу по выбранному индексу в ListBox
            int currentIndex = 0;
            foreach (var task in tasks)
            {
                if (currentIndex == selectedIndex)
                {
                    // Если выбрана сама задача, а не подзадача
                    MessageBox.Show("Выберите подзадачу для отметки", "Информация",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                currentIndex++;

                for (int i = 0; i < task.Subtasks.Count; i++)
                {
                    if (currentIndex == selectedIndex)
                    {
                        parentTask = task;
                        selectedSubtask = task.Subtasks[i];
                        subtaskIndex = i;
                        break;
                    }
                    currentIndex++;
                }

                if (parentTask != null) break;
            }

            if (parentTask == null || selectedSubtask == null) return;

            // Меняем статус выполнения подзадачи
            parentTask.Subtasks[subtaskIndex] = new Subtask(
                selectedSubtask.Title,
                !selectedSubtask.IsCompleted
            );

            RefreshListBox();
            SaveTasks(); // Автосохранение
        }

        private void RefreshListBox()
        {
            tasksListBox.Items.Clear();

            foreach (var task in tasks)
            {
                tasksListBox.Items.Add(task.DisplayText);

                foreach (var sub in task.Subtasks)
                {
                    tasksListBox.Items.Add("    └─ " + sub.DisplayText);
                }
            }
        }
    }

    public class AddSubtaskForm : Form
    {
        public Subtask NewSubtask { get; private set; }

        TextBox titleText;
        CheckBox completedCheck;
        Button ok;
        Button cancel;

        public AddSubtaskForm()
        {
            this.Size = new Size(300, 200);

            titleText = new TextBox()
            {
                Location = new Point(20, 20),
                Size = new Size(240, 20)
            };
            this.Controls.Add(titleText);

            completedCheck = new CheckBox()
            {
                Location = new Point(20, 60),
                Text = "Выполнена"
            };
            this.Controls.Add(completedCheck);

            ok = new Button()
            {
                Location = new Point(20, 110),
                Text = "OK"
            };
            ok.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(titleText.Text)) return;

                NewSubtask = new Subtask(titleText.Text, completedCheck.Checked);
                this.DialogResult = DialogResult.OK;
            };
            this.Controls.Add(ok);

            cancel = new Button()
            {
                Location = new Point(120, 110),
                Text = "Отмена"
            };
            cancel.Click += (s, e) => this.Close();
            this.Controls.Add(cancel);
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

            EditedTask = new Task(titleTextBox.Text, (int)priorityNum.Value, categoryComboBox.SelectedItem?.ToString() ?? "Другое", datePicker.Value, isCompletedCheckBox.Checked)
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

        public List<Subtask> Subtasks { get; set; } = new List<Subtask>();

        // Конструктор без параметров для JSON десериализации
        public Task() { }

        public Task(string title, int priority, string category, DateTime date, bool isCompleted)
        {
            Title = title;
            Priority = priority;
            Category = category;
            Date = date;
            IsCompleted = isCompleted;
        }

        public string DisplayText => $"{Title} [Приоритет: {Priority}] [Категория: {Category}] [Дата: {Date:dd.MM.yyyy}] [Выполнена: {(IsCompleted ? "Да" : "Нет")}]";
    }

    public class Subtask
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }

        // Конструктор без параметров для JSON десериализации
        public Subtask() { }

        public Subtask(string title, bool isCompleted)
        {
            Title = title;
            IsCompleted = isCompleted;
        }

        public string DisplayText => $"{Title} (Выполнена: {(IsCompleted ? "Да" : "Нет")})";
    }
}