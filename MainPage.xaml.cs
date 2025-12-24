using Microsoft.Maui;
using System.Diagnostics.Metrics;

namespace ToDoList
{
    public partial class MainPage : ContentPage
    {
        int _counter = 0;

        public MainPage()
        {
            InitializeComponent();
        }
        
        private void OnTaskCountChanged(bool add_value)
        {
            TaskCount.Text = "Всего задач: " + _counter.ToString();
        }
        private void OnCreateBtnClicked(object? sender, EventArgs e)
        {
            if (TextInput.Text.Length < 3) return;

            var container = new HorizontalStackLayout();

            var newLabel = new Label
            {
                Text = TextInput.Text,
                FontSize = 16,
                TextColor = Colors.Black,
                BackgroundColor = Colors.White,
                Padding = 10,
                WidthRequest = 500,
                MaximumHeightRequest = 500,
                MinimumHeightRequest = 40
            };

            var changeButton = new Button
            {
                Text = "Изменить",
                WidthRequest = 120,
                MaximumHeightRequest = 500,
                MinimumHeightRequest = 40,
                CornerRadius = 0,
                BackgroundColor = Colors.DarkGrey,
                TextColor = Colors.White,
                FontSize = 16
            };

            var deleteButton = new Button
            {
                Text = "×",
                WidthRequest = 30,
                MaximumHeightRequest = 500,
                MinimumHeightRequest = 40,
                CornerRadius = 0,
                BackgroundColor = Colors.Red,
                TextColor = Colors.White,
                FontSize = 18
            };

            changeButton.Clicked += (s, args) =>
            {
                string currentText = newLabel.Text;

                //Убираем Label и кнопки временно
                container.Children.Clear();

                //Создаем Editor для редактирования
                var editor = new Editor
                {
                    Text = currentText,
                    FontSize = 16,
                    TextColor = Colors.Black,
                    BackgroundColor = Colors.White,
                    WidthRequest = 500,
                    MaximumHeightRequest = 500,
                    MinimumHeightRequest = 40,
                };

                // Кнопка сохранить
                var saveButton = new Button
                {
                    Text = "✓",
                    WidthRequest = 30,
                    MaximumHeightRequest = 500,
                    MinimumHeightRequest = 40,
                    CornerRadius = 0,
                    BackgroundColor = Colors.Green,
                    TextColor = Colors.White,
                    FontSize = 18
                };

                // Кнопка отменить
                var cancelButton = new Button
                {
                    Text = "×",
                    WidthRequest = 30,
                    MaximumHeightRequest = 500,
                    MinimumHeightRequest = 40,
                    CornerRadius = 0,
                    BackgroundColor = Colors.Red,
                    TextColor = Colors.White,
                    FontSize = 18
                };

                saveButton.Clicked += (save_s, save_args) =>
                {
                    // Сохраняем изменения
                    newLabel.Text = editor.Text;

                    // Восстанавливаем исходный вид
                    RestoreContainerView(container, newLabel, changeButton, deleteButton);
                };

                cancelButton.Clicked += (cancel_s, cancel_args) =>
                {
                    // Отменяем изменения, восстанавливаем исходный вид
                    RestoreContainerView(container, newLabel, changeButton, deleteButton);
                };

                // Добавляем элементы в контейнер для редактирования
                container.Children.Add(editor);
                container.Children.Add(saveButton);
                container.Children.Add(cancelButton);
            };


            deleteButton.Clicked += (s, args) =>
            {
                // Удаляем контейнер с задачей из родительского контейнера
                if (container.Parent is Layout parentLayout)
                {
                    parentLayout.Children.Remove(container);

                    // Уменьшаем счетчик и обновляем отображение
                    _counter--;
                    OnTaskCountChanged(false);
                }
            };

            _counter++;
            OnTaskCountChanged(true);
            TextInput.Text = string.Empty;
            //LabelsContainer.Children.Add(newLabel);

            container.Children.Add(newLabel);
            container.Children.Add(changeButton);
            container.Children.Add(deleteButton);
            LabelsContainer.Children.Add(container);
        }

        private void RestoreContainerView(HorizontalStackLayout container, Label label, Button changeButton, Button deleteButton)
        {
            container.Children.Clear();

            container.Children.Add(label);
            container.Children.Add(changeButton);
            container.Children.Add(deleteButton);
        }
    }
}
