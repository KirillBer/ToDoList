using CommunityToolkit.Mvvm.ComponentModel;


namespace ToDoList.Models
{
    public partial class ToDoTask : ObservableObject
    {
        //[ObservableProperty]
        //private int id;

        [ObservableProperty]
        private string title = string.Empty;

        [ObservableProperty]
        private bool isEditing = false;

        [ObservableProperty]
        private bool isDisplaying = true;

        [ObservableProperty]
        private string editedText = string.Empty;
    }
}
