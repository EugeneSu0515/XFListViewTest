using ListViewTest.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ListViewTest.ViewModels
{
    public class DetailPageViewModel : BindableBase, INavigationAware
    {
        #region CurrStudent
        private Student _CurrStudent;
        /// <summary>
        /// Current Student
        /// </summary>
        public Student CurrStudent
        {
            get { return this._CurrStudent; }
            set { this.SetProperty(ref this._CurrStudent, value); }
        }
        #endregion


        #region Title
        private string _Title;
        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get { return this._Title; }
            set { this.SetProperty(ref this._Title, value); }
        }
        #endregion


        public DelegateCommand UpdateStudentCommand { get; set; }

        public readonly IPageDialogService _dialogService;

        private readonly IEventAggregator _eventAggregator;

        private readonly INavigationService _navigationService;

        public DetailPageViewModel(IPageDialogService dialogService
            , IEventAggregator eventAggregator
            , INavigationService navigationService)
        {
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            UpdateStudentCommand = new DelegateCommand(async () =>
            {
                _eventAggregator.GetEvent<UpdateStudentEvent>().Publish(new Student
                {
                    Id = CurrStudent.Id,
                    Name = CurrStudent.Name
                });
                await _navigationService.GoBackAsync();
                //await _dialogService.DisplayAlertAsync("Info", CurrStudent.Name, "OK");
            });
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("CurrStudent"))
            {
                CurrStudent = parameters["CurrStudent"] as Student;
                Title = $"{CurrStudent.Name}的資料";
            }
        }
    }
}
