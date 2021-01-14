using PatientCare.Services.DTO;

namespace PatientCare.Services.Interfaces
{
    public interface IPatientService
    {
        void AddImmunisation(int patientId, ImmunisationDto immunisation);
        void CreatePatient(PatientDto patient);
        long GetImmunisationCount(int patientId);
        void Merge(PatientDto sourcePatient, PatientDto destinationPatient);
        void RemoveImmunisation(int patientId, int immunisationId);
        ImmunisationDto GetImmunisation(int patientId, int immunisationId);
    }
}