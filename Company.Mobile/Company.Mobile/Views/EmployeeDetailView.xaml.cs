using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using Company.Mobile.ViewModels;

namespace Company.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeDetailView : ContentPage
    {
        public EmployeeDetailView(EmployeeDetailViewModel vm)
        {
            InitializeComponent();

            BindingContext = vm;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var vm = (EmployeeDetailViewModel)BindingContext;
            
            await Task.Run(() => vm.LoadCommand.Execute(null));
        }
    }
}