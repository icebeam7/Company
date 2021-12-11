using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Company.Mobile.ViewModels;

namespace Company.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeListView : ContentPage
    {
        public EmployeeListView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var vm = (EmployeeListViewModel)BindingContext;
            await Task.Run(() => vm.LoadCommand.Execute(null));
        }
    }
}