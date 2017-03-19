using ListViewTest.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ListViewTest.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        #region Students
        private ObservableCollection<Student> _Students = new ObservableCollection<Student>();
        /// <summary>
        /// Students
        /// </summary>
        public ObservableCollection<Student> Students
        {
            get { return _Students; }
            set { SetProperty(ref _Students, value); }
        }
        #endregion


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


        public DelegateCommand SelectedStudentCommand { get; set; }


        public readonly IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        public MainPageViewModel(IPageDialogService dialogService
            , INavigationService navigationService
            , IEventAggregator eventAggregator)
        {

            _navigationService = navigationService;
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<UpdateStudentEvent>().Subscribe(x =>
            {
                var updateStud = Students.FirstOrDefault(item => item.Id == x.Id);
                if (updateStud != null)
                {
                    updateStud.Name = x.Name;
                }
            });

            SelectedStudentCommand = new DelegateCommand(async () =>
            {
                var naviparams = new NavigationParameters();
                naviparams.Add("CurrStudent", CurrStudent);

                await _navigationService.NavigateAsync("DetailPage", naviparams);
                //_dialogService.DisplayAlertAsync("Info", CurrStudent.Name, "OK");
            });
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("title"))
                Title = (string)parameters["title"] + " and Prism";

            for (int i = 0; i < 100; i++)
            {
                var stu = new Student();
                stu.Id = i + 1;
                stu.Name = "學生 " + (i + 1);
                Students.Add(stu);
            }
        }
    }
}
