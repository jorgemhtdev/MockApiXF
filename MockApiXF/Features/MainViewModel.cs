namespace MockApiXF
{
    using MockApiXF.Base;
    using MockApiXF.Models;
    using MockApiXF.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public class MainViewModel : BaseViewModel
    {
        Comic comic;

        public ObservableCollection<Comic> ComicsCollection { get; set; }

        public MainViewModel() {}

        public async override Task OnAppearingAsync()
        {
            try
            {
                comic = new Comic();

                IEnumerable<Comic> listaComic = await comic.CargarComics();
                ComicsCollection = new ObservableCollection<Comic>(listaComic);
            }
            catch (Exception exception)
            {
                await AlertDialogService.Instance.ShowDialogAsync("Error", exception.Message, "Ok");
                // Guardar el error
            }
        }
    }
}
