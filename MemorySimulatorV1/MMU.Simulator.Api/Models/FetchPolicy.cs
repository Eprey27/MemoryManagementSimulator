// Models/FetchPolicy.cs
namespace MMU.Simulator.Api.Models
{
    public enum FetchPolicy
    {
        DemandPaging,   // Paginación bajo demanda
        Prepaging       // Prepaginación
    }
}