using AutoMapper;
using DialysisManagement.Models;
// Aggiungi qui gli altri using per i DTO quando li creerai

namespace DialysisManagement.Utilities
{
    /// <summary>
    /// Configurazione AutoMapper per mappare entità e DTO
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping bidirezionali Model <-> DTO
            // Verranno aggiunti nella PARTE 4 quando creeremo i Models

            // Esempio (da completare):
            // CreateMap<Patient, PatientDto>().ReverseMap();
            // CreateMap<DialysisSession, DialysisSessionDto>().ReverseMap();
            // CreateMap<LabTest, LabTestDto>().ReverseMap();
        }
    }
}
