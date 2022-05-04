using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Negocio.Core;
using Negocio.DataAccess;
using Negocio.Dtos;
using Negocio.Shared;
using Negocio.Shared.Extensions;

namespace Negocio.Operacion
{ 
    public class CitasDisponiblesQuery : IQuery<IEnumerable<CitasDisponiblesQueryDto>>
    {
        public int DoctorId { get; set; }
        public DateTime Fecha { get; set; } 

        internal sealed class CitasDisponiblesQueryHandler : IQueryHandler<CitasDisponiblesQuery, IEnumerable<CitasDisponiblesQueryDto>>
        {
            private readonly IReadOnlyRepository _readOnlyRepository;
            public CitasDisponiblesQueryHandler(IReadOnlyRepository readOnlyRepository)
            {
                this._readOnlyRepository = readOnlyRepository;
            }
            public IEnumerable<CitasDisponiblesQueryDto> Handle(CitasDisponiblesQuery filtro)
            { 
                var result = _readOnlyRepository.Query<Citas>()
                    .Where(e =>
                                (e.Doctor.Id == filtro.DoctorId) && 
                                e.FechaCita >= filtro.Fecha
                            )
                    .OrderByDescending(e => e.HoraCita)
                    .ThenBy(e => e.Id)
                    .Select(e => new CitasDisponiblesQueryDto
                    {
                             Disponible=false
                            ,FechaCita = e.FechaCita
                            ,HoraCita=e.HoraCita
                            ,Descripcion = Utilerias.ObtieneDescripcionFecha(new DateTime(filtro.Fecha.Year, filtro.Fecha.Month, filtro.Fecha.Day, e.HoraCita, 0, 0), Utilerias.DescripcionCita.reservada.ToString())
                    }).ToList();

                return ObtieneCitasDisponibles(filtro, result).OrderBy(e => e.HoraCita);
            }
            private IEnumerable<CitasDisponiblesQueryDto> ObtieneCitasDisponibles(CitasDisponiblesQuery filtro, List<CitasDisponiblesQueryDto> citasReservadas)
            {
                int noCitasDiarias = 7;
                int horaPrimerCita = 10;
                int horaCita = horaPrimerCita;  

                for (int i = 0; i <= (noCitasDiarias); i++)
                {
                    var existe = citasReservadas.Where(e => e.HoraCita == horaCita).FirstOrDefault();
                    DateTime combine = new DateTime(filtro.Fecha.Year, filtro.Fecha.Month, filtro.Fecha.Day, horaCita, 0, 0);

                    if (existe == null)
                    {
                        var citaDisponible = new CitasDisponiblesQueryDto
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
