using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HReception.Logic.Services.Interfaces.Patients;
using HReception.Logic.Utils.Extensions;
using Xamarin.Forms;

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
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            await GetAllPatients();
            SelectedPatient = Patients.FirstOrDefault();
            base.ViewIsAppearing(sender, e);
            base.ViewIsAppearing(sender, e);
        }

        private async Task GetAllPatients()
        {
            _allPatients = await _patientService.Find(null);
            Patients = _allPatients.ToList();
        }
        #endregion

        #region Properties
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
            var noneSigned = (SearchPatientCode ?? string.Empty).ToNoneSign().ToLower();
            Patients = _allPatients.Where(aa => aa.SearchField.Contains(noneSigned)).ToList();
            SelectedPatient = Patients.FirstOrDefault();
        }

        #endregion

        #region ViewPatientDetailCommand

        private ICommand _ViewPatientDetailCommand;

        public ICommand ViewPatientDetailCommand => _ViewPatientDetailCommand ?? (_ViewPatientDetailCommand = new Command<PatientDto>(async arg => await ViewPatientDetailCommandExecute(arg)));

        private async Task ViewPatientDetailCommandExecute(PatientDto arg)
        {
            if (arg is null)
                return;
            await CoreMethods.PushPageModel<PatientDetailPageModel>(arg);
        }

        #endregion

        #endregion
    }
}