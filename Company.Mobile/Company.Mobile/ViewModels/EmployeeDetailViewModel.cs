using System.Windows.Input;
using System.Threading.Tasks;

using Xamarin.Forms;

using Company.Mobile.Models;
using Company.Mobile.Helpers;
using Company.Mobile.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Company.Mobile.ViewModels
{
    public class EmployeeDetailViewModel : BaseViewModel
    {
        private Employee currentItem;

        public Employee CurrentItem
        {
            get { return currentItem; }
            set { SetProperty(ref currentItem, value); }
        }

        public ObservableCollection<Department> Departments { get; }

        private Department selectedDepartment;

        public Department SelectedDepartment
        {
            get { return selectedDepartment; }
            set 
            { 
                SetProperty(ref selectedDepartment, value);
                CurrentItem.DepartmentId = selectedDepartment.Id;
            }
        }

        public ICommand LoadCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        private async Task Save()
        {
            var success = (CurrentItem.Id == 0) 
                ? await ApiService.AddItem(controller, CurrentItem)
                : await ApiService.UpdateItem(controller, CurrentItem);

            await App.Current.MainPage.DisplayAlert("CompanyApp",
                success ? "The item was saved successfully!" : "There was an error!",
                "OK");

            if (success)
                await App.Current.MainPage.Navigation.PopAsync();
        }

        private async Task Delete()
        {
            var confirm = await App.Current.MainPage.DisplayAlert("CompanyApp", "Are you sure?", "Yes", "No");

            if (confirm)
            {
                var success = await ApiService.DeleteItem(controller);

                await App.Current.MainPage.DisplayAlert("CompanyApp",
                    success ? "The item was deleted successfully!" : "There was an error!",
                    "OK");

                if (success)
                    await App.Current.MainPage.Navigation.PopAsync();

            }
        }

        private async Task Load()
        {
            IsBusy = true;

            var items = await ApiService.GetItems<Department>(Constants.DepartmentsController);

            foreach (var item in items)
                Departments.Add(item);

            var sel = Departments.FirstOrDefault(x => x.Id == CurrentItem.DepartmentId);
            SelectedDepartment = (sel != null) ? sel : Departments.First();

            IsBusy = false;
        }

        public EmployeeDetailViewModel(Employee item)
        {
            CurrentItem = item;
            Departments = new ObservableCollection<Department>();

            controller = (CurrentItem.Id == 0)
                ? Constants.EmployeesController
                : $"{Constants.EmployeesController}/{CurrentItem.Id}";

            LoadCommand = new Command(async () => await Load());
            SaveCommand = new Command(async () => await Save());
            DeleteCommand = new Command(async () => await Delete());
        }
    }
}