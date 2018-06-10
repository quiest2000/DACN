using System;
using System.Threading.Tasks;
using System.Windows.Input;
using HReception.Logic.Services.Interfaces.Patients;
using Xamarin.Forms;
using HReception.UI.Utils.Extensions;
using HReception.Logic.Utils.Extensions;
using System.Collections.Generic;
using System.Linq;
using FreshMvvm;
using HReception.UI.PageModels.Payment;

namespace HReception.UI.PageModels.Common
{
    public class PatientDetailPageModel : PageModelBase
    {
        private PatientDto _cachePatient;

        private readonly IPatientService _patientService;
        public PatientDetailPageModel(IPatientService patientService)
        {
            _patientService = patientService;
        }
        public override void Init(object initData)
        {
            base.Init(initData);

            if (initData is PatientDto)
            {
                CurrentPage.Title = "Chi tiết";
                CurrentPatient = _cachePatient = initData as PatientDto;
                SelectedGenderIndex = Genders.IndexOf(CurrentPatient.Gender);
            }
            else
            {
                CurrentPage.Title = "BN mới";
                //add new patient
                PrepareToCreateCommand.Execute(null);
            }
        }
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
        }

        #region Properties
        public PatientDto CurrentPatient { get; set; }
        public IList<string> Genders => new List<string> { "Nam", "Nữ" };
        int _selectedGenderIndex;

        public int SelectedGenderIndex
        {
            get => _selectedGenderIndex;

            set
            {
                _selectedGenderIndex = value;
                try
                {
                    if (CurrentPatient != null && !Genders.IsNullOrEmpty())
                        CurrentPatient.Gender = Genders.ElementAt(value);
                }
                catch
                {
                    //ignored
                }
            }
        }


        public bool EditMode { get; set; }
        public bool IsAddNew { get; set; }
        #endregion

        #region commands
        #region deleteCommand

        private ICommand _deleteCommand;

        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new Command(async () => { await DeleteCommandExecute(); }));

        private async Task DeleteCommandExecute()
        {
            var mrs = await this.ShowConfirmAsync("Xoá thông tin bệnh nhân?");
            if (!mrs) return;
            try
            {
                IsBusy = true;
                var rs = await _patientService.Delete(CurrentPatient.PatientCode);
                if (!rs)
                {
                    await this.ShowWarningAsync("Không thể xóa bệnh nhân. Vui lòng thử lại sau, cảm ơn");
                    return;
                }
            }
            finally
            {
                IsBusy = false;
            }
            await CoreMethods.PopPageModel(data: true);
        }

        #endregion

        #region PrepareToEditCommand

        private ICommand _prepareToEditCommand;

        public ICommand PrepareToEditCommand => _prepareToEditCommand ?? (_prepareToEditCommand = new Command(PrepareToEditCommandExecute));

        private void PrepareToEditCommandExecute()
        {
            EditMode = !EditMode;
            IsAddNew = false;
        }

        #endregion

        #region PrepareToCreateCommand

        private ICommand _prepareToCreateCommand;

        public ICommand PrepareToCreateCommand => _prepareToCreateCommand ?? (_prepareToCreateCommand = new Command(PrepareToCreateCommandExecute));

        private void PrepareToCreateCommandExecute()
        {
            EditMode = !EditMode;
            IsAddNew = true;
            CurrentPatient = new PatientDto
            {
                PatientCode = "auto_generated",
                DoB = new DateTime(1990, 1, 1),
                Gender = Genders.FirstOrDefault()
            };
        }

        #endregion

        #region SaveCommand

        private ICommand _saveCommand;

        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new Command(async () => { await SaveCommandExecute(); }));

        private async Task SaveCommandExecute()
        {
            if (CurrentPatient.FullName.IsNullOrEmpty())
            {
                await this.ShowInfoAsync("Vui lòng nhập họ tên bệnh nhân");
                return;
            }

            try
            {
                IsBusy = true;
                if (IsAddNew)
                {
                    var rs = await _patientService.Register(CurrentPatient.MapTo<NewPatientRequest>());
                    IsBusy = false;
                    switch (rs.Result)
                    {
                        case NewPatientResults.Ok:
                            //await _messageService.ShowInformationAsync("Tạo bệnh nhân thành công.");
                            EditMode = false;
                            IsAddNew = false;
                            await CoreMethods.PopPageModel(data: true);//data changed
                            break;
                        case NewPatientResults.Failed:
                            await this.ShowWarningAsync("Không thể tạo BN, vui lòng thử lại sau");
                            break;
                        case NewPatientResults.Existed:
                            await this.ShowWarningAsync(
                                "Mã số bệnh nhân này đã tồn tại. Vui lòng nhập mã khác, cảm ơn");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    var updateRs = await _patientService.Update(CurrentPatient.MapTo<UpdatePatientRequest>());
                    IsBusy = false;
                    switch (updateRs.Result)
                    {
                        case UpdatePatientResults.NotFound:
                            await this.ShowWarningAsync(
                                $"Không tìm thấy mã số bệnh nhân [{CurrentPatient.PatientCode}]");
                            break;
                        case UpdatePatientResults.Failed:
                            await this.ShowWarningAsync("Không thể cập nhật thông tin BN, vui lòng thử lại sau");
                            break;
                        case UpdatePatientResults.Ok:
                            //await _messageService.ShowInformationAsync("Cập nhật thông tin bệnh nhân thành công.");
                            EditMode = false;
                            IsAddNew = false;
                            await CoreMethods.PopPageModel(data: true);//data changed
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

        #region CancelCommand

        private ICommand _cancelCommand;

        public ICommand CancelCommand => _cancelCommand ?? (_cancelCommand = new Command(CancelCommandExecute));

        private void CancelCommandExecute()
        {
            EditMode = false;
            IsAddNew = false;
            CurrentPatient = new PatientDto(_cachePatient);
        }

        #endregion

        #region AssignmentCommand

        private ICommand _assignmentCommand;

        public ICommand AssignmentCommand => _assignmentCommand ?? (_assignmentCommand = new Command(async () => { await AssignmentCommandExecute(); }, AssignmentCommandCanExecute));

        private bool AssignmentCommandCanExecute()
        {
            return !EditMode && _cachePatient != null;
        }

        private async Task AssignmentCommandExecute()
        {
            await CoreMethods.PushPageModel<AssignmentPageModel>(_cachePatient);
        }

        #endregion
        #endregion
    }
}
