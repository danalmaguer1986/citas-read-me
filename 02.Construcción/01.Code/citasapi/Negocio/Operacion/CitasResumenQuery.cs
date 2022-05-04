using Negocio.Core;
using Negocio.DataAccess;
using Negocio.Dtos;
using System;
using System.Collections.Generic;
using System.Linq; 

namespace Negocio.Operacion
{
    public class CitasResumenQuery : IQuery<IEnumerable<CitasResumenQueryDto>>
    {
        public int DoctorId { get; set; }
        public DateTime FechaCita { get; set; }

        internal sealed class CitasResumenQueryHandler : IQueryHandler<CitasResumenQuery, IEnumerable<CitasResumenQueryDto>>
        {
            private readonly IReadOnlyRepository _readOnlyRepository;
            public CitasResumenQueryHandler(IReadOnlyRepository readOnlyRepository)
            {
                this._readOnlyRepository = readOnlyRepository;
            } 
            public IEnumerable<CitasResumenQueryDto> Handle(CitasResumenQuery query)
            {
                var result = _readOnlyRepository.Query<Citas>() 
                    .Where(e => e.FechaCita == query.FechaCita 
                                && e.Doctor.Id == query.DoctorId)
                    .Select(e => new CitasResumenQueryDto
                    {
                        FechaCita = e.FechaCita,
                        HoraCita = e.HoraCita,
                        NombrePaciente= e.Paciente,
                        CorreoElectronico = e.CorreoElectronico,
                        Doctor  = e.Doctor.Nombre,
                        Descripcion = Utilerias.ObtieneDescripcionFecha(e.FechaCita, Utilerias.DescripcionCita.reservada.ToString())
                    }).ToList();

                return ComplementaCitasDisponibles(query, result).OrderBy(e => e.HoraCita);
            } 
            private IEnumerable<CitasResumenQueryDto> ComplementaCitasDisponibles(CitasResumenQuery filtro, List<CitasResumenQueryDto> citasReservadas)
            {
                int noCitasDiarias = 7;
                int horaPrimerCita = 10;
                int horaCita = horaPrimerCita;

                for (int i = 0; i <= (noCitasDiarias); i++)
                {
                    var existe = citasReservadas.Where(e => e.HoraCita == horaCita).FirstOrDefault();
                    DateTime combine = new DateTime(filtro.FechaCita.Year, filtro.FechaCita.Month, filtro.FechaCita.Day, horaCita, 0, 0);

                    if (existe == null)
                    {
                        var citaDisponible = new CitasResumenQueryDto
                        {
                            Disponible = true
                            , FechaCita = combine
                            , HoraCita = horaCita
                            , Descripcion = Utilerias.ObtieneDescripcionFecha(combine, Utilerias.DescripcionCita.disponible.ToString())
                        };
                        citasReservadas.Add(citaDisponible);
                    }
                    horaCita++;
                }

                return citasReservadas;
            }
        }
    }
}
