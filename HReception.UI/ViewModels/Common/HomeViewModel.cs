using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HReception.Logic.Infrastructure.Bases;
using HReception.Logic.Services.Interfaces.Patients;
using HReception.Logic.Utils.Extensions;
using Xamarin.Forms;

namespace HReception.UI.ViewModels.Common
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IPatientService _patientService;
        private IList<PatientDto> _allPatients;

        public HomeViewModel(IPatientService patientService)
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

        public DateTime Dob { get; set; }
        public string SearchPatientCode { get; set; }
        public bool EditMode { get; set; }
        public bool IsAddNew { get; set; }
        public List<PatientDto> Patients { get; set; }
        private PatientDto _selectedPatient;

        public PatientDto SelectedPatient
        {
            get => _selectedPatient;
            set
            {
                _selectedPatient = value;
                if (!EditMode)
                    CurrentPatient = new PatientDto(value);
            }
        }

        public PatientDto CurrentPatient { get; set; }
        public IList<string> Genders => new List<string> { "Nam", "Nữ" };

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
            return !EditMode;
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

        #region AssignmentCommand

        private ICommand _AssignmentCommand;

        public ICommand AssignmentCommand => _AssignmentCommand ?? (_AssignmentCommand = new Command(async () => { await AssignmentCommandExecute(); }, CanExecuteAssignmentCommand));

        private bool CanExecuteAssignmentCommand()
        {
            return SelectedPatient != null;
        }

        private async Task AssignmentCommandExecute()
        {
            //var view = ServiceLocator.Default.ResolveType<AssignmentViewModel>();
            //view.Patient = SelectedPatient;
            //_uiCompositionService.Activate(view, DefinedRegions.MainContent);
        }

        #endregion

        #endregion
    }
}