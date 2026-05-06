using Data_Accese_Layer.Entities;

namespace Data_Accese_Layer.IRepos
{
    public interface IPatientRepo
    {

        Task<bool> AddPatient(Patient patient);



    }


}