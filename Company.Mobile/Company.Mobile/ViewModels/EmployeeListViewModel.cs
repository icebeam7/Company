using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using Xamarin.Forms;

using Company.Mobile.Models;
using Company.Mobile.Helpers;
using Company.Mobile.Services;
using Company.Mobile.Views;

namespace Company.Mobile.ViewModels
{
    public class EmployeeListViewModel : BaseViewModel
    {
        public ObservableCollection<Employee> ItemCollection { get; }

        private Employee selectedItem;

        public Employee SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }

        public ICommand LoadCommand { get; private set; }
        public ICommand GoToDetailCommand { get; private set; }
        public ICommand AddNewCommand { get; private set; }

        private async Task Load()
        {
            IsBusy = true;

            var items = await ApiService.GetItems<Employee>(controller);
            SetCollection(items);

            IsBusy = false;
        }

        private async Task AddNew()
        {
            await NavigateToDetail(new Employee());
        }

        private async Task GoToDetail()
        {
            if (selectedItem != null)
                await NavigateToDetail(selectedItem);
        }

        private async Task NavigateToDetail(Employee item)
        {
            var vm = new EmployeeDetailViewModel(item);
            EmployeeDetailView page = new EmployeeDetailView(vm);

            await App.Current.MainPage.Navigation.PushAsync(page);
        }

        private void SetCollection(List<Employee> items)
        {
            while (ItemCollection.Count > 0)
                ItemCollection.RemoveAt(0);

            foreach (var item in items)
                ItemCollection.Add(item);
        }

        public EmployeeListViewModel()
        {
            SelectedItem = null;
            controller = Constants.EmployeesController;

            ItemCollection = new ObservableCollection<Employee>();

            LoadCommand = new Command(async () => await Load());
            AddNewCommand = new Command(async () => await AddNew());
            GoToDetailCommand = new Command(async () => await GoToDetail());
        }
    }
}
