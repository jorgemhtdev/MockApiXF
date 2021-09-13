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

        public ObservableCollection<Comic> comicCollection;

        public MainViewModel() { }

        public async Task OnAppearing()
        {
            try
            {
                comic = new Comic();

                IEnumerable<Comic> listaComic = await comic.CargarComics();
                comicCollection = new ObservableCollection<Comic>(listaComic);
            }
            catch (Exception exception)
            {
                await AlertDialogService.Instance.ShowDialogAsync("Error", exception.Message, "Ok");
                // Guardar el error
            }
        }
    }
}
