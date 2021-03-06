﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HReception.Logic.Services.Interfaces.Patients;
using HReception.Logic.Utils.Extensions;
using Xamarin.Forms;
using FreshMvvm;

namespace HReception.UI.PageModels.Common
{
    public class HomePageModel : PageModelBase
    {
        private readonly IPatientService _patientService;
        private IList<PatientDto> _allPatients;
        public HomePageModel(IPatientService patientService)
        {
            _patientService = patientService;
        }

        #region Overrides
        public override void Init(object initData)
        {
            var tmp = (initData as bool?);
            SelectMode = tmp.HasValue && tmp.Value;
            CurrentPage.Title = SelectMode ? "Chọn bệnh nhân" : "Trang chủ";
            base.Init(initData);
        }
        public override async void ReverseInit(object returnedData)
        {
            var dataChanged = returnedData as bool?;
            if (dataChanged.HasValue && dataChanged.Value)
            {
                await GetAllPatients();
                SelectedPatient = Patients.FirstOrDefault();
            }
            base.ReverseInit(returnedData);
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            await GetAllPatients();
            SelectedPatient = Patients.FirstOrDefault();
            base.ViewIsAppearing(sender, e);
        }

        private async Task GetAllPatients()
        {
            _allPatients = await _patientService.Find(null);
            Patients = _allPatients.ToList();
        }
        #endregion

        #region Properties
        public bool SelectMode { get; set; }

        string _searchPatientCode;

        public string SearchPatientCode
        {
            get => _searchPatientCode;

            set
            {
                _searchPatientCode = value;
                if (value.IsNullOrEmpty())
                    SearchCommand.Execute(null);
            }
        }

        public List<PatientDto> Patients { get; set; }
        private PatientDto _selectedPatient;

        public PatientDto SelectedPatient
        {
            get => _selectedPatient;
            set => _selectedPatient = value;
        }


        #endregion

        #region Commands

        #region CancelCommand

        private ICommand _CancelCommand;

        public ICommand CancelCommand => _CancelCommand ?? (_CancelCommand = new Command(async () => { await CancelCommandExecute(); }, CancelCommandCanExecute));

        private bool CancelCommandCanExecute()
        {
            return SelectMode;
        }

        private async Task CancelCommandExecute()
        {
            await CoreMethods.PopPageModel(null, true);
        }

        #endregion

        #region SearchCommand

        private ICommand _searchCommand;

        /// <summary>
        /// Gets the SearchCommand command.
        /// </summary>
        public ICommand SearchCommand => _searchCommand ??
                                        (_searchCommand = new Command(OnSearchCommandExecute,
                                            CanExecuteSearchCommand));

        private bool CanExecuteSearchCommand()
        {
            return true;
        }

        /// <summary>
        /// Method to invoke when the SearchCommand command is executed.
        /// </summary>
        private void OnSearchCommandExecute()
        {
            try
            {
                IsBusy = true;
                var noneSigned = (SearchPatientCode ?? string.Empty).ToNoneSign().ToLower();
                Patients = _allPatients.Where(aa => aa.SearchField.Contains(noneSigned)).ToList();
                SelectedPatient = Patients.FirstOrDefault();
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

        #region ViewPatientDetailCommand

        private ICommand _ViewPatientDetailCommand;

        public ICommand ViewPatientDetailCommand => _ViewPatientDetailCommand ?? (_ViewPatientDetailCommand = new Command<PatientDto>(async arg => await ViewPatientDetailCommandExecute(arg)));

        private async Task ViewPatientDetailCommandExecute(PatientDto arg)
        {
            if (arg is null)
                return;
            if (SelectMode)
                await CoreMethods.PopPageModel(arg, true);
            else
                await CoreMethods.PushPageModel<PatientDetailPageModel>(arg);

        }

        #endregion
        #region NewPatientCommand

        private ICommand _NewPatientCommand;

        public ICommand NewPatientCommand => _NewPatientCommand ?? (_NewPatientCommand = new Command(async () => { await NewPatientCommandExecute(); }));

        private async Task NewPatientCommandExecute()
        {
            await CoreMethods.PushPageModel<PatientDetailPageModel>(data: true);
        }

        #endregion
        #endregion
    }
}