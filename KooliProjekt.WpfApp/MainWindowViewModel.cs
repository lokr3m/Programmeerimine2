﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using KooliProjekt.WpfApp.Api;

namespace KooliProjekt.WpfApp
{
    public class MainWindowViewModel : NotifyPropertyChangedBase
    {
        public ObservableCollection<Category> Lists { get; private set; }
        public ICommand NewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public Predicate<Category> ConfirmDelete { get; set; }
        public Action<string> OnError { get; set; }  // Добавляем обработку ошибок

        private readonly IApiClient _apiClient;

        public MainWindowViewModel() : this(new ApiClient())
        {
        }

        public MainWindowViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
            Lists = new ObservableCollection<Category>();

            NewCommand = new RelayCommand<Category>(
                list => SelectedItem = new Category()
            );

            SaveCommand = new RelayCommand<Category>(
                async list => await Save(),
                list => SelectedItem != null
            );

            DeleteCommand = new RelayCommand<Category>(
                async list => await Delete(),
                list => SelectedItem != null
            );
        }

        public async Task Load()
        {
            Lists.Clear();
            var result = await _apiClient.List();

            if (result.HasError)
            {
                OnError?.Invoke(result.Error);  // Показываем ошибку в UI
                return;
            }

            foreach (var list in result.Value)
            {
                Lists.Add(list);
            }
        }

        private async Task Save()
        {
            if (SelectedItem == null) return;

            var result = await _apiClient.Save(SelectedItem);

            if (result.HasError)
            {
                OnError?.Invoke(result.Error);
                return;
            }

            await Load();
        }

        private async Task Delete()
        {
            if (SelectedItem == null) return;

            if (ConfirmDelete != null && !ConfirmDelete(SelectedItem))
                return;

            var result = await _apiClient.Delete(SelectedItem.Id);

            if (result.HasError)
            {
                OnError?.Invoke(result.Error);
                return;
            }

            Lists.Remove(SelectedItem);
            SelectedItem = null;
        }

        private Category _selectedItem;
        public Category SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged();
            }
        }
    }
}
